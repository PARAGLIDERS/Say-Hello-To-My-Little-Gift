using System.Collections.Generic;
using UnityEngine;

public abstract class ItemsConfig<T> : ScriptableObject where T : ItemsConfigItem {
	public List<T> Items;

	private void OnValidate() {
		HashSet<string> names = new HashSet<string>();

		foreach (T item in Items) {
			if (names.Add(item.Name))
				continue;
			Debug.LogError($"{item.Name} is already added!");
		}
	}
}

public abstract class ItemsConfigItem {
	public abstract string Name { get; }
}