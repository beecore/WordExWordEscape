using System.Collections;
using TMPro;
using UnityEngine;

public class QuestionGamePlay : Singleton<QuestionGamePlay>
{
    [SerializeField]
    private GameObject btnBack;

    [SerializeField]
    private GameObject header;

    public LevelQuestionReader levelReader;
    public AnswerPanel answerPanel;
    public InputQuestionPanel inputPanel;
    public TMP_Text questionText;

    public static LevelQuestion thisLevel = null;

    [SerializeField]
    private GameObject helpPanel;

    private bool isHelpActive = false;
    private bool isHelpLevel = false;
    private int currentHelpStep = 0;
    void OnEnable()
    {
        Invoke("LoadLevel", 0.1F);
    }
    public void LoadLevel()
    {
        Debug.Log("question");
        thisLevel = levelReader.ReadLevel();
        questionText.text = thisLevel.question;
        CheckForHelp();
    }

    // If help not ever, then it will show help on loading a level.
    private void CheckForHelp()
    {
        if (PlayerPrefs.HasKey("HelpShownQuestion"))
        {
            return;
        }
        isHelpActive = true;
        currentHelpStep = 1;
        helpPanel.SetActive(true);
        isHelpLevel = true;
    }

    // Handles device back button on android.
    public void OnBackButtonPressed()
    {
        if (InputManager.Instance.canInput())
        {
            StartCoroutine(DisableGamePlayScreen());
        }
    }

    // Disables gameplay and loads home screen, will get called on pressing back button of the app.
    private IEnumerator DisableGamePlayScreen()
    {
        btnBack.GetComponent<Animator>().SetTrigger("Exit");
        header.GetComponent<Animator>().SetTrigger("Exit");
        yield return StartCoroutine(levelReader.UnloadLevel());
        Invoke("ActivateHomeScreen", 0.2F);
    }

    // Delayed call to activate home screen.
    private void ActivateHomeScreen()
    {
        gameObject.Deactivate();
        UIController.Instance.homeScreen.Activate();
    }

    // Activates Level Complete screen.
    private void OnLevelComplete()
    {
        UIController.Instance.levelCompleteScreen.Activate();
    }

    // Add Character to answer panel, will be called from input panel.
    public void AddCharacterToAnswer(InputChar inputChar)
    {
        Debug.Log("Add level");
        if (inputChar != null && !inputChar.isEmpty())
        {
            AnswerChar emptyChar = answerPanel.GetEmptyChar();

            if (emptyChar != null && emptyChar.isEmpty())
            {
                emptyChar.SetChacter((char)inputChar.GetCharacter(), inputChar.characterId);
                inputChar.SetEmpty();
                AudioController.Instance.PlayInputSound();
                //fill full charater
                if (answerPanel.isAnswerPanelFull())
                {
                    VerifyAnswer();
                }
            }

            if (isHelpActive && currentHelpStep == 1)
            {
                currentHelpStep = 2;
                helpPanel.GetComponent<HelpQuestion>().ShowHelp(currentHelpStep);
            }
        }
    }

    // Removes the selected character from answer panel.
    public void RemoveCharacterFromAnswer(AnswerChar answerChar)
    {
        Debug.Log("remove word");

        if (answerPanel.isAnswerPanelFull())
        {
            answerPanel.ResetWrongAnswer();
        }
        if (answerChar != null && !answerChar.isEmpty())
        {
            inputPanel.ResetInputChar(answerChar.inputCharacterId);
            answerChar.ResetCharacter();
            AudioController.Instance.PlayInputRevertSound();

            if (isHelpActive && currentHelpStep == 2)
            {
                currentHelpStep = 3;
                helpPanel.GetComponent<HelpQuestion>().ShowHelp(currentHelpStep);
                inputPanel.HightlightForHelp();
            }
        }
    }

    // Checks if answer given by user is correct or not. Correct answer will load the level complete screen.
    public void VerifyAnswer()
    {
        Debug.Log("check level");
        string userAnswer = answerPanel.GetAnswerString();
        if (userAnswer.ToUpper() == thisLevel.answer.ToUpper())
        {
            if (UIController.Instance.currentLevel == 1 && isHelpLevel)
            {
                inputPanel.ResetHelp();
                isHelpLevel = false;
            }
           UIController.Instance.OnLevelCompleted();
        }
        else
        {
            answerPanel.OnWrongAnswerEntered();
        }
    }
}