using System;

namespace Data {
	[Serializable]
	public class SettingsData {
		public SettingsData() {
			Gameplay = new GamePlaySettings();
			Video = new VideoSettings();
			Audio = new AudioSettings();
		}

		public GamePlaySettings Gameplay;
		public VideoSettings Video;
		public AudioSettings Audio;
	}

	[Serializable]
	public class GamePlaySettings {
		public bool FpsCounter;
		public bool CameraShaking;
		public bool Blood;

		public GamePlaySettings() {
			FpsCounter = false;
			CameraShaking = true;
			Blood = true;
		}
	}

	[Serializable]
	public class VideoSettings {
		public bool AntiAliasing;
		public bool Shadows;
		public bool Bloom;
		public bool ColorGrading;
		public bool Vignette;
		public bool AmbientOcclusion;
		//public bool MotionBlur;

		public VideoSettings() {
			AntiAliasing = true;
			Shadows = true;
			Bloom = true;
			ColorGrading = true;
			Vignette = true;
			AmbientOcclusion = true;
			//MotionBlur = true;
		}
	}

	[Serializable]
	public class AudioSettings {
		public SliderSetting SfxVolume;
		public SliderSetting MusicVolume;

		public AudioSettings() {
			SfxVolume = new SliderSetting() {
				Current = 0f,
				Min = -20f,
				Max = 5f,
			};

			MusicVolume = new SliderSetting() {
				Current = 0f,
				Min = -20f,
				Max = 5f,
			};
		}
	}

	[Serializable]
	public class SliderSetting {
		public float Current;
		public float Min;
		public float Max;
	}
}
