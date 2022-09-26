

using UnityEngine;
using Cinemachine;
 

[ExecuteInEditMode] [SaveDuringPlay] [AddComponentMenu("")]
public class LockCameraXZ : CinemachineExtension
{
    [Tooltip("Lock the camera's X position to this value")]
    public float m_XPosition = 10;

    public float m_ZPosition = -6;
 
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.x = m_XPosition;
            state.RawPosition = pos;
            pos.z = m_ZPosition;
            state.RawPosition = pos;
        }
    }
}
