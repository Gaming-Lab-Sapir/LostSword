using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image fillImage;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private bool hideWhenFull = false;

    private const float PercentMultiplier = 100f;
    private const float FullHealthHideThreshold = 0.999f;

    void Awake()
    {
        if (!playerHealth) playerHealth = GetComponentInParent<PlayerHealth>();
    }

    void LateUpdate()
    {
        if (!playerHealth || !fillImage) return;

        float healthFraction = Mathf.Clamp01(
            (float)playerHealth.CurrentHealthPoints / playerHealth.MaxHealthPoints
        );

        fillImage.fillAmount = healthFraction;

        if (hpText)
        {
            int healthPercent = Mathf.RoundToInt(healthFraction * PercentMultiplier);
            hpText.text = healthPercent + "%";
        }

        if (hideWhenFull)
        {
            bool isFull = healthFraction >= FullHealthHideThreshold;
            fillImage.transform.parent.gameObject.SetActive(!isFull);
        }
    }
}
