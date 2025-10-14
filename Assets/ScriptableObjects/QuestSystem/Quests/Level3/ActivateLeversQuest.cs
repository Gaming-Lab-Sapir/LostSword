using System.Collections.Generic;
using UnityEngine;

public class ActivateLeversQuestStep : QuestStep
{
    [SerializeField] private List<string> requiredLeverIds = new() { "Lever1", "Lever2" };

    private readonly HashSet<string> onLevers = new();
    private bool swordPicked;

    private bool IsComplete => onLevers.Count == requiredLeverIds.Count && swordPicked;

    private void OnEnable()
    {
        GameEvents.LeverChanged += OnLeverChanged;
        GameEvents.SwordCollected += OnSwordCollected;

        GameEvents.RaiseGatesProgress(onLevers.Count, requiredLeverIds.Count);
        GameEvents.RaiseSwordProgress(swordPicked ? 1 : 0, 1);
        GameEvents.RaiseQuestProgress(questInfo.id, onLevers.Count + (swordPicked ? 1 : 0), requiredLeverIds.Count + 1);
    }

    private void OnDisable()
    {
        GameEvents.LeverChanged -= OnLeverChanged;
        GameEvents.SwordCollected -= OnSwordCollected;
    }

    private void OnLeverChanged(string leverId, bool isOn)
    {
        if (!isOn || !requiredLeverIds.Contains(leverId)) return;
        if (!onLevers.Add(leverId)) return;

        UpdateUI();
        TryComplete();
    }

    private void OnSwordCollected()
    {
        if (swordPicked) return;
        swordPicked = true;

        UpdateUI();
        TryComplete();
    }

    private void TryComplete()
    {
        if (!IsComplete) return;

        if (questInfo != null)
            GameEvents.RaiseQuestCompleted(questInfo.id); 

        FinishQuest();
    }

    private void UpdateUI()
    {
        GameEvents.RaiseGatesProgress(onLevers.Count, requiredLeverIds.Count);
        GameEvents.RaiseSwordProgress(swordPicked ? 1 : 0, 1);

        GameEvents.RaiseQuestProgress(questInfo.id, onLevers.Count + (swordPicked ? 1 : 0), requiredLeverIds.Count + 1);
    }
}
