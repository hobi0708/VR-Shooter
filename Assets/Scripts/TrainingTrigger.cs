using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TrainingTrigger : MonoBehaviour
{
    public GameObject trainingSettingsPanel;
    public FPSController playerController;

    private void Start()
    {
        trainingSettingsPanel.SetActive(false);
    }

    private void Update()
    {
        if (trainingSettingsPanel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Отключить вращение камеры
            playerController.enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Включить обратно FPS контроллер
            playerController.enabled = true;
        }
    }

    public void CloseMenu()
    {
        trainingSettingsPanel.SetActive(false); 

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerController.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trainingSettingsPanel.SetActive(true);
        }
    }

    
}
