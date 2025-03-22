using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float hp;
    private float maxHp;
    

    private void Start()
    {
        maxHp = hp;
    }

    public float Hp
    {
        get { return hp; }
    }

    public void Increase(float value)
    {
        hp += value;
    }

    public float Percent()
    {
        return hp / maxHp;
    }

    public void Decrease(float value)
    {
        hp -= value;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GetComponent<MouseLook>().enabled = false;
        GetComponent<RayShooter>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(3);
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WaterCollider"))
            Die();
    }
}
