using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioSource _backgroundAudio;
    [SerializeField] AudioSource _vfxAudio;

    [SerializeField] List<MusicByScene> _musicBySceneList;
    [SerializeField] bool _isPause = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RunBackgroundMusic(scene.name);
    }

    public void RunBackgroundMusic(string sceneName)
    {
        if (_isPause)
        {
            _backgroundAudio.UnPause();
            _isPause = false;
            return;
        }

        AudioClip sceneClip = GetAudioClipByScene(sceneName);
        if (sceneClip != null)
        {
            // Kiểm tra nếu nhạc hiện tại đã giống với nhạc của scene mới
            if (_backgroundAudio.clip == sceneClip && _backgroundAudio.isPlaying)
            {
                return; // Không đổi nhạc nếu nhạc đã đúng
            }

            _backgroundAudio.clip = sceneClip;
            _backgroundAudio.Play();
        }
    }


    AudioClip GetAudioClipByScene(string sceneName)
    {
        foreach (var musicByScene in _musicBySceneList)
        {
            if (musicByScene.SceneName == sceneName)
            {
                return musicByScene.AudioClip;
            }
        }
        return null;
    }

    public void PauseBackgroundMusic()
    {
        _backgroundAudio.Pause();
        _isPause = true;
    }

    public AudioSource GetMusicAudioSource()
    {
        return _backgroundAudio;
    }

    public AudioSource GetVFXAudioSource()
    {
        return _vfxAudio;
    }

}


[System.Serializable]
public class MusicByScene
{
    public string SceneName;
    public AudioClip AudioClip;
}
