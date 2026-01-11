using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    [System.Serializable]
    public class ObjectiveData
    {
        public int objID;
        public string description;
        public TextMeshProUGUI textElement;
        [HideInInspector] public bool isCompleted = false;
    }

    public List<ObjectiveData> objectives = new List<ObjectiveData>();
    public Color completedColor = Color.green;
    public TextMeshProUGUI resultText;
    public int countObj = 0;


    public static ObjectiveManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Khởi tạo hiển thị ban đầu
        for (int i = 0; i < objectives.Count; i++)
        {
            UpdateUI(i);
        }
    }

    // Chỉ cần gọi hàm này và truyền ID của nhiệm vụ (0, 1, 2...)
    public void CompleteObjective(int objId)
    {
        int index = objectives.FindIndex(o => o.objID == objId);

        if (index == -1)
        {
            Debug.LogWarning($"Objective ID {objId} not found!");
            return;
        }

        objectives[index].isCompleted = true;
        UpdateUI(index);
        countObj++;
        if (countObj >= objectives.Count)
        {
            GameManager.Instance.WinGame();
            return;
        }
    }

    public ObjectiveData FindObjective(int objId)
    {
        int index = objectives.FindIndex(o => o.objID == objId);

        if (index == -1)
        {
            Debug.LogWarning($"Objective ID {objId} not found!");
            return null;
        }

        return objectives[index];
    }


    private void UpdateUI(int index)
    {
        var obj = objectives[index];
        if (obj.isCompleted)
        {
            obj.textElement.text = $"{obj.description} (Done)";
            obj.textElement.color = completedColor;
        }
        else
        {
            obj.textElement.text = obj.description;
            obj.textElement.color = Color.white;
        }
    }
}