using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : Singleton<BGMusic>
{
	[SerializeField] AudioSource bgAudio;
	bool isInitialized = false;

	void Start() {
		if(!isInitialized) {
			InitBgMusic();
		}
	}

	void InitBgMusic() {
		bool isMusicEnabled = (PlayerPrefs.GetInt ("isMusicEnabled", 0) == 0) ? true : false;
		if(isMusicEnabled) {
			bgAudio.Play();
		}
	}

	public void OnEnable() {
		ProfileManager.OnMusicStatusChangedEvent += OnMusicStatusChanged;
	}

	public void OnDisable() {
		ProfileManager.OnMusicStatusChangedEvent -= OnMusicStatusChanged;
	}

	void OnMusicStatusChanged(bool status) {
		if(!isInitialized) {
			InitBgMusic();
		}

		if(status) {
			bgAudio.Play();
		} else {
			bgAudio.Stop();
		}
	}


	public void PauseBgMusic() {
		if(bgAudio.isPlaying) {
			bgAudio.Pause();
		}
	}

	public void ResumeBgMusic() {
		if(!bgAudio.isPlaying && ProfileManager.Instance.isMusicEnabled) {
			bgAudio.Play();
		}
	}
}
