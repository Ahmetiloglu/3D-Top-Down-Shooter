using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private AnimationController animationController;
    private Quaternion targetRotation;
    public Gun gun;
    
    [Header("Settings")]
    public float rotationSpeed = 400f;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
        if (Input.GetButtonDown("Shoot"))
        {
            gun.Shoot();
        }
    }

    void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (input != Vector3.zero)
        {
            animationController.SetBoolean("Run",true);
            targetRotation = Quaternion.LookRotation(input);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRotation.eulerAngles.y,rotationSpeed * Time.deltaTime);
        }
        else
        {
            animationController.SetBoolean("Run",false); 
        }

        Vector3 motion = input;
        motion *= (Math.Abs(input.x) == 1 && Math.Abs(input.z) == 1) ? .7f : 1;
        
        motion += Vector3.up * -8;
        
        _controller.Move(motion * walkSpeed * Time.deltaTime);
    }
}