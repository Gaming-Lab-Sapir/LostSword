using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartSceneUIManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        if (EventSystem.current && startButton)
            EventSystem.current.SetSelectedGameObject(startButton.gameObject);

        var transition = FindObjectsByType<NamedActionTransition>(FindObjectsSortMode.None)
            .FirstOrDefault(t => t.actionName == "StartGame");

        if (startButton && transition != null)
            startButton.onClick.AddListener(transition.DoAction);

        if (exitButton)
            exitButton.onClick.AddListener(Application.Quit);
    }
}
