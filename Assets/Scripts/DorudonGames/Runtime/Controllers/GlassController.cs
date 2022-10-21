using System;
using DG.Tweening;
using DorudonGames.Runtime.Component;
using DorudonGames.Runtime.Misc;
using UnityEngine;

namespace DorudonGames.Runtime.Manager
{
    public class GlassController : MonoBehaviour
    {
        [SerializeField] private GlassComponent[] glasses;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private Transform placedPosition;
        [SerializeField] private float spawnTime;
        private int _current = 0, _max;

        private void Awake()
        {
            _max = glasses.Length;
        }

        [ContextMenu("DestroyCurrentGlass")]
        public void DestroyCurrentGlass()
        {
            var glass = glasses[_current];
            glass.DestroyGlass();
        }
        
        [ContextMenu("SpawnNextGlass")]
        public void SpawnNextGlass()
        {
            _current++;
            if (_current >= _max)
                _current = 0;
            
            var glass = glasses[_current];
            glass.ResetGlass();
            glass.gameObject.SetActive(true);
            glass.tr.position = spawnPosition.position;
            glass.tr.DOMove(placedPosition.position, spawnTime);
        }

        public GlassComponent GetCurrentGlass() => glasses[_current] ? glasses[_current] : null;
    }
}