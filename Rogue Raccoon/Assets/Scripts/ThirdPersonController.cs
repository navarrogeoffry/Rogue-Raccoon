using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Lumin;
using UnityEngine.TextCore.Text;
using Vector3 = UnityEngine.Vector3;

public class ThirdPersonController : MonoBehaviour
{
    public CharacterController controller;
    float horziontalInput, verticalInput;
    public float moveSpeed = 5;
    [HideInInspector] public Vector3 direction;
    [SerializeField] LayerMask groundMask;
    public float groundDistance = 0.4f;
    public Transform groundCheck;
    [SerializeField] float gravity = -20f;
    [SerializeField] float jumpHeight = 3f;
    Vector3 velocity;
    Vector3 spherePos;
    bool IsGrounded;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMovement();
        Jump();
        Gravity();
    }

    void CharacterMovement() {
        horziontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        direction = transform.forward * verticalInput + transform.right * horziontalInput;

        controller.Move(direction * moveSpeed * Time.deltaTime);
    }

    void Jump(){
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }  

    void Gravity()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (!IsGrounded) {
            velocity.y += gravity * Time.deltaTime;
        } else if (velocity.y < 0) {
            velocity.y = -2f;
        }

        controller.Move(velocity * Time.deltaTime);
    }   
}
