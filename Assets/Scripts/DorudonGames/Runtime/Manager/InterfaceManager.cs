using TMPro;
using System;
using DG.Tweening;
using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.Manager;
using DorudonGames.Runtime.Misc;
using UnityEngine;
using MoreMountains.NiceVibrations;

namespace DorudonGames.Runtime.Manager
{
    public class InterfaceManager : Singleton<InterfaceManager>
    {
        #region Serialzable Fields

        [Header("Transforms")]
        [SerializeField] private RectTransform m_canvas;
        [SerializeField] private RectTransform m_chargeSlot;
        [SerializeField] private RectTransform m_currencySlot;
        [SerializeField] private RectTransform m_floatingSlot;

        [Header("Panels")] 
        [SerializeField] private UIWinPanel m_winPanel;
        [SerializeField] private UILosePanel m_losePanel;
        [SerializeField] private UIPlayer m_playerPanel;
        [SerializeField] private UIUpgrade m_upgradePanel;

        [Header("Texts")] 
        [SerializeField] private TMP_Text m_levelText;
        [SerializeField] private TMP_Text m_currencyText;
        
        [Header("Canvas Groups")] 
        [SerializeField] private CanvasGroup m_menuCanvasGroup;
        [SerializeField] private CanvasGroup m_gameCanvasGroup;
        [SerializeField] private CanvasGroup m_commonCanvasGroup;
        [SerializeField] private CanvasGroup m_settingsCanvasGroup;

        [Header("Prefabs")] 
        [SerializeField] private RectTransform m_chargePrefab;
        [SerializeField] private RectTransform m_currencyPrefab;
        
        #endregion
        
        /// <summary>
        /// Awake.
        /// </summary>
        protected override void Awake()
        {
            //EventService.AddListener<GameStateChangedEvent>(OnGameStateChanged);
            //EventService.AddListener<CurrencyUpdatedEvent>(OnPlayerCurrencyUpdated);
            
            base.Awake();
        }

        /// <summary>
        /// This function helper for change settings panel state.
        /// </summary>
        /// <param name="state"></param>
        public void ChangeSettingsPanelState(bool state)
        {
            if(DOTween.IsTweening(m_settingsCanvasGroup.GetInstanceID()))
                return;
            
            Sequence sequence = DOTween.Sequence();

            sequence.Join(m_settingsCanvasGroup.DOFade(state ? 1 : 0, 0.25F));
            sequence.OnComplete(() =>
            {
                m_settingsCanvasGroup.blocksRaycasts = state;
            });

            sequence.SetId(m_settingsCanvasGroup.GetInstanceID());
            sequence.Play();
        }
        
        /// <summary>
        /// This function helper for fly currency animation to target currency icon.
        /// </summary>
        /// <param name="worldPosition"></param>
        public void FlyChargeFromWorld(Vector3 worldPosition)
        {
            Camera targetCamera = CameraManager.Instance.GetCamera();
            Vector3 screenPosition = GameUtils.WorldToCanvasPosition(m_canvas, targetCamera, worldPosition);
            Vector3 targetScreenPosition = m_canvas.InverseTransformPoint(m_chargeSlot.position);
                
            RectTransform createdCurrency = Instantiate(m_chargePrefab, m_canvas);
            createdCurrency.anchoredPosition = screenPosition;
            createdCurrency.localScale = Vector3.zero;

            Sequence sequence = DOTween.Sequence();

            sequence.Join(createdCurrency.DOScale(Vector3.one * 2.5F, 0.25F).SetEase(Ease.OutBack));
            sequence.Join(createdCurrency.transform.DOLocalMove(targetScreenPosition, 0.25F).SetEase(Ease.Linear).SetDelay(0.25F));
            sequence.Join(createdCurrency.transform.DOScale(Vector3.one, 0.25F).SetEase(Ease.Linear).SetDelay(0.125F));

            sequence.OnComplete(() =>
            {
                Destroy(createdCurrency.gameObject);
            });

            sequence.Play();
            
            SoundManager.Instance.Play(CommonTypes.SFX_COLLECT);
            VibrationManager.Instance.Haptic(HapticTypes.Success);
        }

        /// <summary>
        /// This function helper for fly currency animation to target currency icon.
        /// </summary>
        /// <param name="worldPosition"></param>
        public void FlyCurrencyFromWorld(Vector3 worldPosition)
        {
            Camera targetCamera = CameraManager.Instance.GetCamera();
            Vector3 screenPosition = GameUtils.WorldToCanvasPosition(m_canvas, targetCamera, worldPosition);
            Vector3 targetScreenPosition = m_canvas.InverseTransformPoint(m_currencySlot.position);
                
            RectTransform createdCurrency = Instantiate(m_currencyPrefab, m_canvas);
            createdCurrency.anchoredPosition = screenPosition;
            
            Sequence sequence = DOTween.Sequence();

            sequence.Join(createdCurrency.transform.DOLocalMove(targetScreenPosition, 0.5F));

            sequence.OnComplete(() =>
            {
                Destroy(createdCurrency.gameObject);
            });

            sequence.Play();
            
            SoundManager.Instance.Play(CommonTypes.SFX_CURRENCY_FLY);
            VibrationManager.Instance.Haptic(HapticTypes.Success);
        }
        
        /// <summary>
        /// This function helper for fly currency animation to target currency icon.
        /// </summary>
        /// <param name="screenPosition"></param>
        public void FlyCurrencyFromScreen(Vector3 screenPosition)
        {
            Vector3 targetScreenPosition = m_canvas.InverseTransformPoint(m_currencySlot.position);
                
            RectTransform createdCurrency = Instantiate(m_currencyPrefab, m_canvas);
            createdCurrency.position = screenPosition;
            
            Sequence sequence = DOTween.Sequence();

            sequence.Join(createdCurrency.transform.DOLocalMove(targetScreenPosition, 0.5F));

            sequence.OnComplete(() =>
            {
                Destroy(createdCurrency.gameObject);
            });

            sequence.Play();
            
            SoundManager.Instance.Play(CommonTypes.SFX_CURRENCY_FLY);
            VibrationManager.Instance.Haptic(HapticTypes.Success);
        }

        /// <summary>
        /// This function makes floating text appear.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="color"></param>
        /// <param name="force"></param>
        public void PrintFloatingText(string message, Color color, bool force = false)
        {
            if (DOTween.IsTweening(CommonTypes.TWEEN_FLOATING))
            {
                if(!force)
                    return;

                DOTween.Kill(CommonTypes.TWEEN_FLOATING, true);
            }
            
            GameSettings gameSettings = BootstrapManager.Instance.GetGameSettings();
            TMP_Text createdText = Instantiate(gameSettings.UIFloatingPrefab, m_floatingSlot);

            createdText.text = message;
            createdText.color = color;
            
            createdText.transform.localScale = Vector3.zero;

            Sequence sequence = DOTween.Sequence();

            sequence.Join(createdText.transform.DOScale(Vector3.one, 0.25F));
            sequence.Join(createdText.transform.DOLocalMoveY(1, 0.25F).SetDelay(0.5F));
            sequence.Join(createdText.DOFade(0, 0.25F));

            sequence.SetId(CommonTypes.TWEEN_FLOATING);
            sequence.Play();
        }
        
        /// <summary>
        /// This function called when game state changed.
        /// </summary>
        /// <param name="e"></param>
        private void OnGameStateChanged(GameStateChangedEvent e)
        {
            switch (e.GameState)
            {
                case EGameState.STAND_BY:

                    m_levelText.text = $"Level {LevelService.GetCachedLevel(1)}";
                    
                    break;
                case EGameState.STARTED:
                    
                    m_upgradePanel.UpdateVisualState(false);

                    TouchManager.Instance.ChangeJoystickState(true);
                    GameUtils.SwitchCanvasGroup(m_menuCanvasGroup, m_gameCanvasGroup);
                    
                    break;
                case EGameState.WIN:

                    TouchManager.Instance.ChangeJoystickState(false);
                    m_winPanel.Initialize();

                    break;
                case EGameState.LOSE:
                    
                    TouchManager.Instance.ChangeJoystickState(false);
                    m_losePanel.Initialize();
                    
                    break;
                case EGameState.GAME_STATE_01:

                    TouchManager.Instance.ChangeJoystickState(false);
                    m_upgradePanel.Initialize();
                    
                    break;
            }
        }
        
        /// <summary>
        /// This function called when player currency updated.
        /// </summary>
        /// <param name="e"></param>
        private void OnPlayerCurrencyUpdated(CurrencyUpdatedEvent e)
        {
            string editedText = m_currencyText.text;

            editedText = editedText.Replace(".", "");
            editedText = editedText.Replace(",", "");
            
            int cachedCurrency = int.Parse(editedText);

            float delay = e.UIDelay;
            float duration = e.UIDuration == -1 ? CommonTypes.UI_DEFAULT_FLY_CURRENCY_DURATION : e.UIDuration;
            
            Sequence sequence = DOTween.Sequence();

            sequence.Join(DOTween.To(() => cachedCurrency, x => cachedCurrency = x, e.Currency, duration).SetDelay(delay));

            sequence.OnUpdate(() =>
            {
                m_currencyText.text = $"{cachedCurrency.ToString("N0").Replace(",",String.Empty)}";
            });

            sequence.SetId(m_currencyText.GetInstanceID());
            sequence.Play();
        }

        /// <summary>
        /// This function returns related player panel.
        /// </summary>
        /// <returns></returns>
        public UIPlayer GetPlayerPanel()
        {
            return m_playerPanel;
        }

        /// <summary>
        /// This function called when this component destroyed.
        /// </summary>
        protected override void OnDestroy()
        {
            EventService.RemoveListener<GameStateChangedEvent>(OnGameStateChanged);
            EventService.RemoveListener<CurrencyUpdatedEvent>(OnPlayerCurrencyUpdated);

            base.OnDestroy();
        }
    }  
}

