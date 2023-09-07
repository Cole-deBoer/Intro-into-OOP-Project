using System;
using System.Collections;
using UnityEngine;

internal class SetActiveWhenInRange : MonoBehaviour
{
    public GameObject text;
    public static bool InRange { get; private set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InRange = true;
            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InRange = false;
        text.SetActive(false);
    }
}
