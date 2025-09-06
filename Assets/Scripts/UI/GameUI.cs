using SlowpokeStudio.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SlowpokeStudio.UI
{
    public class GameUI : MonoBehaviour
    {
        [Header("Game UI Reference")]
        [SerializeField] internal TextMeshProUGUI currentLevel;
        [SerializeField] internal Button restartButton;
        [SerializeField] internal Button settingsButton;

        private void OnEnable()
        {
            restartButton.onClick.AddListener(OnRestartButton);
            settingsButton.onClick.AddListener(OnSettingsButton);
        }

        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(OnRestartButton);
            settingsButton.onClick.RemoveListener(OnSettingsButton);
        }

        private void OnRestartButton()
        {

        }

        private void OnSettingsButton()
        {
            GameService.Instance.uiManager.settingsUI.settingsUIObject.SetActive(true);
        }

    }
}
