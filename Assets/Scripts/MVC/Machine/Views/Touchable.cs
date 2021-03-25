using UnityEngine;

namespace SimpleMachine
{
    /// <summary>
    /// Implements object animated touches.
    /// Uses mouse input.
    /// Derived from Interactable.
    /// </summary>
    public class Touchable : Interactable
    {
        public Animator animator;
        public string animatorTrigger;
        
        private void OnMouseDown()
        {
            TryInteract();
        }

        private void OnMouseUp()
        {
            StopInteract();
        }

        public override void Interact()
        {
            SetAnimatorTrigger();
            SwitchState();
        }
        
        /// <summary>
        /// Sets trigger to animator.
        /// </summary>
        private void SetAnimatorTrigger()
        {
            animator.SetTrigger(animatorTrigger);
        }
    }
}
