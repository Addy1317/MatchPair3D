using UnityEngine;

namespace SlowpokeStudio
{
    public class RackTile : MonoBehaviour
    {
        private TrayManager trayManager;

        public void Initialize(TrayManager tray)
        {
            trayManager = tray;
        }

        private void OnMouseDown()
        {
            if (trayManager == null) return;

            // Get 2 cubes (children)
            CubeSelector[] cubes = GetComponentsInChildren<CubeSelector>();

            if (cubes.Length != 2)
            {
                Debug.LogWarning($"Tile {name} does not contain 2 cubes.");
                return;
            }

            this.enabled = false; // prevent double click

            trayManager.AddPairToTray(cubes[0], cubes[1]);
        }
    }
}
