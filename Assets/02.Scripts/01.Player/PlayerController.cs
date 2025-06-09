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
        Vector3 offset = new Vector3(1, 0, 0);
        GameObject weapon = Instantiate(weaponPrefab, weaponHolder.position + offset, Quaternion.identity, weaponHolder);
        weapon.transform.localPosition = new Vector3(2f, 0f, 0f);
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
