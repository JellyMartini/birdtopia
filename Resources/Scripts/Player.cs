using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;

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
    }

    Vector3 GetInput()
    {
        Vector3 velocity = Vector3.right * Input.GetAxis("Player_Horizontal") + 
            Vector3.forward * Input.GetAxis("Player_Vertical");

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
