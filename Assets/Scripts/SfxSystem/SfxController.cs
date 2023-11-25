using System.Collections.Generic;
using UnityEngine;

namespace SfxSystem {
    public class SfxController {
        private readonly Queue<Sfx> _sounds;
        private readonly Transform _container;
        private readonly Dictionary<SfxType, SfxConfigItem> _soundsDictionary;

        public SfxController(Transform parent, SfxConfig config) {
            _container = new GameObject("Sounds").transform;
            _container.SetParent(parent);
            
            _sounds = new Queue<Sfx>();
            for (int i = 0; i < 500; i++) {
                GameObject sfxGo = new GameObject("sfx");
                
                Sfx sfx = sfxGo.AddComponent<Sfx>();
                AudioSource source = sfxGo.AddComponent<AudioSource>();
                source.outputAudioMixerGroup = config.MixerGroup;
                source.playOnAwake = false;

                sfx.transform.SetParent(_container);
                sfx.Init(source);
                
                _sounds.Enqueue(sfx);
            }

            _soundsDictionary = new Dictionary<SfxType, SfxConfigItem>();
            foreach (SfxConfigItem item in config.Items) {
                if (_soundsDictionary.ContainsKey(item.Type)) {
                    Debug.LogError($"sound already exists in dictionary: {item.Type}");
                    continue;
                }

                _soundsDictionary.Add(item.Type, item);
            }
        }

        public void Play(SfxType type, Vector3? position = null) {
            if(!_soundsDictionary.TryGetValue(type, out SfxConfigItem item)) {
                Debug.LogError($"sound is not found in dictionary: {type}");
                return;
            }

            Sfx sfx = _sounds.Dequeue();
            if (position != null) sfx.transform.position = position.Value;
            sfx.Play(item.Clip, item.Volume, item.PitchShift, is3d: position != null);

            _sounds.Enqueue(sfx);
        }

        public void Dispose() {
            Object.Destroy(_container.gameObject);
            _sounds.Clear();
            _soundsDictionary.Clear();
        }
    }
}
