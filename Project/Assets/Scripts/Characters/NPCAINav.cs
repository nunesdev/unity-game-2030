using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// using UnityEngine.SceneManagement;

public class NPCAINav : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public GameObject theDestination;
    public Transform playerTransform; 
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", true);
        
    }

    // Update is called once per frame
    void Update() 
    {
        navMeshAgent.SetDestination(theDestination.transform.position);
    }

    // private void OnCollisionEnter(Collision collisionInfo)
    // {
    //     if(collisionInfo.collider.name == "Player") {
    //         SceneManagement.LoadScene(1);
    //     }
    // }
}
