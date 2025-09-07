using SlowpokeStudio.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SlowpokeStudio.UI
{
    public class LevelFailUI : MonoBehaviour
    {
        [Header("Level UI Reference")]
        [SerializeField] internal GameObject levelFailObject;

        [Header("Level Reference")]
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button homeButton;
        [SerializeField] private Button quitButton;

        private void OnEnable()
        {
            resumeButton.onClick.AddListener(OnRetryButton);
            homeButton.onClick.AddListener(OnHomeButton);
            quitButton.onClick.AddListener(OnQuitButton);
        }

        private void OnDisable()
        {
            resumeButton.onClick.RemoveListener(OnRetryButton);
            homeButton.onClick.RemoveListener(OnHomeButton);
            quitButton.onClick.RemoveListener(OnQuitButton);
        }

        private void OnRetryButton()
        {
            levelFailObject.SetActive(false);
            GameService.Instance.trayManager.ClearTray();
            GameService.Instance.levelManager.LoadLevel(GameService.Instance.levelManager.CurrentLevelIndex);
        }

        private void OnHomeButton()
        {
            GameService.Instance.audioManager.PlaySFX(Audio.SFXType.OnButtonClickSFX);
            GameService.Instance.uiManager.menuUI.menuUIObject.SetActive(true);
            OnRetryButton();
        }

        private void OnQuitButton()
        {
            GameService.Instance.audioManager.PlaySFX(Audio.SFXType.OnButtonClickSFX);
            GameService.Instance.uiManager.menuUI.OnQuitButton();
        }
    }
}
