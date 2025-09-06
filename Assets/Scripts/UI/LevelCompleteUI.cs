using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SlowpokeStudio.UI
{
    public class LevelCompleteUI : MonoBehaviour
    {
        [Header("Level UI Reference")]
        [SerializeField] internal GameObject levelCompletionObject;

        [Header("UI Reference")]
        [SerializeField] internal TextMeshProUGUI coinText;
        [SerializeField] internal Button nextLevelButton;

        private void OnEnable()
        {
            nextLevelButton.onClick.AddListener(OnNextButton);
        }

        private void OnDisable()
        {
            nextLevelButton.onClick.RemoveListener(OnNextButton);
        }

        private void OnNextButton()
        {

        }
    }
}
