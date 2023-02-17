using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DorudonGames.Runtime.EventServices.Resources;

namespace DorudonGames.Runtime.EventServices
{
    public class OnHammerHitEvent : BaseEvent
    {
        public float Damage;
        public  Transform Pos;
    }
}