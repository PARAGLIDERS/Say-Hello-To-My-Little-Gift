using EnemySpawning;
using GunSystem;
using Root;
using System.Collections.Generic;
using TMPro;
using Ui.Components;
using UnityEngine;

namespace Ui.Screens {
	public class UiScreenHud : UiScreen {
        [SerializeField] private GunHudIconsPanel _gunPanel;

		public override void Init() {
            _gunPanel.Init();
		}
	}
}