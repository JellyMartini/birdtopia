using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    private GameObject mainCam;

    public float MoveSpeed { get; set; }
    public float JumpSpeed { get; set; }
    public float Gravity { get; set; }
    public float VerticalAcceleration { get; set; }

    void Awake()
    {
        MoveSpeed = 10.0f;
        JumpSpeed = 5.0f;
        Gravity = -10.0f;
        VerticalAcceleration = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    Vector3 GetInput()
    {
        float CamY = mainCam.transform.rotation.eulerAngles.y;
        float CamY_Sin = Mathf.Sin(CamY * Mathf.Deg2Rad);
        float CamY_Cos = Mathf.Cos(CamY * Mathf.Deg2Rad);

        Vector3 velocity = new Vector3(
            Input.GetAxis("Player_Vertical") * CamY_Sin + Input.GetAxis("Player_Horizontal") * CamY_Cos,
            0.0f,
            Input.GetAxis("Player_Vertical") * CamY_Cos - Input.GetAxis("Player_Horizontal") * CamY_Sin
        );

        if (velocity.magnitude > 1.0f) velocity.Normalize();

        velocity *= MoveSpeed;

        if (controller.isGrounded) VerticalAcceleration = Input.GetAxis("Player_Jump") * JumpSpeed;
        else VerticalAcceleration += Gravity * Time.deltaTime;

        return velocity += VerticalAcceleration * Vector3.up;
    }
    // Update is called once per frame
    void Update()
    {
        controller.Move(GetInput() * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy_Weapon")) Debug.Log("Hit");
    }
}
