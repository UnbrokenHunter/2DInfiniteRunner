using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField, Range(0, 1f)] private float _volume = 0.45f;

    [Space]

    [SerializeField] private bool _finishClipOnLoadScene;

    public void PlayClip()
    {
        if (_finishClipOnLoadScene)
        {
            PlayClipDontDestroyOnLoad();
        }
        else
        {
            PlayClipDestroyOnLoad();
        }
    }

    private void PlayClipDestroyOnLoad()
    {
        TryGetComponent(out AudioSource source);
        if(source == null)
            source = gameObject.AddComponent<AudioSource>();

        if(source.isPlaying) source.Stop();
        source.clip = _clip;
        source.volume = _volume;
        source.Play();
    }

    private void PlayClipDontDestroyOnLoad()
    {
        AudioSource audioSource = Instantiate(new GameObject()).AddComponent<AudioSource>();
        DontDestroyOnLoad(audioSource);

        audioSource.clip = _clip;
        audioSource.volume = _volume;
        audioSource.Play();
        StartCoroutine(audioSource.DestroyAudioOnFinish());
    }
}
