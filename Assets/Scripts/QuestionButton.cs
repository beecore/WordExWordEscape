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
            UIController.Instance.gameScreen.Activate();
            UIController.Instance.homeScreen.Deactivate();

        }
    }
}
