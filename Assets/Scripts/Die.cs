using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    private Animator animator; // ������ �� Animator
    [SerializeField] private GameObject panel;  // ������ �� ������

    private void Start()
    {
        animator= GetComponent<Animator>();
    }

    // ����� ��� ������� ������ ��������
    public void StartCameraAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("CameraAnimation"); // ��������� ��������
        }
    }

    // ����� ��� ��������� UI-������
    public void ActivatePanel()
    {
        if (panel != null)
        {
            panel.SetActive(true); // �������� ������
        }
    }
}
