using Sirenix.OdinInspector;

namespace player
{
    public class ControlsManager : SerializedMonoBehaviour
    {
        public PcPlayerControlsInput PCPlayerControlsInput;
        public MobilePlayerControlsInput MobileControlsInput;
        private IPlayerControlsInput platformSpecificControls;

        private void Awake()
        {
            platformSpecificControls = PCPlayerControlsInput;
            # if UNITY_ANDROID
            platformSpecificControls = MobileControlsInput;
            # endif
        }

        public IPlayerControlsInput GetControls()
        {
            return platformSpecificControls;
        }

    }
}