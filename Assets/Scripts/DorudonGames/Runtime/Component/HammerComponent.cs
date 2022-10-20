using UnityEngine;

namespace DorudonGames.Runtime.Component
{
    public class HammerComponent : MonoBehaviour
    {
        [SerializeField] private float sphereRadius;
        [SerializeField] private float rotateDegree;
        [SerializeField] private float rotationDistance = 0.025f;
    
        private Quaternion _startRotation, _endRotation; 
        private Quaternion _targetRotation;
        private bool _rotateDown = true;


        private static float _rotateSpeed = 30f;

        private void Awake()
        {
            _startRotation = transform.rotation;
            _endRotation = Quaternion.Euler(_startRotation.eulerAngles + new Vector3(0f, 0f, rotateDegree));
            SetTargetDown();
        }

        private void Update()
        {
            if (RotationDistance(transform.rotation, _targetRotation, rotationDistance))
                SwapTargetRotation();
            RotateHammer();
        }

        private void OnTriggerEnter(Collider other)
        {
        
        }

        private void RotateHammer()
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation,_rotateSpeed*Time.deltaTime);
        }

        private void SwapTargetRotation()
        {
            if (_rotateDown)
            {
                SetTargetUp();
            }
            else
            {
                SetTargetDown();
            }
        }

        private void SetTargetUp()
        {
            _rotateDown = false;
            _targetRotation = _startRotation;
        }

        private void SetTargetDown()
        {
            _rotateDown = true;
            _targetRotation = _endRotation;
        }

        public static void SetRotateSpeed(float speed) => _rotateSpeed = speed;

        public bool RotationDistance(Quaternion value, Quaternion about, float range) {
            return Quaternion.Dot(value,about) > 1f-range;
        }

    }
}