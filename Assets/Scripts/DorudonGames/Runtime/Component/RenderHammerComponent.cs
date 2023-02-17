using System;
using System.Collections.Generic;
using UnityEngine;

namespace DorudonGames.Runtime.Component
{
    public class RenderHammerComponent : MonoBehaviour
    {
        [SerializeField] private List<GameObject> renderHammers;
        [SerializeField] private List<DiamondLevel> renderDiamonds;
        [SerializeField] private Transform hammerSlot;
        
        public void SwitchHammer(int idx)
        {
            if (hammerSlot.childCount == 1)
                Destroy(hammerSlot.GetChild(0).gameObject);

            Instantiate(renderHammers[idx], hammerSlot);
        }

        public void SwitchDiamond(int diamondLvl, int idx)
        {
            if (hammerSlot.childCount == 1) 
                Destroy(hammerSlot.GetChild(0).gameObject);

            GameObject obj = Instantiate(renderDiamonds[diamondLvl].renderDiamonds[idx], hammerSlot);
            obj.transform.position += Vector3.up * 0.25f;
        }
    }

    [Serializable]
    public class DiamondLevel
    {
        public GameObject[] renderDiamonds;
    }
}
