using DorudonGames.Runtime.Component;
using DorudonGames.Runtime.Misc;
using UnityEngine;

namespace DorudonGames.Runtime.Manager
{
    public class HammerManager : Singleton<HammerManager>
    {
        [SerializeField] private float lowSpeed, highSpeed;
        [SerializeField] private float timeInterval = 1f;
        private float _cd = 0f;
        private bool _isTriggered = false;

        protected override void Awake()
        {
            base.Awake();
            _cd = timeInterval;
            SetHammerSpeedSlow();
        }

        void Update()
        {
            if (TouchManager.Instance.IsTouchDown())
            {
                _cd = timeInterval;
                _isTriggered = false;
                SetHammerSpeedFast();
            }

            if (_cd > 0 && !_isTriggered)
            {
                _cd -= Time.deltaTime;
            
                if (_cd <= Time.deltaTime)
                {
                    TriggerEvents();
                }
            }
        }

        private void TriggerEvents()
        {
            _isTriggered = true;
            SetHammerSpeedSlow();
            //Enable guide panel
        }

        private void SetHammerSpeedSlow() => HammerComponent.SetRotateSpeed(lowSpeed);

        private void SetHammerSpeedFast() => HammerComponent.SetRotateSpeed(highSpeed);
    }
}