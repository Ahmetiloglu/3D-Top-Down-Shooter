using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    [SerializeField] private AnimationController animationController;
    [SerializeField] private LineRenderer tracer;


    public LayerMask collisionMask;
    public float damage = 1f;
    
    public float rpm;
    private float secondsBetweenShots;
    private float nextPossibleShootTime;
    
    public Transform spawn;


    private void Start()
    {
        secondsBetweenShots = 60 / rpm;
    }
    

    public void Shoot()
    {
        Ray ray = new Ray(spawn.position, transform.forward);
        RaycastHit hit;
        float shotDistance = 20f;

        if (Physics.Raycast(ray, out hit, shotDistance, collisionMask))
        {
            shotDistance = hit.distance;
            if (hit.collider.GetComponent<Enemy>() )
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            
        }

        nextPossibleShootTime = Time.time + secondsBetweenShots;
        GetComponent<AudioSource>().Play();
        // Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red,1);

        if (tracer)
        {
            animationController.SetBoolean("Shoot",true); 
            StartCoroutine("RenderTracer", ray.direction * shotDistance);
        }
        else
        {
            animationController.SetBoolean("Shoot",false); 
        }
    }

/*
    private bool CanShoot()
    {
        bool canShoot = true;
        if (Time.time < nextPossibleShootTime)
        {
            canShoot = false;
        }
        return canShoot;
    }
*/

    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0,spawn.position);
        tracer.SetPosition(1,spawn.position + hitPoint);
        
        yield return null;
        tracer.enabled = false;
      
    }
}
