using SlowpokeStudio.Generic;
using System.Collections;
using System.Collections.Generic;
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
            levelFailObject.SetActive(false);
        }

        private void OnHomeButton()
        {
            GameServices.Instance.uiManager.menuUI.menuUIObject.SetActive(true);
            OnResumeButton();
        }

        private void OnQuitButton()
        {
            GameServices.Instance.uiManager.menuUI.OnQuitButton();
        }
    }
}
