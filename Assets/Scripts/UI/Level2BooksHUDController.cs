using TMPro;
using UnityEngine;

public class Level2BooksHUDController : MonoBehaviour
{
    [SerializeField] private TMP_Text booksText;         
    [SerializeField] private TMP_Text levelCompleteText;  
    [SerializeField] private string questIdFilter = "";   

    private void OnEnable()
    {
        GameEvents.QuestProgress += OnQuestProgress;
        GameEvents.QuestCompleted += OnQuestCompleted;

        if (levelCompleteText) levelCompleteText.gameObject.SetActive(false);
        if (booksText) booksText.text = "0/3"; 
    }

    private void OnDisable()
    {
        GameEvents.QuestProgress -= OnQuestProgress;
        GameEvents.QuestCompleted -= OnQuestCompleted;
    }

    private void OnQuestProgress(string questId, int current, int total)
    {
        if (!PassesFilter(questId)) return;
        if (booksText) booksText.text = $"{current}/{total}";  
    }

    private void OnQuestCompleted(string questId)
    {
        if (!PassesFilter(questId)) return;
        if (levelCompleteText)
        {
            levelCompleteText.text = "LEVEL 2 COMPLETE";
            levelCompleteText.gameObject.SetActive(true);
        }
    }

    private bool PassesFilter(string id)
    {
        return string.IsNullOrEmpty(questIdFilter) ||
               id.Equals(questIdFilter) ||
               id.ToLower().Contains(questIdFilter.ToLower());
    }
}
