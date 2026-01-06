using UnityEditor;
using UnityEngine;

namespace FastMesh_Example
{
    [ExecuteInEditMode]
    public class SceneViewText : MonoBehaviour
    {
        private bool _isShow = true;
        private string _text2 = "These 3D models, all created with \"Fast Mesh - 3D Asset Creation Tool\" (click)";
        private Color _backgroundColor = Color.white;
        private Color _textColor = Color.black;

        private void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            if (_isShow == false)
                return;

            Handles.BeginGUI();
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 20;
            style.normal.textColor = _textColor;
            style.wordWrap = true;

            float width = 420f;
            float height = 50f;
            float x = (sceneView.position.width - width) / 2f;

            float y = 10f;

            GUI.color = _backgroundColor;
            GUI.DrawTexture(new Rect(x - 10, y - 10, width + 20, height + 20), Texture2D.whiteTexture);
            GUI.color = Color.white;

            if (GUI.Button(new Rect(x, y, width, height), _text2, style))
            {
                Application.OpenURL("https://assetstore.unity.com/packages/slug/288711");
            }

            Handles.EndGUI();
        }
    }
}