using System;
using UnityEngine;

namespace DorudonGames.Runtime.Component
{
    public class GlassComponent : MonoBehaviour
    {
        public Transform tr;
        //[SerializeField] private Material glassMaterial;
        private PieceComponent[] _pieces;

        private void Awake()
        {
            _pieces = GetComponentsInChildren<PieceComponent>();
            //SetGlassSettings();
            gameObject.SetActive(false);
        }
        
        /*private void SetGlassSettings()
        {
            foreach (var piece in _pieces)
            {
                piece.SetMaterial(glassMaterial);
            }
        }*/

        public void ResetGlass()
        {
            foreach (var piece in _pieces)
            {
                piece.ResetPiece();
            }
            gameObject.SetActive(false);
        }

        public void DestroyGlass()
        {
            foreach (var piece in _pieces)
            {
                piece.rb.isKinematic = false;
                piece.rb.AddExplosionForce(10f,tr.position,5f);
            }
            DorudonUtils.RunDelayedMethod(() =>
            {
                foreach (var piece in _pieces)
                {
                    piece.SetColliderState(false);
                }
            },2f);
        }
    }
}
