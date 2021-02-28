/*-------------------------------------------------------------------------------------------
 * Copyright (c) Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using System;

using UnityEngine;

namespace Mochizuki.ExtensionsLibrary.Editor
{
    public static class StateMachineBehaviourExtensions
    {
        private static Action<StateMachineBehaviour, StateMachineBehaviour> _callback;

        public static void RegisterCallback(Action<StateMachineBehaviour, StateMachineBehaviour> callback)
        {
            _callback = callback;
        }

        public static void CloneDeepTo(this StateMachineBehaviour source, StateMachineBehaviour dest)
        {
            if (source.GetType() != dest.GetType())
                throw new ArgumentException($"{nameof(source)} and {nameof(dest)} must be same type.");

            _callback?.Invoke(source, dest);
        }
    }
}