using SlowpokeStudio.Generic;
using SlowpokeStudio.Level;
using SlowpokeStudio.Manager;
using UnityEngine;

namespace SlowpokeStudio
{
    public class CubeSelector : MonoBehaviour
    {
        private TrayManager trayManager;
        private bool isSelected = false;

        [Header("Assigned on prefab or in Inspector")]
        public ObjectColor cubeColor;

        [Header("Material Mapping")]
        public Material redMat;
        public Material greenMat;
        public Material blueMat;
        public Material yellowMat;

        private void Start()
        {
            ApplyColor();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            ApplyColor();
        }
#endif

        public void Initialize(TrayManager tray)
        {
            trayManager = tray;
        }

        private void OnMouseDown()
        {
            /*  if (isSelected || trayManager == null) return;

              isSelected = true;
              //trayManager.AddToTray(this);*/
            if (!GameService.Instance.gameManager.IsGamePlayable)
                return;

            Debug.Log("🟡 OnMouseDown triggered on: " + gameObject.name);

            if (trayManager != null)
            {
                trayManager.OnCubeClicked(this); 
            }
            else
            {
                Debug.LogError("❌ trayManager is NULL for " + gameObject.name);
            }
        }

        private void ApplyColor()
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer == null) return;

            switch (cubeColor)
            {
                case ObjectColor.Red:
                    renderer.sharedMaterial = redMat;
                    break;
                case ObjectColor.Green:
                    renderer.sharedMaterial = greenMat;
                    break;
                case ObjectColor.Blue:
                    renderer.sharedMaterial = blueMat;
                    break;
                case ObjectColor.Yellow:
                    renderer.sharedMaterial = yellowMat;
                    break;
            }
        }
    

    }
}
