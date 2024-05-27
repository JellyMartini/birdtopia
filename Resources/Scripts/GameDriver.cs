using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameDriver : MonoBehaviour
{
    private GameObject Duck;
    public Transform DuckSpawn;
    private GameObject Falcon;
    public Transform FalconSpawn;
    private GameObject Heron;
    public Transform HeronSpawn;

    private void Awake()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
    }

    // Start is called before the first frame update
    void Start()
    {
        Duck = Resources.Load<GameObject>("Prefabs/Duck");
        Falcon = Resources.Load<GameObject>("Prefabs/Falcon");
        Heron = Resources.Load<GameObject>("Prefabs/Heron");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) Instantiate(Duck, DuckSpawn);
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) Instantiate(Falcon, FalconSpawn);
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) Instantiate(Heron, HeronSpawn);
    }
}
