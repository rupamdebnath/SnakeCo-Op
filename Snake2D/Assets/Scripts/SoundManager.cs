using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource SoundSfx;

    public AudioSource SoundBGMusic;

    public UISoundType[] sounds;

    //Singleton script for SoundManager Instance
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    //Awake
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        KeepPlaying(UISounds.GameBGMusic);
    }

    public void KeepPlaying(UISounds _sound)
    {
        AudioClip clip = getSoundClip(_sound);
        if (clip != null)
        {
            SoundBGMusic.clip = clip;
            SoundBGMusic.Play();
        }
        else
        {
            Debug.Log("No clip found for Sound Type");
        }
    }

    private AudioClip getSoundClip(UISounds sound)
    {
        UISoundType _soundtype = Array.Find(sounds, s => s.soundType == sound);
        if (_soundtype != null)
            return _soundtype.soundClip;
        return null;
    }

    [Serializable]
    public class UISoundType
    {
        public UISounds soundType;
        public AudioClip soundClip;
    }
    public enum UISounds
    {
        ButtonClick,        
        GameBGMusic,
        FoodPickup,
        GameOver,
        LevelWin
    }
}
