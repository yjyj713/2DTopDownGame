using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    [SerializeField] private GameObject weaponPrefab;

    private PlayerControls playerControls;
    private Vector2 movement;

    private void Start()
    {
        GameObject weapon = Instantiate(weaponPrefab, transform.position + Vector3.right, Quaternion.identity);
        weapon.GetComponent<Weapon>().Init(transform);
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
    }

}
