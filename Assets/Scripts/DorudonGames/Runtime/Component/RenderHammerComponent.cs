using System.Collections.Generic;
using UnityEngine;

namespace DorudonGames.Runtime.Component
{
    public class RenderHammerComponent : MonoBehaviour
    {
        [SerializeField] private List<GameObject> renderHammers;
        [SerializeField] private Transform hammerSlot;
        
        public void SwitchHammer(int idx)
        {
            if (hammerSlot.childCount == 1)
                Destroy(hammerSlot.GetChild(0).gameObject);

            Instantiate(renderHammers[idx], hammerSlot);
        }
    }
}
