using UnityEngine;

namespace Com.Player
{
    public class PlayerAnimatorManager : MonoBehaviour
    {

        #region Variables

        private Animator animator;
        int horizontal;
        int vertical;

        [HideInInspector] public float moveAmount;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            animator = GetComponent<Animator>();
            horizontal = Animator.StringToHash("Horizontal");
            vertical = Animator.StringToHash("Vertical");
        }

        #endregion

        #region Public Methods

        public void UpdateMoveAmount(Vector2 input) {
            moveAmount = Mathf.Clamp01(Mathf.Abs(input.x) + Mathf.Abs(input.y));
        }

        public void UpdateAnimator(float horizontalInput, float verticalInput) {
            float snappedHorizontal;
            float snappedVertical;

            #region - Snapped Horizontal -

            if (horizontalInput > 0 && horizontalInput < 0.55f)
            {
                snappedHorizontal = 0.5f;
            }
            else if (horizontalInput > 0.55f)
            {
                snappedHorizontal = 1f;
            }
            else if (horizontalInput < 0 && horizontalInput > -0.55f)
            {
                snappedHorizontal = -0.5f;
            }
            else if (horizontalInput < -0.55f)
            {
                snappedHorizontal = -1f;
            }
            else
            {
                snappedHorizontal = 0f;
            }

            #endregion

            #region - Snapped Vertical -

            if (verticalInput > 0 && verticalInput < 0.55f)
            {
                snappedVertical = 0.5f;
            }
            else if (verticalInput > 0.55f)
            {
                snappedVertical = 1f;
            }
            else if (verticalInput < 0 && verticalInput > -0.55f)
            {
                snappedVertical = -0.5f;
            }
            else if (verticalInput < -0.55f)
            {
                snappedVertical = -1f;
            }
            else
            {
                snappedVertical = 0f;
            }

            #endregion

            animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
            animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
        }

        #endregion

        #region Private Methods



        #endregion

    }
}