using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class EditorMenu {

	public const string rootfolderName = "PicturePuzzle";
	#region Welcome Screen
	[MenuItem("Hyperbyte/Welcome Screen",false, 0)]
	public static void OpenWelcomeScreen()
	{
		WelcomeScreen.OpenWelcomeWindow();
	}
	#endregion

	#region Hyperbyte Documentation
	[MenuItem("Hyperbyte/Documentation/General", false, 1)]
	public static void OpenGeneralDocument() {
		Application.OpenURL("https://drive.google.com/file/d/1Ml6yWh8LdyBMJMTc0vT0XPVDJS_HA_oY/preview");
	}

	[MenuItem("Hyperbyte/Documentation/SetUp IAP", false, 1)]
	public static void OpenIAPDocumentation() {
		Application.OpenURL("https://drive.google.com/file/d/1tWQfdmYUvNj-N6uSc9I9jzwPRgqcLkqi/preview");
	}

	[MenuItem("Hyperbyte/Documentation/SetUp Ad Network", false, 1)]
	public static void OpenAdSetupDocumentation() {
		Application.OpenURL("https://drive.google.com/file/d/1zROYrDGS6EPz-ixrHI45vxPc8xtIswv0/preview");
	}

    [MenuItem("Hyperbyte/Documentation/How To Add Language?", false, 1)]
    public static void OpenLanguageSetupDocumentation()
    {
        Application.OpenURL("https://drive.google.com/file/d/1LOlxVjYHwRznlqBeHGO9qVvBxcZwPawC/preview");
    }
    #endregion

    #region UnityIAP Setup
    [MenuItem("Hyperbyte/Support-Modules/In-App Purchasing/Setup", false, 2)]
	private static void SetupInAppSupportModule() {
		SetupUnityIAP();
	}

	public static void SetupUnityIAP() {
		if(Directory.Exists ("Assets/Plugins/UnityPurchasing")) {
			ImportUnityIAPSupportPackage();
		} else {
			EditorUtility.DisplayDialog("Alert!", "Seems like Unity IAP SDK is not imported yet, Please enable Unity IAP and Import SDK first. " + 
			"\n\nIf you want a hasslefree ready package with full setup done, please let us know via support email."
			, "OK");
		}
	}

	public static void ImportUnityIAPSupportPackage() {
		AssetDatabase.ImportPackage(Application.dataPath + "/" +rootfolderName +"/Support-Classes/UnityPurchasing/UnityIAPManager.unitypackage",true);
	}

	[MenuItem("Hyperbyte/Support-Modules/In-App Purchasing/Official Document", false, 2)]
	private static void OpenUnityIAPOfficialDocument() {
		Application.OpenURL("https://docs.unity3d.com/Manual/UnityIAPSettingUp.html");
	}
	#endregion

	#region Unity Monetization SDK 3.0 Setup
	[MenuItem("Hyperbyte/Support-Modules/Ad-Network/Unity Monetization/Download SDK", false, 2)]
	private static void DownloadUnityMonetizationSDK() {
		Application.OpenURL("https://assetstore.unity.com/packages/add-ons/services/unity-monetization-3-0-66123");
	}

	[MenuItem("Hyperbyte/Support-Modules/Ad-Network/Unity Monetization/Setup", false, 2)]
	private static void SetupUnityMonetizationSupportModule() {
		SetupUnityMonetizationSDK();
	}

	[MenuItem("Hyperbyte/Support-Modules/Ad-Network/Unity Monetization/Official Document", false, 2)]
	private static void OpenUnityMonetizationOfficialDocument() {
		Application.OpenURL("https://unityads.unity3d.com/help/unity/integration-guide-unity");
	}

	public static void SetupUnityMonetizationSDK() {
		if(Directory.Exists ("Assets/UnityAds")) {
			ImportUnityMonetizationSupportPackage();
		} else {
			EditorUtility.DisplayDialog("Alert!", "Seems like Unity Monetization SDK is not imported yet, Please Download and Import SDK first. " + 
			"\n\nIf you want a hasslefree ready package with full setup done, please let us know via support email."
			, "OK");
		}
	}

	public static void ImportUnityMonetizationSupportPackage() {
		AssetDatabase.ImportPackage(Application.dataPath + "/" +rootfolderName +"/Support-Classes/Monetization/AdSupport-UNITY-UM-3.0.unitypackage",false);
	}
	#endregion

	#region Unity Google AdMob Setup
	[MenuItem("Hyperbyte/Support-Modules/Ad-Network/Google AdMob/Download SDK", false, 2)]
	private static void DownloadGoogleAdMobSDK() {
		Application.OpenURL("https://developers.google.com/admob/unity/start");
	}

	[MenuItem("Hyperbyte/Support-Modules/Ad-Network/Google AdMob/Setup", false, 2)]
	private static void SetupGoogleAdMobSupportModule() {
		SetupGoogleAdMobSupportSDK();
	}

	[MenuItem("Hyperbyte/Support-Modules/Ad-Network/Google AdMob/Official Document", false, 2)]
	private static void OpenAdMobOfficialDocument() {
		Application.OpenURL("https://developers.google.com/admob/unity/start");
	}

	[MenuItem("Hyperbyte/Support-Modules/Ad-Network/Google AdMob/Mediation Integration Guide", false, 2)]
	private static void OpenMediationIntegrationGuide() {
		Application.OpenURL("https://developers.google.com/admob/unity/mediation");
	}

	public static void SetupGoogleAdMobSupportSDK() {
		if(Directory.Exists ("Assets/GoogleMobileAds")) {
			ImportGoogleAdMobSupportPackage();
		} else {
			EditorUtility.DisplayDialog("Alert!", "Seems like Google AdMob SDK is not imported yet, Please Download and Import SDK first. " + 
			"\n\nIf you want a hasslefree ready package with full setup done, please let us know via support email."
			, "OK");
		}
	}

	public static void ImportGoogleAdMobSupportPackage() {
		AssetDatabase.ImportPackage(Application.dataPath + "/" +rootfolderName +"/Support-Classes/Monetization/AdSupport-AdMob-v3.15.1.unitypackage",false);
	}
	#endregion

	#region  Generate Localization Info File
	[MenuItem("Hyperbyte/Localization/Generate Localization Info File", false, 3)]
	
	public static void GenerateLocalizationInfoFile() {
		bool doGenerateLocalizationInfo = false;
		LocalizationInfo localizationInfo = (LocalizationInfo) Resources.Load("Localization/LocalizationInfo") as LocalizationInfo;
		if(localizationInfo != null) {
			doGenerateLocalizationInfo = EditorUtility.DisplayDialog("Alert!", "Localization Info Already Exists, Regenerating it will overright existing file.","Generate","Cancel");
		} else {
			doGenerateLocalizationInfo = true;
		}
		if(doGenerateLocalizationInfo) {
			LocalizationInfo asset = ScriptableObject.CreateInstance<LocalizationInfo>();
			AssetDatabase.CreateAsset(asset, "Assets/" + rootfolderName + "/Resources/Localization/LocalizationInfo.asset");
		}
	}
	#endregion

	#region  Delete Preferences
	[MenuItem("Hyperbyte/Delete Preferences", false, 4)]
	public static void DeleteAllPreferences() {
		PlayerPrefs.DeleteAll();
		EditorPrefs.DeleteKey("WelcomeScreenShown");
	}
	#endregion

	#region CaptureScreenshot
	[MenuItem("Hyperbyte/Capture Screenshot/1X")]
	private static void Capture1XScreenshot() {
		CaptureScreenshot(1);
	}

	[MenuItem("Hyperbyte/Capture Screenshot/2X")]
	private static void Capture2XScreenshot() {
		CaptureScreenshot(2);
	}

	[MenuItem("Hyperbyte/Capture Screenshot/3X")]
	private static void Capture3XScreenshot() {
		CaptureScreenshot(3);
	}

	public static void CaptureScreenshot(int supersize) {
		string imgName = "IMG-"+ DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") +".png";
		ScreenCapture.CaptureScreenshot ((Application.dataPath + "/" + imgName),supersize);
		AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
	}
	#endregion

	[MenuItem("Hyperbyte/Misc./Break Prefab Instance")]
	private static void BreakPrefabInstance() {
		List<GameObject> news = new List<GameObject> ();
		while (Selection.gameObjects.Length > 0) {
			GameObject old = Selection.gameObjects [0];

			string name = old.name;
			int index = old.transform.GetSiblingIndex ();

			GameObject _new = MonoBehaviour.Instantiate (old) as GameObject;
			_new.transform.SetParent(old.transform.parent);
			MonoBehaviour.DestroyImmediate (old);

			_new.name = name;
			_new.transform.SetSiblingIndex (index);
			news.Add (_new);
		}
		Selection.objects = news.ToArray ();
	}
	
}

[InitializeOnLoad]
public class AutorunNew
{
	static AutorunNew()
	{
		EditorApplication.update += RunOnce;
		AssetDatabase.importPackageCompleted += importPackageStartedCallback;	
	}

	static void RunOnce() 
	{
		EditorApplication.update -= RunOnce;
		if(!EditorPrefs.HasKey("WelcomeScreenShown")) {
			WelcomeScreen.OpenWelcomeWindow();
		}
	}

	public static void importPackageStartedCallback(string str) {
		if(str.Contains("GoogleMobileAds")) {
			ShowGoogleAdMobSupportImportAlert();
		} else if(str.Contains("Unity Monetization")) {
			ShowUnityAdSupportImportAlert();
		} else if(str == "UnityIAP") {
			ShowUnityIAPSupportImportAlert();
		} else if(str.Contains("AdSupport")) {
			Application.OpenURL("https://drive.google.com/file/d/1zROYrDGS6EPz-ixrHI45vxPc8xtIswv0/preview");
		} else if(str.Contains("UnityIAPManager")) {
			Application.OpenURL("https://drive.google.com/file/d/1tWQfdmYUvNj-N6uSc9I9jzwPRgqcLkqi/preview");
		}
	}

	public static void ShowGoogleAdMobSupportImportAlert() {
		bool result = EditorUtility.DisplayDialog("Google AdMob SDK Setup!",
		"Seems like you just imported Google AdMob SDK, Please import Google AdMob Support Script to complete setup.", 
		"OK!, Let's do it",
		"I'll do Later!");

		if(result == true) {
			EditorMenu.SetupGoogleAdMobSupportSDK();
		}
	}

	public static void ShowUnityAdSupportImportAlert() {
		bool result = EditorUtility.DisplayDialog("Unity Monetization SDK Setup!",
		"Seems like you just imported Unity Monetization SDK, Please import Unity Monetization Support Script to complete setup.", 
		"OK!, Let's do it",
		"I'll do Later!");

		if(result == true) {
			EditorMenu.SetupUnityMonetizationSDK();
		}
	}

	public static void ShowUnityIAPSupportImportAlert() {
		bool result = EditorUtility.DisplayDialog("Unity IAP SDK Setup!",
		"Seems like you just imported Unity IAP SDK, Please import Unity IAP Support Script to complete setup.", 
		"OK!, Let's do it",
		"I'll do Later!");

		if(result == true) {
			EditorMenu.SetupUnityIAP();
		}
	}
}
