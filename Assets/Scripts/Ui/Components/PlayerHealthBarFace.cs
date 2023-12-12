using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.Components {
	public class PlayerHealthBarFace : MonoBehaviour {
		[SerializeField] private List<PlayerFace> _items;

		public void Init() {

		}
	}

	[Serializable]
	public struct PlayerFace {
		[SerializeField] private Sprite _sprite;
		[SerializeField] private int _value;

		public Sprite Sprite => _sprite;
		public int Value => _value;
	}
}
