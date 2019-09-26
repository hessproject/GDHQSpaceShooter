using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float _speed = 4f;
    [SerializeField] private GameObject _laser;
    private Animator _anim;
    private Player _player;
    private UIManager _uiManager;
    private AudioManager _audioManager;

    private float _fireRate = 3.0f;
    private bool _canFire = true;
    Coroutine _fireCoroutine;

    private void Start()
    {
        _uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _fireCoroutine = StartCoroutine("Fire");
    }
    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.3f)
        {
            transform.position = new Vector3(Random.Range(-10, 10), 6.8f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            _speed = 0;
            _anim.SetTrigger("Explode");
            _audioManager.PlayExplosionSound();
            if (_fireCoroutine != null)
            {
                StopCoroutine(_fireCoroutine);
            }
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, _anim.GetCurrentAnimatorStateInfo(0).normalizedTime);


            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
        }

        if (other.tag.Equals("Weapon"))
        {
            _player.ResetFire();
            _player.AddToScore(10);
            _uiManager.UpdateScore();

            _speed = 0;
            _anim.SetTrigger("Explode");
            _audioManager.PlayExplosionSound();
            if (_fireCoroutine != null)
            {
                StopCoroutine(_fireCoroutine);
            }
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, _anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            Destroy(other.gameObject);
        }
    }

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(Random.Range(0, 1f));
        while (true)
        {
            GameObject enemyLaser = Instantiate(_laser, transform.position, Quaternion.identity);
            enemyLaser.GetComponent<Laser>().AssignEnemyLaser();
            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }
}
