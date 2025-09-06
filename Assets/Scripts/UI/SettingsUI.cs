using SlowpokeStudio.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SlowpokeStudio
{
    public class SettingsUI : MonoBehaviour
    {
        [Header("UI Reference")]
        [SerializeField] internal GameObject settingsUIObject;

        [Header("Settings Reference")]
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button homeButton;
        [SerializeField] private Button quitButton;

        private void OnEnable()
        {
            resumeButton.onClick.AddListener(OnResumeButton);
            homeButton.onClick.AddListener(OnHomeButton);
            quitButton.onClick.AddListener(OnQuitButton);
        }

        private void OnDisable()
        {
            resumeButton.onClick.RemoveListener(OnResumeButton);
            homeButton.onClick.RemoveListener(OnHomeButton);
            quitButton.onClick.RemoveListener(OnQuitButton);
        }

        private void OnResumeButton()
        {
            settingsUIObject.SetActive(false);
        }

        private void OnHomeButton()
        {
            GameService.Instance.uiManager.menuUI.menuUIObject.SetActive(true);
            OnResumeButton();
        }

        private void OnQuitButton()
        {
            GameService.Instance.uiManager.menuUI.OnQuitButton();
        }
    }
}
