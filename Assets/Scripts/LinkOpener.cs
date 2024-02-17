using UnityEngine;
using UnityEngine.SceneManagement;

public class LinkOpener : MonoBehaviour
{
    public GameObject popuppolicy;

     void Start()
    {
        if (PlayerPrefs.GetInt("isPolicy", 0) == 0)
        {
            popuppolicy.SetActive(true);
        }
        else
        {

            SceneManager.LoadScene(1);
        }
    }

    public void OpenBrowser(string url)
    {
        Application.OpenURL(url);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("isPolicy", 1);
    }
}