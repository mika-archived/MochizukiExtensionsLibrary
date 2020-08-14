/*-------------------------------------------------------------------------------------------
 * Copyright (c) Fuyuno Mikazuki / Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

#if UNITY_EDITOR

using System;

using UnityEditor.Animations;

namespace Mochizuki.VRChat.Extensions.Unity
{
    public static class AnimatorStateMachineExtensions
    {
        public static AnimatorStateMachine CloneDeep(this AnimatorStateMachine source)
        {
            var dest = new AnimatorStateMachine
            {
                defaultState = InstanceCaches<AnimatorState>.FindOrCreate(source.defaultState, w => w.CloneDeep()),
                anyStatePosition = source.anyStatePosition,
                entryPosition = source.entryPosition,
                exitPosition = source.exitPosition,
                parentStateMachinePosition = source.parentStateMachinePosition,
                hideFlags = source.hideFlags,
                name = source.name
            };

            foreach (var sourceState in source.states)
            {
                var state = dest.AddState(sourceState.state.name);
                state.Apply(sourceState.state);

                if (InstanceCaches<AnimatorState>.Find(sourceState.state.GetInstanceID()) == null)
                    InstanceCaches<AnimatorState>.Register(sourceState.state.GetInstanceID(), state);
            }

            foreach (var sourceTransition in source.anyStateTransitions)
            {
                AnimatorStateTransition transition = null;
                if (sourceTransition.destinationStateMachine != null)
                    transition = dest.AddAnyStateTransition(InstanceCaches<AnimatorStateMachine>.FindOrCreate(sourceTransition.destinationStateMachine, CloneDeep));

                if (sourceTransition.destinationState != null)
                    transition = dest.AddAnyStateTransition(InstanceCaches<AnimatorState>.FindOrCreate(sourceTransition.destinationState, w => w.CloneDeep()));

                if (transition == null)
                    throw new ArgumentNullException(nameof(transition));

                sourceTransition.CloneTo(transition);
            }

            foreach (var sourceTransition in source.entryTransitions)
            {
                AnimatorTransition transition = null;
                if (sourceTransition.destinationStateMachine != null)
                    transition = dest.AddEntryTransition(InstanceCaches<AnimatorStateMachine>.FindOrCreate(sourceTransition.destinationStateMachine, CloneDeep));

                if (sourceTransition.destinationState != null)
                    transition = dest.AddEntryTransition(InstanceCaches<AnimatorState>.FindOrCreate(sourceTransition.destinationState, w => w.CloneDeep()));

                if (transition == null)
                    throw new ArgumentNullException(nameof(transition));

                transition.CloneTo(sourceTransition);
            }

            foreach (var sourceBehaviour in source.behaviours)
            {
                var behaviour = dest.AddStateMachineBehaviour(sourceBehaviour.GetType());
                sourceBehaviour.CloneDeepTo(behaviour);
            }

            foreach (var sourceStateMachine in source.stateMachines)
                dest.AddStateMachine(InstanceCaches<AnimatorStateMachine>.FindOrCreate(sourceStateMachine.stateMachine, CloneDeep), sourceStateMachine.position);

            return dest;
        }
    }
}

#endif