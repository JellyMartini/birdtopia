using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Player player;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponentsInChildren<Animator>()[0];

        int i = 0;
        while (!animator.name.Equals("sword") && GetComponentsInChildren<Animator>()[i] != null)
        {
            animator = GetComponentsInChildren<Animator>()[i++];
        }
    }

    // Update is called once per frame
    void Update()
    {   
        agent.destination = player.transform.position;
    }
}
