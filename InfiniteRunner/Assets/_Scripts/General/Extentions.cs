using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions 
{
    public static IEnumerator DestroyAudioOnFinish(this AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);

        Debug.Log("Des");
        Object.Destroy(source.gameObject);
    }

    public static IEnumerator DestroyAudioOnFinish(this ParticleSystem particle)
    {
        yield return new WaitForSeconds(particle.emission.GetBurst(0).repeatInterval);

        Debug.Log("Des");
        Object.Destroy(particle.gameObject);
    }

}
