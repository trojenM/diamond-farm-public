using DorudonGames.Runtime.Manager;
using UnityEngine;
using System;
using DorudonGames.Runtime.EventServices.Resources;
using System.Collections;
using System.Collections.Generic;
using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.Misc;

namespace DorudonGames.Runtime.Component

{
    public class PieceComponent : MonoBehaviour
    {
        [SerializeField] private float hp = 30f;
        [SerializeField] private float explosionForce = 3f;
        [SerializeField] private float explosionRadius = 3f;
        [SerializeField] private float scaleDownFactor = 0.02f;
        public Rigidbody rb;
        public Transform tr;
        private Collider _collider;
        private MeshRenderer _meshRenderer;
        private bool _isDead = false;
        private Vector3 _damagePosition = new Vector3(0f,0f,0f);
        private LayerMask _pieceLayer,_defaultLayer;
        private Vector3 _startPosition, _startScale;
        private Quaternion _startRotation;
        private float _fullHp;
      
        private void Awake()
        {
            //EventService.AddListener<OnHammerHitEvent>(Damage);
            rb = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _meshRenderer = GetComponent<MeshRenderer>();
            tr = transform;
            _pieceLayer = LayerMask.NameToLayer("Pieces");
            _defaultLayer = LayerMask.NameToLayer("Default");

            _fullHp = hp;
            _startPosition = tr.localPosition;
            _startRotation = tr.localRotation;
            _startScale = tr.localScale;

            hp = PlayerPrefs.GetFloat(CommonTypes.GLASS_HP_DATA_KEY, _fullHp);
            tr.localScale = Vector3.one * PlayerPrefs.GetFloat(CommonTypes.GLASS_SCALE_DATA_KEY, 1);
        }

        public float HpPercentage()
        {
            return hp / _fullHp;
        }

        //private void Damage(OnHammerHitEvent e)
        //{
        //    TakeDamage(e.Damage , e.Pos.position);
        //    print(hp);
        //}

        public void TakeDamage(float dmg , Vector3 damagePosition)
        {
            hp -= dmg;
            PlayerPrefs.SetFloat(CommonTypes.GLASS_HP_DATA_KEY, hp);
            _damagePosition = damagePosition;
            tr.localScale=tr.localScale - Vector3.one * scaleDownFactor;
            PlayerPrefs.SetFloat(CommonTypes.GLASS_SCALE_DATA_KEY, tr.localScale.x);
            float currentHealthPct = hp/_fullHp;
            HealthBar.Instance.HandleHealthChange(currentHealthPct);
            CheckDeath();
        }

        private void CheckDeath()
        {
            if (hp <= 0 && !_isDead) 
                Die();
        }

        private void Die()
        {
            _isDead = true;
            rb.isKinematic = false;
            rb.AddExplosionForce(explosionForce,_damagePosition,explosionRadius);
            gameObject.layer = _defaultLayer;
            FlowManager.Instance.destroyedPieceCount++;
            FlowManager.Instance.CheckIfDestroyedEnough();
        }

        public void ResetPiece()
        {
            hp = _fullHp;
            _isDead = false;
            tr.localPosition = _startPosition;
            tr.localRotation = _startRotation;
            tr.localScale = _startScale;
            rb.isKinematic = true;
            SetColliderState(true);
            gameObject.layer = _pieceLayer;
            
           // PlayerPrefs.SetFloat(CommonTypes.GLASS_HP_DATA_KEY, hp);
           // PlayerPrefs.SetFloat(CommonTypes.GLASS_SCALE_DATA_KEY, tr.localScale.x);

        }

        public void SetMaterial(Material material) => _meshRenderer.material = material;

        public void SetColliderState(bool b) => _collider.enabled = b;
    }
}
