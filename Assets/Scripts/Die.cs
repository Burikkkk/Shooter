using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    private Animator animator; // Ссылка на Animator
    [SerializeField] private GameObject panel;  // Ссылка на панель

    private void Start()
    {
        animator= GetComponent<Animator>();
    }

    // Метод для запуска второй анимации
    public void StartCameraAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("CameraAnimation"); // Запускаем анимацию
        }
    }

    // Метод для активации UI-панели
    public void ActivatePanel()
    {
        if (panel != null)
        {
            panel.SetActive(true); // Включаем панель
        }
    }
}
