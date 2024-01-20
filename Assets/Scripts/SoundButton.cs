/***************************************************************************************
 * THIS SCRIPT IS ADDED TO SOUND TOGGLE BUTTON.
 ***************************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour 
{
	// The button to turn on/off sound.
	public Button btnSound;	
	// Image of the button on which sound sprite will get assigned. Default on
	public Image btnSoundImage; 
	// Sound on sprite.
	public Sprite soundOnSprite;
	// Sounf off sprite.
	public Sprite soundOffSprite;

	public Text txtOnText;
	public Text txtOffText;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		btnSound.onClick.AddListener(() => 
		{
			if (InputManager.Instance.canInput ()) {
				ProfileManager.Instance.ToggleSoundStatus();
			}
		});
	}

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		ProfileManager.OnSoundStatusChangedEvent += OnSoundStatusChanged;
		initSoundStatus ();
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable()
	{
		ProfileManager.OnSoundStatusChangedEvent -= OnSoundStatusChanged;
	}

	/// <summary>
	/// Inits the sound status.
	/// </summary>
	void initSoundStatus()
	{
		if(ProfileManager.Instance.isSoundEnabled)
		{
			btnSoundImage.sprite = soundOnSprite;
			txtOffText.gameObject.SetActive(false);
			txtOnText.gameObject.SetActive(true);
		}
		else
		{
			btnSoundImage.sprite = soundOffSprite;
			txtOnText.gameObject.SetActive(false);
			txtOffText.gameObject.SetActive(true);
		}
	}

	/// <summary>
	/// Raises the sound status changed event.
	/// </summary>
	/// <param name="isSoundEnabled">If set to <c>true</c> is sound enabled.</param>
	void OnSoundStatusChanged (bool isSoundEnabled)
	{
		if(isSoundEnabled)
		{
			btnSoundImage.sprite = soundOnSprite;
			txtOffText.gameObject.SetActive(false);
			txtOnText.gameObject.SetActive(true);
		}
		else
		{
			btnSoundImage.sprite = soundOffSprite;
			txtOnText.gameObject.SetActive(false);
			txtOffText.gameObject.SetActive(true);
		}
	}	
}
