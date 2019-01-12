#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.XR;

public class XRInitializer : MonoBehaviour
{
    enum AA
    {
        None,
        MSAA2x = 2,
        MSAA4x = 4,
        MSAA8x = 8
    }

    [SerializeField, Header("Common")] AA antiAliasing = AA.MSAA4x;

    [SerializeField, Header("For PC based VR")] TrackingSpaceType trackingSpace = TrackingSpaceType.RoomScale;
    [SerializeField, Range(1f, 1.5f)] float resolutionScale = 1.2f;

    [SerializeField, Header("For Oculus Go / Gear VR")] bool nativeResolutionScale = true;
    [SerializeField] bool setFramerateTo72 = true;

    void Reset()
    {
#if UNITY_EDITOR
        PlayerSettings.virtualRealitySupported = true;
        PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.HiddenByDefault;
        PlayerSettings.forceSingleInstance = true;
        PlayerSettings.stereoRenderingPath = StereoRenderingPath.SinglePass;
        PlayerSettings.SetVirtualRealitySDKs(BuildTargetGroup.Android, new[] {"Oculus"});
#endif
        Apply();
    }

    void Start()
    {
        Apply();
    }

    void OnValidate()
    {
        Apply();
    }

    void Apply()
    {
        QualitySettings.antiAliasing = (int) antiAliasing;
#if UNITY_STANDALONE
        XRDevice.SetTrackingSpaceType(trackingSpace);
        XRSettings.eyeTextureResolutionScale = resolutionScale;
#elif UNITY_ANDROID
        XRSettings.eyeTextureResolutionScale = nativeResolutionScale ? 1.25f : 1f;
        OVRManager.tiledMultiResLevel = OVRManager.TiledMultiResLevel.LMSHigh;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        OVRManager.display.displayFrequency = setFramerateTo72 ? 72f : 60f;
#endif
    }
}