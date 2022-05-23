using System;
using UnityEngine;

namespace player
{
    public class PcPlayerControlsInput : MonoBehaviour, IPlayerControlsInput
    {
        [SerializeField] private MouseHoverDetector pauseButtonHoverDetector;
        private bool _isInGameActionsDisabled;
        
        public bool IsJumpPressed()
        {
            return IsActionPressed(() => Input.GetKeyDown(KeyCode.Space));
        }

        public bool IsPrimaryAttackPressed()
        {
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