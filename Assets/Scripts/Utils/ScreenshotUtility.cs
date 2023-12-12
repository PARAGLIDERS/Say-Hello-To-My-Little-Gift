#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Utils {
	public static class ScreenshotUtility {		
		[MenuItem("Santa/Screenshot")]
		public static void CaptureScreenshot() {
			string directory = "Screenshots";

			if (!AssetDatabase.IsValidFolder("Assets/" + directory)) {
				AssetDatabase.CreateFolder("Assets", directory);
			}

			string id = System.DateTime.Now.ToString("MM_dd_yy (HH-mm-ss)");
			string fileName = $"screenshot_{id}.png";
			string path = Path.Combine($"Assets/{directory}/", fileName);

			ScreenCapture.CaptureScreenshot(path);
			AssetDatabase.Refresh();
			Debug.LogError("screenshot captured");
		}
	}
}
#endif
