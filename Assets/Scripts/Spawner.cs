using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }
    public Transform[] spawnPoints;

    private StageData currentStage;
    private Coroutine spawnRoutine;
    private int aliveCount;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartSpawning(StageData stage)
    {
        StopSpawning();
        currentStage = stage;
        aliveCount = 0;
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null) StopCoroutine(spawnRoutine);
        spawnRoutine = null;
        currentStage = null;
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (currentStage == null) yield break;

            if (aliveCount < currentStage.maxSimultaneous)
            {
                SpawnOne();
            }
            yield return new WaitForSeconds(currentStage.spawnInterval);
        }
    }

    private void SpawnOne()
    {
        var prefab = currentStage.useRandomPrefabs
            ? currentStage.enemyPrefabs[Random.Range(0, currentStage.enemyPrefabs.Length)]
            : currentStage.enemyPrefabs[0];

        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject go = SimplePool.Spawn(prefab, spawnPoint.position, spawnPoint.rotation);
        Zombie2 z = go.GetComponent<Zombie2>();
        Health zombieHealth = z.GetComponent<Health>();
        if (z != null)
        {
            zombieHealth.OnDeath += HandleZombieDeath;
            aliveCount++;
        }
    }

    private void HandleZombieDeath()
    {
        aliveCount = Mathf.Max(0, aliveCount - 1);
    }
}
