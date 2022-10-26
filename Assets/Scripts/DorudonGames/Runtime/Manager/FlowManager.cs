using DorudonGames.Runtime.Controllers;
using DorudonGames.Runtime.Misc;
using UnityEngine;

namespace DorudonGames.Runtime.Manager
{
    public class FlowManager : Singleton<FlowManager>
    {
        public float destroyedPieceCount = 0f;
        [SerializeField] private float destroyedThreshold = 10f;
        [SerializeField] private GlassController glassController;
        [SerializeField] private DiamondController diamondController;
        public bool isAnimating = false;
        
        void Start()
        {
            CurrentFlow();
        }

        public void NextFlow()
        {
            destroyedPieceCount = 0;
            isAnimating = false;
            glassController.SpawnNextGlass();
            diamondController.SpawnNextDiamond();
        }

        public void CurrentFlow()
        {
            destroyedPieceCount = 0;
            isAnimating = false;
            glassController.SpawnCurrentGlass();
            diamondController.SpawnCurrentDiamond();
        }

        public void CheckIfDestroyedEnough()
        {
            if(isAnimating)
                return;
            
            if (destroyedPieceCount >= destroyedThreshold)
            {
                isAnimating = true;
                HammerManager.Instance.StopHammers();
                glassController.DestroyCurrentGlass();
                diamondController.AnimateAndDestroy();
            }
        }
    }
}
