using System.Collections.Generic;
using UnityEngine;

public class ActivateLeversQuestStep : QuestStep
{
    [SerializeField] private List<string> requiredLeverIds = new() { "Lever1", "Lever2" };
    private HashSet<string> onLevers = new();

    private void OnEnable()
    {
        GameEvents.LeverChanged += OnLeverChanged;
        GameEvents.RaiseQuestProgress(questInfo.id, onLevers.Count, requiredLeverIds.Count);
    }

    private void OnDisable()
    {
        GameEvents.LeverChanged -= OnLeverChanged;
    }

    private void OnLeverChanged(string leverId, bool isOn)
    {
        if (!isOn || !requiredLeverIds.Contains(leverId)) return;
        if (!onLevers.Add(leverId)) return;

        UpdateUI();
        if (onLevers.Count == requiredLeverIds.Count) FinishQuest();
    }
    private void UpdateUI()
    {
        GameEvents.RaiseQuestProgress(questInfo.id, onLevers.Count, requiredLeverIds.Count);
        Debug.Log($"Levers: {onLevers.Count}/{requiredLeverIds.Count}");
    }
}
