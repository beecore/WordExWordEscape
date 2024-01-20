/***************************************************************************************
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using System.Linq;

public class LevelReader : Singleton<LevelReader> 
{
	[SerializeField] PuzzleGrid puzzleGrid;
	LevelInfo currentEpisodeInfo = null;

	void LoadLevelInfoData() 
	{
		if(currentEpisodeInfo == null || currentEpisodeInfo.episodeNumber != UIController.Instance.currentEpisode) {
			currentEpisodeInfo = (LevelInfo) Resources.Load("Levels/Episode-"+UIController.Instance.currentEpisode) as LevelInfo;
		}
	}
	
	public Level LoadLevel() 
	{
		return ReadLevel();
	}

	public IEnumerator UnloadLevel() {
		yield return new WaitForSeconds(0.05F);
	}

	public Level ReadLevel() 
	{
		LoadLevelInfoData();
		Level thisLevel = currentEpisodeInfo.allLevels.Find( o => o.levelNumber == UIController.Instance.currentLevel.ToString());
		
		if(thisLevel != null) 
		{
			LocalizedAnswer localizedAnswer = thisLevel.localizedAnswers.Find( o => o.languageCode == LocalizationManager.Instance.getCurrentLanguageCode());
			thisLevel.answer = localizedAnswer.answer;

			puzzleGrid.PreparePuzzleGrid(thisLevel.allPictures);
			GamePlay.Instance.inputPanel.PrepareInputPanel(thisLevel.answer.ToUpper());
			if(!GamePlay.Instance.answerPanel.gameObject.activeSelf) {
				GamePlay.Instance.answerPanel.gameObject.SetActive(true);
			}
			GamePlay.Instance.answerPanel.PrepareAnswerPanel(thisLevel.answer.Length);
		}
		return thisLevel;
	}
}
