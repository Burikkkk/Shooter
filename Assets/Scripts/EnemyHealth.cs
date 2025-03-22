using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hp;
    private ReactiveTarget _reactiveTarget;
    private float maxHp;

    private void Start()
    {
        maxHp = hp;
        _reactiveTarget = GetComponent<ReactiveTarget>();
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
        else
        {
            _reactiveTarget.ReactToSmallHit();
        }
    }

    public void Die()
    {
        //GetComponent<Animator>().SetBool("Dead", true);
        _reactiveTarget.ReactToHit();

    }
}
