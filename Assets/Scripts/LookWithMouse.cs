using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWithMouse : MonoBehaviour
{
    public Camera TPCam;
    
    public Transform player;
    public Transform weapon;
    
    [SerializeField] private float sensitivity;
    [SerializeField] private float rotationX = 0f;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_STANDALONE_WIN
        //Cursor.lockState = CursorLockMode.Locked;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Rotate around x-axis on y
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); 
        
        this.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        weapon.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        
        // Rotate around y-axis on x
        player.Rotate(Vector3.up * mouseX);
        
        //TPCam.gameObject.transform.Rotate(Vector3.up * mouseX);
    }
}
