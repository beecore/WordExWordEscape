/***************************************************************************************
 * THIS SCRIPT IS ADDED TO NOTIFICATION TOGGLE BUTTON.
 ***************************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotificationButton : MonoBehaviour 
{
	// The button to turn on/off notification.
	public Button btnNotification;	
	// Image of the button on which notification sprite will get assigned. Default on
	public Image btnNotificationImage; 
	// Notification on sprite.
	public Sprite notificationOnSprite;
	// Notification off sprite.
	public Sprite notificationOffSprite;

	public Text txtOnText;
	public Text txtOffText;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		btnNotification.onClick.AddListener(() => 
		{
			if (InputManager.Instance.canInput ()) {
				ProfileManager.Instance.ToggleNotificationStatus();
			}
		});
	}

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		ProfileManager.OnNotificationStatusChangedEvent += OnNotificationStatusChanged;
		initNotificationStatus ();
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable()
	{
		ProfileManager.OnNotificationStatusChangedEvent -= OnNotificationStatusChanged;
	}

	/// <summary>
	/// Inits the notification status.
	/// </summary>
	void initNotificationStatus()
	{
		if(ProfileManager.Instance.isNotificationEnabled)
		{
			btnNotificationImage.sprite = notificationOnSprite;
			txtOffText.gameObject.SetActive(false);
			txtOnText.gameObject.SetActive(true);
		}
		else
		{
			btnNotificationImage.sprite = notificationOffSprite;
			txtOnText.gameObject.SetActive(false);
			txtOffText.gameObject.SetActive(true);
		}
	}

	/// <summary>
	/// Raises the notification status changed event.
	/// </summary>
	/// <param name="isNotificationEnabled">If set to <c>true</c> is notification enabled.</param>
	void OnNotificationStatusChanged (bool isNotificationEnabled)
	{
		if(isNotificationEnabled)
		{
			btnNotificationImage.sprite = notificationOnSprite;
			txtOffText.gameObject.SetActive(false);
			txtOnText.gameObject.SetActive(true);
		}
		else
		{
			btnNotificationImage.sprite = notificationOffSprite;
			txtOnText.gameObject.SetActive(false);
			txtOffText.gameObject.SetActive(true);
		}
	}	
}
