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
        {
            Destroy(_enemyAI);
        }

        StartCoroutine(DieCoroutine(3.0f));
    }

    private IEnumerator DieCoroutine(float waitSecond)
    {

        //anim
        yield return new WaitForSeconds(waitSecond);

        Destroy(this.transform.gameObject);
    }

}