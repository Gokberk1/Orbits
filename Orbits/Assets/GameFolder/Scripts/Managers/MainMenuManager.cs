using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private TMP_Text _newBestText;

        [SerializeField] private AudioClip _clickClip;

        [SerializeField] private float _animationTime;
        [SerializeField] private AnimationCurve _speedCurve;
        
        private void Awake()
        {
            if (GameManager.Instance.IsInitialized)
            {
                StartCoroutine(ShowScore());
            }
            else
            {
                _scoreText.gameObject.SetActive(false);
                _newBestText.gameObject.SetActive(false);
                _highScoreText.text = GameManager.Instance.HighScore.ToString();
            }
        }

        IEnumerator ShowScore()
        {
            int tempScore = 0;
            _scoreText.text = tempScore.ToString();

            int currentScore = GameManager.Instance.CurrentScore;
            int highScore = GameManager.Instance.HighScore;

            if(currentScore > highScore)
            {
                _newBestText.gameObject.SetActive(true);
                GameManager.Instance.HighScore = currentScore;
            }
            else
            {
                _newBestText.gameObject.SetActive(false);
            }

            _highScoreText.text = GameManager.Instance.HighScore.ToString();

            float speed = 1 / _animationTime;
            float timeElapsed = 0f;

            while(timeElapsed < 1f)
            {
                timeElapsed += speed * Time.deltaTime;
                tempScore = (int)(_speedCurve.Evaluate(timeElapsed) * currentScore);
                _scoreText.text = tempScore.ToString();
                yield return null;
            }

            tempScore = currentScore;
            _scoreText.text = tempScore.ToString();
        }

        public void ClickedPlay()
        {
            SoundManager.Instance.PlaySound(_clickClip);
            GameManager.Instance.GoToGamePlay();
        }
    }
}

