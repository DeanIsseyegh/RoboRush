using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace player
{
    public class PlayerControlsInput : MonoBehaviour
    {
        [SerializeField] private MouseHoverDetector pauseButtonHoverDetector;
        private bool _isInGameActionsDisabled;

        void Update()
        {
            // _pointerOverUI = EventSystem.current.IsPointerOverGameObject();
        }
        
        public bool IsJumpPressed()
        {
            return IsActionPressed(() => Input.GetKeyDown(KeyCode.Space));
        }

        public bool IsPrimaryAttackPressed()
        {
            // if (_pointerOverUI)
            // {
                // Debug.Log("Event system over game object");
                // return false;
            // }
            if (pauseButtonHoverDetector.IsHoverOver()) return false;
            return IsActionPressed(() => Input.GetKeyDown(KeyCode.Mouse0));
        }

        public bool IsSecondaryAttackPressed()
        {
            return IsActionPressed(() => Input.GetKeyDown(KeyCode.Mouse1));
        }

        public bool IsLeftPressed()
        {
            return IsActionPressed(() => Input.GetKey(KeyCode.A));
        }

        public bool IsRightPressed()
        {
            return IsActionPressed(() => Input.GetKey(KeyCode.D));
        }

        private bool IsActionPressed(Func<bool> action)
        {
            if (_isInGameActionsDisabled) return false;
            return action.Invoke();
        } 

        public void DisableInGameActions()
        {
            _isInGameActionsDisabled = true;
        }
        
        public void EnableInGameActions()
        {
            _isInGameActionsDisabled = false;
        }
    }
}