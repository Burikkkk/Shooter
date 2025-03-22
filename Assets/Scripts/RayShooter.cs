using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayShooter : MonoBehaviour
{
    [SerializeField] private int bulletsAmount;
    [SerializeField] private float damage = 1.0f;
    [SerializeField] private ParticleSystem shotSystem;
    [SerializeField] private TMP_Text skeletonsText;
    [SerializeField] private TMP_Text piratesText;
    [SerializeField] private int skeletonsAmount;
    [SerializeField] private int piratesAmount;

    private Camera _camera;

    void Start()
    {
        EnemiesCounter.Initialize(skeletonsText, piratesText, skeletonsAmount, piratesAmount);
        _camera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && bulletsAmount > 0)
        {
            Shoot();
        }
        if (EnemiesCounter.CheckWin())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 1.0f;
            SceneManager.LoadSceneAsync(2);
        }
        if(bulletsAmount==0 && EnemiesCounter.CheckWin() == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 1.0f;
            SceneManager.LoadSceneAsync(3);
        }

    }

    public int BulletsAmount
    {
        get { return bulletsAmount; }
    }


    private void Shoot()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        Ray ray = _camera.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        bulletsAmount--;
        shotSystem.Play();

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            EnemyHealth health = hitObject.GetComponent<EnemyHealth>();

            if (health != null)
            {
                health.Decrease(damage);
            }
            
        }
    }
   
}