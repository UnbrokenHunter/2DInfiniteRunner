using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    #region Inspector Variables

    [Header("Points")]
    [SerializeField] private int _points;
    [SerializeField] private TMP_Text _pointsText;

    [Header("Power Variables")]
    [SerializeField] private float _powerLossMultiplier;
    [SerializeField] private float _addPowerAmount = 10;

    [Header("Power Settings")]
    [SerializeField] private float _power;
    [SerializeField] private float _maxPower;
    [SerializeField] private float _speedBonus;
    [SerializeField] private Slider _powerSlider;

    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _maxFallSpeed;
    [SerializeField] private float _jumpDecay;
    [SerializeField] private float _jumpMultiplier;

    [Header("Misc")]
    [SerializeField] private bool _isInvincable;

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

        _powerSlider.maxValue = _maxPower;
        _powerSlider.minValue = 0;

    }

    private void Update()
    {
        HandlePower();
        HandleMovement();
    }

    private void HandlePower()
    {
        if (_power == 0)
        {
            Die();
            return;
        }

        _power -= Time.deltaTime * _powerLossMultiplier;

        _powerSlider.value = _power;


        _speedBonus = (_maxPower / _power);
    }

    private void HandleMovement()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new (direction.x, 0), distance, _layerMask);

        // Handle Flipping
        if(hit.collider != null)
            OnFlip?.Invoke();

        // Jump
        float vertical = HandleJump();

        // Horizontal
        _rb.velocity = new (_speed * direction.x * _speedBonus, vertical);
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

    #region Effects

    public void Die()
    {
        if (_isInvincable) return;
        print("Die");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddPower()
    {
        _power += _addPowerAmount;
    }
    
    public void AddPoint()
    {
        _points++;
        _pointsText.text = "Points: " + _points.ToString();
    }

    #endregion

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
