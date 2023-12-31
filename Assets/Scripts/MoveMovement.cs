using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MoveMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (InventorySystem.instance.isOpen == false && CraftingSystem.Instance.isOpen == false) {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            yRotation += mouseX;
           
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }
}
