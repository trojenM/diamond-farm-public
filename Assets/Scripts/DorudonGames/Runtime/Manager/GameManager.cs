using System;
using DorudonGames.Runtime.EventServices;
using DorudonGames.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace DorudonGames.Runtime.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        // Private Field
        private int _creditAmount;
        private int _levelIndex = 1;
        private int _levelAmount;
        

        public int GetCurrentLevel => _levelIndex;
        public int GetLevelAmount => _levelAmount;
        public int GetCreditAmount => _creditAmount;
        public void SetCreditAmount(int x) => _creditAmount = x;
        
        protected override void Awake()
        {
            base.Awake();
            
            _levelIndex = PlayerPrefs.GetInt(CommonTypes.LEVEL_ID_DATA_KEY);
            _creditAmount = PlayerPrefs.GetInt(CommonTypes.CREDIT_DATA_KEY);
            CheckIndex();
            
            Application.targetFrameRate = CommonTypes.DEFAULT_FPS;
        }

        private void Start()
        {
            LoadLevel();
        }

        void LoadLevel()
        {
            CheckIndex();
            PlayerPrefs.SetInt(CommonTypes.LEVEL_ID_DATA_KEY, _levelIndex);
            SceneManager.LoadScene(_levelIndex);
        }
        
        void CheckIndex()
        {
            if (_levelIndex == 0)
                _levelIndex = 1;
        }
        
    }
}
