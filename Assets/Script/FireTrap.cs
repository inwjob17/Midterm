using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public GameObject firePrefab; 
    public Transform firePoint;   
    public float interval = 5f;   

    private GameObject currentFire;
    private float timer;
    private bool isFireActive = false;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            if (!isFireActive)
            {
                SpawnFire();
            }
            else
            {
                StopFire();
            }

            timer = 0f; 
            isFireActive = !isFireActive;
        }
    }

    void SpawnFire()
    {
        if (firePrefab != null && firePoint != null)
        {
            currentFire = Instantiate(firePrefab, firePoint.position, firePoint.rotation);
            
            currentFire.transform.parent = firePoint;
        }
    }

    void StopFire()
    {
        if (currentFire != null)
        {
            Destroy(currentFire);
        }
    }
}