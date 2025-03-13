using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCameraSimulator : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float lookSpeed = 2f;

    private Transform cameraTransform;

    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    private void Start()
    {
        // Get the AR Camera (Usually under XROrigin)
        cameraTransform = Camera.main.transform;

        // Disable script on mobile builds
        if (Application.isMobilePlatform)
        {
            this.enabled = false;
        }
    }

    private void Update()
    {
        // Move the XR Origin (not just the camera)
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float moveY = (Input.GetKey(KeyCode.Q) ? -1 : Input.GetKey(KeyCode.E) ? 1 : 0) * moveSpeed * Time.deltaTime;
        transform.position += transform.right * moveX + transform.up * moveY + transform.forward * moveZ;

        // Rotate Camera (Right-Click + Drag) - up/down rotation handled here
        if (Input.GetMouseButton(1))  // Right-click to rotate
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseY = -Input.GetAxis("Mouse Y") * lookSpeed;

            // Rotate the XR Origin around Y-axis for left/right movement
            transform.Rotate(Vector3.up, mouseX, Space.World);

            // Rotate the camera for up/down movement (clamp to avoid flipping)
            currentRotationX += mouseY;
            currentRotationX = Mathf.Clamp(currentRotationX, -80f, 80f);
            cameraTransform.localRotation = Quaternion.Euler(currentRotationX, 0f, 0f);
        }
    }
}
