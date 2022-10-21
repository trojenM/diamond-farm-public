using DorudonGames.Runtime.Component;
using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.EventServices.Resources.Game;
using DorudonGames.Runtime.Misc;
using DorudonGames.Runtime.Scriptables;
using UnityEngine;

namespace DorudonGames.Runtime.Manager
{
    public class HammerManager : Singleton<HammerManager>
    {
        [SerializeField] private HammerComponent[] hammers;
        [SerializeField] private UpgradeInfo speedUpgrade;
        [SerializeField] private float lowSpeed, highSpeed;
        [SerializeField] private float timeInterval = 1f;
        private float _cd = 0f;
        private bool _isTriggered = false;

        protected override void Awake()
        {
            base.Awake();
            //EventService.AddListener<UpgradeEarnedEvent>();
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

        public void StopHammers()
        {
            foreach (var hammer in hammers)
            {
                hammer.StopHammer();
            }
        }
        
        public void StartHammers()
        {
            foreach (var hammer in hammers)
            {
                hammer.StartHammer();
            }   
        }
        
        public void GetSpeedUpgrade(UpgradeEarnedEvent e)
        {
                   
        }

        private void SetHammerSpeedSlow() => HammerComponent.SetRotateSpeed(lowSpeed);

        private void SetHammerSpeedFast() => HammerComponent.SetRotateSpeed(highSpeed);
    }
}
