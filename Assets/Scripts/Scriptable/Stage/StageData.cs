using UnityEngine;

[CreateAssetMenu(menuName = "Game/Stage Data")]
public class StageData : ScriptableObject
{
    public string stageName;
    public int requiredKills = 10;
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 1.5f; // thời gian giữa 2 spawn
    public int maxSimultaneous = 5; // tối đa zombie tồn tại cùng lúc
    public bool useRandomPrefabs = true;
}
