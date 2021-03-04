/*-------------------------------------------------------------------------------------------
 * Copyright (c) Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using UnityEditor.Animations;

namespace Mochizuki.ExtensionsLibrary.Editor.Extensions
{
    public static class AnimatorControllerLayerExtensions
    {
        public static AnimatorControllerLayer CloneDeep(this AnimatorControllerLayer source)
        {
            var dest = new AnimatorControllerLayer
            {
                stateMachine = source.stateMachine.CloneDeep(),
                avatarMask = source.avatarMask,
                blendingMode = source.blendingMode,
                defaultWeight = source.defaultWeight,
                iKPass = source.iKPass,
                name = source.name,
                syncedLayerAffectsTiming = source.syncedLayerAffectsTiming,
                syncedLayerIndex = source.syncedLayerIndex
            };

            return dest;
        }
    }
}