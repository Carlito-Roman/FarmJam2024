using Unity.VisualScripting;
using UnityEngine;

namespace Com.Player
{
    public class PlayerInteractionManager : MonoBehaviour
    {

        #region Variables

        private Camera playerCamera;
        [SerializeField] private float rayDistance = 3f;

        [SerializeField] private LayerMask interactableLayer;

        private PlayerUI UI;
        private PlayerInputManager inputManager;

        private bool isInteracting = false;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake() => playerCamera = Camera.main;

        private void Start() { 
            UI = GetComponent<PlayerUI>();
            inputManager = GetComponent<PlayerInputManager>();
        }

        private void Update()
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, rayDistance, interactableLayer)) {

                IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();
                Promptable prompt = hitInfo.collider.GetComponent<Promptable>();

                if (interactable != null) {

                    if (inputManager.playerActions.Interact.triggered) {
                        StartInteraction(interactable);
                    }

                    if (prompt != null) { UI.UpdatePromptText(prompt.promptedMessage); }
                } 
            } else {
                UI.UpdatePromptText("");
            }
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods

        private void StartInteraction(IInteractable interactable) {
            interactable.Interact(this, out bool interactionSuccessfull);
            isInteracting = true;
        }

        private void EndInteraction() {
            isInteracting = false;
        }

        #endregion

    }
}
