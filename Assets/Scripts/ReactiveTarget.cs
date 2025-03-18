using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    [SerializeField] private float hitForce;
    private EnemyAI _enemyAI;

    // Start is called before the first frame update
    void Start()
    {
        _enemyAI = GetComponent<EnemyAI>();
    }

    public void ReactToHit(Transform playerTransform, Vector3 hitPoint)
    {
        if (_enemyAI != null)
        {
            Destroy(_enemyAI);
        }

        StartCoroutine(DieCoroutine(3, playerTransform, hitPoint));
    }

    private IEnumerator DieCoroutine(float waitSecond, Transform playerTransform, Vector3 hitPoint)
    {
        //transform.Rotate(-45, 0, 0);
        GetComponent<Rigidbody>().AddForceAtPosition(hitForce * playerTransform.forward, hitPoint);
        yield return new WaitForSeconds(waitSecond);

        Destroy(this.transform.gameObject);
    }

}