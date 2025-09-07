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
            restartButton.onClick.AddListener(OnReloadButton);
            settingsButton.onClick.AddListener(OnSettingsButton);
        }

        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(OnReloadButton);
            settingsButton.onClick.RemoveListener(OnSettingsButton);
        }

        internal void OnReloadButton()
        {
            GameService.Instance.audioManager.PlaySFX(Audio.SFXType.OnButtonClickSFX);
            Debug.Log("Restarting current level...");
            GameService.Instance.trayManager.ClearTray();
            int currentIndex = GameService.Instance.levelManager.CurrentLevelIndex;
            GameService.Instance.levelManager.LoadLevel(currentIndex);
        }

        private void OnSettingsButton()
        {
            GameService.Instance.audioManager.PlaySFX(Audio.SFXType.OnButtonClickSFX);
            GameService.Instance.uiManager.settingsUI.settingsUIObject.SetActive(true);
        }

    }
}
