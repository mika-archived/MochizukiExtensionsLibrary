/*-------------------------------------------------------------------------------------------
 * Copyright (c) Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using Mochizuki.ExtensionsLibrary.Runtime.Exceptions;

using UnityEditor;

using UnityEngine;

namespace Mochizuki.ExtensionsLibrary.Editor.UI
{
    public static class EditorUtilityExtensions
    {
        public static string GetSaveFilePath(string title, string name, string extension)
        {
            var file = EditorUtility.SaveFilePanelInProject(title, name, extension, "");
            return string.IsNullOrWhiteSpace(file) ? throw new InvalidPathException("Dialog returns invalid or empty path.") : file;
        }

        public static string GetSaveFolderPath(string title, string name)
        {
            var folder = EditorUtility.SaveFolderPanel(title, "", name);
            if (string.IsNullOrWhiteSpace(folder))
                throw new InvalidPathException("Dialog returns invalid or empty path.");
            if (!folder.Contains(Application.dataPath))
                throw new InvalidPathException("Dialog returns invalid or empty path.");
            return $"Assets{folder.Replace(Application.dataPath, "")}";
        }
    }
}