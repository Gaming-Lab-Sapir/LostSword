using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private string requiredQuestId = "Level3"; 

    void OnEnable() => GameEvents.QuestCompleted += OnQuestCompleted;
    void OnDisable() => GameEvents.QuestCompleted -= OnQuestCompleted;

    void OnQuestCompleted(string questId)
    {
        if (questId == requiredQuestId)
            Destroy(gameObject); 
    }
}