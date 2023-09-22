using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public static PlayMusic Instance;
    
    [SerializeField] private AudioClipSettings intro;

    [SerializeField] private AudioClipSettings music;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        AudioManager.instance.PlayAudio(intro);
        StartCoroutine(WaitForNextAudio(music));
    }
    
    private IEnumerator WaitForNextAudio(AudioClipSettings clip)
    {
        yield return new WaitForSecondsRealtime(intro.clip.length);

        AudioManager.instance.PlayAudio(clip);
    }

}
