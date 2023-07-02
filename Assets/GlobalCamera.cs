using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCamera : MonoBehaviour
{

    [SerializeField]
    GameObject trackedEntity;
    Vector3 velocity;
    [SerializeField]
    float updateSpeed = 0.2F;
    Vector3 trackedOffset;

    void Start()
    {
        trackedEntity = GameObject.Find("PlayerRed");
        // TODO: checks in case trackedEntity is not player
        velocity = trackedEntity.GetComponent<Rigidbody2D>().velocity;
        trackedOffset = Vector3.zero;
        trackedOffset.z = transform.position.z - trackedEntity.transform.position.z;
        transform.position = trackedEntity.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, trackedEntity.transform.position + trackedOffset, ref velocity, updateSpeed);
        
    }
}
