using UnityEngine;

namespace Com.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerLocomotion : MonoBehaviour
    {

        #region Variables

        private CharacterController characterController;

        [Header("Player Camera")]
        [SerializeField] private Transform playerCam;

        [Header("Movement Speed")]
        [SerializeField] private float moveSpeed;
        private Vector3 moveDirection;

        [Header("Rotation Smoothing")]
        [SerializeField] private float smoothing = 15f;

        #endregion

        #region MonoBehaviour Callbacks

        private void Start() => characterController = GetComponent<CharacterController>();



        #endregion

        #region Public Methods

        public void HandlePlayerMovement(Vector2 input)
        {
            moveDirection = (playerCam.forward * input.y) + (playerCam.right * input.x);
            moveDirection.Normalize();
            moveDirection.y = 0;

            Vector3 movementVelocity = moveDirection;

            if (isGrounded()) {
                movementVelocity.y = -2f;
            }  else {
                movementVelocity.y += Physics.gravity.y * Time.deltaTime;
            }

            characterController.Move(movementVelocity * moveSpeed * Time.deltaTime);
        }

        public void HandlePlayerRotation(Vector2 input)
        {
            Vector3 targetDirection = Vector3.zero;

            targetDirection = playerCam.forward * input.y + playerCam.right * input.x;
            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero) {
                targetDirection = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothing * Time.deltaTime);

            transform.rotation = playerRotation;
        }

        #endregion

        #region Private Methods

        private bool isGrounded()
        {
            return characterController.isGrounded;
        }

        #endregion

    }
}
