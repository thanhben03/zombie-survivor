using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective4 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObjectiveManager.Instance.CompleteObjective(4);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
