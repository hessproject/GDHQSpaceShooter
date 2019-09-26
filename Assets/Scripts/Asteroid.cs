using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _spinSpeed = 15f;
    [SerializeField] private GameObject _explosion;
    private SpawnManager _spawnManager;
    private AudioManager _audioManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _spinSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            GameObject explosion = Instantiate(_explosion, transform.position, transform.rotation);
            _audioManager.PlayExplosionSound();
            _spawnManager.StartSpawning();
            Destroy(gameObject);
            Destroy(other.gameObject);
            Destroy(explosion, 1.65f);
        }
    }
}
