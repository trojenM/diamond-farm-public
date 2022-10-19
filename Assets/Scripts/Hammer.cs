using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.EventServices.Resources;
using DorudonGames.Runtime.Misc;
using MoreMountains.NiceVibrations;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace DorudonGames.Runtime.Manager
{
    public class Hammer : MonoBehaviour
    {
        [SerializeField] private Transform startPoint;
        [SerializeField] private float sphereRadius;
        private Animator animator;
        private Vector3 direction;

        private void Awake()
        {
            EventService.AddListener<OnTapDownEvent>(SpeedUp);
            animator = GetComponent<Animator>();
            
        }

        private void SpeedUp(OnTapDownEvent e)
        {
            animator.SetFloat("HammerAnimationSpeed",e.speed );
        }
        private void FixedUpdate()
        {
            direction = new Vector3(1, 1, 1);
            if (Physics.SphereCast(startPoint.position ,sphereRadius,direction,out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject.layer == 3 && !Destruction.Instance.ObjectsToBeDestruct.Contains(hitInfo.rigidbody))
                {
                    Destruction.Instance.ObjectsToBeDestruct.Add(hitInfo.rigidbody);
                }
            }
        }
    }
}
