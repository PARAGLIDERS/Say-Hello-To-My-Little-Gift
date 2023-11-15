using System.Threading.Tasks;
using UnityEngine;

namespace Misc.Root {
	public class Bootstrap : MonoBehaviour {
		[SerializeField] private Resources _resources;
		
		public static Bootstrap INSTANCE;

		private void Awake() {
			if (INSTANCE) {
				Destroy(gameObject);
				return;
			}

			INSTANCE = this;
			DontDestroyOnLoad(gameObject);

			Core.Init(transform, _resources);
		}

		private void Update() {
			Core.Update();
		}
	}
}