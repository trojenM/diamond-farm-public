 using System;
 using DG.Tweening;
 using DorudonGames.Runtime.EventServices;
 using DorudonGames.Runtime.Manager;
 using DorudonGames.Runtime.Misc;
 using UnityEngine;
 using MoreMountains.NiceVibrations;

 namespace DorudonGames.Runtime.Manager
 {
     public class InterfaceManager : Singleton<InterfaceManager>
     {
         [Header("Transforms")]
         [SerializeField] private RectTransform m_canvas;
         [SerializeField] private RectTransform m_chargeSlot;
         [SerializeField] private RectTransform m_currencySlot;
         [SerializeField] private RectTransform m_floatingSlot;

         [Header("Canvas Groups")] 
         [SerializeField] private CanvasGroup m_menuCanvasGroup;
         [SerializeField] private CanvasGroup m_gameCanvasGroup;
         [SerializeField] private CanvasGroup m_commonCanvasGroup;
         [SerializeField] private CanvasGroup m_settingsCanvasGroup;
     }  
 }

