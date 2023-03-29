using System.Collections;
using UnityEngine;

public class TweenUI : MonoBehaviour
{

    [SerializeField] private float _initialWait = 0;

    [Space]

    [SerializeField] private float _tweenTime;
    [SerializeField] private float _yStartPosition = -500;
    [SerializeField] private float _yEndPosition = 0;

    [Space]

    [SerializeField] private bool _destroyWithPlayer;
    [SerializeField] private float _destroyAfter;

    private RectTransform _rectTransform;

    private void OnEnable()
    {
        if(_destroyWithPlayer)
            PlayerController.OnDie += Die;

        transform.localScale = Vector3.zero;

        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.position = new Vector3(_rectTransform.position.x, _yStartPosition, _rectTransform.position.z);


        StartCoroutine(nameof(InitialWait));
    }

    private void OnDestroy()
    {
        if (_destroyWithPlayer)
            PlayerController.OnDie -= Die;
    }

    private void Die() => Destroy(gameObject);

    private IEnumerator InitialWait()
    {
        yield return new WaitForSeconds(_initialWait);

        LeanTween.moveY(_rectTransform, _yEndPosition, _tweenTime);
        LeanTween.scale(_rectTransform, Vector3.one, _tweenTime);

        if (_destroyAfter != 0)
            StartCoroutine(nameof(DestroyAfter));

    }

    private IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds (_destroyAfter);

        LeanTween.moveY(_rectTransform, _yStartPosition, _tweenTime);
        LeanTween.scale(_rectTransform, Vector3.zero, _tweenTime);

        Destroy(gameObject);
    }

}
