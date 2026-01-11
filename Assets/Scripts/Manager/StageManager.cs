using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public StageData[] stages;
    public int currentStageIndex = 0;

    public int killsThisStage { get; private set; }

    public event Action<int, int> OnProgressChanged; // (kills, required)
    public event Action<int> OnStageStarted; // stageIndex
    public event Action<int> OnStageCompleted; // stageIndex
    public event Action OnStageFinished; // stageIndex



    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartStage(currentStageIndex);
    }

    public void StartStage(int index)
    {
        if (index < 0 || index >= stages.Length)
        {
            Debug.LogWarning("Stage index out of range");
            return;
        }
        //GameObject stageP = Instantiate(stagePrefab);
        //stageP.transform.SetParent(stageUI.transform);
        //Destroy(stageP, 2f);
        currentStageIndex = index;
        killsThisStage = 0;
        OnStageStarted?.Invoke(index);
        OnProgressChanged?.Invoke(killsThisStage, stages[index].requiredKills);

        // Tell spawner to start using this stage
        Spawner.Instance.StartSpawning(stages[index]);
    }

    public void RegisterKill()
    {
        killsThisStage++;
        var required = stages[currentStageIndex].requiredKills;
        OnProgressChanged?.Invoke(killsThisStage, required);

        if (killsThisStage >= required)
        {
            CompleteStage();
        }
    }

    private void CompleteStage()
    {
        Spawner.Instance.StopSpawning();
        OnStageCompleted?.Invoke(currentStageIndex);
        Debug.Log($"Stage {currentStageIndex} completed!");

        // chuyển sang stage tiếp (hoặc show menu)
        int next = currentStageIndex + 1;
        if (next < stages.Length) StartStage(next);
        else
        {
            OnStageFinished?.Invoke();
            Debug.Log("Stage finished");
        }
    }
}
