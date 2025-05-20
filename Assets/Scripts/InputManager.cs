using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    public Vector2 moveDir;

    [HideInInspector]public UnityEvent OnBombPressed;

    public void OnAttack(CallbackContext context) {
        if (context.performed) OnBombPressed?.Invoke();
    }

    public void OnMove(CallbackContext context){
        if(context.performed) moveDir = context.ReadValue<Vector2>();
        else if(context.canceled) moveDir = Vector2.zero;
    }
}
