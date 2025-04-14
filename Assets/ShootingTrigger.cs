using System;
using UnityEngine;

public class ShootingTrigger : MonoBehaviour
{
    public FPSController FPSController;

    public bool inTriggerArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inTriggerArea)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                FPSController.AllowShooting();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTriggerArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTriggerArea = false;
        }
    }
}
