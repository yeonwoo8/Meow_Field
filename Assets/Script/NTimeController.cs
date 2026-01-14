using UnityEngine;
using UnityEngine.UI;

public class NTimeController : MonoBehaviour
{
    public Text clockText;

    private void Update()
    {

        if (GameManager.minutes % 10 == 0)
        {
            clockText.text = $"{GameManager.hours:D2}:{GameManager.minutes:D2}";

        }
    }
}
