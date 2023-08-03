using System;
using System.Collections;
using OccaSoftware.BOP;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace GLI_Framework
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AI_Agent : MonoBehaviour
    {
        [SerializeField] private float _timeDelayLow = 5f;
        [SerializeField] private float _timeDelayHigh = 10f;
        private Animator _anim;
        

        enum AI_States
        {
            Run,
            Hide,
            Death
        }
        private  NavMeshAgent _agent;
        private Barriers _barrier;
        [Range(1,19)]
        [SerializeField]
        private int _currentBarrier = 1;
        private AI_States _currentState = AI_States.Run;
        
        void Awake() {
            _anim = GetComponent<Animator> ();
            _barrier = GameObject.FindGameObjectWithTag("Barriers").GetComponent<Barriers>();
            if (_barrier == null) {
                Debug.LogError("Barrier not found");
            }
        }

        private void OnEnable()
        {
            this.transform.position = _barrier._barriers[0].transform.position;
            _agent = this.GetComponent<NavMeshAgent>();
            _agent.enabled = true;
            _agent.SetDestination(_barrier._barriers[1].transform.position);
        }

        private void Update() {
            if (_currentState == AI_States.Run)
            {
                if (_agent.remainingDistance < .05f)
                {
                    StartCoroutine(Hiding());
                    _currentBarrier++;
                    if (_currentBarrier < _barrier._barriers.Length)
                    {
                        _agent.SetDestination(_barrier._barriers[_currentBarrier].transform.position);
                    }
                    else
                    {
                        var PoolInstance = GetComponent<Instance>();
                        if (PoolInstance != null)
                        {
                            if (PoolInstance.GetPoolerOrigin() != null)
                            {
                                PoolInstance.Despawn();
                            }
                        }
                    }
                }
            }
            _anim.SetFloat("Speed", _agent.velocity.magnitude);
        }
        
        private IEnumerator Hiding() {
            _currentState = AI_States.Hide;
            _anim.SetBool("Hiding" , true);
            float timeDelay = Random.Range(_timeDelayLow, _timeDelayHigh);
            float currenTime = Time.time;
            yield return new WaitForSeconds(timeDelay);
            float passedTime = currenTime - Time.time;
            _currentState = AI_States.Run;
            _anim.SetBool("Hiding" , false);
        }

        public void Death()
        {
            _anim.SetBool("Death" , true);
            _currentState = AI_States.Death;
            Singleton.Instance.GameManager.AddScore(50);
        }
    }
}