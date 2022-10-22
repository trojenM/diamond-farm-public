using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
using DorudonGames.Runtime.Enum;
using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.EventServices.Resources.Game;
using DorudonGames.Runtime.Manager;
using DorudonGames.Runtime.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeItem : MonoBehaviour
{
    [SerializeField] private UpgradeInfo info;
    [SerializeField] private TMP_Text headerText;
    [SerializeField] private TMP_Text reqText;
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
        Interactable = true;
        iconImage.sprite = info.Icon;
        iconImage.color = Color.blue;
        headerText.text = info.Header;
        UpgradeType = info.UpgradeType;
        UpgradeLevel = PlayerPrefs.GetInt(info.UpgradeType.ToString(), 1);
        UpdateButton();
    }

    public virtual void Select()
    {
        if (!Interactable)
            return;
        
        print("test");
        // 1 2 3 4  
        if (GameManager.Instance.GetCreditAmount >= info.LevelsAndCosts[UpgradeLevel-1].Cost)
        {
            EventDispatchers.DispatchUpgradeEarned(UpgradeType, info.LevelsAndCosts[UpgradeLevel-1].LevelValue);
            UpgradeLevel += 1;
            PlayerPrefs.SetInt(info.UpgradeType.ToString(), UpgradeLevel);
            SoundManager.Instance.Play("UpgradeEarned");
            UpdateButton();
        }
    }

    private void UpdateButton()
    {
        if (UpgradeLevel >= info.LevelsAndCosts.Length)
        {
            reqText.text = "MAX";
            Interactable = false;
            return;
        }

        reqText.text = "$" + info.LevelsAndCosts[UpgradeLevel - 1].Cost;
    }
}
