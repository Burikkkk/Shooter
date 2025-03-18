using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip run;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float speedBoost = 2f;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private float damage = 10f;
    [SerializeField] private GameObject playerSword;
    private float rayLength = 1.0f;
    private bool speedBoosted = false;
    private bool attacks = false;
    private bool dead;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    private Health health;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
     
    }

    private void Update()
    {
        hpSlider.value = health.Percent();
        if(dead)
            return;
        
        bool isGrounded = CheckGrounded();
        
        if (isGrounded)
        {
            State = States.Idle;
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
        }
        else
        {
            if (rb.velocity.y > 0.0f) // летит вверх
            {
                State = States.Jump;
                
            }
            else // падает
            {
                State = States.Fall;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            ChangeSound(run);
            speedBoosted = true;
            State = States.Slide;
            speed *= speedBoost;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedBoosted = false;
            speed /= speedBoost;
        }
        if (Input.GetButton("Horizontal"))
            Run();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            Attack();
    }

    private void ChangeSound(AudioClip clip) {
    
        audio.PlayOneShot(clip);
    }


    private void Run()
    {
        if (CheckGrounded())
        {
            State = States.Run;
        }

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        //sprite.flipX = dir.x < 0.0f;

         var direction = dir.x < 0.0f ? -1 : 1;
         var spriteScale = sprite.gameObject.transform.localScale;
         if((direction < 0 && spriteScale.x > 0) || (direction > 0 && spriteScale.x < 0))
             spriteScale.x *= -1;
         sprite.gameObject.transform.localScale = spriteScale;

    }



    private bool CheckGrounded()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, rayLength, groundLayer.value);
    }

    private void Jump()
    {
        ChangeSound(jump);
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        State = States.Jump;
    }

    private void Attack()
    {
        ChangeSound(attack);
        State = States.Attack;
        attacks = true;
    }

    public void Die()
    {
        ChangeSound(death);
        State = States.DeathNoEffect;
        dead = true;
        hpSlider.gameObject.SetActive(false);
    }

    public void SetAttacks(bool value)
    {
        attacks = value;
    }

    public float Damage
    {

        get { return damage; }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DeathBox"))
        {
            Die();
        }
    }

    public enum States
    {

        Idle, 
        Run,
        Slide,
        Attack,
        HurtNoEffect,
        DeathNoEffect,
        Jump,
        Fall
    }

    private States State
    {
        get { return (States)animator.GetInteger("State"); }
        set
        {
            if(attacks && value != States.DeathNoEffect)
                return;
            if(animator.GetInteger("State") != (int)value) 
                animator.SetInteger("State", (int)value);
        }
  
    }
}
