using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    private float xRotation;
    private float yRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // locks the cursor
        Cursor.visible = false; // makes the cursor invisible
    }

    // Update is called once per frame
    void Update()
    {
        // vvvvv gets the mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX; // sets the xRotation to the mouseX
        xRotation -= mouseY; // same but for Y
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // locks the rotation vertically

        // applies these rotations
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
