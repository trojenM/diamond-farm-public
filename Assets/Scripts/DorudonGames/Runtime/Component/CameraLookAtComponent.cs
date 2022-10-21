using Cinemachine;
using DorudonGames.Runtime.Manager;
using UnityEngine;

namespace DorudonGames.Runtime.Component
{
    public class CameraLookAtComponent : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed;
        [SerializeField] private bool rotateable;
    
        private CinemachineFreeLook _cineMachineFreeLook;

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            UpdateCameraRotation();
        }

        private void Initialize()
        {
            _cineMachineFreeLook = CameraManager.Instance.GetFreeLookCamera();
            _cineMachineFreeLook.Follow = transform;
            _cineMachineFreeLook.LookAt = transform;
        }

        private void UpdateCameraRotation()
        {
            if (rotateable && !Input.GetMouseButton(0))
            {
                _cineMachineFreeLook.m_XAxis.m_InputAxisValue = 0;
                return;
            }
        
            _cineMachineFreeLook.m_XAxis.m_InputAxisValue = TouchManager.Instance.GetTouchDirection().x * rotateSpeed;
        }
    }
}
