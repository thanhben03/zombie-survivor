using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform zombieSpawnPosition;
    public GameObject dangerZone;
    public GameObject warningAlert;
    public AudioClip warningSound;
    private float repeatCycle = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(AlertWarning());
            InvokeRepeating("EnemySpawner", 1f, repeatCycle);
            Destroy(gameObject, 10f);
            gameObject.GetComponent<BoxCollider>().enabled = false;

            GameManager.Instance.audioSource.PlayOneShot(warningSound);
        }
    }

    void EnemySpawner()
    {
        GameObject zombieSpawn = Instantiate(zombiePrefab, zombieSpawnPosition.position, zombieSpawnPosition.rotation);
        zombieSpawn.SetActive(true);
    }

    IEnumerator AlertWarning()
    {
        warningAlert.SetActive(true);
        yield return new WaitForSeconds(3f);
        warningAlert.SetActive(false);
    }
}
