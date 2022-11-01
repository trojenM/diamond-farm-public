using System;
using DorudonGames.Runtime.Enum;
using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.EventServices.Resources.Game;
using DorudonGames.Runtime.Manager;
using DorudonGames.Runtime.Misc;
using UnityEngine;

namespace DorudonGames.Runtime.Component
{
    public class HammerComponent : MonoBehaviour
    {
        [SerializeField] private ParticleSystem sparkleParticle;
        [SerializeField] private ParticleSystem spawnParticle;
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
        private bool _hasStoppedSwitch = false;
        private Collider _collider;
        private float _hammerSpeed;
        private float incomeMul;

        public int GetIncome() { return (int)((damage) * (incomeMul-(incomeMul * 0.7f))); }
        
        private void Awake()
        {
            EventService.AddListener<UpdateHammerSpeedEvent>(UpdateHammerSpeed);
            EventService.AddListener<UpdateHammerIncomeEvent>(UpdateIncomeMultiplier);
            _collider = GetComponent<Collider>();
            _startRotation = transform.localRotation;
            _endRotation = Quaternion.Euler(_startRotation.eulerAngles + new Vector3(0f, 0f, rotateDegree));
            SetTargetDown();
        }

        private void Update()
        {
            if (_hasStoppedSwitch == _hasStopped)
            {
                if (!_hasStoppedSwitch)
                    StartHammer();
                else
                    StopHammer();

                _hasStoppedSwitch = !_hasStoppedSwitch;
            }

            if (!_hasStopped)
            {
                if (RotationDistance(transform.localRotation, _targetRotation, rotationDistance))
                    SwapTargetRotation();
            }

            RotateHammer();
        }

        private void UpdateHammerSpeed(UpdateHammerSpeedEvent e)
        {
            _hammerSpeed = e.Speed;
            _hasStopped = e.IsHammerStopped;
        }

        private void UpdateIncomeMultiplier(UpdateHammerIncomeEvent e)
        {
            incomeMul = e.Income;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PieceComponent>()) return;

            OnHammerHit();
        }

        private void RotateHammer()
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _targetRotation, _hammerSpeed * Time.deltaTime);
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
            SoundManager.Instance.Play(CommonTypes.SFX_HAMMER_HIT);
            LevelManager.Instance.IncreaseCreditAmount(GetIncome());
            InterfaceManager.Instance.FlyCurrencyTextFromWorld(castPosition.position, GetIncome());
            SetTargetUp();
            //EventDispatchers.DispatchOnHammerHit(damage , castPosition);
            
            RaycastHit[] hitInfo = Physics.SphereCastAll(castPosition.position, sphereRadius, castPosition.forward, 10f, layerMask,QueryTriggerInteraction.UseGlobal);
            
            foreach (var hit in hitInfo)
            {
                if (hit.transform.TryGetComponent(out PieceComponent piece))
                {
                    piece.TakeDamage(damage,castPosition.position);
                }
            }
        }
       

       
       

        private void StartHammer()
        {
            SetTargetDown();
            _hasStopped = false;
            _collider.enabled = true;
        }
        
        private void StopHammer()
        {
            SetTargetUp();
            _hasStopped = true;
            _collider.enabled = false;
        }

        //public static void SetRotateSpeed(float speed) => _rotateSpeed = speed;

        public bool RotationDistance(Quaternion value, Quaternion about, float range) {
            return Quaternion.Dot(value,about) > 1f-range;
        }

        public void PlaySpawnParticle()
        {
            spawnParticle.Play();
        }
        
        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(castPosition.position + castPosition.forward * (sphereRadius/2), sphereRadius);
        }


    }
}