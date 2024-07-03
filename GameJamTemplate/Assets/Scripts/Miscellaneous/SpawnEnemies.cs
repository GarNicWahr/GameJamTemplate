using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] Enemies;
    public Transform[] SpawnPoints;
    public float SpawnInterval = 60f;
    public int EnemiesPerWave = 4;
    public Transform parentObject;

    private float _timeSinceLastSpawn;

    void Start()
    {

        _timeSinceLastSpawn = 0f;

        // Spawn Enemies on beginning of scene
        SpawnEnemies();
    }

    void Update()
    {
        // Update Time since last spawn
        _timeSinceLastSpawn += Time.deltaTime;

        // Check if time elapsed to Spawn Enemies
        if (_timeSinceLastSpawn >= SpawnInterval)
        {
            SpawnEnemies();
            _timeSinceLastSpawn = 0f;
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < EnemiesPerWave; i++)
        {
            // Get Random EnemieType and SpawnPoint
            int enemyIndex = Random.Range(0, Enemies.Length);
            int spawnPointIndex = Random.Range(0, SpawnPoints.Length);
            SpawnInterval = SpawnInterval - 0.5f;

            // Initialize Enemies
            GameObject newEnemy = Instantiate(Enemies[enemyIndex], SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation, parentObject);
        }
    }
}
