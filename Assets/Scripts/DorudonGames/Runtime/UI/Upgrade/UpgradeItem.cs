using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private TMP_Text levelText;
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
        headerText.text = info.Header;
        UpgradeType = info.UpgradeType;
        UpgradeLevel = PlayerPrefs.GetInt(info.UpgradeType.ToString(), 1);
        levelText.text = ("$" + info.LevelsAndCosts[UpgradeLevel - 1].Cost);
    }

    public  virtual void Select()
    {

        if (!Interactable)
            return;
        if(UpgradeLevel <= info.LevelsAndCosts.Length && GameManager.Instance.GetCreditAmount >= info.LevelsAndCosts[UpgradeLevel-1].Cost)
        {
            UpgradeLevel += 1;
            PlayerPrefs.SetInt(info.UpgradeType.ToString(), UpgradeLevel);
            EventDispatchers.DispatchUpgradeEarned(UpgradeType, UpgradeLevel);
            SoundManager.Instance.Play("UpgradeEarned");
            if(UpgradeLevel < info.LevelsAndCosts.Length)
            {
                levelText.text = ("$" + info.LevelsAndCosts[UpgradeLevel - 1].Cost);
            }
            else
            {
                levelText.text = (" Max ");
                Interactable = false;
            }
     
        }
        
       
    }

}
