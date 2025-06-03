using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        // Kiểm tra nhân vật có đang đứng trên mặt đất không
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Lấy input
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move.normalized * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Reset tất cả hướng
        animator.SetBool("isForward", false);
        animator.SetBool("isBackward", false);
        animator.SetBool("isLeft", false);
        animator.SetBool("isRight", false);

        // Bật hướng tương ứng
        if (z > 0) animator.SetBool("isForward", true);
        else if (z < 0) animator.SetBool("isBackward", true);
        else if (x < 0) animator.SetBool("isLeft", true);
        else if (x > 0) animator.SetBool("isRight", true);
    }
}