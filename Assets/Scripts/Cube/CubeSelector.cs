using SlowpokeStudio.Level;
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
            // Auto-apply color when changed in Inspector
            ApplyColor();
        }
#endif

        public void Initialize(TrayManager tray)
        {
            trayManager = tray;
        }

        private void OnMouseDown()
        {
            if (isSelected || trayManager == null) return;

            isSelected = true;
            trayManager.AddToTray(this);
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
