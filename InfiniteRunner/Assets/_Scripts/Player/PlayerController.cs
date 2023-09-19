using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
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
	[SerializeField] private int distanceScore = 0;
    [SerializeField] private int minDistanceScore = 1000;
	[SerializeField] private TMP_Text _pointsText;
    [SerializeField] private TMP_Text _pointsText2;

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
    [SerializeField] public float _maxFallSpeed;
    [SerializeField] private float _jumpDecay;
    [SerializeField] private float _jumpMultiplier;
    [SerializeField] private float _jumpCostInPower;
    [SerializeField] private float _speedyMultiplier = 1.5f;

    [Header("Misc")]
    [SerializeField] private bool _isInvincable;
    [SerializeField] private bool _isFrozen = false;
    [SerializeField] private MMF_Player _deathFeedback;
	[SerializeField] private GameObject _deathMenu;
	[SerializeField] private GameObject _respawnMenu;

	[Space]

    [SerializeField] private TrailRenderer _mainTrail;
    [SerializeField] private TrailRenderer _powerTrail;

    [Header("Other")]
    [SerializeField] private UnityEvent OnJumpEvent;
    [SerializeField] private LayerMask _layerMask;

	#endregion
    
	#region Internal Variables

	private Rigidbody _rb;
    private TrailRenderer _trailRend;
    private MeshRenderer _spriteRend;
    private readonly float distance = 0.8f;
    private Vector3 direction = Vector2.right;
    private bool _jumpWasPressed = false;
    public bool IsDead { get => _isFrozen; }
    public static event Action OnDie;
    private bool _visibleMode = true;
    private bool _isPowerSliderNotNull;

    #endregion

    private void Start()
    {
        _isPowerSliderNotNull = _powerSlider != null;
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = new Vector2(_speed, 0);

        _powerSlider.maxValue = _maxPower;
        _powerSlider.minValue = 0;
        _trailRend = GetComponentInChildren<TrailRenderer>();
        _spriteRend = GetComponentInChildren<MeshRenderer>();

        if (GameState.Instance.gamemode == Gamemodes.Speedy)
        {
            _speed *= _speedyMultiplier;

		}
    }
    public void OnToggle()
    {
        _visibleMode = _visibleMode != true;
        
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
        HandlePower();
        HandleMovement();
        HandlePoints();
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

    private void HandlePoints()
    {
		distanceScore = _points * 50;
		distanceScore += (int) transform.position.y;

		var text = distanceScore + "m";

		_pointsText.text = text;
		_pointsText2.text = text;
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

    public bool Die()
    {
        if (_isInvincable) return false;

        if(_health > 1)
        {
            _health--; 
            print("Health: " + _health);

            AddPower(100f);

            Destroy(_heartContainer.transform.GetChild(_heartContainer.transform.childCount - 1).gameObject);

            return false;
        }

        print("Die");

        _deathFeedback.PlayFeedbacks();
        OnDie?.Invoke();

        if (_isPowerSliderNotNull)
            _powerSlider.gameObject.SetActive(false);

		HighScore.Instance.CheckScore(distanceScore);

        if(HighScore.Instance.CheckCoinCount() > 0 && distanceScore > minDistanceScore)
        {
            _respawnMenu.SetActive(true);
        }
        else
        {
			_deathMenu.SetActive(true);
        }

        _isFrozen = true;


        return true;
    }

    public void Respawn()
    {
		_powerSlider.gameObject.SetActive(true);

		_respawnMenu.SetActive(false);
		_isFrozen = false;
		_deathFeedback.PlayFeedbacks();
        AddPower(100);
        AddHealth();
        StartBonus(50, 5);

        // The lightning power gives invincability
		//StartCoroutine(RespawnTime());

	}

	public IEnumerator RespawnTime()
    {
        _isInvincable = true;
        yield return new WaitForSeconds(1f);
        _isInvincable = false;
    }


	public void AddPower(float amt)
    {
        print("Add Power");
        _power += amt;
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
	}

    public void StartBonus(float speed, float time)
    {
		StartCoroutine(InvincableTime(speed, time));
	}

	public IEnumerator InvincableTime(float speed, float time)
	{
		_isInvincable = true;
		_maxFallSpeed += speed;

        _mainTrail.gameObject.SetActive(false);
        _powerTrail.gameObject.SetActive(true);

		yield return new WaitForSeconds(time);

		print("Lightning Bonus Over");

		_maxFallSpeed -= speed;

        _mainTrail.gameObject.SetActive(true);
        _powerTrail.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

		_isInvincable = false;

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
