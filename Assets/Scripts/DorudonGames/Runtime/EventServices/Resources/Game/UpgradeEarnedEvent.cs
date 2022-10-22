using DorudonGames.Runtime.Enum;

namespace DorudonGames.Runtime.EventServices.Resources.Game
{
    public class UpgradeEarnedEvent : BaseEvent
    {
        public UpgradeType UpgradeType;
        public float UpgradeLevelValue;
    }
}