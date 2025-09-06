using SlowpokeStudio.Audio;
using SlowpokeStudio.Currency;
using SlowpokeStudio.Event;
using SlowpokeStudio.Level;
using SlowpokeStudio.Manager;
using SlowpokeStudio.Spawn;
using SlowpokeStudio.UI;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio.Generic
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        [Header("Service")]
        [SerializeField] internal AudioManager audioManager;
        [SerializeField] internal CurrencyManager currencyManager;
        [SerializeField] internal EventManager eventManager;
        [SerializeField] internal GameManager gameManager;
        [SerializeField] internal LevelManager levelManager;
        [SerializeField] internal TrayManager trayManager;
        [SerializeField] internal UIManager uiManager;

        protected override void Awake()
        {
            base.Awake();
            if (Instance == this)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            InitializeServices();
        }

        private void InitializeServices()
        {
            var services = new Dictionary<string, Object>
            {
            { "AudioManager", audioManager },
            { "CurrencyManager", currencyManager },
            { "EventManager", eventManager },
            { "GameManager", gameManager },
            { "LevelManager", levelManager },
            { "TrayManager", trayManager },
            { "UIManager", uiManager }
            };

            foreach (var service in services)
            {
                if (service.Value == null)
                {
                    Debug.LogError($"{service.Key} failed to initialize.");
                }
                else
                {
                    Debug.Log($"{service.Key} is initialized.");
                }
            }
        }
    }
}
