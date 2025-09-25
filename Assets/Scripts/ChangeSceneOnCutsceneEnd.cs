using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ChangeSceneOnCutsceneEnd : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] PlayableDirector playableDirector;

    private void OnTriggerEnter2D()
    {
        playableDirector.Play();
    }

    void Start()
    {
        playableDirector.stopped += ChangeScene;
    }

    private void ChangeScene(PlayableDirector obj)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnDestroy()
    {
        playableDirector.stopped -= ChangeScene;
    }
}
