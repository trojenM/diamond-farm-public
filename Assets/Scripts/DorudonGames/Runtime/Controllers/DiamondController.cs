using System;
using DG.Tweening;
using DorudonGames.Runtime.Component;
using DorudonGames.Runtime.Manager;
using UnityEngine;

namespace DorudonGames.Runtime.Controllers
{
    public class DiamondController : MonoBehaviour
    {
        [SerializeField] private DiamondComponent[] diamonds;
        [SerializeField] private ParticleSystem destroyParticle;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private Transform placedPosition;
        [SerializeField] private Transform destroyPosition;
        [SerializeField] private float spawnTime;
        [SerializeField] private float animationTime;
        private int _current = 0, _max;

        private void Awake()
        {
            _max = diamonds.Length;
        }

        public void SpawnNextDiamond()
        {
            _current++;
            if (_current >= _max)
                _current = 0;

            var diamond = diamonds[_current];
            diamond.gameObject.SetActive(true);
            diamond.tr.position = spawnPosition.position;
            diamond.tr.DOMove(placedPosition.position, spawnTime);
        }

        public void AnimateAndDestroy()
        {
            var diamond = diamonds[_current];
            Sequence sequence = DOTween.Sequence();
            sequence.Join(diamond.tr.DOLocalRotate(new Vector3(0, 720, 0), animationTime, RotateMode.FastBeyond360).SetRelative(true)
                .SetEase(Ease.Linear));
            sequence.Join(diamond.tr.DOMove(destroyPosition.position, animationTime));
            sequence.OnComplete(() =>
            {
                LevelManager.Instance.IncreaseCreditAmount(100);
                InterfaceManager.Instance.FlyCurrencyFromWorld(diamond.transform.position);
                destroyParticle.Play();
                diamond.gameObject.SetActive(false);
                FlowManager.Instance.NextFlow();
            });
        }
    }
}