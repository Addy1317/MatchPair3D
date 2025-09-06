using UnityEngine;

namespace SlowpokeStudio.Level
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Level Settings")]
        public LevelSO levelDatabase;
        public Transform levelParent;

        private int currentLevelIndex = 0;
        private GameObject currentLevelInstance;

        private void Start()
        {
            LoadLevel(currentLevelIndex);
        }

        public void LoadLevel(int index)
        {
            if (index < 0 || index >= levelDatabase.levels.Count)
            {
                Debug.LogError("Level index out of range!");
                return;
            }

            if (currentLevelInstance != null)
            {
                Destroy(currentLevelInstance);
            }

            currentLevelIndex = index;
            currentLevelInstance = Instantiate(levelDatabase.levels[index].levelObject, levelParent);
            Debug.Log("Loaded Level: " + levelDatabase.levels[index].levelName);
        }

        public void LoadNextLevel()
        {
            int nextIndex = currentLevelIndex + 1;
            if (nextIndex >= levelDatabase.levels.Count)
            {
                Debug.Log("All levels completed!");
                return;
            }

            LoadLevel(nextIndex);
        }

        public void RestartCurrentLevel()
        {
            LoadLevel(currentLevelIndex);
        }

        public int GetCurrentLevelNumber()
        {
            return currentLevelIndex + 1;
        }

        public string GetCurrentLevelName()
        {
            return levelDatabase.levels[currentLevelIndex].levelName;
        }
    }
}
