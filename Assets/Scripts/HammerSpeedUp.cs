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
    public class HammerSpeedUp : MonoBehaviour
    {
       
        private void Update()
        {   
            AnimationSpeedUp();
        }
        
        private void AnimationSpeedUp()
        {
            if (TouchManager.Instance .IsTouchDown())
            {
                OnTapDownEvent onTopDownEvent = new OnTapDownEvent()
                {
                    speed = 10
                };
                EventService.DispatchEvent(onTopDownEvent);
                StartCoroutine(DelayBeforeCheck());
            }
        }

        IEnumerator DelayBeforeCheck()
        {
            yield return new WaitForSeconds(.1f);
            if (!TouchManager.Instance.IsTouchDown())
            {
                OnTapDownEvent onTopDownEvent = new OnTapDownEvent()
                {
                    speed = 1
                };
                EventService.DispatchEvent(onTopDownEvent);
            }
        }
    } 
}
