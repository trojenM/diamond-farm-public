using System;
using System.Collections.Generic;
using DG.Tweening;
using DorudonGames.Runtime.Component;
using DorudonGames.Runtime.Enum;
using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.EventServices.Resources.Game;
using DorudonGames.Runtime.Manager;
using DorudonGames.Runtime.Misc;
using UnityEngine;

namespace DorudonGames.Runtime.Controllers
{
    public class DiamondController : MonoBehaviour
    {
        [SerializeField] private List<Diamond> diamondList;
        [SerializeField] private ParticleSystem destroyParticle;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private Transform placedPosition;
        [SerializeField] private Transform destroyPosition;
        [SerializeField] private float spawnTime;
        [SerializeField] private float animationTime;
        private int _current = 0, _max, _diaLevel = 0, _spawnedLevel = 0, _spawnedIndex = 0;
        private int incomeMul;

        private void Awake()
        {
            _current = PlayerPrefs.GetInt(CommonTypes.CURRENT_DIAMOND_DATA_KEY, 0);
            EventService.AddListener<UpgradeEarnedEvent>(OnUpgradeEarned);
            
        }

        public void SpawnNextDiamond()
        {
            _current++;
            if (_current >= _max)
                  _current = 0;
            PlayerPrefs.SetInt(CommonTypes.CURRENT_DIAMOND_DATA_KEY, _current);

            var diamond = diamondList[_diaLevel].diamondItems[_current];
            _spawnedLevel = _diaLevel;
            _spawnedIndex = _current;
            diamond.gameObject.SetActive(true);
            diamond.tr.position = spawnPosition.position;
            diamond.tr.DOMove(placedPosition.position, spawnTime).SetEase(Ease.OutBounce);
        }

        public void SpawnCurrentDiamond()
        {
            var diamond = diamondList[_diaLevel].diamondItems[_current];
            _spawnedLevel = _diaLevel;
            _spawnedIndex = _current;
            diamond.gameObject.SetActive(true);
            diamond.tr.position = placedPosition.position;
        }

        public void AnimateAndDestroy()
        {
            var diamond = diamondList[_spawnedLevel].diamondItems[_spawnedIndex];
            Sequence sequence = DOTween.Sequence();
            sequence.Join(diamond.tr.DOLocalRotate(new Vector3(0, 720, 0), animationTime, RotateMode.FastBeyond360).SetRelative(true)
                .SetEase(Ease.Linear));
            sequence.Join(diamond.tr.DOMove(destroyPosition.position, animationTime));
            sequence.OnComplete(() =>
            {
                LevelManager.Instance.IncreaseCreditAmount(350 * incomeMul);
                SoundManager.Instance.Play(CommonTypes.SFX_DIAMOND_DESTROY);
                InterfaceManager.Instance.FlyCurrencyTextFromWorld(diamond.transform.position, 350 * incomeMul);
                InterfaceManager.Instance.ActivateNewDiamondAchievedScreen(_spawnedLevel, _spawnedIndex, 350 * incomeMul);
                destroyParticle.Play();
                diamond.gameObject.SetActive(false);
                FlowManager.Instance.NextFlow();
            });
        }

        private void OnUpgradeEarned(UpgradeEarnedEvent e)
        {
            if (e.UpgradeType != UpgradeType.INCOME)
                return;
            
            _diaLevel = (int)e.UpgradeLevelValue-1;
            incomeMul = (int)e.UpgradeLevelValue;
            _max = diamondList[_diaLevel].diamondItems.Length;
            if (_current >= _max)
                _current = 0;
        }
    }

    [Serializable]
    public class Diamond
    {
        public DiamondComponent[] diamondItems;
    }
}