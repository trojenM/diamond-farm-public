using System;
using System.Collections;
using System.Collections.Generic;
using DorudonGames.Runtime.Enum;
using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.EventServices.Resources.Game;
using DorudonGames.Runtime.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeItem : MonoBehaviour
{
    [SerializeField] private UpgradeInfo info;
    [SerializeField] private TMP_Text headerText;
    [SerializeField] private Image iconImage;
    
    protected UpgradeType UpgradeType;
    protected int UpgradeLevel;
    protected bool Interactable;

    
    public void SetInteractable(bool x) => Interactable = x;
    public bool GetInteractable => Interactable;

    protected void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        iconImage.sprite = info.Icon;
        headerText.text = info.Header;
        UpgradeType = info.UpgradeType;
        UpgradeLevel = PlayerPrefs.GetInt(info.UpgradeType.ToString(), 1);
    }

    protected virtual void Select()
    {
        if (!Interactable)
            return;
        
        UpgradeLevel += 1;
        PlayerPrefs.SetInt(info.UpgradeType.ToString(), UpgradeLevel);
        DispatchUpgradeEarned();
    }


    private void DispatchUpgradeEarned()
    {
        UpgradeEarnedEvent upgradeEarnedEvent = new UpgradeEarnedEvent()
        {
            UpgradeType = UpgradeType,
            UpgradeLevel = UpgradeLevel,
        };
        EventService.DispatchEvent(upgradeEarnedEvent);
    }
    
}
