using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isOpen = false;
    private bool isAvailable = true;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isAvailable && !isOpen && Input.GetKeyDown(KeyCode.Tab)){
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            isOpen = true;
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isOpen = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Disable()
    {
        isAvailable = false;
    }

    public void Enable()
    {
        isAvailable = true;
    }

    public void Restart()
    {
        // Reload the scene
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Deactivate()
    {
        isAvailable = false;
    }

    public void Activate()
    {
        isAvailable = true;
    }


}
