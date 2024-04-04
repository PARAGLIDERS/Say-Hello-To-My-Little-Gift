using Data;
using Root;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace PostProcessing {
	public class PostProcessingService {
		private readonly PostProcessProfile _profile;
		private readonly DataService _dataService;

		public PostProcessingService(PostProcessProfile profile) {
			_profile = profile;
			_dataService.OnSave += ApplySettings;
		}

		public void Dispose() {
			_dataService.OnSave -= ApplySettings;

			ToggleDepthOfField(false);
			ToggleGreyScale(false);
			ToggleExposure(false);
		}

		public void ToggleDepthOfField(bool toggle) {
			Toggle<DepthOfField>(toggle);
		}

		public void ToggleGreyScale(bool toggle) {
			_profile.GetSetting<ColorGrading>().saturation.overrideState = toggle;
		}

		public void ToggleExposure(bool toggle) {
			_profile.GetSetting<ColorGrading>().postExposure.overrideState = toggle;
		}

		public void ApplySettings() {
			VideoSettings settings = _dataService.Data.Settings.Video;

			Toggle<Bloom>(settings.Bloom);
			Toggle<ColorGrading>(settings.ColorGrading);
			Toggle<Vignette>(settings.Vignette);
			Toggle<AmbientOcclusion>(settings.AmbientOcclusion);

			QualitySettings.shadows = settings.Shadows ? ShadowQuality.All : ShadowQuality.Disable;
			QualitySettings.antiAliasing = settings.AntiAliasing ? 2 : 0;
		}

		private void Toggle<T>(bool toggle) where T : PostProcessEffectSettings {
			_profile.GetSetting<T>().enabled.value = toggle;
		}
	}
}
