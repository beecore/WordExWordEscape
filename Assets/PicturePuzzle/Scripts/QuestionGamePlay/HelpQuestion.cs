using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpQuestion : MonoBehaviour
{
    [SerializeField]
    GameObject firstInputChar;
    [SerializeField]
    GameObject helpContent_1;
    [SerializeField]
    GameObject helpContent_2;
    [SerializeField]
    GameObject helpContent_3;

    GameObject firstAnswerChar;

    // First step of help will get activated on enable of this script component.
    void OnEnable()
    {
        ShowHelp(1);
    }

    // Loads the given stop of the help.
    public void ShowHelp(int helpStep)
    {
        if (helpStep == 1)
        {
            helpContent_1.SetActive(true);
            Canvas c = firstInputChar.AddComponent<Canvas>();
            c.overridePixelPerfect = false;
            c.overrideSorting = true;
            c.sortingOrder = 1;
            firstInputChar.AddComponent<GraphicRaycaster>();
        }

        else if (helpStep == 2)
        {
            helpContent_1.SetActive(false);
            Destroy(firstInputChar.GetComponent<GraphicRaycaster>());
            Destroy(firstInputChar.GetComponent<Canvas>());

            helpContent_2.SetActive(true);
            firstAnswerChar = QuestionGamePlay.Instance.answerPanel.transform.GetChild(0).gameObject;
            Canvas c = firstAnswerChar.AddComponent<Canvas>();
            c.overridePixelPerfect = false;
            c.overrideSorting = true;
            c.sortingOrder = 1;
            firstAnswerChar.AddComponent<GraphicRaycaster>();
        }

        else if (helpStep == 3)
        {
            helpContent_2.SetActive(false);
            Destroy(firstAnswerChar.GetComponent<GraphicRaycaster>());
            Destroy(firstAnswerChar.GetComponent<Canvas>());

            helpContent_3.SetActive(true);
            transform.SetSiblingIndex(QuestionGamePlay.Instance.answerPanel.transform.parent.GetSiblingIndex());

            PlayerPrefs.SetInt("HelpShownQuestion", 1);
            Invoke("DisableHelp", 3F);
        }

    }

    // Disable's help.
    void DisableHelp()
    {
        gameObject.SetActive(false);
    }
}
