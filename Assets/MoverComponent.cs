using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using DG.Tweening;
using DorudonGames.Runtime.Enum;
using DorudonGames.Runtime.EventServices.Resources.Game;
using UnityEditor.MPE;
using UnityEngine;
using EventService = DorudonGames.Runtime.EventServices.EventService;

public class MoverComponent : MonoBehaviour
{
    [SerializeField] private Transform [] hammerSlots;
    [SerializeField] private GameObject[] hammers;
    private int currentHammerIdx;
    private int currenSlotIdx;
    private int upgradeLevel;
    private bool firstRun = true;

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
            Instantiate(hammers[currentHammerIdx], hammerSlots[currenSlotIdx]);
        }
        else if (hammerSlots[currenSlotIdx].childCount == 1)
        {
            Destroy(hammerSlots[currenSlotIdx].GetChild(0).gameObject);
            Instantiate(hammers[currentHammerIdx], hammerSlots[currenSlotIdx]);
        }
    }

    private void InitializeHammer()
    {
        for (int i = 0; i < upgradeLevel+1; i++)
        {
            int remainder = i % 4;
            int hammerIdx = (i - remainder) / 4;
            
            if (hammerSlots[remainder].childCount == 0)
            {
                Instantiate(hammers[hammerIdx], hammerSlots[remainder]);
            }
            else if (hammerSlots[remainder].childCount == 1)
            {
                Destroy(hammerSlots[remainder].GetChild(0).gameObject);
                Instantiate(hammers[hammerIdx], hammerSlots[remainder]);
            }
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
             InitializeHammer();
             firstRun = false;
         }
         else
             UpgradeHammer();
    }
}
