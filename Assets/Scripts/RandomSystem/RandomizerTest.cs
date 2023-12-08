#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace RandomSystem {
    public class RandomizerTest {
        [MenuItem("Santa/Test/Randomizer")]
        public static void Run() {
            const int itemsCount = 10;
            const int maxItemChance = 101;
            const int iterationsCount = 10;

            List<TestRandomizerItem> testRandomizerItems = new List<TestRandomizerItem>() {
                //new TestRandomizerItem(0, 10),
                //new TestRandomizerItem(1, 10),
                //new TestRandomizerItem(2, 10),
                //new TestRandomizerItem(3, 10),
                //new TestRandomizerItem(4, 10),
            };

            for (int i = 0; i < itemsCount; i++) {
                int chance = Random.Range(0, maxItemChance);
                TestRandomizerItem item = new TestRandomizerItem(i, chance);
                testRandomizerItems.Add(item);
            }

            Randomizer<TestRandomizerItem> randomizer = new Randomizer<TestRandomizerItem>(testRandomizerItems);

            for (int i = 0; i < iterationsCount; i++) {
                TestRandomizerItem item = randomizer.GetItem();
                testRandomizerItems[item.Index].AddTestIteration();
            }

            int sum = testRandomizerItems.Sum(x => x.Chance);
            for (int i = 0; i < testRandomizerItems.Count; i++) {
                float chance = 100 * (float) testRandomizerItems[i].Chance / sum;
                float result = 100 * (float) testRandomizerItems[i].TestIterations / iterationsCount;
                Debug.LogError($"chance: {chance}% | result: {result}%");
            }

            Debug.LogError("randomizer test finished, check a look");
        }

        private class TestRandomizerItem : IRandomizerItem {
            private int _testIterations;
            private int _index;
            private int _chance;

            public int Index => _index;
            public int Chance => _chance;
            public int TestIterations => _testIterations;

            public TestRandomizerItem(int index, int chance) {
                _index = index;
                _chance = chance;
            }

            public void AddTestIteration() {
                _testIterations++;
            }
        }
    }
}
#endif