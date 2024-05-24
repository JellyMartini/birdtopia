using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falcon : MonoBehaviour
{
    private GameObject player;
    private GameObject playerViewModel;
    private bool attack;
    private Vector3 startPos;
    private Vector3 targetPos;
    public float slerpWeight;
    public float lerpWeight;

    public float orbitingDistance;
    public float falconHeight;

    private int tick;
    public int tick_max;

    // Start is called before the first frame update
    void Start()
    {
        attack = false;
        player = GameObject.Find("Player");
        playerViewModel = GameObject.Find("ViewModel");
        transform.position = player.transform.position + playerViewModel.transform.TransformDirection(Vector3.back * orbitingDistance) + Vector3.up * falconHeight;
        startPos = transform.position;
        targetPos = transform.position;
        tick = -1;
        tick_max = (int) (tick_max / Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Slerp(startPos, targetPos + Vector3.up, tick / (float) tick_max);
    }

    private void FixedUpdate()
    {
        tick = (tick + 1) % tick_max;
        if (tick == tick_max / 2) Swoop();
        else if (tick < tick_max / 2)
        {
            startPos = transform.position;
            targetPos = player.transform.position + playerViewModel.transform.TransformDirection(Vector3.back * orbitingDistance) + Vector3.up * falconHeight;
        }
    }

    private void Swoop()
    {
        startPos = transform.position;
        targetPos = playerViewModel.transform.TransformDirection(Vector3.forward * orbitingDistance) + Vector3.up * 2.0f;
    }
}
