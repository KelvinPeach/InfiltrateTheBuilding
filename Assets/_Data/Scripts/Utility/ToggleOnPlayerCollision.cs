using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOnPlayerCollision : MonoBehaviour
{
    [SerializeField] GameObject gameObjectToToggle;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObjectToToggle.SetActive(!gameObjectToToggle.activeSelf);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObjectToToggle.SetActive(!gameObjectToToggle.activeSelf);
        }
    }
}