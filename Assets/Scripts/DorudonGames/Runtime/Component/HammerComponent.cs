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
        [SerializeField] private float damage = 15f;
        private Quaternion _startRotation, _endRotation; 
        private Quaternion _targetRotation;
        private bool _rotateDown = true;
        private bool _hasStopped = false;
        private Collider _collider;


        private static float _rotateSpeed = 30f;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _startRotation = transform.localRotation;
            _endRotation = Quaternion.Euler(_startRotation.eulerAngles + new Vector3(0f, 0f, rotateDegree));
            SetTargetDown();
        }

        private void Update()
        {
            if (!_hasStopped)
            {
                if (RotationDistance(transform.localRotation, _targetRotation, rotationDistance))
                    SwapTargetRotation();
            }
            
            RotateHammer();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PieceComponent>()) return;
            
            OnHammerHit();
        }

        private void RotateHammer()
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _targetRotation,_rotateSpeed*Time.deltaTime);
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
                    piece.TakeDamage(damage,castPosition.position);
                }
            }
        }

        public void StartHammer()
        {
            SetTargetDown();
            _hasStopped = false;
            _collider.enabled = true;
        }
        
        public void StopHammer()
        {
            SetTargetUp();
            _hasStopped = true;
            _collider.enabled = false;
        }

        public static void SetRotateSpeed(float speed) => _rotateSpeed = speed;

        public bool RotationDistance(Quaternion value, Quaternion about, float range) {
            return Quaternion.Dot(value,about) > 1f-range;
        }
        
        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(castPosition.position + castPosition.forward * (sphereRadius/2), sphereRadius);
        }


    }
}