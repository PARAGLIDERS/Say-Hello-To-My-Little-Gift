using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

namespace RandomSystem {
    public class Randomizer<TItem> where TItem : IRandomizerItem {
        private readonly List<TItem> _items;
        private readonly int _sum;

        public Randomizer(List<TItem> items) { 
            _items = items;
            _sum = 0;

            for (int i = 0; i < _items.Count; i++) {
                _sum += _items[i].Chance;
            }
        }
                
        public TItem GetItem() {
            if(_items.Count == 0) {
                Debug.LogError("no items in randomizer!");
                return default;
            }

            int random = Random.Range(0, _sum + 1);
            int prev = 0;

            for (int i = 0; i < _items.Count; i++) {
                if (_items[i].Chance + prev >= random) {
                    return _items[i];
                }

                prev += _items[i].Chance;
            }

            Debug.LogError("randomizer is broken!");
            return default;
        }
    }

    public interface IRandomizerItem {
        int Chance { get; }
    }
}
