using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private int initialArrowCount = 0;
    public int CurrentArrowCount { get; private set; }
    const float StickDeadzone = 0.2f;
    //private Animator playerAnimator; for now there is no animation for that
    InputSystem inputActions;

    private void Awake()
    {
        //playerAnimator = GetComponent<Animator>();
        inputActions = new();
        CurrentArrowCount = initialArrowCount;

        if (CurrentArrowCount > 0)
            GameEvents.RaiseArrowPickedUp(CurrentArrowCount);
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Shoot.performed += HandleShootInput;
        GameEvents.ArrowPickedUp += AddArrows;
    }

    private void OnDisable()
    {
        inputActions.Player.Shoot.performed -= HandleShootInput;
        inputActions.Disable();
        GameEvents.ArrowPickedUp -= AddArrows;
    }

    public void AddArrows(int amountToAdd)
    {
        CurrentArrowCount += amountToAdd;
    }

    private void HandleShootInput(InputAction.CallbackContext ctx)
    {
        TryShoot();
    }

    private void TryShoot()
    {
        if (CurrentArrowCount <= 0) return;

        Vector2 direction = GetShootDirection();
        ShootArrow(direction);

        CurrentArrowCount = Mathf.Max(0, CurrentArrowCount - 1);
        GameEvents.RaiseArrowShot();  
    }

    private Vector2 GetShootDirection()
    {
        Vector2 stick = Gamepad.current?.rightStick.ReadValue() ?? Vector2.zero;
        if (stick.sqrMagnitude > StickDeadzone)
            return stick.normalized;

        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        return (mouseWorld - (Vector2)arrowSpawnPoint.position).normalized;
    }

    private void ShootArrow(Vector2 direction)
    {
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
        newArrow.GetComponent<Arrow>().Initialize(direction);
    }
}
