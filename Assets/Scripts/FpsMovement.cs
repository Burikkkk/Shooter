using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 6.0f;
    [SerializeField] private float _gravity = -9.8f;

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
            _rb.freezeRotation = true; // Отключаем вращение, чтобы не было коллизий с физикой
        }
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float deltaX = Input.GetAxis("Horizontal") * _speed;
        float deltaZ = Input.GetAxis("Vertical") * _speed;

        Vector3 moveDirection = transform.right * deltaX + transform.forward * deltaZ;
        moveDirection.y = _rb.velocity.y; // Сохраняем существующую вертикальную скорость (гравитация)

        _rb.velocity = new Vector3(moveDirection.x, _rb.velocity.y + _gravity * Time.deltaTime, moveDirection.z);
    }
}
