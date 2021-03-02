/*-------------------------------------------------------------------------------------------
 * Copyright (c) Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

using UnityEngine;

namespace Mochizuki.ExtensionsLibrary.Examples
{
    [InitializeOnLoad]
    public static class DependencyInstaller
    {
        private const string SCOPE = "moe.mochizuki";
        private const string VERSION = "1.0.0";

        private static readonly Regex RegistryRegex = new Regex(@"^(?=.*""url"":\s+""https://registry.npmjs.com"")(?=.*""scopes"":\s+\[\s*""moe.mochizuki""\s*\])");
        private static readonly ListRequest ListRequest;
        private static readonly string ManifestPath = Path.Combine(Application.dataPath, "..", "Packages", "manifest.json");

        private static readonly string[] Dependencies =
        {
            "moe.mochizuki.extensions-library.dotnet",
            "moe.mochizuki.extensions-library.editor",
            "moe.mochizuki.extensions-library.engine",
            "moe.mochizuki.extensions-library.avatar",
            "moe.mochizuki.extensions-library.compat"
        };

        static DependencyInstaller()
        {
            ListRequest = Client.List(true);
            EditorApplication.update += OnUpdate;
        }

        private static void OnUpdate()
        {
            if (!ListRequest.IsCompleted)
                return;

            EditorApplication.update -= OnUpdate;

            var localPackages = ListRequest.Result;

            foreach (var dependency in Dependencies)
                if (localPackages.All(w => $"{w.name}@{w.version}" != $"{dependency}@{VERSION}"))
                    Install($"{dependency}@{VERSION}");

            // ClearLogs();
        }

        private static void ClearLogs()
        {
            var asm = Assembly.GetAssembly(typeof(SceneView));
            var logEntries = asm.GetType("UnityEditor.LogEntries");
            var method = logEntries.GetMethod("Clear");
            method?.Invoke(new object(), null);
        }

        private static void Install(string package)
        {
            if (!IsAlreadyRegisteredScope())
                InstallRegistry();

            InstallPackage(package);
        }

        private static bool IsAlreadyRegisteredScope()
        {
            var json = ReadManifest().Replace("\r", "").Replace("\n", "");
            if (json.Contains("scopedRegistries") && RegistryRegex.IsMatch(json))
                return true;

            return false;
        }

        private static void InstallRegistry()
        {
            var json = ReadManifest();
            var registry = @"
{
      ""name"": ""Mochizuki"",
      ""url"": ""https://registry.npmjs.com"",
      ""scopes"": [""moe.mochizuki""]
}".Trim();

            if (json.Contains("scopedRegistries"))
            {
                // Insert new scope to head of scopedRegistries
                var i = json.IndexOf("{", json.IndexOf("scopedRegistries", StringComparison.Ordinal), StringComparison.Ordinal);
                json = $"{json.Substring(0, i)}{Environment.NewLine}{registry},{Environment.NewLine}{json.Substring(i)}";
            }
            else
            {
                // Insert scopedRegistries section to head of JSON
                var section = $@"
  ""scopedRegistries"": [
    {registry}
  ],
".Trim();

                json = $"{{{Environment.NewLine}{section}{Environment.NewLine}{json.Substring(1)}";
            }

            using (var sw = new StreamWriter(ManifestPath))
                sw.WriteLine(json);
        }

        private static void InstallPackage(string package)
        {
            var request = Client.Add(package);
            while (!request.IsCompleted) { }

            if (request.Error != null)
                Debug.LogError(request.Error.message);
        }

        private static string ReadManifest()
        {
            using (var sr = new StreamReader(ManifestPath))
                return sr.ReadToEnd();
        }
    }
}