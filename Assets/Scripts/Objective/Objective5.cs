using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective5 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vehicle"))
        {
            ObjectiveManager.Instance.CompleteObjective(5); 
        }
    }
}
