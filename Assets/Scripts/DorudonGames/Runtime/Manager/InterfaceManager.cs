 using System;
 using DG.Tweening;
 using DorudonGames.Runtime.Component;
 using DorudonGames.Runtime.EventServices;
 using DorudonGames.Runtime.EventServices.Resources.Game;
 using DorudonGames.Runtime.Manager;
 using DorudonGames.Runtime.Misc;
 using DorudonGames.Runtime.UI;
 using UnityEngine;
 using MoreMountains.NiceVibrations;
 using TMPro;
 using Random = UnityEngine.Random;

 namespace DorudonGames.Runtime.Manager
 {
     public class InterfaceManager : Singleton<InterfaceManager>
     {
         [Header("Transforms")]
         [SerializeField] private RectTransform canvas;
         [SerializeField] private RectTransform chargeSlot;
         [SerializeField] private RectTransform creditSlot;
         
         [Header("Texts")] 
         [SerializeField] private TMP_Text creditText;

         [Header("Prefabs")] 
         [SerializeField] private RectTransform chargePrefab;
         [SerializeField] private RectTransform creditPrefab;
         [SerializeField] private TMP_Text creditTextPrefab;

         [SerializeField] private RenderHammerComponent renderHammer;
         [SerializeField] private NewHammerAchievedUI newHammerAchievedUI;


         protected override void Awake()
         {
             base.Awake();
             EventService.AddListener<CreditUpdatedEvent>(OnCreditUpdated);
         }

         public void ActivateNewHammerAchievedScreen(int hammerIdx)
         {
             renderHammer.SwitchHammer(hammerIdx);
             newHammerAchievedUI.ShowNewHammerUIPanel();
         }

         /// <summary>
         /// This function helper for fly currency animation to target currency icon.
         /// </summary>
         /// <param name="worldPosition"></param>
         public void FlyChargeFromWorld(Vector3 worldPosition)
         {
             Camera targetCamera = CameraManager.Instance.GetCamera();
             Vector3 screenPosition = GameUtils.WorldToCanvasPosition(canvas, targetCamera, worldPosition);
             Vector3 targetScreenPosition = canvas.InverseTransformPoint(chargeSlot.position);
                
             RectTransform createdCurrency = Instantiate(chargePrefab, canvas);
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
            
             SoundManager.Instance.Play("CreditIncrease");
             VibrationManager.Instance.Haptic(HapticTypes.Success);
         }
         
         /// <summary>
         /// This function helper for fly currency animation to target currency icon.
         /// </summary>
         /// <param name="screenPosition"></param>
         public void FlyCurrencyFromWorld(Vector3 worldPosition)
         {
             Camera targetCamera = CameraManager.Instance.GetCamera();
             Vector3 screenPosition = GameUtils.WorldToCanvasPosition(canvas, targetCamera, worldPosition);
             Vector3 targetScreenPosition = canvas.InverseTransformPoint(creditSlot.position);
                
             RectTransform createdCurrency = Instantiate(creditPrefab, canvas);
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
             Vector3 targetScreenPosition = canvas.InverseTransformPoint(creditSlot.position);
                
             RectTransform createdCurrency = Instantiate(creditPrefab, canvas);
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
         
         // public void FlyCurrencyTextFromScreen(Vector3 screenPosition, int amount)
         // {
         //     Vector3 targetScreenPosition = canvas.InverseTransformPoint(creditSlot.position);
         //        
         //     TMP_Text createdCurrency = Instantiate(creditTextPrefab, canvas);
         //     createdCurrency.text = "+" + amount;
         //     createdCurrency.rectTransform.anchoredPosition = screenPosition + new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0);
         //    
         //     Sequence sequence = DOTween.Sequence();
         //
         //     sequence.Append(createdCurrency.rectTransform.DOScale(0f, .7f));
         //     sequence.Join(createdCurrency.transform.DOLocalMove(screenPosition + Vector3.up * 175f, .7F));
         //
         //     sequence.OnComplete(() =>
         //     {
         //         Destroy(createdCurrency.gameObject);
         //     });
         //
         //     sequence.Play();
         //    
         //     SoundManager.Instance.Play(CommonTypes.SFX_CURRENCY_FLY);
         //     VibrationManager.Instance.Haptic(HapticTypes.Success);
         // }
         
         public void FlyCurrencyTextFromWorld(Vector3 worldPosition, int amount)
         {
             Camera targetCamera = CameraManager.Instance.GetCamera();
             Vector3 screenPosition = GameUtils.WorldToCanvasPosition(canvas, targetCamera, worldPosition);
             //Vector3 targetScreenPosition = m_canvas.InverseTransformPoint(m_currencySlot.position);
                
             TMP_Text createdCurrency = Instantiate(creditTextPrefab, canvas);
             createdCurrency.text = "+" + amount;
             createdCurrency.rectTransform.anchoredPosition = screenPosition;
            
            
             Sequence sequence = DOTween.Sequence();

             sequence.Append(createdCurrency.rectTransform.DOScale(0f, .7f));
             sequence.Join(createdCurrency.transform.DOLocalMove(screenPosition + Vector3.up * 175f, .7F));
            
             sequence.OnComplete(() =>
             {
                 Destroy(createdCurrency.gameObject);
             });

             sequence.Play();

             VibrationManager.Instance.Haptic(HapticTypes.Success);
         }
         
         /// <summary>
         /// This function called when player currency updated.
         /// </summary>
         /// <param name="e"></param>
         private void OnCreditUpdated(CreditUpdatedEvent e)
         {
             string editedText = creditText.text;

             editedText = editedText.Replace(".", "");
             editedText = editedText.Replace(",", "");
            
             int cachedCurrency = int.Parse(editedText);

             float delay = e.UIDelay;
             float duration = e.UIDuration == -1 ? CommonTypes.UI_DEFAULT_FLY_CURRENCY_DURATION : e.UIDuration;
            
             Sequence sequence = DOTween.Sequence();

             sequence.Join(DOTween.To(() => cachedCurrency, x => cachedCurrency = x, e.Credit, duration).SetDelay(delay));

             sequence.OnUpdate(() =>
             {
                 creditText.text = $"{cachedCurrency.ToString("N0").Replace(",",String.Empty)}";
             });

             sequence.SetId(creditText.GetInstanceID());
             sequence.Play();
         }
         
         protected override void OnDestroy()
         {
             EventService.RemoveListener<CreditUpdatedEvent>(OnCreditUpdated);
             base.OnDestroy();
         }
     }  
 }

