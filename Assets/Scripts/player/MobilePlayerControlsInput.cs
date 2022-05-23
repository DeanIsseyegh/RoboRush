using System;
using ui;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace player
{
    public class MobilePlayerControlsInput : MonoBehaviour, IPlayerControlsInput
    {
        [SerializeField] private PointTouchDetector leftButton;
        [SerializeField] private PointTouchDetector rightButton;
        [SerializeField] private PointTouchDetector jumpButton;
        [SerializeField] private PointTouchDetector primaryAttackButton;
        [SerializeField] private PointTouchDetector secondaryAttackButton;

        private bool _isLeftPressed;
        private bool _isRightPressed;
        private MobileControls _currentPressed;

        enum MobileControls
        {
            LEFT,
            RIGHT,
            JUMP,
            PRIMARY_ATTACK,
            SECONDARY_ATTACK,
            NOTHING
        }
        
        private void Update()
        {
            if (leftButton.IsPressed())
                _currentPressed = MobileControls.LEFT;
            else if (rightButton.IsPressed())
                _currentPressed = MobileControls.RIGHT;
            else if (jumpButton.IsPressed())
                _currentPressed = MobileControls.JUMP;
            else if (primaryAttackButton.IsPressed())
                _currentPressed = MobileControls.PRIMARY_ATTACK;
            else if (secondaryAttackButton.IsPressed())
                _currentPressed = MobileControls.SECONDARY_ATTACK;
            else
                _currentPressed = MobileControls.NOTHING;
        }

        public bool IsJumpPressed()
        {
            return _currentPressed == MobileControls.JUMP;
        }

        public bool IsPrimaryAttackPressed()
        {
            return _currentPressed == MobileControls.PRIMARY_ATTACK;
        }

        public bool IsSecondaryAttackPressed()
        {
            return _currentPressed == MobileControls.SECONDARY_ATTACK;
        }

        public bool IsLeftPressed()
        {
            return _currentPressed == MobileControls.LEFT;
        }

        public bool IsRightPressed()
        {
            return _currentPressed == MobileControls.RIGHT;
        }

        public void DisableInGameActions()
        {
        }

        public void EnableInGameActions()
        {
        }
    }
}