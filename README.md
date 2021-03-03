# .NET Extension Library for Unity and VRChat

.NET Extensions Library for Unity (and VRChat).

## Packages

| Package Namespace                         | Namespace                             | Version | Description                                          |
| ----------------------------------------- | ------------------------------------- | ------- | ---------------------------------------------------- |
| `moe.mochizuki.extensions-library.avatar` | `Mochizuki.ExtensionsLibrary.Avatar`  | `1.0.0` | class library for VRChat SDK3 Avatars                |
| `moe.mochizuki.extensions-library.compat` | `Mochizuki.ExtensionsLibrary.Compat`  | `1.0.0` | compatible classes for migrating from older versions |
| `moe.mochizuki.extensions-library.dotnet` | `Mochizuki.ExtensionsLibrary.Runtime` | `1.0.0` | .NET extension classes for .NET 4.5                  |
| `moe.mochizuki.extensions-library.editor` | `Mochizuki.ExtensionsLibrary.Editor`  | `1.0.0` | .NET extension classes for UnityEngine.Editor        |
| `moe.mochizuki.extensions-library.engine` | `Mochizuki.ExtensionsLibrary.Engine`  | `1.0.0` | Unity C# scripts                                     |
| `moe.mochizuki.extensions-library.usharp` | `Mochizuki.ExtensionsLibrary.USharp`  | `1.0.0` | class library for VRChat SDK3 World (UdonSharp)      |

## Requirements

- Unity 2018.4.20f1 or greater
- VRCSDK3 Avatars 2021.02.23.11.41 or greater (`~.avatar` package)
- VRCSDK3 World 2021.02.23.11.40 or greater (`~.usharp` package)
- UdonSharp 0.19.3 or greater (`~.usharp` package)

## How to use

I recommend installing these libraries later via UPM rather than including them in UnityPackage.  
This can be achieved by including the following code in the UnityPackage Editor Script.

```csharp
// In Unity 2018.x
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

            ClearLogs();
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
```

## Documentation

https://docs.mochizuki.moe/extensions-library/

## Development

### Requirements

- Unity 2018.4.20f1
- Node.js 14.x with Yarn v1
- VRCSDK3 Avatars 2021.02.23.11.41 or greater
- VRCSDK3 World 2021.02.23.11.40 or greater
- UdonSharp 0.19.3 or greater

### Contributing

1. Fork and Clone this repository.
2. Write the code
3. Commit your works
4. Create a new Pull Request.

## License

MIT by [@6jz](https://twitter.com/6jz)
