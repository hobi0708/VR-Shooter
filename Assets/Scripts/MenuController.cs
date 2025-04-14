using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public bool isInGame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInGame)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Menu();
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PlayFPS()
    {
        SceneManager.LoadScene("ShootingRangeFPS");
    }
    
    public void PlayVR()
    {
        SceneManager.LoadScene("ShootingRangeVR");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
