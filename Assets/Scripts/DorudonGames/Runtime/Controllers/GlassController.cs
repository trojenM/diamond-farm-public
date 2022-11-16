using System;
using System.Collections.Generic;
using DG.Tweening;
using DorudonGames.Runtime.Component;
using DorudonGames.Runtime.Enum;
using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.EventServices.Resources.Game;
using DorudonGames.Runtime.Misc;
using UnityEngine;
using EventService = DorudonGames.Runtime.EventServices.EventService;

namespace DorudonGames.Runtime.Manager
{
    public class GlassController : MonoBehaviour
    {
        [SerializeField] private List<Glass> glasses;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private Transform placedPosition;
        [SerializeField] private float spawnTime;
        private int glassLevel = 0, _current = 0, _max;

        private void Awake()
        {
            //_current = PlayerPrefs.GetInt(CommonTypes.CURRENT_DIAMOND_DATA_KEY, 0);
            EventService.AddListener<UpgradeEarnedEvent>(OnUpgradeEarned);
            // EventService.AddListener<OnHammerHitEvent>(OnHammerHit);
            _max = glasses[glassLevel].glassItems.Length;
        }

        [ContextMenu("DestroyCurrentGlass")]
        public void DestroyCurrentGlass()
        {
            var glass = glasses[glassLevel].glassItems[_current];
            glass.DestroyGlass();
        }
        
        [ContextMenu("SpawnNextGlass")]
        public void SpawnNextGlass()
        {
            glassLevel++;
            _current++;
            if (_current >= _max )
                _current = 0;
            if (glassLevel >=4)
                glassLevel  = 0;

            var glass = glasses[glassLevel].glassItems[_current];
            glass.ResetGlass();
            glass.gameObject.SetActive(true);
            glass.tr.position = spawnPosition.position;
            HealthBar.Instance.HandleHealthChangeAnim(glass.GetFirstPieceHealthPercentage());
            glass.tr.DOMove(placedPosition.position, spawnTime).SetEase(Ease.OutBounce).OnComplete(() => HammerManager.Instance.StartHammers());
        }

        public void SpawnCurrentGlass()
        {
            var glass = glasses[glassLevel].glassItems[_current];
            //glass.ResetGlass();
            glass.gameObject.SetActive(true);
            glass.tr.position = placedPosition.position;
            HealthBar.Instance.HandleHealthChange(glass.GetFirstPieceHealthPercentage());
            HammerManager.Instance.StartHammers();
        }

        private void OnUpgradeEarned(UpgradeEarnedEvent e)
        {
            if (e.UpgradeType != UpgradeType.INCOME)
                return;

           // glassLevel = (int)e.UpgradeLevelValue - 1;
        }

        // private void OnHammerHit(OnHammerHitEvent e)
        // {
        //     var glass = glasses[glassLevel].glassItems[_current];
        //     HealthBar.Instance.HandleHealthChange(glass.GetFirstPieceHealthPercentage());
        // }

        public GlassComponent GetCurrentGlass() => glasses[glassLevel].glassItems[_current] ? glasses[glassLevel].glassItems[_current] : null;
    }
    
    [Serializable]
    public class Glass
    {
        public GlassComponent[] glassItems;
    }
}