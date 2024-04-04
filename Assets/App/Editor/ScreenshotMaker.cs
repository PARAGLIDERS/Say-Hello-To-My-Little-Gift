using System.IO;
using UnityEditor;
using UnityEngine;

namespace Utils {
	public static class ScreenshotMaker {
		private static string _directory = "Screenshots";

		[MenuItem(Constants.MenuFolder + nameof(CaptureScreenshot))]
		public static void CaptureScreenshot() {
			if (!AssetDatabase.IsValidFolder("Assets/" + _directory)) {
				AssetDatabase.CreateFolder("Assets", _directory);
			}

			string id = System.DateTime.Now.ToString("MM_dd_yy (HH-mm-ss)");
			string fileName = $"screenshot_{id}.png";
			string path = Path.Combine($"Assets/{_directory}/", fileName);

			ScreenCapture.CaptureScreenshot(path);
			AssetDatabase.Refresh();

			Debug.Log("screenshot captured");
		}		
	}
}
