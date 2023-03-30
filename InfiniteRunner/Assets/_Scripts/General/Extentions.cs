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
}
