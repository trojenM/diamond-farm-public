using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
using DorudonGames.Runtime.Misc;

namespace DorudonGames.Runtime.Component
{
    public class HealthBar : Singleton <HealthBar>
    {
        [SerializeField] private Image foregroundImage;
        [SerializeField] private float fillSpeed = 5f;
        private float _currentAmount = 0f;
        private float _targetAmount = 0f;
        private Color startColor, endColor;
        [SerializeField] private string startColorCode;
        [SerializeField] private string endColorCode;

        /*private void LateUpdate()
        {
            _currentAmount = foregroundImage.fillAmount;
            foregroundImage.fillAmount = Mathf.MoveTowards(_currentAmount, _targetAmount, fillSpeed*Time.deltaTime);
            
        }*/

        protected override void Awake()
        {
            startColor = HexToColor(startColorCode);
            endColor = HexToColor(endColorCode);
            
            base.Awake();
        }

        public void HandleHealthChange(float pct)
        {
            _targetAmount = pct;
            foregroundImage.fillAmount = _targetAmount;
            foregroundImage.color = Color.Lerp(startColor, endColor, _targetAmount);
        }

        public void HandleHealthChangeAnim(float pct)
        {
            _targetAmount = pct;
            foregroundImage.DOFillAmount(_targetAmount, 0.75f);
            foregroundImage.DOColor(endColor, 0.75f);

        }

        private Color HexToColor(string htmlValue)
        {
            Color color;
            if (ColorUtility.TryParseHtmlString(htmlValue, out color))
            {
                return color;
            }
            
            return Color.white;
        }
    }
}
