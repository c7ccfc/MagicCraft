using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;

    public float spawnInterval = 10f;

    public Vector2 spawnAreaSize = new Vector2(10f,10f);

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while(true){
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(-spawnAreaSize.x/2,spawnAreaSize.x/2),
            Random.Range(-spawnAreaSize.y/2,spawnAreaSize.y/2)
        );
        Instantiate(Enemy,spawnPosition, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
