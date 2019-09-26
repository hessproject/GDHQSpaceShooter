using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _laserSound;
    private AudioSource _explosionSound;
    private AudioSource _powerupSound;
    // Start is called before the first frame update
    void Start()
    {
        _laserSound = transform.Find("LaserShot").gameObject.GetComponent<AudioSource>();
        _explosionSound = transform.Find("ExplosionSound").gameObject.GetComponent<AudioSource>();
        _powerupSound = transform.Find("PowerupSound").gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLaserSound()
    {
        _laserSound.Play();
    }

    public void PlayExplosionSound()
    {
        _explosionSound.Play();
    }

    public void PlayPowerupSound()
    {
        _powerupSound.Play();
    }
}
