using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCamera : MonoBehaviour
{

    [SerializeField] GameObject trackedEntity;
    // how fast the camera follows the trackedEntity
    [SerializeField] float updateSpeed = 0.2F;

    Vector3 velocity;
    Vector3 trackedOffset;

    void Start()
    {
        trackedEntity = GameObject.Find("PlayerRed");
        // TODO: checks in case trackedEntity is not player
        velocity = trackedEntity.GetComponent<Rigidbody2D>().velocity;
        // z offset so the camera always stays on top of the trackedEntity
        trackedOffset = Vector3.zero;
        trackedOffset.z = transform.position.z - trackedEntity.transform.position.z;
        transform.position = trackedEntity.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // delayed smooth following animation that looks nice
        transform.position = Vector3.SmoothDamp(transform.position, trackedEntity.transform.position + trackedOffset, ref velocity, updateSpeed);
        
    }
}
