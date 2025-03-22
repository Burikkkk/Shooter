using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FpsMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float speedBoost = 1.5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("Rigidbody is missing!");
        }
        else
        {
            _rb.freezeRotation = true; // ��������� ��������, ����� �� ���� �������� � �������
        }
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            speed *= speedBoost;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.Mouse1))
        {
            speed /= speedBoost;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        if (deltaX != 0 || deltaZ != 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        Vector3 moveDirection = transform.right * deltaX + transform.forward * deltaZ;
        moveDirection.y = _rb.velocity.y; // ��������� ������������ ������������ �������� (����������)

        _rb.velocity = new Vector3(moveDirection.x, _rb.velocity.y, moveDirection.z);
    }

    private void Jump()
    {
        if (CheckGrounded())
        {
            _rb.AddForce(jumpForce * transform.up);   
        }
        else
        {
            Debug.Log("Not grounded");
        }
    }
    
    private bool CheckGrounded()
    {
        var rayPosition = transform.position;
        rayPosition.y += 1.5f;
        return Physics.Raycast(rayPosition, Vector3.down, rayLength, groundLayer.value);
    }
}
