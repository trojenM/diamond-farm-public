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

    private void OnUpgradeEarned(UpgradeEarnedEvent e)
    {
        if (e.UpgradeType != UpgradeType.POWER)
            return;
        
        int remainder = ((int)e.UpgradeLevelValue) % 4;
         currenSlotIdx = remainder;
         currentHammerIdx = (((int)e.UpgradeLevelValue) - remainder) / 4;
         
         UpgradeHammer();
    }
}
