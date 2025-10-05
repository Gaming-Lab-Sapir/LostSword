using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    [Header("Quest Info")]
    [SerializeField] protected QuestInfoSO questInfo;

    private bool isFinished = false;

    protected void FinishQuest()
    {
        if (isFinished) return;
        isFinished = true;

        if (QuestManager.Instance != null)
            QuestManager.Instance.MarkCompleted(questInfo);
    }
}

