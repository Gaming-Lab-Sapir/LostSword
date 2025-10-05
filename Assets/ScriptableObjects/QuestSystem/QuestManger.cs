using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    private readonly HashSet<string> completed = new HashSet<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void MarkCompleted(QuestInfoSO quest)
    {
        if (quest == null) return;
        completed.Add(quest.id);
    }

    public bool IsCompleted(QuestInfoSO quest)
    {
        return quest != null && completed.Contains(quest.id);
    }
}
