using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnStateEnter : MonoBehaviour
{
    private bool isLoading;
    private string lastLoaded;

    private void OnEnable()  => Events.OnStateEnter += HandleEnter;
    private void OnDisable() => Events.OnStateEnter -= HandleEnter;

    private void HandleEnter(GameState state)
    {
        if (isLoading) return;

        string target = state.gameObject.name; 

        if (SceneManager.GetActiveScene().name == target) return;
        if (lastLoaded == target) return;
 
        isLoading = true;
        lastLoaded = target;
        SceneManager.LoadScene(target);
        isLoading = false;
    }
}
