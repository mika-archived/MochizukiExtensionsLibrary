/*-------------------------------------------------------------------------------------------
 * Copyright (c) Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using Mochizuki.ExtensionsLibrary.Avatar.Extensions;
using Mochizuki.ExtensionsLibrary.Editor.Extensions;

using VRC.SDK3.Avatars.Components;

namespace Mochizuki.ExtensionsLibrary.Avatar
{
    // ReSharper disable once InconsistentNaming
    public static class VRCStateMachineCallbackFactory
    {
        public static void RegisterCallbacks()
        {
            StateMachineBehaviourExtensions.RegisterCallback((source, dest) =>
            {
                switch (source)
                {
                    case VRCAnimatorLayerControl sourceAlc:
                    {
                        var behaviour = dest as VRCAnimatorLayerControl;
                        sourceAlc.CloneTo(behaviour);
                        break;
                    }

                    case VRCAnimatorLocomotionControl sourceAlc:
                    {
                        var behaviour = dest as VRCAnimatorLocomotionControl;
                        sourceAlc.CloneTo(behaviour);
                        break;
                    }

                    case VRCAnimatorTemporaryPoseSpace sourceTps:
                    {
                        var behaviour = dest as VRCAnimatorTemporaryPoseSpace;
                        sourceTps.CloneTo(behaviour);
                        break;
                    }

                    case VRCAnimatorTrackingControl sourceTc:
                    {
                        var behaviour = dest as VRCAnimatorTrackingControl;
                        sourceTc.CloneTo(behaviour);
                        break;
                    }

                    case VRCAvatarParameterDriver sourceApd:
                    {
                        var behaviour = dest as VRCAvatarParameterDriver;
                        sourceApd.CloneTo(behaviour);
                        break;
                    }

                    case VRCPlayableLayerControl sourcePlc:
                    {
                        var behaviour = dest as VRCPlayableLayerControl;
                        sourcePlc.CloneTo(behaviour);
                        break;
                    }
                }
            });
        }
    }
}