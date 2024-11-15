﻿using Data;
using DG.Tweening;
using Root;
using System;
using Ui;
using Ui.Screens;
using Ui.UiKit;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Screens {
	public class SettingsScreen : UiScreenBlurredBack {
		[SerializeField] private Button _backButton;
		[SerializeField] private Button _dropButton;

		[Header("Gameplay")]
		[SerializeField] private UiToggle _fpsCounter;
		[SerializeField] private UiToggle _cameraShaking;
		[SerializeField] private UiToggle _blood;

		[Header("Video")]
		[SerializeField] private UiToggle _antiAliasing;
		[SerializeField] private UiToggle _shadows;
		[SerializeField] private UiToggle _bloom;
		[SerializeField] private UiToggle _colorGrading;
		[SerializeField] private UiToggle _vignette;
		[SerializeField] private UiToggle _ambientOcclusion;

		[Header("Audio")]
		[SerializeField] private UiSlider _sfxVolume;
		[SerializeField] private UiSlider _musicVolume;

		[SerializeField] private AnimGroup[] _animGroups;

		[Serializable]
		private struct AnimGroup {
			public RectTransform[] Elements;
		}

		private SettingsData _data;

		// totally needs refactor
		public override void Init() {
			_backButton.onClick.AddListener(HandleBackButton);
			_dropButton.onClick.AddListener(HandleDropButton);

			_data = Core.DataController.Data.Settings;

			_fpsCounter.Init(_data.Gameplay.FpsCounter, value => {
				_data.Gameplay.FpsCounter = value;
				Save();
			});

			_cameraShaking.Init(_data.Gameplay.CameraShaking, value => {
				_data.Gameplay.CameraShaking = value;
				Save();
			});

			_blood.Init(_data.Gameplay.Blood, value => {
				_data.Gameplay.Blood = value;
				Save();
			});

			_antiAliasing.Init(_data.Video.AntiAliasing, value => {
				_data.Video.AntiAliasing = value;
				Save();
			});

			_shadows.Init(_data.Video.Shadows, value => {
				_data.Video.Shadows = value;
				Save();
			});

			_bloom.Init(_data.Video.Bloom, value => {
				_data.Video.Bloom = value;
				Save();
			});

			_colorGrading.Init(_data.Video.ColorGrading, value => {
				_data.Video.ColorGrading = value;
				Save();
			});

			_vignette.Init(_data.Video.Vignette, value => {
				_data.Video.Vignette = value;
				Save();
			});

			_ambientOcclusion.Init(_data.Video.AmbientOcclusion, value => {
				_data.Video.AmbientOcclusion = value;
				Save();
			});

			SliderSetting sfxVolume = _data.Audio.SfxVolume;
			_sfxVolume.Init(sfxVolume.Current, sfxVolume.Min, sfxVolume.Max, value => {
				_data.Audio.SfxVolume.Current = value;
				Save();
			});

			SliderSetting musicVolume = _data.Audio.MusicVolume;
			_musicVolume.Init(musicVolume.Current, musicVolume.Min, musicVolume.Max, value => {
				_data.Audio.MusicVolume.Current = value;
				Save();
			});
		}

		private void Save() {
			Core.DataController.Save();
		}

		private void HandleBackButton() {
			Core.DataController.Save();
			Core.UiController.HideCurrent();
		}

		private void HandleDropButton() {
			Core.DataController.Data.DropSettingsData();
			Core.DataController.Save();
			Core.UiController.Show(UiScreenType.Settings);
		}

		protected override void PlayEnterAnim() {
			base.PlayEnterAnim();

			Sequence seq = DOTween.Sequence();

			for (int i = 0; i < _animGroups.Length; i++) {
				for (int j = 0; j < _animGroups[i].Elements.Length; j++) {
					RectTransform element = _animGroups[i].Elements[j];
					element.localScale = Vector3.zero;
					seq.Insert(Constants.UiAnimDelay + Constants.UiAnimInterval * j, element
						.DOScale(1f, Constants.UiAnimDuration)
						.SetEase(Ease.OutBack));
				}
			}

			_backButton.transform.localScale = Vector3.zero;
			_dropButton.transform.localScale = Vector3.zero;

			seq.Append(_backButton.transform.DOScale(1f, Constants.UiAnimDuration).SetEase(Ease.OutBack));
			seq.Append(_dropButton.transform.DOScale(1f, Constants.UiAnimDuration).SetEase(Ease.OutBack));

			seq.SetUpdate(true);
		}
	}
}
