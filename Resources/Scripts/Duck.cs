using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Duck : Enemy
{
    private NavMeshAgent agent;
    private Player player;
    private Animator swordAnim;
    private int tick, tick_max;
    public float tick_interval, hitDistance, hitRadius;
    private LayerMask playerLayerMask = 6;

    private int hit_cooldown, hit_cooldown_max;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        swordAnim = GetComponentsInChildren<Animator>()[1];

        tick = -1;
        tick_max = (int) (tick_interval / Time.fixedDeltaTime);
        hit_cooldown_max = tick_max / 2;
        hit_cooldown = hit_cooldown_max + 1;

        Health = 100.0f;
        Damage = 10.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tick = (tick + 1) % tick_max;
        if (tick == 0) agent.destination = player.transform.position;

        if (((swordAnim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.1f && swordAnim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f < 0.15f) ||
           (swordAnim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.6f && swordAnim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f < 0.65f))
           && hit_cooldown > hit_cooldown_max)
            if (ShootRay())
            {
                Debug.Log("Hit");
                hit_cooldown = 0;
                Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward) * hitDistance, Color.green);
            }
            else Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward) * hitDistance, Color.red);
        hit_cooldown++;
    }

    private bool ShootRay()
    {
        if (Physics.SphereCast(transform.position, hitRadius, transform.TransformDirection(Vector3.forward), out RaycastHit sphereHit, hitDistance, ~playerLayerMask))
            return true;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit lineHit, hitDistance, ~playerLayerMask))
            return true;
        return false;
    }
    
}
