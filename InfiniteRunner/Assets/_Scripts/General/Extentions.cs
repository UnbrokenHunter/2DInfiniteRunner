using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions 
{
	public static IEnumerator DestroyAudioOnFinish(this AudioSource source)
	{
		if (source.clip != null)
		{
			float timeRemaining = source.clip.length - source.time;
			Debug.Log("Destroy Object in " + timeRemaining + " seconds Object: " + source.gameObject.name);

			yield return new WaitForSeconds(timeRemaining);

			Debug.Log("Object Destroyed");
			Object.Destroy(source.gameObject);
		}
		else
		{
			Debug.LogWarning("AudioSource does not have a clip!");
		}
	}

	public static IEnumerator DestroyParticleOnFinish(this ParticleSystem particle)
    {
        yield return new WaitForSeconds(particle.emission.GetBurst(0).repeatInterval);

        Debug.Log("Des");
        Object.Destroy(particle.gameObject);
    }

}
