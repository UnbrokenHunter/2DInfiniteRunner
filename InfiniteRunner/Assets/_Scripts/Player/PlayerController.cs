using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    #region Inspector Variables

    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _maxFallSpeed;
    [SerializeField] private float _jumpDecay;
    [SerializeField] private float _jumpMultiplier;

    [Header("Other")]
    [SerializeField] private UnityEvent OnJump;
    [SerializeField] private LayerMask _layerMask;

    #endregion

    #region Internal Variables

    private Rigidbody2D _rb;
    private event Action OnFlip;
    private readonly float distance = 0.7f;
    private Vector3 direction = Vector2.right;

    #endregion

    private void Awake() => OnFlip += Flip;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(_speed, 0);
    }

    private void Update() => HandleMovement();

    private void HandleMovement()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new (direction.x, 0), distance, _layerMask);

        // Handle Flipping
        if(hit.collider != null)
            OnFlip?.Invoke();

        // Jump
        float vertical = HandleJump();

        // Horizontal
        _rb.velocity = new (_speed * direction.x, vertical);
    }

    private float HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump?.Invoke();
            print("Jump");
            return _jumpMultiplier;
        }
        
        return Mathf.Lerp(_rb.velocity.y, _maxFallSpeed, _jumpDecay);
    }

    #region Events

    private void Flip() => direction *= -1;

    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.DrawLine(transform.position, transform.position + (direction * distance));
    }

    #endregion

}
