using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SessionManager : MonoBehaviour 
{
	public static event Action<SessionInfo> OnSessionUpdatedEvent;
	bool isFreshLauched = true;

	TimeSpan backgroundThreshHoldTimeSpan = new TimeSpan(0,0,10);

	int sessionCount = 0;
	int sessionOfDay = 0;
	int daysSinceInstalled = 0;
	TimeSpan durationSinceLastAccessed;

	void Start() 
	{	
		if(!PlayerPrefs.HasKey("firstOpenDate")) {
			PlayerPrefs.SetString("firstOpenDate", DateTime.Now.ToBinary().ToString());
		}

		Invoke("CheckForSessionStatus",1F);
	}

	void OnApplicationPause(bool pauseStatus)
	{
		if(pauseStatus) {
			isFreshLauched = false;
			PlayerPrefs.SetString("lastPauseTime",DateTime.Now.ToBinary().ToString());
			PlayerPrefs.SetString("lastAccessedDate",DateTime.Now.ToBinary().ToString());
		} else 
		{
			if(!isFreshLauched) {
				
				bool doCheckForSessionChange = true;

				if(PlayerPrefs.HasKey("lastPauseTime")) {

					DateTime lastOpenedTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("lastPauseTime")));
					TimeSpan pauseDuration = (DateTime.Now - lastOpenedTime);
					
					if(pauseDuration.TotalSeconds < backgroundThreshHoldTimeSpan.TotalSeconds) {
						doCheckForSessionChange = false;
					}
				}

				if(doCheckForSessionChange) {
					Invoke("CheckForSessionStatus",1F);
				}
			}
		}
	}


	void CheckForSessionStatus() 
	{
		//Calculate Session Count.
		sessionCount = 1;
		
		if(PlayerPrefs.HasKey("sessionCount")) {
			sessionCount = PlayerPrefs.GetInt("sessionCount",1);
		}
		sessionCount ++;
		PlayerPrefs.SetInt("sessionCount", sessionCount);
		PlayerPrefs.DeleteKey("lastPauseTime");

		//SessionOfTheDay
		sessionOfDay = PlayerPrefs.GetInt("sessionOfDay_"+DateTime.Now.Year + "_" + DateTime.Now.DayOfYear, 0);
		sessionOfDay += 1;
		PlayerPrefs.SetInt("sessionOfDay_"+DateTime.Now.Year + "_" + DateTime.Now.DayOfYear, sessionOfDay);

		//Days Since Last Accessed
		DateTime lastAccessedDate = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("lastAccessedDate",DateTime.Now.ToBinary().ToString())));
		durationSinceLastAccessed = (DateTime.Now - lastAccessedDate);

		//Days Since Installed

		SessionInfo info = new SessionInfo(sessionCount, sessionOfDay, durationSinceLastAccessed);

		if(OnSessionUpdatedEvent != null) {
			OnSessionUpdatedEvent.Invoke(info);
		}
	}

	void OnApplicationQuit() {
		PlayerPrefs.SetString("lastAccessedDate",DateTime.Now.ToBinary().ToString());
	}
}

public class SessionInfo {
	public int sessionCount = 0;
	public int sessionOfDay = 0;
	public TimeSpan durationSinceLastAccessed;

	public SessionInfo(int _sessionCount, int _sessionOfDay, TimeSpan _durationSinceLastAccessed) {
		this.sessionCount = _sessionCount;
		this.sessionOfDay = _sessionOfDay;
		this.durationSinceLastAccessed = _durationSinceLastAccessed;
	}
}
