using System.Collections.Generic;
using DorudonGames.Runtime.Component;
using DorudonGames.Runtime.Enum;
using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.EventServices.Resources.Game;
using DorudonGames.Runtime.Misc;
using DorudonGames.Runtime.Scriptables;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DorudonGames.Runtime.Manager
{
    public class HammerManager : Singleton<HammerManager>
    {
        //[SerializeField] private HammerComponent[] hammers;
        //[SerializeField] private UpgradeInfo speedUpgrade;
        //[SerializeField] private float lowSpeed, highSpeed;
        //[SerializeField] private float timeInterval = 1f;

        [SerializeField] private bool isHammerStopped;
        [SerializeField] private float baseSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float decreaseInterval;
        [SerializeField] private int incrementSegment;
        //private float _cd = 0f;
        //private bool _isTriggered = false;
        private LayerMask _uiLayer;
        private float _interval;
        private float _hammerSpeedValue;
        private float _hammerSpeedMul = 1;

        protected override void Awake()
        {
            base.Awake();
            EventService.AddListener<UpgradeEarnedEvent>(OnUpgradeEarned);
            _interval = decreaseInterval;
            _uiLayer = LayerMask.NameToLayer("UI");
        }

        private void OnUpgradeEarned(UpgradeEarnedEvent e)
        {
            if (e.UpgradeType == UpgradeType.SPEED)
                _hammerSpeedMul = e.UpgradeLevelValue;
            else if (e.UpgradeType == UpgradeType.POWER)
                SetHammerPower(e.UpgradeLevelValue);
        }

        void Update()
        {
            ManageHammerSpeed();
            EventDispatchers.DispatchHammerSpeed((baseSpeed + (_hammerSpeedValue * (maxSpeed - baseSpeed))) * _hammerSpeedMul, isHammerStopped);
        }
        
        private void ManageHammerSpeed()
        {
            if (TouchManager.Instance.IsTouchDown())
            {
                _hammerSpeedValue += 1f / incrementSegment; 
                _interval = decreaseInterval * 4f;
                if (!EventSystem.current.IsPointerOverGameObject() && !IsPointerOverUIElement())
                    EventDispatchers.DispatchHideUpgrades(true);
            }
            
            _interval -= Time.deltaTime;
            
            if (_interval <= 0f)
            {
                _hammerSpeedValue -= 1f / incrementSegment;
                _interval = decreaseInterval;
                if(!IsPointerOverUIElement())
                    EventDispatchers.DispatchHideUpgrades(false);
            }

            _hammerSpeedValue = Mathf.Clamp01(_hammerSpeedValue);
        }
        
        public void SetHammerPower(float power) { EventDispatchers.DispatchHammerPower(power); }
        public void StopHammers() { isHammerStopped = true; }
        public void StartHammers() { isHammerStopped = false; }

        public bool IsPointerOverUIElement()
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResults());
        }
    
        private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
        {
            for (int index = 0; index < eventSystemRaysastResults.Count; index++)
            {
                RaycastResult curRaysastResult = eventSystemRaysastResults[index];
                if (curRaysastResult.gameObject.layer == _uiLayer)
                    return true;
            }
            return false;
        }
        
        
        static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }
        
    }
}
