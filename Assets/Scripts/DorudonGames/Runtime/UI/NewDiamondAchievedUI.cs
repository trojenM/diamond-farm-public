using DG.Tweening;
using DorudonGames.Runtime.Manager;
using TMPro;
using UnityEngine;

namespace DorudonGames.Runtime.UI
{
    public class NewDiamondAchievedUI : MonoBehaviour
    {
        [SerializeField] private GameObject screenObj;
        [SerializeField] private RectTransform radialTr;
        [SerializeField] private RectTransform newDiamondAchievedTr;
        [SerializeField] private TMP_Text valueText;
        [SerializeField] private GameObject tapToContinueObj;
        [SerializeField] private Camera renderCamera;

        public void ShowNewDiamondUIPanel(int gemValue)
        {
            renderCamera.gameObject.SetActive(true);
            screenObj.SetActive(true);
            radialTr.localScale = Vector3.zero;
            newDiamondAchievedTr.localScale = Vector3.zero;
            tapToContinueObj.SetActive(false);
            valueText.rectTransform.localScale = Vector3.zero;
            valueText.text = "Value: $" + gemValue;
            Sequence seq = DOTween.Sequence();
            seq.Append(radialTr.DOScale(Vector3.one, 0.5f));
            seq.Append(newDiamondAchievedTr.DOScale(Vector3.one, 0.5f));
            seq.Join(valueText.rectTransform.DOScale(Vector3.one, 0.5f).OnComplete(() => tapToContinueObj.SetActive(true)));
            seq.Play();
        }

        public void HideNewDiamondUIPanel()
        {
            screenObj.SetActive(false);
            renderCamera.gameObject.SetActive(false);
            InterfaceManager.Instance.FlyCurrencyFromScreen(radialTr.position);
        }
    }
}