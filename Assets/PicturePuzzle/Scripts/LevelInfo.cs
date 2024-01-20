/***************************************************************************************
 * THIS SCRIPT IS A SCRIPTABLE OBJECT FOR CREATING EPISODE CONTENT.
 ***************************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelInfo : ScriptableObject
{
    public int episodeNumber;
    public string episodeName;
    public List<Level> allLevels;
}

[System.Serializable]
public class Level {
    [HideInInspector] public string levelNumber;
    public List<Sprite> allPictures = new List<Sprite>() {null, null, null, null};
    [HideInInspector] public string answer;
    public List<LocalizedAnswer> localizedAnswers = new List<LocalizedAnswer>();
}

[System.Serializable]
public class LocalizedAnswer {
    [HideInInspector] public string languageCode;
    public string answer;

    public LocalizedAnswer(string _languageCode) {
        this.languageCode = _languageCode;
    }
}