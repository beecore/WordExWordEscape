using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("quang cao OnEnable");
        AdManager.OnRewardedFinishedEvent += OnRewardedFinished;
    }

    private void OnDisable()
    {
        Debug.Log("quang cao OnDisable");
        AdManager.OnRewardedFinishedEvent -= OnRewardedFinished;
    }
    public void OnWatchVideoButtonPressed()
    {
        if (InputManager.Instance.canInput())
        {
            AdManager.Instance.ShowRewarded();
        }
    }
    private void OnRewardedFinished(bool isCompleted)
    {
        Debug.Log("quang cao OnRewardedFinished");
        if (InputManager.Instance.canInput())
        {
            Debug.Log("game " + UIController.Instance.typeGame);
            if (UIController.Instance.typeGame == 0)
            {
                UIController.Instance.gameScreen.GetComponent<GamePlay>().LoadLevel();
            }
            else if (UIController.Instance.typeGame == 1)
            {

                UIController.Instance.questionScreen.GetComponent<QuestionGamePlay>().LoadLevel();
            }

            gameObject.Deactivate();
        }
    }
}
