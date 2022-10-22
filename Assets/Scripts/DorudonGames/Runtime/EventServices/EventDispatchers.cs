using DorudonGames.Runtime.Enum;
using DorudonGames.Runtime.EventServices.Resources.Game;

namespace DorudonGames.Runtime.EventServices
{
    public static class EventDispatchers
    {
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
    }
}