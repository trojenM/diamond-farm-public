using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.EventServices.Resources;
using DorudonGames.Runtime.Misc;
using MoreMountains.NiceVibrations;
using UnityEngine;


namespace DorudonGames.Runtime.Manager
{
    public class VibrationManager : Singleton<VibrationManager>
    {
        #region Private Fields

        private bool state;

        #endregion
        
        /// <summary>
        /// This function called when before first frame.
        /// </summary>
        protected override void Awake()
        {
            state = PlayerPrefs.GetInt(CommonTypes.VIBRATION_STATE_KEY, 1) != 0;
            base.Awake();
        }

        /// <summary>
        /// This function helper for vibrate.
        /// </summary>
        public void Vibrate()
        {
            if(!state)
                return;
            
            MMVibrationManager.Vibrate();
        }

        /// <summary>
        /// This function helper for haptic.
        /// </summary>
        public void Haptic(HapticTypes hapticType)
        {
            if(!state)
                return;
            
            MMVibrationManager.Haptic(hapticType);
        }

        /// <summary>
        /// This function helper for change vibration state.
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState()
        {
            state = !state;
            PlayerPrefs.SetInt(CommonTypes.VIBRATION_STATE_KEY, state ? 1 : 0);
        }
        
        /// <summary>
        /// This function returns related state.
        /// </summary>
        /// <returns></returns>
        public bool GetState()
        {
            return state;
        }
    }
}