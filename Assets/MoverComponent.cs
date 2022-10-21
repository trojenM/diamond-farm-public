using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoverComponent : MonoBehaviour
{
    [SerializeField] private Transform hammerParent;
    [SerializeField] private Transform [] movementPositions;
    private int _current=0, _max;
    private float _cd = 0f, _interval = 3f;
    
    void Start()
    {
        _cd = _interval;
        _max = movementPositions.Length;
    }   

    // Update is called once per frame
    void Update()
    {
        _cd -= Time.deltaTime;
            
        if (_cd <= 0)
        {
            _cd = _interval;
            TriggerEvents();
        }
    }

    private void TriggerEvents()
    {
        _current++;
        if (_current >= _max)
            _current = 0;

        hammerParent.DOMove(movementPositions[_current].position, 0.4f);
        hammerParent.DORotateQuaternion(movementPositions[_current].rotation, 0.4f);
    }
}
