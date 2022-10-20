using System;
using DorudonGames.Runtime.Misc;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.EventServices.Resources.Game;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace DorudonGames.Runtime.Manager
{
    public class LevelManager : Singleton<LevelManager>
    {
        protected override void Awake()
        {
            base.Awake();
            
        }

        private void Start()
        {
            EventDispatchers.OnCreditUpdatedDispatcher(GameManager.Instance.GetCreditAmount, 0f, 0f);
        }

        private void Update()
        {
            #if UNITY_EDITOR 
            if (Input.GetKeyDown(KeyCode.M))
                IncreaseCreditAmount(500);
            #endif
        }

        public void NextLevel()
        {
            int idx = SceneManager.GetActiveScene().buildIndex + 1;
            
            if (idx <= GameManager.Instance.GetLevelAmount)
                SceneManager.LoadScene(idx);
            else
            {
                idx = 1;
                SceneManager.LoadScene(idx);
            }
            
            PlayerPrefs.SetInt("levelIndex",idx);
        }
        
        public void RestartLevel()
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene);
        }

        public void IncreaseCreditAmount(int increaseAmount)
        {
            int newCreditAmount = GameManager.Instance.GetCreditAmount + increaseAmount;
            GameManager.Instance.SetCreditAmount(newCreditAmount);
            PlayerPrefs.SetInt(CommonTypes.CREDIT_DATA_KEY, newCreditAmount);
            
            SoundManager.Instance.Play("CreditIncrease");
            EventDispatchers.OnCreditUpdatedDispatcher(newCreditAmount, 0f, -1f);
        }
    }
}
