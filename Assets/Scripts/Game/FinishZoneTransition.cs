using UnityEngine;

public class FinishZoneTransition : MonoBehaviour
{
    [SerializeField] private bool onlyOnce = true;
    [SerializeField] private string actionName;

    private bool used;
    private NamedActionTransition transition;

    private void Awake()
    {
        NamedActionTransition[] actionTransitions = FindObjectsByType<NamedActionTransition>(FindObjectsSortMode.None);

        for (int i = 0; i < actionTransitions.Length; i++)
        {
            if (actionTransitions[i] != null && actionTransitions[i].actionName == actionName)
            {
                transition = actionTransitions[i];
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (onlyOnce && used) return;

        bool isPlayer = other.CompareTag("Player");
        if (!isPlayer) return;

        used = true;
        if (transition != null) transition.DoAction();
    }
}
