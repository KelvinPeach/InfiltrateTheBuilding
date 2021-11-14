using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;

    void Start()
    {
        if (!target)
            target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (target)
            transform.position = new Vector3(target.position.x + offset.x, transform.position.y + offset.y, target.position.z + offset.z);
    }
}