using DG.Tweening;
using UnityEngine;

namespace GunSystem.ConcreteGuns {
	public class DoubleShotgun : Gun {
		[SerializeField] private Transform _body;
		[SerializeField] private Vector3 _orientation;

		protected override void OnShoot() {
			base.OnShoot();
			Vector3 targetRotation = _orientation * 360;
			_body.DOLocalRotate(targetRotation, 1f / _config.FireRate, RotateMode.LocalAxisAdd)
				.SetEase(Ease.OutBack);
		}
	}
}
