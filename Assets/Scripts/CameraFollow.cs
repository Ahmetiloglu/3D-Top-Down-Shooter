using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform target;

    
    [SerializeField] private Vector3 cameraTarget;
    public float smoothSpeed = 8f;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    private void Update()
    {
        cameraTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.position = Vector3.Lerp(transform.position, cameraTarget , Time.deltaTime * smoothSpeed);
    }
}