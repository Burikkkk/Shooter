using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    private Slider healthBar;

    private void Start()
    {
        healthBar = GetComponent<Slider>();
    }

    void Update()
    {
        healthBar.value = playerHealth.Percent();
    }
}
