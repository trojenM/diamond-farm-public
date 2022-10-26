using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using DG.Tweening;
using DorudonGames.Runtime.Component;
using DorudonGames.Runtime.Enum;
using DorudonGames.Runtime.EventServices.Resources.Game;
using DorudonGames.Runtime.Manager;
using UnityEngine;
using EventService = DorudonGames.Runtime.EventServices.EventService;

public class MoverComponent : MonoBehaviour
{
    [SerializeField] private Transform [] hammerSlots;
    [SerializeField] private GameObject[] hammers;
    private int currentHammerIdx;
    private int currenSlotIdx;
    private int upgradeLevel;
    private bool firstRun;

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
    }

    private void OnUpgradeEarned(UpgradeEarnedEvent e)
    {
        if (e.UpgradeType != UpgradeType.POWER)
            return;

        upgradeLevel = (int)e.UpgradeLevelValue;
        int remainder = (upgradeLevel) % 4;
         currenSlotIdx = remainder;
         currentHammerIdx = ((upgradeLevel) - remainder) / 4;

         if (firstRun)
         {
             print("test");
             
             if (remainder == 0)
                 InterfaceManager.Instance.ActivateNewHammerAchievedScreen(currentHammerIdx - 1);
             
             UpgradeHammer();
         }
         
         
         // if (firstRun)
         // {
         //     InitializeHammer();
         //     firstRun = false;
         // }
         // else
         // {
         //     if (remainder == 0)
         //         InterfaceManager.Instance.ActivateNewHammerAchievedScreen(currentHammerIdx - 1);
         //     
         //     UpgradeHammer();
         // }
    }
}
