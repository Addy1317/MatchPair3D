using SlowpokeStudio.Generic;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SlowpokeStudio.Level
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Level Settings")]
        [SerializeField] private LevelSO levelDatabase;
        [SerializeField] private Transform levelParent;
        [SerializeField] private TextMeshProUGUI levelText;

        private int currentLevelIndex = 0;
        public int CurrentLevelIndex => currentLevelIndex;

        private GameObject currentLevelInstance;
        public List<CubeSelector> activeRackCubes = new List<CubeSelector>();

        private void Start()
        {
            LoadLevel(currentLevelIndex);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                LoadNextLevel();
            }
        }

        public void LoadLevel(int index)
        {
            GameService.Instance.trayManager.ClearTray();

            if (index < 0 || index >= levelDatabase.levels.Count)
            {
                Debug.LogError("Level index out of bounds!");
                return;
            }

            if (currentLevelInstance != null)
            {
                Destroy(currentLevelInstance);
            }

            currentLevelInstance = Instantiate(
                levelDatabase.levels[index].levelObject,
                levelParent.position,
                Quaternion.identity,
                levelParent
            );

            currentLevelIndex = index;

            if (levelText != null)
            {
                levelText.text = $"Level: {levelDatabase.levels[index].levelName}";
            }

            CubeSelector[] allCubes = currentLevelInstance.GetComponentsInChildren<CubeSelector>(true);
            foreach (var selector in allCubes)
            {
                selector.Initialize(GameService.Instance.trayManager);
            }

            GameService.Instance.gameManager.RegisterLevelCubes(new System.Collections.Generic.List<CubeSelector>(allCubes));

            RackTile[] allTiles = currentLevelInstance.GetComponentsInChildren<RackTile>(true);
            foreach (var tile in allTiles)
            {
                tile.Initialize(GameService.Instance.trayManager);
            }
        }

        public void LoadNextLevel()
        {
            int nextIndex = currentLevelIndex + 1;

            if (nextIndex >= levelDatabase.levels.Count)
                nextIndex = 0; 

            LoadLevel(nextIndex);
        }
    }
}
