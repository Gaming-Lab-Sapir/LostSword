using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowMagazineUI : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Transform slotsParent;   
    [SerializeField] private Image tickPrefab;        
    [SerializeField] private int maxSlots = 30;

    [Header("Colors")]
    [SerializeField] private Color fullColor = Color.white;
    [SerializeField] private Color emptyColor = new Color(1f, 1f, 1f, 0.15f);

    private readonly List<Image> ticks = new List<Image>();
    private int arrows = 0;

    private void OnEnable()
    {
        GameEvents.ArrowPickedUp += OnPicked;
        GameEvents.ArrowShot += OnShot;
    }

    private void OnDisable()
    {
        GameEvents.ArrowPickedUp -= OnPicked;
        GameEvents.ArrowShot -= OnShot;
    }

    private void Start()
    {
        BuildPool();
        UpdateUI();
    }

    private void BuildPool()
    {
        for (int i = slotsParent.childCount - 1; i >= 0; i--)
            Destroy(slotsParent.GetChild(i).gameObject);

        ticks.Clear();
        for (int i = 0; i < maxSlots; i++)
        {
            var img = Instantiate(tickPrefab, slotsParent);
            img.name = $"Tick_{i}";
            img.color = emptyColor;
            ticks.Add(img);
        }
    }

    private void OnPicked(int amount)
    {
        arrows = Mathf.Clamp(arrows + Mathf.Abs(amount), 0, maxSlots);
        UpdateUI();
    }

    private void OnShot()
    {
        arrows = Mathf.Clamp(arrows - 1, 0, maxSlots);
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < ticks.Count; i++)
            ticks[i].color = (i < arrows) ? fullColor : emptyColor;
    }
}
