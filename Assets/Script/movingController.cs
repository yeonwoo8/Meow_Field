using UnityEngine;
using UnityEngine.SceneManagement;

public class movingController : MonoBehaviour
{
    public GameObject Event;
    public GameObject Player;
    public TimeController timeController;

    public void clickMovingButton()
    {
        DontDestroyOnLoad(Event);
        DontDestroyOnLoad(timeController);


        SceneManager.LoadScene("cooking_room", LoadSceneMode.Single);
    }
}
