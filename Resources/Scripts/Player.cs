using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    private GameObject mainCam;
    private GameObject ViewModel;
    public float enemyPushForce;

    public float MoveSpeed { get; set; }
    public float JumpSpeed { get; set; }
    public float Gravity { get; set; }
    public float VerticalAcceleration { get; set; }

    private Item[] inventory;

    void Awake()
    {
        MoveSpeed = 10.0f;
        JumpSpeed = 10.0f;
        Gravity = -15.0f;
        VerticalAcceleration = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        ViewModel = GameObject.Find("ViewModel");
        controller = GetComponent<CharacterController>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        ViewModel.transform.rotation = Quaternion.Euler(0.0f, mainCam.transform.eulerAngles.y, 0.0f);
    }

    Vector3 GetInput()
    {
        float CamY = mainCam.transform.rotation.eulerAngles.y;
        float CamY_Sin = Mathf.Sin(CamY * Mathf.Deg2Rad);
        float CamY_Cos = Mathf.Cos(CamY * Mathf.Deg2Rad);

        Vector2 PlayerInputs = new Vector2(Input.GetAxis("Player_Horizontal"), Input.GetAxis("Player_Vertical"));
        
        if (PlayerInputs.magnitude > 1.0f) PlayerInputs.Normalize();

        Vector3 velocity = new Vector3(
            PlayerInputs.y * CamY_Sin + PlayerInputs.x * CamY_Cos,
            0.0f,
            PlayerInputs.y * CamY_Cos - PlayerInputs.x * CamY_Sin
        );

        if (velocity.magnitude > 0.1f) 
            ViewModel.transform.LookAt(transform.position + transform.TransformDirection(velocity));

        ViewModel.transform.rotation = Quaternion.Euler(0.0f, ViewModel.transform.rotation.eulerAngles.y, 0.0f);

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
            controller.Move((transform.position - other.transform.position).normalized * enemyPushForce * Time.fixedDeltaTime);
    }
}
