using System;
using Cinemachine;
using DorudonGames.Runtime.Misc;
using UnityEngine;

namespace DorudonGames.Runtime.Manager
{
    public class CameraManager : Singleton<CameraManager>
    {
        #region Serializable Fields

        [Header("General")] 
        [SerializeField] private Camera m_camera;
        [SerializeField] private CinemachineFreeLook m_freeLookCamera;

        #endregion
        

        /// <summary>
        /// This function returns related camera component.
        /// </summary>
        /// <returns></returns>
        public Camera GetCamera()
        {
            return m_camera;
        }
        
        /// <summary>
        /// This function returns related virtual camera component.
        /// </summary>
        /// <returns></returns>
        public CinemachineFreeLook GetFreeLookCamera()
        {
            return m_freeLookCamera;
        }
    }  
}

