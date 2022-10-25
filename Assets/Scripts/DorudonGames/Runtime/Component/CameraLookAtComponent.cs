using Cinemachine;
using DorudonGames.Runtime.Manager;
using UnityEngine;

namespace DorudonGames.Runtime.Component
{
    public class CameraLookAtComponent : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed;
        [SerializeField] private bool rotateable;
        private float _interval = .12f;
        private float _cd = 0;
        private bool _isHolding = false;
    
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

            if (Input.GetMouseButtonDown(0))
            {
                _cd = _interval;
                _isHolding = true;
            }
            
            if (_isHolding)
            {
                _cd -= Time.deltaTime;
                
                if (Input.GetMouseButtonUp(0))
                {
                    _isHolding = false;
                }

                if (_cd <= 0)
                {
                    _cineMachineFreeLook.m_XAxis.m_InputAxisValue = TouchManager.Instance.GetTouchDirection().x * rotateSpeed;
                }
            }
        }
    }
}
