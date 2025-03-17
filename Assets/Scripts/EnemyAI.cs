using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float obstacleRande = 5.0f;
    [SerializeField] private bool _alive = true;

    [SerializeField]
    private GameObject[] _fireballsPrefab;
    private GameObject _fireball;


    // Start is called before the first frame update
    void Start()
    {
        _alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (hitObject.GetComponent<CharacterController>()) //нужно поменять на тег
                {
                    if (_fireball == null)
                    {
                        int randFireball = Random.Range(1, _fireballsPrefab.Length);
                        _fireball = Instantiate(_fireballsPrefab[randFireball]) as GameObject;
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRande)
                {
                    float angleRotation = Random.Range(-100, 100);
                    transform.Rotate(0, angleRotation, 0);
                }
            }
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}