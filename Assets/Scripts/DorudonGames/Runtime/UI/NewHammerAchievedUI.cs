using DG.Tweening;
using UnityEngine;

namespace DorudonGames.Runtime.UI
{
    public class NewHammerAchievedUI : MonoBehaviour
    {
        [SerializeField] private GameObject screenObj;
        [SerializeField] private RectTransform radialTr;
        [SerializeField] private RectTransform newHammerAchievedTr;
        [SerializeField] private GameObject tapToContinueObj;

        public void ShowNewHammerUIPanel()
        {
            screenObj.SetActive(true);
            radialTr.localScale = Vector3.zero;
            newHammerAchievedTr.localScale = Vector3.zero;
            tapToContinueObj.SetActive(false);
            Sequence seq = DOTween.Sequence();
            seq.Append(radialTr.DOScale(Vector3.one, 0.5f));
            seq.Append(newHammerAchievedTr.DOScale(Vector3.one, 0.5f).OnComplete((() => tapToContinueObj.SetActive(true))));
        }

        public void HideNewHammerUIPanel()
        {
            screenObj.SetActive(false);
        }
    }
}
