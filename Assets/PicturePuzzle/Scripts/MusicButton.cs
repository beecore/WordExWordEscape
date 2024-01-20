/***************************************************************************************
 * THIS SCRIPT IS ADDED TO MUSIC TOGGLE BUTTON.
 ***************************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour 
{
	// The button to toggle music, assigned from inspector.
	public Button btnMusic;
	// The image of the button.
	public Image btnMusicImage;
	// The On sprite for music.
	public Sprite musicOnSprite;
	// The off sprite for music.
	public Sprite musicOffSprite;

	public Text txtOnText;
	public Text txtOffText;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		btnMusic.onClick.AddListener(() => {
			if (InputManager.Instance.canInput ()) {
				ProfileManager.Instance.ToggleMusicStatus	();
			}
		});
	}

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		ProfileManager.OnMusicStatusChangedEvent += OnMusicStatusChanged;
		initMusicStatus ();
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable()
	{
		ProfileManager.OnMusicStatusChangedEvent -= OnMusicStatusChanged;
	}

	/// <summary>
	/// Inits the music status.
	/// </summary>
	void initMusicStatus()
	{
		if(ProfileManager.Instance.isMusicEnabled)
		{
			btnMusicImage.sprite = musicOnSprite;
			txtOffText.gameObject.SetActive(false);
			txtOnText.gameObject.SetActive(true);
		}
		else
		{
			btnMusicImage.sprite = musicOffSprite;
			txtOnText.gameObject.SetActive(false);
			txtOffText.gameObject.SetActive(true);
		}
	}

	/// <summary>
	/// Raises the music status changed event.
	/// </summary>
	/// <param name="isMusicEnabled">If set to <c>true</c> is music enabled.</param>
	void OnMusicStatusChanged (bool isMusicEnabled)
	{
		if(isMusicEnabled)
		{
			btnMusicImage.sprite = musicOnSprite;
			txtOffText.gameObject.SetActive(false);
			txtOnText.gameObject.SetActive(true);
		}
		else
		{
			btnMusicImage.sprite = musicOffSprite;
			txtOnText.gameObject.SetActive(false);
			txtOffText.gameObject.SetActive(true);
		}
	}	
}
