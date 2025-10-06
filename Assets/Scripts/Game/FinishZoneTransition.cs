using UnityEngine;

public class FinishZoneTransition : MonoBehaviour
{
    [Header("Transition")]
    [SerializeField] private string actionName;
    [SerializeField] private bool onlyOnce = true;

    [Header("Requirement")]
    [SerializeField] private QuestInfoSO requiredQuest; 

    private bool used;
    private NamedActionTransition transition;

    private void Awake()
    {
        NamedActionTransition[] all = FindObjectsByType<NamedActionTransition>(FindObjectsSortMode.None);
        for (int i = 0; i < all.Length; i++)
        {
            if (all[i] != null && all[i].actionName == actionName)
            {
                transition = all[i];
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (onlyOnce && used) return;
        if (!other.CompareTag("Player")) return;

        if (requiredQuest != null && (QuestManager.Instance == null || !QuestManager.Instance.IsCompleted(requiredQuest)))
        {
            Debug.Log("quest not completed");
            return;
        }

        used = true;
        Debug.Log("finish");
        if (transition != null) transition.DoAction();
    }
}
