﻿using System;
using UnityEngine;

namespace DorudonGames.Runtime.Component
{
    public class DiamondComponent : MonoBehaviour
    {
        public Transform tr;

        private void Awake()
        {
            tr = transform;
        }
    }
}