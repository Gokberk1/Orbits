using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

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

        [SerializeField] AudioSource _effectSource;

        public void PlaySound(AudioClip clip)
        {
            _effectSource.PlayOneShot(clip);
        }
    }
}

