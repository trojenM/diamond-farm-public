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
        
        public static void DispatchUpgradeEarned(UpgradeType upgradeType, int upgradeLevel)
        {
            UpgradeEarnedEvent upgradeEarnedEvent = new UpgradeEarnedEvent()
            {
                UpgradeType = upgradeType,
                UpgradeLevel = upgradeLevel,
            };
            EventService.DispatchEvent(upgradeEarnedEvent);
        }
    }
}