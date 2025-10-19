using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameLostReturn : MonoBehaviour
{
    
    [SerializeField] TMP_Text messageText;         
    [SerializeField] float showSeconds = 1.5f;      

    void OnEnable() => GameEvents.GameLost += OnGameLost;
    void OnDisable() => GameEvents.GameLost -= OnGameLost;

    void OnGameLost()
    {
        if (messageText) { messageText.gameObject.SetActive(true); }
        StartCoroutine(ReturnAfterDelay());
    }

    System.Collections.IEnumerator ReturnAfterDelay()
    {
        yield return new WaitForSecondsRealtime(showSeconds);
        Time.timeScale = 1f;
        AppQuitter.Quit();
    }
}


