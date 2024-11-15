using UnityEngine;

namespace Root {
	public class FpsCounter : MonoBehaviour{
		[SerializeField] private Gradient _colorGradient;

		private float deltaTime = 0.0f;
		private bool _isActive => Core.DataController?.Data.Settings.Gameplay.FpsCounter ?? false;
		
		private void Update() {
			if (!_isActive) {
				return;
			}

			deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
		}

		private void OnGUI() {
			if(!_isActive) {
				return;
			}

			int w = Screen.width, h = Screen.height;
			GUIStyle style = new GUIStyle();
			Rect rect = new Rect(0, 0, w, h * 2 / 100);
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = h * 2 / 100;
			float msec = deltaTime * 1000.0f;
			float fps = 1.0f / deltaTime;
			string text = $"{msec:0.0} ms ({fps:0.} fps)";
			style.normal.textColor = _colorGradient.Evaluate(fps / 60f);
			GUI.Label(rect, text, style);
		}
    }
}