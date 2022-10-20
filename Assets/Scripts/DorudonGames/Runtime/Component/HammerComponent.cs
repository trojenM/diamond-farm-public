using DorudonGames.Runtime.Manager;
using UnityEngine;

namespace DorudonGames.Runtime.Component
{
    public class HammerComponent : MonoBehaviour
    {
        [SerializeField] private ParticleSystem sparkleParticle;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Transform castPosition;
        [SerializeField] private float sphereRadius;
        [SerializeField] private float rotateDegree;
        [SerializeField] private float rotationDistance = 0.025f;
    
        private Quaternion _startRotation, _endRotation; 
        private Quaternion _targetRotation;
        private bool _rotateDown = true;
        private Collider _collider;


        private static float _rotateSpeed = 30f;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
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
            if (!other.GetComponent<PieceComponent>()) return;
            
            OnHammerHit();
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
            _collider.enabled = false;
        }

        private void SetTargetDown()
        {
            _rotateDown = true;
            _targetRotation = _endRotation;
            _collider.enabled = true;
        }

        private void OnHammerHit()
        {
            sparkleParticle.Play();
            SetTargetUp();

            RaycastHit[] hitInfo = Physics.SphereCastAll(castPosition.position, sphereRadius, castPosition.forward, 10f, layerMask,QueryTriggerInteraction.UseGlobal);
            
            foreach (var hit in hitInfo)
            {
                if (hit.transform.TryGetComponent(out PieceComponent piece))
                {
                    piece.rb.isKinematic = false;
                    piece.rb.mass = 10;
                }
            }
        }

        public static void SetRotateSpeed(float speed) => _rotateSpeed = speed;

        public bool RotationDistance(Quaternion value, Quaternion about, float range) {
            return Quaternion.Dot(value,about) > 1f-range;
        }

    }
}