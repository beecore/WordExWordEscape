using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionButton : MonoBehaviour
{
    public void QuestionButtonPressed()
    {
        if (InputManager.Instance.canInput())
        {
           
            UIController.Instance.typeGame = 0;
            if (UIController.Instance.answerPanel != null)
            {
                if (UIController.Instance.answerPanel.transform.childCount > 0)
                {
                    UIController.Instance.answerPanel.Clear();
                }

            }
            UIController.Instance.gameScreen.Activate();
            UIController.Instance.homeScreen.Deactivate();

        }
    }
}
