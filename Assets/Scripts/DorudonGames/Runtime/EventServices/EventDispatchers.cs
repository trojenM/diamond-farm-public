using DorudonGames.Runtime.Enum;
using DorudonGames.Runtime.EventServices.Resources.Game;
using UnityEngine;

namespace DorudonGames.Runtime.EventServices
{
    public static class EventDispatchers
    {
        private static bool beforeHideState = true;
        
        public static void OnCreditUpdatedDispatcher(int credit, float uiDelay, float uiDuration)
        {
            CreditUpdatedEvent creditUpdatedEvent = new CreditUpdatedEvent()
            {
                Credit = credit,
                UIDelay = uiDelay,
                UIDuration = uiDuration,
            };
            EventService.DispatchEvent(creditUpdatedEvent);
        }
        
        public static void DispatchUpgradeEarned(UpgradeType upgradeType, float upgradeLevelValue)
        {
            UpgradeEarnedEvent upgradeEarnedEvent = new UpgradeEarnedEvent()
            {
                UpgradeType = upgradeType,
                UpgradeLevelValue = upgradeLevelValue,
            };
            EventService.DispatchEvent(upgradeEarnedEvent);
        }

        public static void DispatchHammerSpeed(float speed, bool isHammerStopped = false)
        {
            UpdateHammerSpeedEvent updateHammerSpeedEvent = new UpdateHammerSpeedEvent()
            {
                Speed = speed,
                IsHammerStopped = isHammerStopped,
            };
            EventService.DispatchEvent(updateHammerSpeedEvent);
        }

        public static void DispatchHammerPower(float power)
        {
            UpdateHammerPowerEvent updateHammerPowerEvent = new UpdateHammerPowerEvent()
            {
                Power = power,
            };
            EventService.DispatchEvent(updateHammerPowerEvent);
        }

        public static void DispatchHideUpgrades(bool isHide)
        {
            if (beforeHideState == isHide)
                return;
            
            HideUpgradesEvent hideUpgradesEvent = new HideUpgradesEvent()
            {
                IsHide = isHide,
            };
            EventService.DispatchEvent(hideUpgradesEvent);
            
            beforeHideState = isHide;
        }
        public static void DispatchOnHammerHit(float damage , Transform castPosition )
        {
            OnHammerHitEvent onHammerHitEvent = new OnHammerHitEvent()
            {
                Damage = damage,
                Pos = castPosition 

            };
            EventService.DispatchEvent(onHammerHitEvent);
        }
       
    }
}