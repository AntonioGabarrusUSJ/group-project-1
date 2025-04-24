using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform cameraPos;
    public Transform orientation;

    public float sensitivityX;
    public float sensitivityY;

    private float xRotation;
    private float yRotation;

    void Update()
    {
        transform.parent.position = cameraPos.position;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
