/***************************************************************************************
 * THIS SCRIPT IS PUZZLE-GRID GAMEOBJECT OF GAMEPLAY. IT WILL SET LEVEL PICURES TO THE
 * GAMEPLAY GRID OF PICTURES.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGrid : MonoBehaviour {

	[SerializeField] Image[] pictureFrames;
	
	public void PreparePuzzleGrid(List<Sprite> allPictures) {
		pictureFrames[0].sprite= allPictures[0];
		//pictureFrames[1].sprite= allPictures[1];
		//pictureFrames[2].sprite= allPictures[2];
		//pictureFrames[3].sprite= allPictures[3];

		transform.localScale = Vector3.one;
	}
    public void PrepareQUestionGrid(List<Sprite> allPictures)
    {
        pictureFrames[0].sprite = allPictures[0];
        pictureFrames[1].sprite = allPictures[1];
        transform.localScale = Vector3.one;
    }
}
