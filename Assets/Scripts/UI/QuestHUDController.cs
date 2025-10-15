using TMPro;
using UnityEngine;

public class QuestHUDController : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text killsText;
    [SerializeField] private TMP_Text levelCompleteText;

    private void OnEnable()
    {
        GameEvents.QuestCountersChanged += OnCountersChanged;
        GameEvents.QuestCompleted += OnQuestCompleted;

        if (levelCompleteText) levelCompleteText.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GameEvents.QuestCountersChanged -= OnCountersChanged;
        GameEvents.QuestCompleted -= OnQuestCompleted;
    }

    private void OnCountersChanged(int coins, int kills, int coinsTotal, int killsTotal)
    {
        if (coinsText) coinsText.text = $"Coins: {coins}/{coinsTotal}";
        if (killsText) killsText.text = $"Kills: {kills}/{killsTotal}";
    }

    private void OnQuestCompleted(string questId)
    {
        if (levelCompleteText)
        {
            levelCompleteText.text = "LEVEL 1 COMPLETE";
            levelCompleteText.gameObject.SetActive(true);
        }
    }
}
