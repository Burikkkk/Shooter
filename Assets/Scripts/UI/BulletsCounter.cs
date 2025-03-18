using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletsCounter : MonoBehaviour
{
    [SerializeField] private RayShooter shooter;
    [SerializeField] private TMP_Text counterText;

    private void Update()
    {
        counterText.text = shooter.BulletsAmount.ToString();
    }
}
