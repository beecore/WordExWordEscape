/***************************************************************************************
 * ZOOM-IN AND ZOOM-OUT THE PUZZLE PICTURES, THIS SCRIPT IS ATTACHED TO ALL PUZZLE 
 * PICTURES OF GAMEPLAY.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PictureZoom : MonoBehaviour {

	[SerializeField] Transform frameParent;

	bool isZoomed = false;
	void Start() {
		GetComponent<Button>().onClick.AddListener(OnZoomButtonPressed);
	}

	// Handles zoom button.
	void OnZoomButtonPressed() {
		if(InputManager.Instance.canInput()) {
			if(isZoomed) {
				ZoomOut();
			} else {
				ZoomIn();
			}
		}
	}

	// Zoom-In the picture.
	void ZoomIn() {
		frameParent.SetAsLastSibling();
		GetComponent<Animator>().SetTrigger("ZoomIn");
		isZoomed = true;
	}

	// Zoom-out the picture.
	void ZoomOut() {
		GetComponent<Animator>().SetTrigger("ZoomOut");
		isZoomed = false;
	}

	void ResetSiblingOrder() {
		frameParent.SetAsFirstSibling();
	}
}
