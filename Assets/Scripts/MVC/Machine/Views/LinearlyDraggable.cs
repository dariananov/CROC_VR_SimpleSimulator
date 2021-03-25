using System;
using UnityEngine;

namespace SimpleMachine
{
    /// <summary>
    /// Implements linear object dragging between default
    /// state and active state.
    /// Uses mouse input.
    /// Tracks active/default state switches.
    /// Derived from Interactable.
    /// </summary>
    public class LinearlyDraggable : Interactable
    {
        public Vector3 defaultPosition = Vector3.zero;
        public Vector3 activePosition = Vector3.one;

        private Camera _mainCamera;
        private Vector3 _screenPoint;
        private Vector3 _offset;

        private Vector3 _moveAxis;
        
        enum State
        {
            DEFAULT,
            ACTIVE
        }
        private State _state;

        public override void Init(int index)
        {
            base.Init(index);
            
            _mainCamera = Camera.main;
            if (_mainCamera == null)
            {
                throw new NullReferenceException("Main camera is not found.");
            }

            _state = State.DEFAULT;
            _moveAxis = activePosition - defaultPosition;
            SetAxisPosition(defaultPosition);
        }
        
        private void OnMouseDown()
        {
            _screenPoint = _mainCamera.WorldToScreenPoint(gameObject.transform.position);

            _offset = transform.position - _mainCamera.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y,_screenPoint.z));
        }

        private void OnMouseUp()
        {
            StopInteract();
        }
        
        private void OnMouseDrag()
        {
            TryInteract();
        }

        public override void Interact()
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
            Vector3 curPosition = _mainCamera.ScreenToWorldPoint(curScreenPoint) + _offset;
            
            SetAxisPosition(curPosition);
        }

        /// <summary>
        /// Projects 2d mouse dragging on object dragging axis.
        /// Invokes SwitchState() if object reached default/active state position.
        /// </summary>
        private void SetAxisPosition(Vector3 value)
        {
            var newPositionOnAxis = Vector3.Project(value - defaultPosition, _moveAxis);

            transform.position = defaultPosition + GetRangedValue(_moveAxis, newPositionOnAxis);
        }

        /// <summary>
        /// Puts value inside available range on axis.
        /// Invokes SwitchState() if value is outside range.
        /// </summary>
        /// <returns>
        /// Point on axis between default and active states positions.
        /// </returns>
        /// <param name="axis">Object dragging axis.</param>
        /// <param name="value">Point on axis.</param>
        private Vector3 GetRangedValue(Vector3 axis, Vector3 value)
        {
            if (Vector3.Dot(axis, value) < 0)
            {
                if (_state == State.ACTIVE)
                {
                    _state = State.DEFAULT;
                    SwitchState();
                }
                return Vector3.zero;
            }

            if (axis.magnitude < value.magnitude)
            {
                if (_state == State.DEFAULT)
                {
                    _state = State.ACTIVE;
                    SwitchState();
                }
                return _moveAxis;
            }

            return value;
        }
    }
}
