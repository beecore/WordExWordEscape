using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Linq;

public class LevelGenerator : MonoBehaviour 
{
    public const string rootfolderName = "PicturePuzzle";

    static int totalEpisodes = 12;
	static List<LocalizedLevels> allLocalizedLevels = new List<LocalizedLevels>();

	[MenuItem("Hyperbyte/Level Generator/Generate All Levels", false, 3)]
	public static void GenerateAllLevels() 
	{
		PrepareLocaliztionContentFromCSVs();	

		string levelsPath = Application.dataPath + "/"+ rootfolderName +"/All-Levels/Episodes";
		LocalizationInfo localizationInfo = (LocalizationInfo) Resources.Load("Localization/LocalizationInfo") as LocalizationInfo;

		int episodeIndex = 1;
		foreach(string directory in Directory.GetDirectories(levelsPath)) 
		{
			int levelNumber = 1;
			LevelInfo asset = ScriptableObject.CreateInstance<LevelInfo>();
			asset.allLevels = new List<global::Level>();
			asset.episodeNumber = episodeIndex;
			asset.episodeName = "Episode " + episodeIndex;

			foreach(string levelPath in Directory.GetDirectories(directory)) 
			{
				// string levelName = Path.GetExtension(levelPath).Remove(0,1);
				Level level = new Level();
				
				foreach(Langauge lang in localizationInfo.LanguageList) {
					if(lang.isAvailable) {
						string localizedAnswer = GetLocalizedLevel(lang.LangaugeCode, episodeIndex, levelNumber);
						LocalizedAnswer answer = new LocalizedAnswer(lang.LangaugeCode);
						answer.answer = localizedAnswer;
						level.localizedAnswers.Add(answer);
					}
				}

				level.levelNumber = levelNumber.ToString();
				level.allPictures = new List<Sprite>();

				foreach(string imagePath in Directory.GetFiles(levelPath,"*.jpg")) {
					string relativePath = imagePath.Replace(Application.dataPath,"Assets");
					Sprite sp = (Sprite) AssetDatabase.LoadAssetAtPath(relativePath, typeof(Sprite));
					level.allPictures.Add(sp);

				}
				asset.allLevels.Add(level);
				levelNumber++;
			}

			AssetDatabase.CreateAsset(asset, "Assets/"+ rootfolderName + "/All-Levels/Resources/Levels/Episode-" +episodeIndex +".asset");
			AssetDatabase.SaveAssets();
			episodeIndex++;
		}

		totalEpisodes = (episodeIndex - 1);
	}

	static void PrepareLocaliztionContentFromCSVs() 
	{
		allLocalizedLevels = new List<LocalizedLevels>();
		string CSVDirectory = Application.dataPath + "/"+rootfolderName +"/All-Levels/CSV";


		foreach(string localizedCSV in Directory.GetFiles(CSVDirectory,"*.csv")) { 
			LocalizedLevels levels = GenerateLocalizeLevels(localizedCSV);
			if(levels != null) {
				allLocalizedLevels.Add(levels);
			}
		}
	}

	static LocalizedLevels GenerateLocalizeLevels(string localizedCSV) {
		StreamReader reader = new StreamReader(localizedCSV);
		
		string fileName = Path.GetFileNameWithoutExtension(localizedCSV);
		string languageCode = fileName.Split('-')[1];

		LocalizedLevels levels = new LocalizedLevels();
		levels.languageCode = languageCode;

		levels.allEpisodes = new List<Episode>();

		for(int i = 0; i < totalEpisodes; i++) {
			levels.allEpisodes.Add(new Episode());
		}
		
		while(!reader.EndOfStream) {
			string thisLine = reader.ReadLine();
			 LineInfo info = GetLineInfo(thisLine);
		
			if(info != null && info.levelNumber > 0) {
				
				int index = 0;

				foreach(string word in info.allWords) {
					levels.allEpisodes[index].allLevels.Add(word);
					index ++;	
				}
			}
		}

		return levels;
	}

	static string GetLocalizedLevel(string languageCode, int episodeIndex, int levelIndex) {
		return allLocalizedLevels.Find( o => o.languageCode == languageCode).allEpisodes[episodeIndex-1].allLevels[levelIndex-1];
	}

	static LineInfo GetLineInfo(string line) {
		char[] charSeparators = new char[] { ';' };
		List<string> allWords = line.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries).ToList();

		if(allWords.Count > 2) {
			int levelNumber = 0;
			int.TryParse(allWords[0], out levelNumber);

			if(levelNumber > 0) {
				LineInfo info = new LineInfo();
				info.levelNumber = levelNumber;
				allWords.RemoveAt(0);
				info.allWords = allWords;
				return info;
			}
		}
		
		return null;
	}

	public class LocalizedLevels {
		public string languageCode;
		public List<Episode> allEpisodes;
	}

	public class Episode {

		public Episode() {
			allLevels = new List<string>();
		}

		public int episodeNumber;
		public List<string> allLevels;
	}

	public class LineInfo {
		public int levelNumber = 0;
		public List<string> allWords = new List<string>();
	}
}
