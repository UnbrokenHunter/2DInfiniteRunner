using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField, Range(0, 1f)] private float _volume = 0.45f;

    [Space]

    [SerializeField] private bool _playOnStart;
    [SerializeField] private bool _finishClipOnLoadScene;
    [SerializeField] private bool _dontDestroyOnLoad = false;

    [Space]

    [SerializeField] private AudioClip _nextClip;
    [SerializeField] private bool _loopClipTwo;
    private bool isLooping = false;

	private void Start() { if (_playOnStart) PlayClip(); }

	public void PlayClip()
    {
        PlayClip(_clip);
    }

	public void PlayClip(AudioClip clip)
    {
        if (_finishClipOnLoadScene || _dontDestroyOnLoad)
        {
            PlayClipDontDestroyOnLoad(clip);
        }
        else
        {
            PlayClipDestroyOnLoad(clip);
        }

        if (_nextClip != null && !isLooping)
        {
            StartCoroutine(WaitForNextAudio(_nextClip));
        }
    }

    private IEnumerator WaitForNextAudio(AudioClip clip)
    {
        yield return new WaitForSecondsRealtime(_clip.length);

        PlayClip(clip);
    }

    private void PlayClipDestroyOnLoad(AudioClip clip)
    {
        TryGetComponent(out AudioSource source);
        if(source == null)
            source = gameObject.AddComponent<AudioSource>();

        if(source.isPlaying) source.Stop();
        source.clip = clip;
        source.volume = _volume;
        source.Play();
    }

    private void PlayClipDontDestroyOnLoad(AudioClip clip)
    {
        AudioSource source;

		if (_dontDestroyOnLoad)
        {
		    TryGetComponent(out source);
			if (source == null)
				source = gameObject.AddComponent<AudioSource>();
		}
		else
        {
			source = Instantiate(new GameObject(name:Random.Range(0, 100).ToString()).AddComponent<AudioSource>());
        }

        DontDestroyOnLoad(source);

		source.clip = clip;
        source.volume = _volume;
        source.Play();

        if (!_dontDestroyOnLoad)
        {
            StartCoroutine(source.DestroyAudioOnFinish());
        }

        if (_loopClipTwo)
        {
            if (clip == _nextClip)
            {
                source.loop = true;
                isLooping = true;

			}
        }
    }
}
