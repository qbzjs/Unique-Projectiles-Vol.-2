using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Juhyung
{
    public class SoundManager : MonoBehaviour
    {
        #region singleton
        private static SoundManager _instance;

        public static SoundManager Instance
        {
            get
            {
                // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
                if (!_instance)
                {
                    _instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;

                    if (_instance == null)
                        Debug.Log("no Singleton obj");
                }
                return _instance;
            }
        }
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
            else if (_instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
        #endregion singleton

        #region SoundType
        public enum DEFINE
        {
            BGM,
            SFX,

            DEFINE_END
        }
        #endregion SoundType

        private AudioSource[] _audioSources = new AudioSource[(int)DEFINE.DEFINE_END];
        private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

        private void Start()
        {
            Init();
        }
        public void Init()
        {
            GameObject root = GameObject.Find("@Sound");
            if(root == null)
            {
                root = new GameObject("@Sound");
                DontDestroyOnLoad(root);

                string[] sound_Names = System.Enum.GetNames(typeof(DEFINE));
                for(int i = 0; i < sound_Names.Length - 1; ++i)
                {
                    GameObject go = new GameObject(sound_Names[i]);
                    _audioSources[i] = go.AddComponent<AudioSource>();
                    go.transform.parent = root.transform;
                }

                _audioSources[(int)DEFINE.BGM].loop = true;
            }
        }

        public void Clear()
        {
            foreach(AudioSource audioSource in _audioSources)
            {
                audioSource.clip = null;
                audioSource.Stop();
            }

            _audioClips.Clear();
        }

        public void Play(AudioClip audioClip, DEFINE type = DEFINE.SFX, float pitch = 1.0f)
        {
            if (audioClip == null) return;

            switch (type)
            {
                case DEFINE.BGM:
                    { 
                        AudioSource audioSource = _audioSources[(int)DEFINE.BGM];
                        if (audioSource.isPlaying)
                        {
                            audioSource.Stop();
                        }

                        audioSource.pitch = pitch;
                        audioSource.clip = audioClip;
                        audioSource.Play();
                    }

                    break;

                case DEFINE.SFX:
                    {
                        AudioSource audioSource = _audioSources[(int)DEFINE.SFX];
                        audioSource.pitch = pitch;
                        audioSource.PlayOneShot(audioClip);
                    }

                    break;
            }
        }

        public void Play(string path, DEFINE type = DEFINE.SFX, float pitch = 1.0f)
        {
            AudioClip audioClip = GetOrAddAudioClip(path, type);
            Play(audioClip, type, pitch);
        }

        AudioClip GetOrAddAudioClip(string path, DEFINE type = DEFINE.SFX)
        {
            if (path.Contains("Sounds/") == false)
            {
                path = $"Sounds/{path}";
            }

            AudioClip audioClip;

            if (type == DEFINE.BGM)
            {
                audioClip = Resources.Load<AudioClip>(path);
            }
            else
            {
                if (_audioClips.TryGetValue(path, out audioClip) == false)
                {
                    audioClip = Resources.Load<AudioClip>(path);
                    _audioClips.Add(path, audioClip);
                }
            }

            if (audioClip == null)
                Debug.Log($"AudioClip Missing ! {path}");

            return audioClip;
        }
    }
}
