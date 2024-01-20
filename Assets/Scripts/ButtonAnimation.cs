/***************************************************************************************
 * THIS SCRIPT DEPENDS ON THE ANIMATOR. ADD THIS SCRIPT TO ANY BUTTON TO ANIMATE BUTTON
 * ON CLICK. 
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonAnimation : MonoBehaviour 
{
	[SerializeField] bool doAnimate = true;
	Button thisButton;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		thisButton = GetComponent<Button>();
		if(GetComponent<Animator>() == null) {
			doAnimate = false;
		}
	}

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		thisButton.onClick.AddListener(()=> {
			if(doAnimate) {
				thisButton.GetComponent<Animator>().SetTrigger("Press");
			}
			AudioController.Instance.PlayButtonClickSound();
		});
	}
}
