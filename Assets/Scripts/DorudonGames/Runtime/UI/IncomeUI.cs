using DorudonGames.Runtime.Component;
using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.EventServices.Resources.Game;
using TMPro;
using UnityEngine;

namespace DorudonGames.Runtime.UI
{
    public class IncomeUI : MonoBehaviour
    {
        [SerializeField] private Transform incomeMover;
        [SerializeField] private TMP_Text incomeText;
        private int hammersIncome;

        private void Awake()
        {
            EventService.AddListener<UpdateHammerSpeedEvent>(OnHammerSpeedUpdate);
        }

        private void OnHammerSpeedUpdate(UpdateHammerSpeedEvent e)
        {
            float speed = e.Speed;
            speed = Mathf.Lerp(1f, e.HammerSpeedMul*2, e.SpeedValue);
            incomeText.text = "$" + (int)(hammersIncome *  speed) + "/sec";
        }

        public void CalculateHammersIncome()
        {
            int hammerIncomeSum = 0;
            foreach (var hammer in incomeMover.GetComponentsInChildren<HammerComponent>())
            {
                hammerIncomeSum += hammer.GetIncome();
            }

            hammersIncome = hammerIncomeSum;
            print(hammerIncomeSum);
        }
    }
}
