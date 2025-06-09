using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private PlayerControls playerControls;
    private Vector2 movement;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move(movement);
    }

}
