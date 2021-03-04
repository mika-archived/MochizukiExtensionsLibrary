/*-------------------------------------------------------------------------------------------
 * Copyright (c) Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using UnityEngine;

namespace Mochizuki.ExtensionsLibrary.Editor.Extensions
{
    public static class AnimatorExtensions
    {
        public static bool IsHumanoid(this Animator obj)
        {
            // ReSharper disable once Unity.NoNullPropagation
            // ReSharper disable once Unity.InefficientPropertyAccess
            return obj?.avatar.isHuman == true && obj.avatar.isValid;
        }
    }
}