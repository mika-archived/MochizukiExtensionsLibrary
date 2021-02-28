/*-------------------------------------------------------------------------------------------
 * Copyright (c) Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using System;

using UnityEngine;

namespace Mochizuki.ExtensionsLibrary.USharp.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class UdonBehaviourAttribute : PropertyAttribute
    {
        public Type Type { get; }

        public UdonBehaviourAttribute(Type type)
        {
            Type = type;
        }
    }
}