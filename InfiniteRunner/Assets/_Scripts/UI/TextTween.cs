using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTween : MonoBehaviour
{

    [SerializeField] private float _waitTime;

    [SerializeField] private float _distance;
    [SerializeField] private float _duration;

    private void OnEnable()
    {
        Vector3 _currentScale = transform.localScale;
        transform.localScale = Vector3.zero;

        float _currentRot = transform.rotation.w;
        transform.localRotation = new Quaternion(0, 0, 0.8838704f, 0.4677318f);

        print("Text Tween");
        LeanTween.moveLocalY(gameObject, _distance, _duration);
        LeanTween.rotateZ(gameObject, _currentRot, _duration);

        LeanTween.scale(gameObject, _currentScale, _duration);

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(_waitTime);
        Destroy(transform.parent.gameObject);
    }

}
