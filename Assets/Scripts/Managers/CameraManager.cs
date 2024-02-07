using Com.Player;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    #region Variables

    private Transform player;

    [Header("Anchor Target")]
    [SerializeField] private Transform anchorTarget;
    [SerializeField] private Vector3 offset;

    [Header("Camera Smoothing")]
    [SerializeField] private float camSmoothing = 0.2f;
    private Vector3 cameraVelocity;

    [Header("Look Sensitivity")]
    [SerializeField] private float lookSensitivityX = 200f;
    [SerializeField] private float lookSensitivityY = 100f;

    [Header("Y-Axis Rotation Clamp")]
    [SerializeField] private float xRotMinMax = 40f;
    private float xRotation = 0;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake() => player = FindObjectOfType<PlayerInputManager>().transform;

    private void LateUpdate() {
        FollowPlayer();
    }

    #endregion

    #region Public Methods

    public void HandleCameraLook(Vector2 input) {
        float MouseX = input.x * lookSensitivityX * Time.deltaTime;
        float MouseY = input.y * lookSensitivityY * Time.deltaTime;

        anchorTarget.Rotate(Vector3.up * MouseX);

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -xRotMinMax, xRotMinMax);
        anchorTarget.rotation = Quaternion.Euler(xRotation, anchorTarget.localEulerAngles.y, 0f);
    }

    #endregion

    #region Private Methods

    private void FollowPlayer() {
        Vector3 targetPos = player.position + offset;
        anchorTarget.position = Vector3.SmoothDamp(anchorTarget.position, targetPos, ref cameraVelocity, camSmoothing);
    }


    #endregion

}
