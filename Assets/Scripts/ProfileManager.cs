/***************************************************************************************
 * THIS CONTROLLER HANDLES THE STATUS OF USER SETTING LIKE MUSIC, SOUND AND NOTIFICATION 
 * PREFERENCE. MOSTLY YOU WON'T NEED TO TOUCH THIS CONTROLLER UNLESS YOU WANT TO ADD ANY 
 * MORE USER CHOICE AND SAVE IT. 
 ***************************************************************************************/

using UnityEngine;
using System.Collections;
using System;

public class ProfileManager : Singleton<ProfileManager>
{
	public static event Action<bool> OnSoundStatusChangedEvent;
	public static event Action<bool> OnMusicStatusChangedEvent;
	public static event Action<bool> OnNotificationStatusChangedEvent;

	[HideInInspector] public bool isSoundEnabled = true;
	[HideInInspector] public bool isMusicEnabled = true;
	[HideInInspector] public bool isNotificationEnabled = true;

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		initProfileStatus ();
	}

	/// <summary>
	/// Inits the audio status.
	/// </summary>
	public void initProfileStatus ()
	{
		isSoundEnabled = (PlayerPrefs.GetInt ("isSoundEnabled", 0) == 0) ? true : false;
		isMusicEnabled = (PlayerPrefs.GetInt ("isMusicEnabled", 0) == 0) ? true : false;
		isNotificationEnabled = (PlayerPrefs.GetInt ("isNotificationEnabled", 0) == 0) ? true : false;

		if ((!isSoundEnabled) && (OnSoundStatusChangedEvent != null)) {
			OnSoundStatusChangedEvent.Invoke (isSoundEnabled);
		}
		if ((!isMusicEnabled) && (OnMusicStatusChangedEvent != null)) {
			OnMusicStatusChangedEvent.Invoke (isMusicEnabled);
		}
		if((!isNotificationEnabled) && (OnNotificationStatusChangedEvent != null)) {
			OnNotificationStatusChangedEvent.Invoke(isNotificationEnabled);
		}
	}

	/// <summary>
	/// Toggles the sound status.
	/// </summary>
	public void ToggleSoundStatus ()
	{
		isSoundEnabled = (isSoundEnabled) ? false : true;
		PlayerPrefs.SetInt ("isSoundEnabled", (isSoundEnabled) ? 0 : 1);

		if (OnSoundStatusChangedEvent != null) {
			OnSoundStatusChangedEvent.Invoke (isSoundEnabled);
		}
	}

	/// <summary>
	/// Toggles the music status.
	/// </summary>
	public void ToggleMusicStatus ()
	{
		isMusicEnabled = (isMusicEnabled) ? false : true;
		PlayerPrefs.SetInt ("isMusicEnabled", (isMusicEnabled) ? 0 : 1);

		if (OnMusicStatusChangedEvent != null) {
			OnMusicStatusChangedEvent.Invoke (isMusicEnabled);
		}
	}

	/// <summary>
	/// Toggles the notification status.
	/// </summary>
	public void ToggleNotificationStatus ()
	{
		isNotificationEnabled = (isNotificationEnabled) ? false : true;
		PlayerPrefs.SetInt ("isNotificationEnabled", (isNotificationEnabled) ? 0 : 1);

		if (OnNotificationStatusChangedEvent != null) {
			OnNotificationStatusChangedEvent.Invoke (isNotificationEnabled);
		}
	}
}
