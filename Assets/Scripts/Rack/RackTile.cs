using System.Collections;
using System.Collections.Generic;
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

            // Fetch both cubes under this tile
            CubeSelector[] cubes = GetComponentsInChildren<CubeSelector>();

            if (cubes.Length != 2)
            {
                Debug.LogWarning($"Tile {gameObject.name} does not contain 2 cubes.");
                return;
            }

            // Disable further clicks
            this.enabled = false;

            // Send pair to tray
            trayManager.AddPairToTray(cubes[0], cubes[1]);

            // Optional: Play click sound or visual feedback
        }
    }
}
