using Unity.VisualScripting;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{

    public GameObject pauseMenu;
    private bool isPaused = false;
    private bool isSaved = false;
    public GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;

        }
    }


    public void Save()
    {
        gameManager.SaveGame();
        Debug.Log("저장 완료!");
        isSaved = true;
    }

    public void Quit()
    {
        if (!isSaved)
        {
            Debug.Log("저장부터 진행해주세요!");
        }
        else
        {
            Application.Quit();
        }
    }
}
