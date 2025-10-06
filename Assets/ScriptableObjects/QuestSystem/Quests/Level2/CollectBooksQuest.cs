using System.Collections.Generic;
using UnityEngine;

public class CollectBooksQuest : QuestStep
{
    [SerializeField] private int booksToCollect = 3;

    private readonly HashSet<string> collected = new HashSet<string>();

    private void OnEnable()
    {
        GameEvents.BookCollected += OnBookCollected;
        UpdateUI();
    }

    private void OnDisable()
    {
        GameEvents.BookCollected -= OnBookCollected;
    }

    private void OnBookCollected(string bookId)
    {
        if (!collected.Add(bookId)) return;   
        UpdateUI();
        if (collected.Count >= booksToCollect) FinishQuest();
    }

    private void UpdateUI()
    {
        GameEvents.RaiseQuestProgress(questInfo.id, collected.Count, booksToCollect);
        Debug.Log($"Books: {collected.Count}/{booksToCollect}");
    }
}
