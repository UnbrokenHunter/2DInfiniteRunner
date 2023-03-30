using MoreMountains.Feedbacks;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
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
    [SerializeField] private float _jumpCostInPower;

    [Header("Misc")]
    [SerializeField] private bool _isInvincable;
    [SerializeField] private bool _isFrozen = false;
    [SerializeField] private MMF_Player _deathFeedback;
    [SerializeField] private GameObject _deathMenu;

    [Header("Other")]
    [SerializeField] private UnityEvent OnJumpEvent;
    [SerializeField] private LayerMask _layerMask;

    #endregion

    #region Internal Variables

    private Rigidbody _rb;
    private readonly float distance = 0.8f;
    private Vector3 direction = Vector2.right;
    private bool _jumpWasPressed = false;
    public bool IsDead { get => _isFrozen; }
    public static event Action OnDie;

    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = new Vector2(_speed, 0);

        _powerSlider.maxValue = _maxPower;
        _powerSlider.minValue = 0;

    }

    public void OnFire(InputValue value)
    {
        if (_isFrozen) return;
        
        _jumpWasPressed = value.isPressed;

    }

    private void FixedUpdate()
    {
        if (_isFrozen) return;

        HandlePower();
        HandleMovement();
    }

    private void HandlePower()
    {
        if (_power <= 0.1f)
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
		bool hit = Physics.Raycast(transform.position, new Vector3(direction.x, 0, 0), distance, _layerMask);

        // Handle Flipping
        if (hit)
            direction *= -1;

        // Jump
        float vertical = HandleJump();

        // Horizontal
        _rb.velocity = new ((_speed * direction.x * _speedBonus), vertical);
    }

    private float HandleJump()
    {
        if (_jumpWasPressed)
        {
            _jumpWasPressed = false;

            OnJumpEvent?.Invoke();

            _power -= _jumpCostInPower;

			return _jumpMultiplier;
        }
        
        return Mathf.Lerp(_rb.velocity.y, _maxFallSpeed, _jumpDecay);
    }

    #region Effects

    public void Die()
    {
        if (_isInvincable) return;
        print("Die");
        
        OnDie?.Invoke();
        _deathFeedback?.PlayFeedbacks();

        if (_powerSlider != null)
            Destroy(_powerSlider.gameObject);

		HighScore.Instance.CheckScore(_points);

		_deathMenu.SetActive(true);

        _isFrozen = true;

    }

    public void AddPower()
    {
        print("Add Power");
        _power += _addPowerAmount;
        if (_power > _maxPower)
            _power = _maxPower;
    }
    
    public void AddPoint()
    {
        _points++;
		_pointsText.text = "Score: " + _points.ToString();
    }

    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (direction * distance));
    }

    #endregion

}
