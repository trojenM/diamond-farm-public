using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.Misc;

namespace DorudonGames.Runtime.Component
{
    public class HealthBar : Singleton <HealthBar>
    {
        [SerializeField] private Image foregroundImage;
        [SerializeField] private float _duration;
        

       
        public void HandleHealthChange(float pct)
        {
            foregroundImage.DOFillAmount(pct, _duration);
        }
    }
}
