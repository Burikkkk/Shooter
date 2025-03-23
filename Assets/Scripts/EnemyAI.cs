using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float obstacleRande = 5.0f;
    [SerializeField] private float viewRadius = 6.0f;
    [SerializeField] private float shootRange = 7.0f;
    [SerializeField] private float shootCooldown = 2.0f;
    [SerializeField] private float damage;
    [SerializeField] private ParticleSystem shotSystem;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private bool _alive = true;

    [SerializeField] private AudioClip shotSound; 
    private AudioSource audioSource;

    private bool _isAttacking;
    private bool _walkingToPlayer;
    private bool _canShoot = true;
    private bool _isHit;
    private Transform playerTransform;
    private Animator _animator;

    
    void Start()
    {
        _animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); 
    }

    
    void Update()
    {
        if (_alive)
        {
            if (_isAttacking || CheckIfPlayerSeen())
            {
                _isAttacking = true;
                AttackPlayer();
            }
            else
            {
                Walk();
            }
        }
    }
    
    private bool CheckIfPlayerSeen()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, viewRadius, playerLayer);
        if (hitColliders.Length != 0)
        {
            playerTransform = hitColliders[0].transform;
            return true;
        }

        return false;
    }

    private void AttackPlayer()
    {

        LookAtPlayer();
        
        if (_walkingToPlayer && !_isHit)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);  // идем на игрока
        }

        var distance = (transform.position - playerTransform.position).magnitude;
        if (distance <= shootRange)
        {
            _walkingToPlayer = false;
            if (_canShoot)
            {
                StartShootAnimation();
            }
        }
        else
        {
            _walkingToPlayer = true;
        }
        
    }

    private void LookAtPlayer()
    {
        transform.LookAt(playerTransform, transform.up);  // смотрим на игрока (но только по горизонтали)
        var rotation = transform.eulerAngles;
        rotation.x = 0.0f;
        rotation.z = 0.0f;
        transform.eulerAngles = rotation;
    }

    private void StartShootAnimation()
    {
        _animator.SetBool("Attacking", true);
    }

    public void HitEnemy()
    {
        _isHit = true;
        _animator.SetBool("Hit", true);
        _animator.SetBool("Attacking", false);
    }

    public void Die()
    {
        _animator.SetBool("Dead", true);
        EnemiesCounter.DecreaseEnemy(gameObject.tag);
    }

    public void UnsetAnimatorBool(string name)
    {
        switch (name)
        {
            case "Hit":
                _isHit = false;
                break;
            default:
                break;
        }
        _animator.SetBool(name, false);
    }
    
    private void Shoot()
    {
        var rayPosition = transform.position;
        rayPosition.y += 1.5f;
        Ray ray = new Ray(rayPosition, transform.forward);
        RaycastHit hit;
        shotSystem.Play();
        audioSource.PlayOneShot(shotSound);
        StartCoroutine(StartShootCooldown());
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.transform == playerTransform)
            {
                Health playerHealth = hitObject.GetComponent<Health>();
                playerHealth.Decrease(damage);
            }
        }
    }
    
    private IEnumerator StartShootCooldown()
    {
        _canShoot = false;
        
        yield return new WaitForSeconds(shootCooldown);

        _canShoot = true;
    }

    private void Walk()
    {
        if(_isHit)
            return;
        
        transform.Translate(0, 0, speed * Time.deltaTime);

        var rayPosition = transform.position;
        rayPosition.y += 1.5f;
        
        Ray ray = new Ray(rayPosition, transform.forward);
        RaycastHit hit;
            
        if (Physics.Raycast(ray, out hit))
        {
           
            if (hit.distance < obstacleRande)
            {
                float angleRotation = Random.Range(-100, 100);
                transform.Rotate(0, angleRotation, 0);
            }
        }
    }
    
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}