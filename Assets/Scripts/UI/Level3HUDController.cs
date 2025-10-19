using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level3HUDController : MonoBehaviour
{
    [SerializeField] TMP_Text leversText;
    [SerializeField] TMP_Text swordText;
    [SerializeField] TMP_Text levelCompleteText;
    [SerializeField] string questIdFilter = "Level3";
    [SerializeField] List<string> requiredLeverIds = new() { "Lever1", "Lever2" };
    private readonly HashSet<string> onLevers = new();


    void OnEnable()
    {
        GameEvents.LeverChanged += OnLever;
        GameEvents.SwordProgressChanged += OnSword;
        GameEvents.QuestCompleted += OnQuestCompleted;

        if (levelCompleteText) levelCompleteText.gameObject.SetActive(false);
        if (leversText) leversText.text = "Levers: 0/2";
        if (swordText) swordText.text = "Sword: 0/1";
    }

    void OnDisable()
    {
        GameEvents.LeverChanged -= OnLever;
        GameEvents.SwordProgressChanged -= OnSword;
        GameEvents.QuestCompleted -= OnQuestCompleted;
    }

    private void OnLever(string leverId, bool isOn)
    {
        if (string.IsNullOrEmpty(leverId)) return;
        if (requiredLeverIds.Count > 0 && !requiredLeverIds.Contains(leverId)) return;

        if (isOn) onLevers.Add(leverId);
                  
        if (leversText) leversText.text = $"Levers: {onLevers.Count}/{requiredLeverIds.Count}";
    }
    void OnSword(int current, int total) => swordText.text = $"Sword: {current}/{total}";

    void OnQuestCompleted(string questId)
    {
        if (!questId.ToLower().Contains(questIdFilter.ToLower())) return;
        if (levelCompleteText)
        {
            levelCompleteText.text = "LEVEL 3 COMPLETE";
            levelCompleteText.gameObject.SetActive(true);
        }
    }
}
