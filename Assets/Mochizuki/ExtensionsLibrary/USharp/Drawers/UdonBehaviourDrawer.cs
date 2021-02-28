/*-------------------------------------------------------------------------------------------
 * Copyright (c) Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using Mochizuki.ExtensionsLibrary.USharp.Attributes;

using UdonSharp;

using UnityEditor;

using UnityEngine;

using VRC.Udon;

namespace Mochizuki.ExtensionsLibrary.USharp.Drawers
{
    [CustomPropertyDrawer(typeof(UdonBehaviourAttribute))]
    internal class UdonBehaviourDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.objectReferenceValue != null)
            {
                var behaviour = property.objectReferenceValue as UdonBehaviour;
                if (!IsValidUdonSharpProgram(attribute as UdonBehaviourAttribute, behaviour))
                    property.objectReferenceValue = null;
            }

            EditorGUI.PropertyField(position, property, label);
        }

        private static bool IsValidUdonSharpProgram(UdonBehaviourAttribute attr, UdonBehaviour behaviour)
        {
            return UdonSharpProgramAsset.GetBehaviourClass(behaviour) == attr.Type;
        }
    }
}