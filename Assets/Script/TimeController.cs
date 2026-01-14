using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeController : MonoBehaviour
{
    public Button potButton;
    public GameObject skyquad;
    public GameObject nightquad;
    public GameObject midnightquad;
    public TextMeshProUGUI clockText;

    private int lastHour = -1;
    private int lastMinute = -1;

    private void Update()
    {
        int hours = GameManager.hours;
        int minutes = GameManager.minutes;

        if (clockText != null && (minutes != lastMinute || hours != lastHour))
        {

            if (minutes % 10 == 0)
            {
                clockText.text = $"{hours:D2}:{minutes:D2}";
            };
            lastMinute = minutes;
            lastHour = hours;
        }

        // 시간대 바뀔 때만 quad 변경


        if (hours != lastHour)
        {
            if (skyquad != null && midnightquad != null && nightquad != null)
            {
                skyquad.SetActive(false);
                midnightquad.SetActive(false);
                nightquad.SetActive(false);

                if (hours >= 17 && hours < 19)
                {
                    midnightquad.SetActive(true);
                }
                else if (hours >= 19 && hours < 23)
                {
                    nightquad.SetActive(true);
                    potButton?.gameObject.SetActive(true);
                }
                else
                {
                    skyquad.SetActive(true);
                    potButton?.gameObject.SetActive(false);
                }
            }

            lastHour = hours;
        }
    }
}
