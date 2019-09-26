using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] private float _fallSpeed = 3f;

    //0 = Triple shot
    //1 = Speed
    //2 = Shield
    [SerializeField] private int _powerUpID;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _fallSpeed * Time.deltaTime);
        if(transform.position.y < -5.5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null) {
                switch (_powerUpID)
                {
                    case 0:
                        player.EnableTripleShot();
                        break;
                    case 1:
                        player.EnableSpeedBoost();
                        break;
                    case 2:
                        player.EnableShield();
                        break;
                    default:
                        Debug.Log("Reached default powerup somehow");
                        break;
                }
            }
            Destroy(gameObject);
        }
    }
}
