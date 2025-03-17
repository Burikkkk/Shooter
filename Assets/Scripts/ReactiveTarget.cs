using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    private EnemyAI _enemyAI;

    // Start is called before the first frame update
    void Start()
    {
        _enemyAI = GetComponent<EnemyAI>();
    }

    public void ReactToHit()
    {
        if (_enemyAI != null)
            _enemyAI.SetAlive(false);

        StartCoroutine(DieCoroutine(3));
    }

    private IEnumerator DieCoroutine(float waitSecond)
    {
        this.transform.Rotate(45, 0, 0);

        yield return new WaitForSeconds(waitSecond);

        Destroy(this.transform.gameObject);
    }
}