using DayNightCycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DayNightCycle {
    public class DayNightViewer : MonoBehaviour {
        [SerializeField] private DayNightConfig _config;
        
        private DayNightController _controller;

        private void Awake() {
            _controller = new DayNightController(transform, _config);
        }

        private void Start() {
            _controller.Start();
        }

        private void Update() {
            _controller.Update();
        }
    }
}
