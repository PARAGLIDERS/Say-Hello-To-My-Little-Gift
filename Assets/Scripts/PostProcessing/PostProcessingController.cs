using Data;
using Root;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace PostProcessing {
	public class PostProcessingController {
		private readonly PostProcessProfile _profile;

		public PostProcessingController(PostProcessProfile profile) {
			_profile = profile;
			Core.DataController.OnSave += ApplySettings;
		}

		public void Dispose() {
			Core.DataController.OnSave -= ApplySettings;
			SetActiveDepthOfField(false);
			SetActiveGreyScale(false);
		}

		public void SetActiveDepthOfField(bool active) {
			_profile.GetSetting<DepthOfField>().enabled.value = active;
		}

		public void SetActiveGreyScale(bool active) {
			_profile.GetSetting<ColorGrading>().saturation.overrideState = active;
		}

		public void ApplySettings() {
			VideoSettings settings = Core.DataController.Data.Settings.Video;

			_profile.GetSetting<Bloom>().enabled.value = settings.Bloom;
			_profile.GetSetting<ColorGrading>().enabled.value = settings.ColorGrading;
			_profile.GetSetting<Vignette>().enabled.value = settings.Vignette;
			_profile.GetSetting<AmbientOcclusion>().enabled.value = settings.AmbientOcclusion;
			//_profile.GetSetting<MotionBlur>().enabled.value = data.Video.MotionBlur;
			_profile.GetSetting<Bloom>().enabled.value = settings.Bloom;

			QualitySettings.shadows = settings.Shadows ? ShadowQuality.All : ShadowQuality.Disable;
			QualitySettings.antiAliasing = settings.AntiAliasing ? 2 : 0;
		}
	}
}
