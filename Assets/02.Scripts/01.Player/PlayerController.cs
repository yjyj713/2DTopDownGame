using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Transform weaponHolder;

    private PlayerControls playerControls;
    private Vector2 movement;

    private void Start()
    {
       
    }

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

        if (movement.x != 0)
        {
            spriteRenderer.flipX = movement.x < 0;
        }
    }

}
