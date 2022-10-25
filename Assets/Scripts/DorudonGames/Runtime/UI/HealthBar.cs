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
        [SerializeField] private float fillSpeed = 5f;
        private float _currentAmount = 0f;
        private float _targetAmount = 0f;

        private void LateUpdate()
        {
            _currentAmount = foregroundImage.fillAmount;
            foregroundImage.fillAmount = Mathf.MoveTowards(_currentAmount, _targetAmount, fillSpeed*Time.deltaTime);
        }

        public void HandleHealthChange(float pct)
        {
            _targetAmount = pct;
        }
    }
}
