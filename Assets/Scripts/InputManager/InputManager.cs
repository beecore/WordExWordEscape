/***************************************************************************************
 * THIS SCRIPTS HANDLES INPUT, TOUCH HOLDING, LOCK INPUT ETC.
 ***************************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;

/// <summary>
/// Input manager.
/// </summary>
public class InputManager : Singleton<InputManager>
{
	static bool isTouchAvailable = true;
	public EventSystem eventSystem;

	/// <summary>
	/// Cans the input.
	/// </summary>
	/// <returns><c>true</c>, if input was caned, <c>false</c> otherwise.</returns>
	/// <param name="delay">Delay.</param>
	/// <param name="disableOnAvailable">If set to <c>true</c> disable on available.</param>
	public bool canInput (float delay = 0.25F, bool disableOnAvailable = true)
	{
		bool status = isTouchAvailable;
		if (status && disableOnAvailable) {
			isTouchAvailable = false;
			eventSystem.enabled = false;

			StopCoroutine ("EnableTouchAfterDelay");
			StartCoroutine ("EnableTouchAfterDelay", delay);

		}
		return status;
	}

	public void DisableTouch()
	{
		isTouchAvailable = false;
		eventSystem.enabled = false;
	}

	/// <summary>
	/// Disables the touch for delay.
	/// </summary>
	/// <param name="delay">Delay.</param>
	public void DisableTouchForDelay (float delay = 0.25F)
	{
		isTouchAvailable = false;
		eventSystem.enabled = false;

		StopCoroutine ("EnableTouchAfterDelay");
		StartCoroutine ("EnableTouchAfterDelay", delay);
	}

	/// <summary>
	/// Enables the touch.
	/// </summary>
	public void EnableTouch ()
	{
		isTouchAvailable = true;
		eventSystem.enabled = true;
	}

	/// <summary>
	/// Enables the touch after delay.
	/// </summary>
	/// <returns>The touch after delay.</returns>
	/// <param name="delay">Delay.</param>
	public IEnumerator EnableTouchAfterDelay (float delay)
	{
		yield return new WaitForSeconds (delay);
		EnableTouch ();
	}
}
