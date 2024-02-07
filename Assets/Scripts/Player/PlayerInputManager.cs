using Unity.VisualScripting;
using UnityEngine;

namespace Com.Player
{
    public class PlayerInputManager : MonoBehaviour
    {

        #region Variables

        private PlayerInput playerInput;
        [HideInInspector] public PlayerInput.PlayerActions playerActions;

        //Script References
        private PlayerLocomotion locomotion;
        private PlayerAnimatorManager animatorManager;

        private CameraManager camManager;

        #endregion

        #region MonoBehaviour Callbacks

        private void Update() {
            locomotion.HandlePlayerMovement(playerActions.Movement.ReadValue<Vector2>());
            locomotion.HandlePlayerRotation(playerActions.Movement.ReadValue<Vector2>());

            animatorManager.UpdateMoveAmount(playerActions.Movement.ReadValue<Vector2>());
            animatorManager.UpdateAnimator(0, animatorManager.moveAmount);

            camManager.HandleCameraLook(playerActions.Look.ReadValue<Vector2>());
        }

        #region - Enable / Disable -

        private void OnEnable() {
            if (playerInput == null) {
                playerInput = new PlayerInput();
                playerActions = playerInput.Player;

                playerActions.Enable();
            }

            GetScriptReferences();
        }

        private void OnDisable()  {
            playerActions.Disable();
        }

        #endregion

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods

        private void GetScriptReferences() {
            locomotion = GetComponent<PlayerLocomotion>();
            animatorManager = GetComponent<PlayerAnimatorManager>();

            camManager = FindObjectOfType<CameraManager>();
        }

        #endregion

    }
}

