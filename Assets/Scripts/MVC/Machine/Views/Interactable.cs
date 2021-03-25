using System;
using UnityEngine;

namespace SimpleMachine
{
    /// <summary>
    /// Base class for interactable mechanisms in machine.
    /// Invokes events of user interactions.
    /// </summary>
    public class Interactable : MonoBehaviour
    {
        private EventHandler<int> _onTryInteract;
        public event EventHandler<int> OnTryInteract 
        {
            add 
            {
                if (_onTryInteract == null) 
                {
                    _onTryInteract += value;
                }
            }
            remove => _onTryInteract -= value;
        }
        
        public event EventHandler<int> OnStopInteract;
        
        public event EventHandler<int> OnStateSwitched;

        private int _index;
        
        /// <summary>
        /// Initialize object with its order in machine.
        /// </summary>
        public virtual void Init(int index)
        {
            _index = index;
        }
        
        /// <summary>
        /// Converts user input to object movement or animation.
        /// </summary>
        public virtual void Interact() { }

        /// <summary>
        /// Invokes event OnTryInteract.
        /// </summary>
        protected void TryInteract()
        {
            _onTryInteract?.Invoke(this, _index);
        }
        
        /// <summary>
        /// Invokes event OnStopInteract.
        /// </summary>
        protected void StopInteract()
        {
            OnStopInteract?.Invoke(this, _index);
        }
        
        /// <summary>
        /// Invokes event OnStateSwitched.
        /// </summary>
        protected void SwitchState()
        {
            OnStateSwitched?.Invoke(this, _index);
        }
    }
}
