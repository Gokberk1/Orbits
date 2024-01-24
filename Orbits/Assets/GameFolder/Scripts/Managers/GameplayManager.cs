using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Managers
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private GameObject _scorePrefab;

        private int _score;

        private void Awake()
        {
            GameManager.Instance.IsInitialized = true;
            _score = 0;
            _scoreText.text = _score.ToString();
            SpawnScore();
        }

        void SpawnScore()
        {
            Instantiate(_scorePrefab);
        }

        public void UpdateScore()
        {
            _score++;
            _scoreText.text = _score.ToString();
            SpawnScore();
        }

        public void GameEnded()
        {
            GameManager.Instance.CurrentScore = _score;
            StartCoroutine(GameOver());
        }

        IEnumerator GameOver()
        {
            yield return new WaitForSeconds(2f);
            GameManager.Instance.GoToMainMenu();
        }
    }
}

