using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioSource _backgroundAudio;

    [SerializeField] AudioSource _vfxAudio;

    [SerializeField] List<MusicByState> _musicByStateList;
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
    }
  

    public void RunBackgroundMusic(GameState gameState)
    {
        switch (gameState) {
            case GameState.Pause:
                _backgroundAudio.Pause();
                _isPause = true;
                break;
            case GameState.Loading:
            case GameState.Menu:
            case GameState.Play:
                if(_isPause) _backgroundAudio.UnPause();
                else
                {
                    _backgroundAudio.clip = GetAudioClipByState(gameState);
                    _backgroundAudio.Play();
                }
                break;
        }
    }

    AudioClip GetAudioClipByState(GameState gameState) {
        foreach(var musicByState in _musicByStateList)
        {
            if(musicByState.State == gameState)
            {
                return musicByState.AudioClip;
            }
        }
        return null;
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

public enum GameState
{
    Pause,
    Loading,
    Play,
    Menu
}

[System.Serializable]
public class MusicByState
{
    public GameState State;
    public AudioClip AudioClip;
}


