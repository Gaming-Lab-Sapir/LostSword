using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private string leverId; 
    private bool isOn;                      

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || isOn) return; 
        isOn = true;
        GameEvents.RaiseLeverChanged(leverId, true);
    }
}
