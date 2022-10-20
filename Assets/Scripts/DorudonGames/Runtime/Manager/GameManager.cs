using System;
using DorudonGames.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DorudonGames.Runtime.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        // Serialize Field
        [SerializeField] private int creditAmount;
        
        // Private Field
        private int _levelIndex = 1;
        private int _levelAmount;
        

        public int GetCurrentLevel => _levelIndex;
        public int GetLevelAmount => _levelAmount;
        public int GetCreditAmount => creditAmount;
        public void SetCreditAmount(int x) => creditAmount = x;
        
        protected override void Awake()
        {
            base.Awake();
            
            _levelIndex = PlayerPrefs.GetInt(CommonTypes.LEVEL_ID_DATA_KEY);
            creditAmount = PlayerPrefs.GetInt(CommonTypes.CREDIT_DATA_KEY);
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
