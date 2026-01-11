using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveComplete : MonoBehaviour
{
    [Header("Objective to complete")]
    public TextMeshProUGUI objective1;
    public TextMeshProUGUI objective2;
    public TextMeshProUGUI objective3;
    public TextMeshProUGUI objective4;

    public static ObjectiveComplete Instance;
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    public void GetObjectDone(bool obj1, bool obj2, bool obj3, bool obj4)
    {
        if (obj1)
        {
            objective1.text += " (Done)";
            objective1.color = Color.green;
        } else
        {
            objective1.text = "01. FIND THE RIFLE";
        }

        if (obj2)
        {
            objective2.color = Color.green;

            objective2.text += " (Done)";
        }
        else
        {
            objective2.text = "02. FIND THE VILLAGERS";
        }

        if (obj3)
        {
            objective3.color = Color.green;
            objective3.text += " (Done)";
        }
        else
        {
            objective3.text = "03. FIND VEHICLE";
        }

        if (obj4)
        {
            objective4.color = Color.green;
            objective4.text += " (Done)";
        }
        else
        {
            objective4.text = "04. TAKE ALL OF THE VILLAGERS INTO VEHICLE";
        }
    }
}
