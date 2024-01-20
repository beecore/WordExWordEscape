using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class WelcomeScreen : EditorWindow 
{
	private const float width = 600;
	private const float height = 600;

	GUISkin guiSkin;
	private static Texture2D textureHeaderLogo;

	public static void OpenWelcomeWindow()
	{
		GetWindow<WelcomeScreen>(true);
	}

	public static void CloseWelcomeWindow()
	{
		GetWindow<WelcomeScreen>().Close();
	}

	public void OnGUI()
	{
		InitializeResources();
		WelcomeScreen window = (WelcomeScreen)EditorWindow.GetWindowWithRect(typeof(WelcomeScreen), new Rect(0, 0, 100, 150));
		window.minSize = new Vector2(1100,650);

		GUI.skin = guiSkin;
		GUI.Box(new Rect(0,0, window.position.size.x, window.position.size.y),"",guiSkin.GetStyle("BoxBG"));
		GUI.Box(new Rect(0,0, (window.position.size.x), 90),"",guiSkin.GetStyle("BoxHeader"));

		GUILayout.BeginArea (new Rect((window.position.size.x/2) - 100, 10 , 250, 90));
		if(GUILayout.Button("", guiSkin.GetStyle("TitleButton"), null)) {
			Application.OpenURL("https://assetstore.unity.com/publishers/32803");
		}
		GUILayout.EndArea ();

		GUILayout.BeginArea(new Rect(0, 100 , (window.position.size.x), window.position.size.y));
		GUILayout.Label("Welcome to Hyperbyte Studios.", guiSkin.GetStyle("TitleText"));
		
		GUILayout.Label(
			"Thank you purchasing this game template. " +
			"We hope you will like this asset and we will provide you best possible assistence " +
			"and support to make this worth for you.");

		GUILayout.Label(
			"Before getting started, you need to complete few simple setups.", guiSkin.GetStyle("TextNoteHeader"));
		GUILayout.BeginHorizontal();
		GUILayout.Label("1) Setup Unity IAP.", guiSkin.GetStyle("TextSubtitle"));
		GUILayout.Space(10);
		
		if(GUILayout.Button("See Documentation Here..", guiSkin.GetStyle("BtnDocument"))) {
			Application.OpenURL("https://drive.google.com/file/d/1tWQfdmYUvNj-N6uSc9I9jzwPRgqcLkqi/preview");
		}

		GUILayout.EndHorizontal();
		GUILayout.Label(
			"A) To setup Unity IAP, Please enable In-App Purchasing from Window ~> Services, " +
			"After Enabling please import Unity In-App Purchasing SDK by pressing import button.", guiSkin.GetStyle("TextInstruction"));
		GUILayout.Label(
			"B) After Importing Unity InApp-Purchasing SDK, please import our in-app support script from " +
		"Hyperbyte ~> Support Modules ~> In-App Purchasing ~> Setup. You're almost done with In-App Setup.", guiSkin.GetStyle("TextInstruction"));
		
		GUILayout.Label(
			"C) To customize inapp Skus and reward, please open IAPManager.cs script and you can customize it. " +
			"for more detailed instructions, please follow our integration documentation attached with package.", guiSkin.GetStyle("TextInstruction"));

		GUILayout.BeginHorizontal();
		GUILayout.Label("2) Setup ADs SDK (Unity Monetization SDK 3.0 OR Google AdMob).", guiSkin.GetStyle("TextSubtitle"));
		GUILayout.Space(10);
		
		if(GUILayout.Button("See Documentation Here..", guiSkin.GetStyle("BtnDocument"))) {
			Application.OpenURL("https://drive.google.com/file/d/1zROYrDGS6EPz-ixrHI45vxPc8xtIswv0/preview");
		}

		GUILayout.EndHorizontal();
		
		GUILayout.Label(
			"A) To setup Ad Network, Please download and import respective ad network", guiSkin.GetStyle("TextInstruction"));
		GUILayout.Label(
			"B) After Importing Ads SDK, please import our Ad Integration support script from " +
			"Hyperbyte ~> Support Modules ~> Ad Network. Then import respective Ad Network Support script.", guiSkin.GetStyle("TextInstruction"));
		
		GUILayout.Label(
			"C) To customize Ad support script and setup ad keys, please followe our integration documentation.(ReadMe.pdf).", guiSkin.GetStyle("TextInstruction"));

		GUILayout.Label("We are available 24X7 for all type of support related to this asset. Please get in touch for any query, suggestions, bug report or feature requirement at "+
		"<color=red>support@hyperbytestudios.com</color> and we'll to provide you best possible assistence.");

		GUILayout.Space(10);
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();

		if(GUILayout.Button("Great, Get Started Now!", guiSkin.GetStyle("BtnGetStarted"))) {
			EditorPrefs.SetInt("WelcomeScreenShown",1);
			Application.OpenURL("https://drive.google.com/file/d/1Ml6yWh8LdyBMJMTc0vT0XPVDJS_HA_oY/preview");
			WelcomeScreen.CloseWelcomeWindow();

		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.Space(10);
		GUILayout.Button("",guiSkin.GetStyle("LineHorizontal"));
		GUILayout.Space(10);
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();

		if(GUILayout.Button("", guiSkin.GetStyle("ButtonRate"))) {
			Application.OpenURL("https://assetstore.unity.com/preview/135941/410721");
		}

		if(GUILayout.Button("", guiSkin.GetStyle("ButtonMoreApps"))) {
			Application.OpenURL("https://assetstore.unity.com/publishers/32803");
		}

		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndArea ();
	}

	void InitializeResources() 
	{
		guiSkin = (GUISkin)Resources.Load("EditorGUI");
	}
}
