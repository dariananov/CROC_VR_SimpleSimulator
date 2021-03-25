using System;
using UnityEngine;

namespace SimpleMachine
{
    /// <summary>
    /// Implements logic of exercise mechanism.
    /// Gives permissions for ordered objects interactions.
    /// </summary>
    [CreateAssetMenu]
    public class MachineModel : ScriptableObject
    {
        public event Action OnBackwardComplete;
        
        private int _mechanismCount;
        private int _mechanismIndex;

        private enum MachineState
        {
            FORWARD,
            BACKWARD
        }
        private MachineState _state;

        private bool[] _mechanismsActivity;

        /// <summary>
        /// Inits Model with start exercise data
        /// and current exercise mechanisms count.
        /// </summary>
        public void InitModel(int mechanismCount)
        {
            if (mechanismCount < 1)
            {
                throw new ArgumentException("There must be at least one mechanism in machine.");
            }

            _mechanismCount = mechanismCount;
            _mechanismIndex = 0;
            _state = MachineState.FORWARD;
            
            _mechanismsActivity = new bool[mechanismCount];
        }

        /// <summary>
        /// Switches mechanism state if available.
        /// Switches exercise stage if all mechanisms interacted.
        /// Invokes event on exercise finished.
        /// </summary>
        /// <returns>
        /// True if state is changed, False if new state is not available.
        /// </returns>
        /// <param name="index">Index of interacted object in machine.</param>
        public bool SwitchMechanismState(int index)
        {
            if (index != _mechanismIndex)
                return false;

            _mechanismsActivity[_mechanismIndex] = !_mechanismsActivity[_mechanismIndex];

            switch (_state)
            {
                case MachineState.FORWARD:
                    _mechanismIndex += 1;
                    if (_mechanismIndex >= _mechanismCount)
                    {
                        _mechanismIndex -= 1;
                        _state = MachineState.BACKWARD;
                    }
                    break;
                case MachineState.BACKWARD:
                    _mechanismIndex -= 1;
                    if (_mechanismIndex < 0)
                    {
                        _mechanismIndex += 1;
                        _state = MachineState.FORWARD;
                        OnBackwardComplete?.Invoke();
                    }
                    break;
            }
            
            return true;
        }
        
        /// <summary>
        /// Checks if interaction with object is available.
        /// </summary>
        /// <returns>
        /// True if state is changed, False if new state is not available.
        /// </returns>
        /// <param name="index">Index of interacted object in machine.</param>
        public bool CanInteract(int index)
        {
            return index == _mechanismIndex;
        }
    }
}
