using System;
using DG.Tweening;
using DorudonGames.Runtime.Enum;
using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.EventServices.Resources.Game;
using DorudonGames.Runtime.Manager;
using DorudonGames.Runtime.Misc;
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
    protected RectTransform RectTr;
    protected float DefaultAnchorY;


    public void SetInteractable(bool x) => Interactable = x;
    public bool GetInteractable => Interactable;

    protected void Awake()
    {
        Initialize();
        EventService.AddListener<HideUpgradesEvent>(OnHideUpgradesEvent);
    }

    private void Start()
    {
        EventDispatchers.DispatchUpgradeEarned(UpgradeType, info.LevelsAndCosts[UpgradeLevel-1].LevelValue);
    }

    public void OnHideUpgradesEvent(HideUpgradesEvent e)
    {
        if (e.IsHide)
        {
            RectTr.DOAnchorPosY(DefaultAnchorY - 750f, 0.5f);
        }
        else
        {
            RectTr.DOAnchorPosY(DefaultAnchorY, 0.5f);
        }
    }

    private void Initialize()
    {
        Interactable = true;
        iconImage.sprite = info.Icon;
        headerText.text = info.Header;
        UpgradeType = info.UpgradeType;
        RectTr = GetComponent<RectTransform>();
        DefaultAnchorY = RectTr.anchoredPosition.y;
        UpgradeLevel = PlayerPrefs.GetInt(info.UpgradeType.ToString(), 1);
        UpdateButton();
    }

    public virtual void Select()
    {
        if (!Interactable)
            return;
        
        if (GameManager.Instance.GetCreditAmount >= info.LevelsAndCosts[UpgradeLevel-1].Cost)
        {
            LevelManager.Instance.IncreaseCreditAmount(- info.LevelsAndCosts[UpgradeLevel-1].Cost);
            UpgradeLevel += 1;
            PlayerPrefs.SetInt(info.UpgradeType.ToString(), UpgradeLevel);
            EventDispatchers.DispatchUpgradeEarned(UpgradeType, info.LevelsAndCosts[UpgradeLevel-1].LevelValue);
            SoundManager.Instance.Play(CommonTypes.SFX_UPGRADE_EARNED);
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
