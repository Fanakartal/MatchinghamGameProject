using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject _manager;
    
    [SerializeField]
    private CharacterController charController;

    public GameObject[] weapons;
    public GameObject currentWeapon;
    private int weaponIndex = 0;
    
    [SerializeField] private float xMov;
    [SerializeField] private float zMov;
    public float moveSpeed = 10f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    private float gravity = -9.81f * 2;
    private Vector3 gravitationalVelocity;

    public float jumpHeight = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        _manager = GameObject.FindWithTag("GameManager");
        
        charController = GetComponent<CharacterController>();

        if (!currentWeapon.activeInHierarchy)
        {
            currentWeapon.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _manager.GetComponent<UIManagerScript>().ChangeWeapon();
            currentWeapon = weapons[weaponIndex++ % weapons.Length];
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && gravitationalVelocity.y < 0)
        {
            gravitationalVelocity.y = -2f;
        }
        
        xMov = Input.GetAxis("Horizontal");
        zMov = Input.GetAxis("Vertical");
        Vector3 movement = transform.right * xMov + transform.forward * zMov;
        charController.Move(movement * (moveSpeed * Time.deltaTime));

        gravitationalVelocity.y += gravity * Time.deltaTime;
        charController.Move(gravitationalVelocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            gravitationalVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
