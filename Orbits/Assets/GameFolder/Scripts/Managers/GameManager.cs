using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public bool IsInitialized { get; set; }
        public int CurrentScore { get; set; }

        private string _highScoreKey = "HighScore";

        public int HighScore
        {
            get
            {
                return PlayerPrefs.GetInt(_highScoreKey, 0);
            }
            set
            {
                PlayerPrefs.SetInt(_highScoreKey, value);
            }
        }

        private const string _mainMenu = "MainMenu";
        private const string _gamePlay = "GamePlay";

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Init()
        {
            IsInitialized = false;
            CurrentScore = 0;
        }

       public void GoToMainMenu()
        {
            SceneManager.LoadScene(_mainMenu);
        }
       public void GoToGamePlay()
        {
            SceneManager.LoadScene(_gamePlay);
        }
    }
}

