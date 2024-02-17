/***************************************************************************************
 * THIS CONTROLLER IS RESPONSIBLE FOR ALMOST ALL UI WORKFLOW AND GAME PROGRESS HANDLING.
 * ANY UI SCREEN/POPUP CAN BE ACTIVATED AND DEACTIVATED USING THIS CONTROLLER.
 ***************************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    private List<string> screenStack = new List<string>();

    [SerializeField]
    private Canvas UICanvas;

    public GameObject homeScreen;
    public GameObject episodeScreen;
    public GameObject levelScreen;
    public GameObject gameScreen;
    public GameObject shopScreen;
    public GameObject settingScreen;
    public GameObject levelCompleteScreen;
    public GameObject quitGameScreen;
    public GameObject selectLanguageScreen;
    public GameObject commonPopup;
    public GameObject purchaseSuccessPopup;
    public GameObject purchaseFailurePopup;
    public GameObject revealLetterPopup;
    public GameObject removeLettersPopup;
    public GameObject getMoreCoinsPopup;
    public GameObject questionScreen;
    public GameObject nextLevelPopup;
    public AnswerPanel answerPanel;

    public float screenAspect = 1.775F;

    public static event Action<float> OnScreenAsectDetected;

    [System.NonSerialized]
    public int currentProgressLevel = 0;

    [System.NonSerialized]
    public int currentProgressEpisode = 0;

    [System.NonSerialized]
    public int currentEpisode = 1;

    [System.NonSerialized]
    public int currentLevel = 1;

    public int levelsPerEpisode = 20;
    public int totalEpisodes = 12;

    [System.NonSerialized]
    public int currentPlayingLevel = 1;

    [System.NonSerialized]
    public int totalPlayedLevel = 1;

    [System.NonSerialized]
    public int currentProgressQuestionLevel = 0;

    [System.NonSerialized]
    public int currentProgressQuestionEpisode = 0;

    [System.NonSerialized]
    public int currentQuestionEpisode = 1;

    [System.NonSerialized]
    public int currentQuestionLevel = 1;

    public int levelsPerQuestionEpisode = 35;
    public int totalQuestionEpisodes = 1;

    [System.NonSerialized]
    public int currentPlayingQuestionLevel = 1;

    [System.NonSerialized]
    public int totalPlayedQuestionLevel = 1;

    private enum TYPEGAME
    {
        FOURIMAGE,
        QUESTION
    }

    [System.NonSerialized]
    public int typeGame = (int)TYPEGAME.QUESTION;

    private void Start()
    {
        LoadLevel(typeGame);
        Application.targetFrameRate = 60;
        homeScreen.Activate();
    }

    public void LoadLevel(int typeGame)
    {
        Debug.Log("load level");
        switch (typeGame)
        {
            case (int)TYPEGAME.FOURIMAGE:
                currentProgressEpisode = PlayerPrefs.GetInt("currentEpisode", 1);
                currentProgressLevel = PlayerPrefs.GetInt("currentLevel", 1);

                currentEpisode = currentProgressEpisode;
                currentLevel = currentProgressLevel;

                totalPlayedLevel = ((currentProgressEpisode - 1) * levelsPerEpisode) + currentProgressLevel;
                currentPlayingLevel = totalPlayedLevel;

                break;

            case (int)TYPEGAME.QUESTION:
                currentProgressQuestionEpisode = PlayerPrefs.GetInt("currentQuestionEpisode", 1);
                currentProgressQuestionLevel = PlayerPrefs.GetInt("currentQuestionLevel", 1);

                currentQuestionEpisode = currentProgressQuestionEpisode;
                currentQuestionLevel = currentProgressQuestionLevel;

                totalPlayedQuestionLevel = ((currentProgressQuestionEpisode - 1) * levelsPerQuestionEpisode) + currentProgressQuestionLevel;
                currentPlayingQuestionLevel = totalPlayedQuestionLevel;
                break;
        }
    }

    private void OnEnable()
    {
        screenAspect = (((float)Screen.height) / ((float)Screen.width));

        if (OnScreenAsectDetected != null)
        {
            OnScreenAsectDetected.Invoke(screenAspect);
        }

        LocalizationManager.OnLanguageChangedEvent += OnLanguageChanged;
    }

    private void OnDisable()
    {
        LocalizationManager.OnLanguageChangedEvent -= OnLanguageChanged;
    }

    private void OnLanguageChanged(string languageCode)
    {
        //      Debug.Log("OnLanguageChanged");
        //      currentProgressEpisode = PlayerPrefs.GetInt("currentEpisode_"+languageCode,1);
        //currentProgressLevel = PlayerPrefs.GetInt("currentLevel_"+languageCode,1);

        //currentEpisode = currentProgressEpisode;
        //currentLevel = currentProgressLevel;

        //      currentProgressQuestionEpisode = PlayerPrefs.GetInt("currentQuestionEpisode_" + languageCode, 1);
        //      currentProgressQuestionLevel = PlayerPrefs.GetInt("currentQuestionLevel_" + languageCode, 1);

        //      currentQuestionEpisode = currentProgressQuestionEpisode;
        //      currentQuestionLevel = currentProgressQuestionLevel;
    }

    // Handles the device back button, this will be used for android only.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (InputManager.Instance.canInput())
            {
                if (screenStack.Count > 0)
                {
                    ProcessBackButton(Peek());
                }
            }
        }
    }

    // Spawn and activate the given script name. this is not used in the game.
    public GameObject SpawnUIScreen(string name)
    {
        GameObject thisScreen = (GameObject)Instantiate(Resources.Load("Prefabs/UIScreens/" + name));
        thisScreen.name = name;
        thisScreen.transform.SetParent(UICanvas.transform);
        thisScreen.transform.localPosition = Vector3.zero;
        thisScreen.transform.localScale = Vector3.one;
        thisScreen.GetComponent<RectTransform>().sizeDelta = Vector3.zero;
        thisScreen.Activate();
        return thisScreen;
    }

    // Adds the latest activated gameobject to stack.
    public void Push(string screenName)
    {
        if (!screenStack.Contains(screenName))
        {
            screenStack.Add(screenName);
        }
    }

    // Returns the name of last activated gameobject from the stack.
    public string Peek()
    {
        if (screenStack.Count > 0)
        {
            return screenStack[screenStack.Count - 1];
        }
        return "";
    }

    // Removes the last gameobject name from the stack.
    public void Pop(string screenName)
    {
        if (screenStack.Contains(screenName))
        {
            screenStack.Remove(screenName);
        }
    }

    // On pressing back button of device, the last added popup/screen will get deactivated based on state of the game.
    private void ProcessBackButton(string currentScreen)
    {
        switch (currentScreen)
        {
            case "HomeScreen":
                quitGameScreen.Activate();
                break;

            case "GameScreen":
                gameScreen.GetComponent<GamePlay>().OnBackButtonPressed();
                break;

            case "Shop":
                shopScreen.Deactivate();
                break;

            case "Settings":
                settingScreen.Deactivate();
                break;

            case "QuitGame":
                quitGameScreen.Deactivate();
                break;

            case "SelectLanguage":
                selectLanguageScreen.Deactivate();
                break;

            case "CommonPopup":
                commonPopup.Deactivate();
                break;

            case "PurchaseSuccess":
                purchaseSuccessPopup.Deactivate();
                break;

            case "PurchaseFailure":
                purchaseFailurePopup.Deactivate();
                break;

            case "RevealCharacterPopup":
                revealLetterPopup.Deactivate();
                break;

            case "RemoveLettersPopup":
                removeLettersPopup.Deactivate();
                break;

            case "GetMoreCoins":
                getMoreCoinsPopup.Deactivate();
                break;

            case "EpisodeScreen":
                episodeScreen.Deactivate();
                break;

            case "LevelScreen":
                levelScreen.Deactivate();
                break;

            case "QuestionScreen":
                questionScreen.Deactivate();
                break;
        }
    }

    // Quits the game.
    public void QuitGame()
    {
        Invoke("QuitGameAfterDelay", 0.5F);
    }

    private void QuitGameAfterDelay()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
			AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call<bool>("moveTaskToBack" , true);
#elif UNITY_IOS
			Application.Quit();
#endif
    }

    // Handles the level progress and completion state.
    public void OnLevelCompleted()
    {
        Debug.Log("OnLevelCompleted");
        switch (typeGame)
        {
            case 0:
                if (currentLevel < levelsPerEpisode)
                {
                    currentLevel++;
                }
                else
                {
                    if (currentEpisode < totalEpisodes)
                    {
                        currentLevel = 1;
                        currentEpisode++;
                    }
                    else
                    {
                        currentLevel = 1;
                        currentEpisode = 1;
                    }
                }
                totalPlayedLevel = ((currentProgressEpisode - 1) * levelsPerEpisode) + currentProgressLevel;
                currentPlayingLevel = ((currentEpisode - 1) * levelsPerEpisode) + currentLevel;

                if (currentPlayingLevel > totalPlayedLevel)
                {
                    currentProgressEpisode = currentEpisode;
                    currentProgressLevel = currentLevel;
                }

                PlayerPrefs.SetInt("currentEpisode", currentProgressEpisode);
                PlayerPrefs.SetInt("currentLevel", currentProgressLevel);

                levelCompleteScreen.Activate();
                AudioController.Instance.PlayLevelUpSound();
                break;

            case 1:
                if (currentQuestionLevel < levelsPerQuestionEpisode)
                {
                    currentQuestionLevel++;
                }
                else
                {
                    if (currentQuestionEpisode < totalQuestionEpisodes)
                    {
                        currentQuestionLevel = 1;
                        currentQuestionEpisode++;
                    }
                    else
                    {
                        currentQuestionLevel = 1;
                        currentQuestionEpisode = 1;
                    }
                }
                totalPlayedQuestionLevel = ((currentProgressQuestionEpisode - 1) * levelsPerQuestionEpisode) + currentProgressQuestionLevel;
                currentPlayingQuestionLevel = ((currentQuestionEpisode - 1) * levelsPerQuestionEpisode) + currentQuestionLevel;

                if (currentPlayingQuestionLevel > totalPlayedQuestionLevel)
                {
                    currentProgressQuestionEpisode = currentQuestionEpisode;
                    currentProgressQuestionLevel = currentQuestionLevel;
                }

                PlayerPrefs.SetInt("currentQuestionEpisode", currentProgressQuestionEpisode);
                PlayerPrefs.SetInt("currentQuestionLevel", currentProgressQuestionLevel);

                levelCompleteScreen.Activate();
                AudioController.Instance.PlayLevelUpSound();
                break;
        }
    }

    // Show common pop-up.
    public void ShowMessage(string title, string message)
    {
        commonPopup.Activate();
        commonPopup.GetComponent<CommonPopup>().SetText(title, message);
    }

    // Load specific selected level.
    public void LoadSelectedLevel(int episodeNumber, int levelNumber)
    {
        currentEpisode = episodeNumber;
        currentLevel = levelNumber;
        currentPlayingLevel = ((currentEpisode - 1) * levelsPerEpisode) + currentLevel;

        GamePlay.Instance.LoadLevel();
    }

    // Unload unused asset. please call this on safe place as it might give a slight lag.
    public void ClearCache()
    {
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
}