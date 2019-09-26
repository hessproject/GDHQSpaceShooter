using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] private float _speed = 8f;
    private bool _isEnemyLaser = false;

    private AudioManager _audioManager;
    // Start is called before the first frame update
    void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        _audioManager.PlayLaserSound();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser)
        {
            MoveDown();
        }
        else
        {
            MoveUp();
        }
    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.up * -_speed * Time.deltaTime);

        if (transform.position.y < -6)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && _isEnemyLaser)
        {
            other.GetComponent<Player>().Damage();
            Destroy(gameObject);
        }
    }
}
