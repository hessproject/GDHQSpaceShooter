using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerUps;

    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemy");
        StartCoroutine("SpawnPowerup");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemy()
    {
        while (!_stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemy, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            float randomSpawn = Random.Range(.2f, 3f);
            yield return new WaitForSeconds(randomSpawn);
        }
    }

    IEnumerator SpawnPowerup()
    {
        while (!_stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerupIndex = Random.Range(0, _powerUps.Length);
            GameObject newPowerup = Instantiate(_powerUps[randomPowerupIndex], posToSpawn, Quaternion.identity);

            float randomSpawn = Random.Range(15f, 31f);
            yield return new WaitForSeconds(randomSpawn);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
