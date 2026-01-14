using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{

    public List<GameObject> tutorialPanels;
    private int currentIndex = 0;

    public GameObject grn_bean;
    public GameObject red_bean;
    public GameObject purple_bean;
    public float fallSpeed = 100f;

    private bool beansDropped = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0f;  // 게임 멈춤
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        if (!PlayerPrefs.HasKey("IsFirstPlay"))
        {
            PlayerPrefs.SetInt("IsFirstPlay",1);
            PlayerPrefs.Save();


            for (int i = 0; i < tutorialPanels.Count; i++)
            {
                tutorialPanels[i].SetActive(i == 0);

            }

        }
        else
        {
            foreach (var panel in tutorialPanels)
            {
                panel.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 패널이 없으면 바로 리턴
        if (tutorialPanels.Count == 0)
            return;

        // currentIndex가 범위를 벗어나면 리턴
        if (currentIndex >= tutorialPanels.Count)
        {
            Time.timeScale = 1f;  // 시간 되돌리기
            return;
        }

        if (tutorialPanels[currentIndex].activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextPanel();
        }
    }

    void ShowNextPanel()
    {
        // 현재 패널 끄기
        tutorialPanels[currentIndex].SetActive(false);
        currentIndex++;

        if (currentIndex < tutorialPanels.Count)
        {
            // 다음 패널 켜기
            tutorialPanels[currentIndex].SetActive(true);

            if (currentIndex == 1 && !beansDropped)
            {
                StartCoroutine(DropBeans());
                beansDropped = true;
            }
        }
    }



    IEnumerator DropBeans()
    {
        for (int i = 0; i < 30; i++)
        {
            grn_bean.transform.position += Vector3.down * 0.1f;
            red_bean.transform.position += Vector3.down * 0.1f;
            purple_bean.transform.position += Vector3.down * 0.1f;
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }

}
