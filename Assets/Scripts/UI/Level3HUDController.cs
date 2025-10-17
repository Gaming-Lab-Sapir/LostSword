using TMPro;
using UnityEngine;

public class Level3HUDController : MonoBehaviour
{
    [SerializeField] TMP_Text gatesText;
    [SerializeField] TMP_Text swordText;
    [SerializeField] TMP_Text levelCompleteText;
    [SerializeField] string questIdFilter = "Level3"; 

    void OnEnable()
    {
        GameEvents.SwordProgressChanged += OnSword;
        GameEvents.QuestCompleted += OnQuestCompleted;

        if (levelCompleteText) levelCompleteText.gameObject.SetActive(false);
        if (gatesText) gatesText.text = "Gates: 0/2";
        if (swordText) swordText.text = "Sword: 0/1";
    }

    void OnDisable()
    {
        GameEvents.SwordProgressChanged -= OnSword;
        GameEvents.QuestCompleted -= OnQuestCompleted;
    }

    void OnGates(int current, int total) => gatesText.text = $"Gates: {current}/{total}";
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
