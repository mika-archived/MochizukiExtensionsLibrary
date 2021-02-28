/*-------------------------------------------------------------------------------------------
 * Copyright (c) Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using System.Linq;

using VRC.SDK3.Avatars.Components;
using VRC.SDKBase;

namespace Mochizuki.ExtensionsLibrary.Avatar
{
    // ReSharper disable once InconsistentNaming
    public static class VRCAvatarParameterDriverExtensions
    {
        public static void CloneTo(this VRCAvatarParameterDriver source, VRCAvatarParameterDriver dest)
        {
            source.ApplySettings = dest.ApplySettings;
            source.parameters = dest.parameters.Select(w => new VRC_AvatarParameterDriver.Parameter { name = w.name, value = w.value }).ToList();
            source.debugString = dest.debugString;
            source.name = dest.name;
            source.hideFlags = dest.hideFlags;
        }
    }
}