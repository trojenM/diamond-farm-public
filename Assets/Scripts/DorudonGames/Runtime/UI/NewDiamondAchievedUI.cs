using DG.Tweening;
using UnityEngine;

namespace DorudonGames.Runtime.UI
{
    public class NewDiamondAchievedUI : MonoBehaviour
    {
        [SerializeField] private GameObject screenObj;
        [SerializeField] private RectTransform radialTr;
        [SerializeField] private RectTransform newDiamondAchievedTr;
        [SerializeField] private GameObject tapToContinueObj;
        [SerializeField] private Camera renderCamera;

        public void ShowNewDiamondUIPanel()
        {
            renderCamera.gameObject.SetActive(true);
            screenObj.SetActive(true);
            radialTr.localScale = Vector3.zero;
            newDiamondAchievedTr.localScale = Vector3.zero;
            tapToContinueObj.SetActive(false);
            Sequence seq = DOTween.Sequence();
            seq.Append(radialTr.DOScale(Vector3.one, 0.5f));
            seq.Append(newDiamondAchievedTr.DOScale(Vector3.one, 0.5f).OnComplete((() => tapToContinueObj.SetActive(true))));
        }

        public void HideNewDiamondUIPanel()
        {
            screenObj.SetActive(false);
            renderCamera.gameObject.SetActive(false);
        }
    }
}