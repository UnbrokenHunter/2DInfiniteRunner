using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(this);
		}
	}

	public void PlayAudio(AudioClipSettings settings)
	{
		if (!audioSources.ContainsKey(settings.name)) 
		{
			var newSource = gameObject.AddComponent<AudioSource>();
			audioSources.Add(settings.name, newSource);
		}

		var source = audioSources[settings.name];

		if (source.isPlaying) source.Stop();
		source.clip = settings.clip;
		source.volume = settings.volume;
		source.loop = settings.loop;
		source.Play();

		print("Audio: " +  source.name);

	}

}

[Serializable]
public class AudioClipSettings
{
	[Header("Audio Clip Settings")]
	public string name;
	public AudioClip clip;
	[Range(0, 1f)] public float volume = 0.45f;
	public bool loop = false;
}
