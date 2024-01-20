using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedTextPro : MonoBehaviour
{
    public string txtTag;
    TextMeshProUGUI thisText;

    void Awake()
    {
        thisText = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        LocalizationManager.OnLanguageChangedEvent += OnLanguageChangedEvent;
        Invoke("SetLocalizedText", 0.01F);
    }

    void OnDisable()
    {
        LocalizationManager.OnLanguageChangedEvent -= OnLanguageChangedEvent;
    }

    public void SetLocalizedText()
    {
        if (!string.IsNullOrEmpty(txtTag.Trim()))
        {
            //thisText.SetLocalizedTextForTag(txtTag);
        }
    }

    void OnLanguageChangedEvent(string langCode)
    {
        SetLocalizedText();
    }
}
