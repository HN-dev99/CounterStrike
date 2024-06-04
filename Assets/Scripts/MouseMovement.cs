using UnityEngine;


public class MouseMovement : MonoBehaviour
{
    [SerializeField] private float mouseSpeed = 500f;
    float xRotate;
    float yRotate;
    [SerializeField] private Transform playerBody;

    float topClamp = -90f;
    float bottomClamp = 90f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;

        xRotate -= mouseY;
        xRotate = Mathf.Clamp(xRotate, topClamp, bottomClamp);
        yRotate += mouseX;

        transform.localRotation = Quaternion.Euler(xRotate, yRotate, 0);
    }
}
