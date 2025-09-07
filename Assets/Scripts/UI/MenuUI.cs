using SlowpokeStudio.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SlowpokeStudio.UI
{
    public class MenuUI : MonoBehaviour
    {
        [Header("Menu Panel")]
        [SerializeField] internal GameObject menuUIObject;

        [Header("Buttons Reference")]
        [SerializeField] internal Button playButton;
        [SerializeField] internal Button quitButton;

        private void Awake()
        {
            menuUIObject.SetActive(true);
        }

        private void OnEnable()
        {
            playButton.onClick.AddListener(OnplayButton);
            quitButton.onClick.AddListener(OnQuitButton);
        }

        private void OnDisable()
        {
            playButton.onClick.AddListener(OnplayButton);
            quitButton.onClick.AddListener(OnQuitButton);
        }

        internal void OnplayButton()
        {
            GameService.Instance.audioManager.PlaySFX(Audio.SFXType.OnButtonClickSFX);
            GameService.Instance.gameManager.IsGamePlayable = true;
            menuUIObject.SetActive(false);
        }

        internal void OnQuitButton()
        {
            GameService.Instance.audioManager.PlaySFX(Audio.SFXType.OnButtonClickSFX);
            Application.Quit();
        }
    }
}
