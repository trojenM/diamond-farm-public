using System.Collections;
using System.Collections.Generic;
using DorudonGames.Runtime.Manager;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;

public class EButton : Button
{
    protected override void Start()
    {
        onClick.AddListener(() =>
        {
            if (!IsInteractable())
                return;
            
            VibrationManager.Instance.Haptic(HapticTypes.HeavyImpact);
        });
    }
}