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
            if(Physics.Raycast(ray, out hitInfo, rayDistance, interactableLayer)) {

                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();

                if (interactable != null)  {
                    UI.UpdatePromptText(interactable.promptedMessage);

                    if(inputManager.playerActions.Interact.triggered) {
                        interactable.BaseInteract();
                    }
                } 
            } else {
                UI.UpdatePromptText(string.Empty);
            }
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods



        #endregion

    }
}
