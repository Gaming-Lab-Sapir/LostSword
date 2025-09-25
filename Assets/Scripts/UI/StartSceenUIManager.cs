using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceenUIManager : MonoBehaviour
{
    [SerializeField] string firstLevelName = "Level1";
    [SerializeField] string secondLevelName = "Level2";
    [SerializeField] string thirdLevelName = "Level3";
    [Header("Buttons")]
    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button backButton;

    [Header("Panels")]
    [SerializeField] GameObject buttonsPanel;

    void Start()
    {

        EventSystem.current.SetSelectedGameObject(startButton.gameObject);
        AddButtonsListeners();
        AssignNamedActionTransition();
    }

    private void AddButtonsListeners()
    {
        startButton.onClick.AddListener(() => SceneManager.LoadScene(firstLevelName));
        exitButton.onClick.AddListener(() => Application.Quit());
    }

    private void AssignNamedActionTransition()
    {
        var transitions = FindObjectsByType<NamedActionTransition>(FindObjectsSortMode.None);
        var buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        foreach (var transition in transitions)
        {
            var selectedButton = buttons.FirstOrDefault(x => x.name.Equals(transition.actionName));
            if (selectedButton != null)
            {
                selectedButton.onClick.AddListener(transition.DoAction);
            }
        }
    }

}
