using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip _moveClip, _loseClip, _pointClip;
    [SerializeField] private GameplayManager _gm;
    [SerializeField] private GameObject _explosionPrefab, _scoreParticle;
    private bool _canClick;

    [SerializeField] private List<float> _rotateRadiuses;
    [SerializeField] private float _startRotateRadius;
    [SerializeField] private float _moveTime;
    [SerializeField] private float _rotateSpeed;
    private float _currentRotateRadius;
    private int _level;

    [SerializeField] private Transform _rotateTransform;
    

    private void Start()
    {
        _canClick = true;
        _level = 0;
        _currentRotateRadius = _startRotateRadius;
    }

    private void FixedUpdate()
    {
        transform.localPosition = Vector3.up * _currentRotateRadius;
        float rotateValue = _rotateSpeed * Time.fixedDeltaTime;
        rotateValue *= (_startRotateRadius / _currentRotateRadius);
        _rotateTransform.Rotate(0, 0, rotateValue);
    }

    private void Update()
    {
        if(_canClick && Input.GetMouseButtonDown(0))
        {
            SoundManager.Instance.PlaySound(_moveClip);
            StartCoroutine(ChangeRadius());
        }
    }

    IEnumerator ChangeRadius()
    {
        _canClick = false;
        float moveStartRadius = _rotateRadiuses[_level];
        float moveEndRadius = _rotateRadiuses[(_level + 1) % _rotateRadiuses.Count];
        float moveOffset = moveEndRadius - moveStartRadius;
        float speed = 1 / _moveTime;
        float timeElapsed = 0f;

        while(timeElapsed < 1f)
        {
            timeElapsed += speed * Time.fixedDeltaTime;
            _currentRotateRadius = moveStartRadius + timeElapsed * moveOffset;
            yield return new WaitForFixedUpdate();
        }

        _canClick = true;

        _level = (_level + 1) % _rotateRadiuses.Count;
        _currentRotateRadius = _rotateRadiuses[_level];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySound(_loseClip);
            _gm.GameEnded();
            Destroy(gameObject);
            return;
        }

        if (collision.CompareTag("Score"))
        {
            Destroy(Instantiate(_scoreParticle, transform.position, Quaternion.identity), 2f);
            SoundManager.Instance.PlaySound(_pointClip);
            _gm.UpdateScore();
            collision.gameObject.GetComponent<Score>().ScoreAdded();
            return;
        }
    }
}


