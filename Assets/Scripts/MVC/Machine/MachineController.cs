using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleMachine
{
    /// <summary>
    /// Controller for exercise mechanism and user interaction with it.
    /// Handles Interactable objects events.
    /// Uses MachineModel data to invoke outside machine events.
    /// </summary>
    public class MachineController : MonoBehaviour
    {
        public MachineModel machineModel;
        public List<Interactable> InteractableObjects = new List<Interactable>();
        
        public event Action OnUserMistake;
        public event Action OnExerciseComplete;
        
        private void Start()
        {
            machineModel.InitModel(InteractableObjects.Count);
            machineModel.OnBackwardComplete += () => { OnExerciseComplete?.Invoke(); };
    
            for (int i = 0; i < InteractableObjects.Count; i++)
            {
                InteractableObjects[i].Init(i);
                InteractableObjects[i].OnTryInteract += HandleInteraction;
                InteractableObjects[i].OnStateSwitched += HandleSwitchState;
                InteractableObjects[i].OnStopInteract += HandleStopInteract;
            }
        }
    
        /// <summary>
        /// Gets Model permission for interaction.
        /// Invokes Interactable.Interact() or outside mistake event.
        /// </summary>
        private void HandleInteraction(object obj, int index)
        {
            if (!(obj is Interactable interactableObject))
                return;
            
            if (machineModel.CanInteract(index))
            {
                interactableObject.Interact();
                return;
            }
            
            OnUserMistake?.Invoke();
            interactableObject.OnTryInteract -= HandleInteraction;
        }
        
        private void HandleStopInteract(object obj, int index)
        {
            if (!(obj is Interactable interactableObject))
                return;
            
            interactableObject.OnTryInteract += HandleInteraction;
        }
        
        /// <summary>
        /// Provides new interactable object state to Model.
        /// Invokes outside mistake event if state is not available.
        /// </summary>
        private void HandleSwitchState(object obj, int index)
        {
            if (!(obj is Interactable interactableObject))
                return;
            
            if (!machineModel.SwitchMechanismState(index))
            {
                OnUserMistake?.Invoke();
            }
                
            interactableObject.OnTryInteract -= HandleInteraction;
        }
    }
}
