using UnityEngine;
using UnityEngine.UI;

public class UIcoinController : MonoBehaviour
{

    public Text COINText;

    // Update is called once per frame
    void Update()
    {
        COINText.text = $"{GameManager.coin}";
    }
}
