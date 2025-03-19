using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    [SerializeField] private int bulletsAmount;
    [SerializeField] private float damage = 1.0f;
    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && bulletsAmount > 0)
        {
            Shoot();
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