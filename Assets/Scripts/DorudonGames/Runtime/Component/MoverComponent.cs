using DorudonGames.Runtime.Enum;
using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.EventServices.Resources.Game;
using DorudonGames.Runtime.Manager;
using DorudonGames.Runtime.UI;
using UnityEngine;
using EventService = DorudonGames.Runtime.EventServices.EventService;

namespace DorudonGames.Runtime.Component
{
    public class MoverComponent : MonoBehaviour
    {
        [SerializeField] private IncomeUI incomeUI;
        [SerializeField] private Transform [] hammerSlots;
        [SerializeField] private GameObject[] hammers;
        private int currentHammerIdx;
        private int currenSlotIdx;
        private int upgradeLevel;
        private bool firstRun;
        private float incomeMul;

        private void Awake()
        {
            EventService.AddListener<UpgradeEarnedEvent>(OnUpgradeEarned);
        }

        void Start()
        {
        }

        private void UpgradeHammer()
        {
            if (hammerSlots[currenSlotIdx].childCount == 0)
            {
                var hammer = Instantiate(hammers[currentHammerIdx], hammerSlots[currenSlotIdx]);
                hammer.GetComponentInChildren<HammerComponent>().PlaySpawnParticle();
            }
            else if (hammerSlots[currenSlotIdx].childCount == 1)
            {
                Destroy(hammerSlots[currenSlotIdx].GetChild(0).gameObject);
                var hammer = Instantiate(hammers[currentHammerIdx], hammerSlots[currenSlotIdx]);
                hammer.GetComponentInChildren<HammerComponent>().PlaySpawnParticle();
            }
            EventDispatchers.DispatchHammerIncome(incomeMul);

        }

        public void InitializeHammer()
        {
            for (int i = 0; i < 4; i++)
            {
                if (upgradeLevel - i <= -1)
                {
                    firstRun = true;
                    return;
                }

                int remainder = (upgradeLevel - i) % 4;
                int hammerIdx = ((upgradeLevel - i) - remainder) / 4;
                Instantiate(hammers[hammerIdx], hammerSlots[remainder]);
            }

            EventDispatchers.DispatchHammerIncome(incomeMul);
            incomeUI.CalculateHammersIncome();
            firstRun = true;
        }

        private void OnUpgradeEarned(UpgradeEarnedEvent e)
        {
            if (e.UpgradeType == UpgradeType.POWER)
            {
                upgradeLevel = (int)e.UpgradeLevelValue;
                int remainder = (upgradeLevel) % 4;
                currenSlotIdx = remainder;
                currentHammerIdx = ((upgradeLevel) - remainder) / 4;

                if (firstRun)
                {
                    if (remainder == 0)
                        InterfaceManager.Instance.ActivateNewHammerAchievedScreen(currentHammerIdx - 1);
             
                    UpgradeHammer();
                    incomeUI.CalculateHammersIncome();
                }
            }
            else if (e.UpgradeType == UpgradeType.INCOME)
            {
                incomeMul = e.UpgradeLevelValue;
                
                if (firstRun)
                {
                    EventDispatchers.DispatchHammerIncome(incomeMul);
                    incomeUI.CalculateHammersIncome();
                }
            }
        }
    }
}
