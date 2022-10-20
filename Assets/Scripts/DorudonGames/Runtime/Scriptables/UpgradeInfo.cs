using DorudonGames.Runtime.Enum;
using UnityEngine;

namespace DorudonGames.Runtime.Scriptables
{
    [CreateAssetMenu(fileName = "UpgradeInfo", menuName = "ScriptableObjects/UpdateInfo", order = 1)]
    public class UpgradeInfo : ScriptableObject
    {
        public Sprite Icon;
        public string Header;
        public UpgradeType UpgradeType;
    }
}