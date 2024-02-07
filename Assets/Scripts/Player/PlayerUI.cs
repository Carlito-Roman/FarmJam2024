using UnityEngine;
using TMPro;

namespace Com.Player
{
    public class PlayerUI : MonoBehaviour
    {

        #region Variables

        [SerializeField] private TextMeshProUGUI promptText;

        #endregion

        #region MonoBehaviour Callbacks



        #endregion

        #region Public Methods

        public void UpdatePromptText(string message) {
            promptText.text = message;
        }

        #endregion

        #region Private Methods



        #endregion

    }
}
