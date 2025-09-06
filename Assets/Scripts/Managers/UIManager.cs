using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] internal GameUI gameUI;
        [SerializeField] internal MenuUI menuUI;
        [SerializeField] internal SettingsUI settingsUI;
        [SerializeField] internal LevelCompleteUI LevelCompleteUI;
        [SerializeField] internal LevelFailUI LevelFailUI;
        
    }
}
