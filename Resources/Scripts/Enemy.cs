using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Player player;
    private Animator swordAnim;
    private int tick, tick_max;
    public float tick_interval, hitDistance, hitRadius;
    private LayerMask playerLayerMask = 6;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        swordAnim = GetComponentsInChildren<Animator>()[1];

        tick = -1;
        tick_max = (int) (tick_interval / Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tick = (tick + 1) % tick_max;
        if (tick == 0) agent.destination = player.transform.position;

        if ((swordAnim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.1f && swordAnim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f < 0.15f) ||
           (swordAnim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.6f && swordAnim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f < 0.65f))
            if (ShootRay()) Debug.Log("Hit");
    }

    private bool ShootRay()
    {
        if (Physics.SphereCast(transform.position, hitRadius, transform.TransformDirection(Vector3.forward), out RaycastHit sphereHit, hitDistance, ~playerLayerMask))
        {
            Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward) * hitDistance, Color.green);
            return true;
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit lineHit, hitDistance, playerLayerMask))
        {
            Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward) * hitDistance, Color.green);
            return true;
        }
        Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward) * hitDistance, Color.red, 1.0f);
        return false;
    }

    
}
