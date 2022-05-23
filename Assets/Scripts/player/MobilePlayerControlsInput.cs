using ui;
using UnityEngine;

namespace player
{
    public class MobilePlayerControlsInput : MonoBehaviour, IPlayerControlsInput
    {
        [SerializeField] private PointTouchDetector leftButton;
        [SerializeField] private PointTouchDetector rightButton;
        [SerializeField] private PointTouchDetector jumpButton;
        [SerializeField] private PointTouchDetector primaryAttackButton;
        [SerializeField] private PointTouchDetector secondaryAttackButton;

        public bool IsJumpPressed()
        {
            return jumpButton.IsPressed();
        }

        public bool IsPrimaryAttackPressed()
        {
            return primaryAttackButton.IsPressed();
        }

        public bool IsSecondaryAttackPressed()
        {
            return secondaryAttackButton.IsPressed();
        }

        public bool IsLeftPressed()
        {
            return leftButton.IsPressed();
        }

        public bool IsRightPressed()
        {
            return rightButton.IsPressed();
        }

        public void DisableInGameActions()
        {
        }

        public void EnableInGameActions()
        {
        }
    }
}