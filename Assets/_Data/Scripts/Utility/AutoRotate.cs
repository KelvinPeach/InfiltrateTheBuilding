using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [SerializeField] Vector3 rotateSpeed;

    void Update()
    {
        transform.Rotate(rotateSpeed.x * Time.deltaTime, rotateSpeed.y * Time.deltaTime, rotateSpeed.z * Time.deltaTime);
    }
}