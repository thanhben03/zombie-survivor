using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform spawnPoint;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        Debug.Log("Selected Character Index: " + selectedCharacter);
        GameObject playerSpawner = playerPrefabs[selectedCharacter];
        playerSpawner.SetActive(true);
        Instantiate(playerSpawner, spawnPoint.position, spawnPoint.rotation);
        
    }
}
