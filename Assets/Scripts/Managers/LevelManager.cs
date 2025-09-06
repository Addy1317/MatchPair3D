using TMPro;
using UnityEngine;

namespace SlowpokeStudio.Level
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Level Settings")]
        public LevelSO levelDatabase;
        public Transform levelParent;

        [SerializeField] private TextMeshProUGUI levelText;

        private int currentLevelIndex = 0;
        private GameObject currentLevelInstance;

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
            if (index < 0 || index >= levelDatabase.levels.Count)
            {
                Debug.LogError("Level index out of bounds!");
                return;
            }

            // Destroy previous level if exists
            if (currentLevelInstance != null)
                Destroy(currentLevelInstance);

            // Instantiate new level
            currentLevelInstance = Instantiate(
                levelDatabase.levels[index].levelObject,
                levelParent.position,
                Quaternion.identity,
                levelParent
            );

            currentLevelIndex = index;

            // Update level name UI
            if (levelText != null)
            {
                levelText.text = $"Level: {levelDatabase.levels[index].levelName}";
            }
        }

        public void LoadNextLevel()
        {
            int nextIndex = currentLevelIndex + 1;

            if (nextIndex >= levelDatabase.levels.Count)
                nextIndex = 0; // Optional: loop back to first level

            LoadLevel(nextIndex);
        }
    }
}
