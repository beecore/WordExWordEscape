using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelQuestionReader : MonoBehaviour
{
    [SerializeField]
    PuzzleGrid puzzleGrid;
    LevelInfoQuestion currentEpisodeInfo = null;
    void LoadLevelInfoData()
    {
        if (currentEpisodeInfo == null)
        {
            currentEpisodeInfo = (LevelInfoQuestion)Resources.Load("Questions/Episode-" + UIController.Instance.currentQuestionEpisode) as LevelInfoQuestion;

        }
    }

    public LevelQuestion LoadLevel()
    {
        return ReadLevel();
    }

    public IEnumerator UnloadLevel()
    {
        yield return new WaitForSeconds(0.05F);
    }

    public LevelQuestion ReadLevel()
    {
        LoadLevelInfoData();
        LevelQuestion thisLevel = currentEpisodeInfo.allLevels.Find(o => o.levelNumber == UIController.Instance.currentQuestionLevel.ToString());
        if (thisLevel != null)
        {

            puzzleGrid.PrepareQUestionGrid(thisLevel.allPictures);
            QuestionGamePlay.Instance.inputPanel.PrepareInputPanel(thisLevel.answer.Trim().ToUpper());
            if (!QuestionGamePlay.Instance.answerPanel.gameObject.activeSelf)
            {
                QuestionGamePlay.Instance.answerPanel.gameObject.SetActive(true);
            }
            QuestionGamePlay.Instance.answerPanel.PrepareAnswerPanel(thisLevel.answer.Length);
        }
        return thisLevel;
    }
}
