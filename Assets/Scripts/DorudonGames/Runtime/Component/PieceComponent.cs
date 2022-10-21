using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PieceComponent : MonoBehaviour
{
    public float hp = 30f;
    public float explosionForce = 3f;
    public float explosionRadius = 3f;
    public float scaleDownFactor = 0.02f;
    public Rigidbody rb;
    private Transform _tr;
    private bool _isDead = false;
    private Vector3 _damagePosition = new Vector3(0f,0f,0f);
    private LayerMask _defaultLayer;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _tr = transform;
        _defaultLayer = LayerMask.NameToLayer("Default");
    }

    public void TakeDamage(float dmg, Vector3 damagePosition)
    {
        hp -= dmg;
        _damagePosition = damagePosition;
        _tr.localScale=_tr.localScale - Vector3.one * scaleDownFactor;
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
    }
}
