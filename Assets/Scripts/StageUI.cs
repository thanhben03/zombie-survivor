using UnityEngine;
using TMPro;

public class StageUI : MonoBehaviour
{
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI zombieRemainText;

    public GameObject stagePrefab;
    public GameObject stageUI;

    public GameObject missonCompletedPref;

    private void Start()
    {
        StageManager.Instance.OnProgressChanged += UpdateProgress;
        StageManager.Instance.OnStageCompleted += UpdateCompletedStage;
        StageManager.Instance.OnStageStarted += UpdateStartStage;
        StageManager.Instance.OnStageFinished += StageFinished;
    }

    private void OnDisable()
    {
        if (StageManager.Instance != null)
            StageManager.Instance.OnProgressChanged -= UpdateProgress;
    }

    private void UpdateProgress(int kills, int required)
    {
        zombieRemainText.text = "Zombie: " + kills + "/" + required;
        Debug.Log("Kills: " + kills + " Required: " + required);
    }

    private void UpdateStartStage(int stage)
    {
        //stageText.text = "Stage: " + stage++;
        GameObject stageP = Instantiate(stagePrefab);
        TextMeshProUGUI stageTxt = stageP.GetComponent<TextMeshProUGUI>();
        stage += 1;
        stageTxt.text = "Stage " + stage;
        stageP.transform.SetParent(stageUI.transform);
        Destroy(stageP, 2f);
    }

    private void UpdateCompletedStage(int stage)
    {
        
    }

    private void StageFinished()
    {
        ObjectiveManager.Instance.CompleteObjective(2);
        GameObject missonCompletedObj = Instantiate(missonCompletedPref);
        missonCompletedObj.transform.SetParent(stageUI.transform);
        Destroy(missonCompletedObj, 2f);

    }
}
