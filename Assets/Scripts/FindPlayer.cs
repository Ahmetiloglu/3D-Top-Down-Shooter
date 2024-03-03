using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class FindPlayer : MonoBehaviour
{
    public Transform destination;
    private NavMeshAgent ai;
    

    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        ai.SetDestination(destination.position);
        DestroyAI();
    }
    
    
    private void DestroyAI()
    {
        if (ai.remainingDistance < 0.5f && ai.hasPath)
        {
            ai.ResetPath();
            Destroy(this.gameObject , 0.5f);
        }
    }
}

 