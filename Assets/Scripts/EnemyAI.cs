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
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private bool _alive = true;

    private bool _isAttacking = false;
    private bool _walkingToPlayer = false;
    private bool _canShoot = true;
    private Transform playerTransform;

    [SerializeField]
    private GameObject[] _fireballsPrefab;
    private GameObject _fireball;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
        transform.LookAt(playerTransform, transform.up);  // смотрим на игрока
        if (_walkingToPlayer)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);  // идем на игрока
        }

        var distance = (transform.position - playerTransform.position).magnitude;
        if (distance <= shootRange) // && cooldown)
        {
            _walkingToPlayer = false;
            if(_canShoot)
                Shoot();
        }
        else
        {
            _walkingToPlayer = true;
        }
        
    }
    
    private void Shoot()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
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
        transform.Translate(0, 0, speed * Time.deltaTime);
        
        Ray ray = new Ray(transform.position, transform.forward);
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