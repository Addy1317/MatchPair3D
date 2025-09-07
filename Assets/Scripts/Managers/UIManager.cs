using UnityEngine;

namespace SlowpokeStudio.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] internal GameUI gameUI;
        [SerializeField] internal MenuUI menuUI;
        [SerializeField] internal SettingsUI settingsUI;
        [SerializeField] internal LevelCompleteUI levelCompleteUI;
        [SerializeField] internal LevelFailUI levelFailUI;  
    }
}
