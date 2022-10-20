using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceComponent : MonoBehaviour
{
    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
