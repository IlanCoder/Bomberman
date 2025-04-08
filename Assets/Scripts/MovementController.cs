using UnityEngine;

public class MovementController : MonoBehaviour
{
    InputManager inputManager;
    CharacterController characterController;

    [SerializeField] float speed;
    
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (inputManager.moveDir.magnitude != 0)
        {
            transform.forward = new Vector3(inputManager.moveDir.x, 0, inputManager.moveDir.y);
            characterController.Move(transform.forward * speed * Time.deltaTime);
        }
    }
}
