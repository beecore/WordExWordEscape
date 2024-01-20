using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelInfoQuestion", menuName = "ScriptableObject/LevelInfoQuestion")]
public class LevelInfoQuestion : ScriptableObject
{
    public int episodeNumber;
    public string episodeName;
    public List<LevelQuestion> allLevels;
}
[System.Serializable]
public class LevelQuestion
{
    public string levelNumber;
    public string question;
    public string answer;
    public List<Sprite> allPictures = new List<Sprite>() { null, null, null, null };
}
