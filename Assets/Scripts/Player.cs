using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player variables
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _fireRate = 0.4f;
    [SerializeField] private int _lives = 3;
    [SerializeField] private float _speedMultiplier = 2;

    //Prefabs
    [SerializeField] private GameObject _laser;
    [SerializeField] private GameObject _tripleShot;
    private GameManager _gameManager;
    private AudioManager _audioManager;
    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private GameObject _playerShield;
    private GameObject _leftEngineDamage;
    private GameObject _rightEngineDamage;

    //Current state
    private bool _canFire = true;
    private Coroutine _fireCoroutine;
    [SerializeField] public bool _tripleShotEnabled = false;
    [SerializeField] public bool _speedBoostEnabled = false;
    [SerializeField] public bool _shieldEnabled = false;
    private int _score;

    //Unity Methods
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.FindGameObjectWithTag("Respawn").GetComponent<SpawnManager>();
        _playerShield = transform.Find("Shield").gameObject;
        _leftEngineDamage = transform.Find("LeftEngineDamage").gameObject;
        _rightEngineDamage = transform.Find("RightEngineDamage").gameObject;
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        _uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        float speedBoostSpeed = _speed * _speedMultiplier;
    }

    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && _canFire)
        {
            Fire();
        }
    }

    //Custom Public Methods
    public void ResetFire()
    {
        if(_fireCoroutine != null)
        {
            StopCoroutine(_fireCoroutine);
        }
        _canFire = true;
    }

    public void Damage()
    {
        if (!_shieldEnabled)
        {
            _lives--;
            _uiManager.UpdateLives(_lives);

            if (_lives == 2)
            {
                _rightEngineDamage.SetActive(true);
            }
            else if(_lives == 1)
            {
                _rightEngineDamage.SetActive(true);
                _leftEngineDamage.SetActive(true);
            }
            else if (_lives < 1)
            {
                _uiManager.GameOver();
                _gameManager.GameOver();
                _spawnManager.OnPlayerDeath();
                _audioManager.PlayExplosionSound();
                StopCoroutine(_fireCoroutine);
                Destroy(gameObject);
            }
        } else
        {
            _shieldEnabled = false;
            _playerShield.SetActive(false);
        }

    }

    public void EnableTripleShot()
    {
        _tripleShotEnabled = true;
        StartCoroutine(TripleShotCoolDown());
    }

    public void EnableSpeedBoost()
    {
        _speedBoostEnabled = true;
        StartCoroutine(SpeedBoostCoolDown());
    }

    public void EnableShield()
    {
        _shieldEnabled = true;
        _playerShield.SetActive(true);
    }

    public void AddToScore(int scoreToAdd)
    {
        _score += scoreToAdd;
    }

    public int GetScore()
    {
        return _score;
    }

    //Custom Private Methods
    private void Fire()
    {
        if (_tripleShotEnabled)
        {
            Instantiate(_tripleShot, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        } else
        {
            Instantiate(_laser, new Vector3(transform.position.x, transform.position.y + 1.05f, 0), Quaternion.identity);
        }
        _audioManager.PlayLaserSound();
        _canFire = false;
        _fireCoroutine = StartCoroutine(FireRate());
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float currentSpeed = _speedBoostEnabled ? _speed * _speedMultiplier : _speed;
        transform.Translate(new Vector3(horizontalInput, verticalInput) * currentSpeed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11f)
        {
            transform.position = new Vector3(-11f, transform.position.y, 0);
        }
        else if (transform.position.x < -11f)
        {
            transform.position = new Vector3(11f, transform.position.y, 0);
        }
    }

    //Coroutines
    private IEnumerator FireRate()
    {
        yield return new WaitForSeconds(_fireRate);
        _canFire = true;
    }

    private IEnumerator TripleShotCoolDown()
    {
        yield return new WaitForSeconds(5f);
        _tripleShotEnabled = false;
    }

    private IEnumerator SpeedBoostCoolDown()
    {
        yield return new WaitForSeconds(5f);
        _speedBoostEnabled = false;
    }
}
