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

    [Header("Health")]
    [SerializeField] private int _health = 1;
    [SerializeField] private GameObject _heartUI;
    [SerializeField] private GameObject _heartContainer;

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
    private TrailRenderer _trailRend;
    private SpriteRenderer _spriteRend;
    private readonly float distance = 0.8f;
    private Vector3 direction = Vector2.right;
    private bool _jumpWasPressed = false;
    public bool IsDead { get => _isFrozen; }
    public static event Action OnDie;
    private bool _visibleMode = true;
    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = new Vector2(_speed, 0);

        _powerSlider.maxValue = _maxPower;
        _powerSlider.minValue = 0;
        _trailRend = GetComponentInChildren<TrailRenderer>();
        _spriteRend = GetComponentInChildren<SpriteRenderer>();
    }
    public void ToggleMode(){
        if (UnityEngine.Input.GetKeyUp(KeyCode.T)){
            if (_visibleMode == true){
                _visibleMode = false;
            }
            else{
                _visibleMode = true;
            }
        }
        if (_visibleMode){
            _trailRend.enabled = false;
            _spriteRend.enabled = true;
        }
        else{
            // _trailRend.enabled = true;
            _spriteRend.enabled = false;
        }
    }
    public void OnFire(InputValue value)
    {
        if (_isFrozen) return;
        
        _jumpWasPressed = value.isPressed;

    }

    private void FixedUpdate()
    {
        if (_isFrozen) return;
        ToggleMode();
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
            _trailRend.enabled = true;
            OnJumpEvent?.Invoke();

            _power -= _jumpCostInPower;

			return _jumpMultiplier;
        }
        if(Time.time % 3 == 0){
            _trailRend.enabled = false;
        }
        return Mathf.Lerp(_rb.velocity.y, _maxFallSpeed, _jumpDecay);
    }

    #region Effects

    public bool Die()
    {
        if (_isInvincable) return true;

        if(_health > 1)
        {
            _health--; 
            print("Health: " + _health);

            Destroy(_heartContainer.transform.GetChild(_heartContainer.transform.childCount - 1).gameObject);

            return false;
        }

        print("Die");

        _deathFeedback.PlayFeedbacks();
        OnDie?.Invoke();

        if (_powerSlider != null)
            Destroy(_powerSlider.gameObject);

		HighScore.Instance.CheckScore(_points);

		_deathMenu.SetActive(true);

        _isFrozen = true;

        return true;

    }

    public void AddPower()
    {
        print("Add Power");
        _power += _addPowerAmount;
        if (_power > _maxPower)
            _power = _maxPower;
    }
    
    public void AddHealth()
    {
        print("Add Health");

        Instantiate(_heartUI, _heartContainer.transform);

        _health++;
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
