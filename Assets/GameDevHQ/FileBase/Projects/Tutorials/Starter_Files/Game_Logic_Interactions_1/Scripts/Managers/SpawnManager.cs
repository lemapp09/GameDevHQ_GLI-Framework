using System;
using GLI_Framework;
using OccaSoftware.BOP;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private  AI_Agent _aiPrefab;
        [SerializeField] public Pooler _objectPool;
        private Barriers _barrier;
        private bool _isGameOver = false;
        private static SpawnManager _instance;
        public static SpawnManager Instance { get { return _instance; } }

        private void OnEnable() {
            Actions.GameOver += OnGameOver;
        }

        private void OnGameOver()
        {
            _isGameOver = true;
        }

        private void Awake() {
            if (_instance != null && _instance != this) {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }

            _barrier = GameObject.FindGameObjectWithTag("Barriers").GetComponent<Barriers>();
            if (_barrier == null) {
                Debug.LogError("Barrier not found");
            }
        }

        private void Update() {
            if (_objectPool.GetPoolStats().PoolInactiveCount > 0 && !_isGameOver) {
                Spawn();
            }
        }

        public void Spawn() {
            var _currentBarrier = Random.Range(0, _barrier._barriers.Length - 2 );
            GameObject ai = _objectPool.GetFromPool(_barrier._barriers[_currentBarrier].transform.position, 
                Quaternion.identity,this.transform);
            ai.transform.position = _barrier._barriers[_currentBarrier].transform.position;
            ai.GetComponent<AI_Agent>()._currentBarrier = _currentBarrier;
        }

        private void OnDisable() {
            Actions.GameOver -= OnGameOver;
        }
    }
}