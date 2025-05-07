using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    InputSystem_Actions actions;

    public Vector2 moveDir;

    [HideInInspector]public UnityEvent OnBombPressed;

    void Start()
    {
        actions = new InputSystem_Actions();
        actions.Enable();
        actions.Player.Move.performed += i => moveDir = i.ReadValue<Vector2>();
        actions.Player.Move.canceled += i => moveDir = Vector2.zero;
        actions.Player.Attack.performed += i => OnBombPressed?.Invoke();
    }

}
