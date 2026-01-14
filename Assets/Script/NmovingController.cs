using UnityEngine;
using UnityEngine.SceneManagement;

public class NmovingController : MonoBehaviour
{


    public NTimeController ntimeController;

    public void clickMovingButton()
    {
        DontDestroyOnLoad(ntimeController);


        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }
}
