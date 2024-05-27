using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Transform player;
    private Vector3 duckPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        duckPos = player.position;
        if (GameObject.FindGameObjectWithTag("Duck") != null)
        {
            duckPos = Vector3.zero;
            int iter = 0;
            foreach (GameObject duck in GameObject.FindGameObjectsWithTag("Duck"))
            {
                duckPos += duck.transform.position;
                iter++;
            }
            duckPos /= (float) iter;
        }

        transform.LookAt((player.position + duckPos) / 2.0f);
    }
}
