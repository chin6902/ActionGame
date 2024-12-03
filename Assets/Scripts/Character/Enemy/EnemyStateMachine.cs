using System;
using UnityEngine;
using UnityEngine.AI;

namespace ActionGame
{
    public class EnemyStateMachine : StateMachine2
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
        [field: SerializeField] public NavMeshAgent Agent { get; private set; }
        [field: SerializeField] public EnemyDamage EnemyDamage { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public Target Target { get; private set; }
        [field: SerializeField] public GameObject EnemyVisual { get; private set; }
        [field: SerializeField] public GameObject Phase2Visual { get; private set; }
        [field: SerializeField] public GameObject DropVisual { get; private set; }
        [field: SerializeField] public bool isDragon { get; private set; }
        [field: SerializeField] public bool canScream { get; set; }
        [field: SerializeField] public float hitTakable { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float PlayerChasingRange { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float AttackRange2 { get; private set; }
        [field: SerializeField] public int AttackDamage { get; private set; }
        [field: SerializeField] public int AttackDamage2 { get; private set; }
        [field: SerializeField] public float idleDuration { get; private set; }
        [field: SerializeField] public float wanderDistanceMax { get; private set; }
        [field: SerializeField] public float wanderDistanceMin { get; private set; }
         public float AttackCount { get; set; }

        public GameObject Player { get; private set; }

        private float hitTaken = 0f;

        public event Action OnStartScream;
        public event Action OnStopScream;

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");

            Agent.updatePosition = false;
            Agent.updateRotation = false;

            SwitchState(new EnemyIdleState(this));
        }

        private void OnEnable()
        {
            Health.OnTakeDamage += HandleTakeDamage;
            Health.OnDie += HandleDie;
        }

        private void OnDisable()
        {
            Health.OnTakeDamage -= HandleTakeDamage;
            Health.OnDie -= HandleDie;
        }

        private void HandleTakeDamage()
        {
            hitTaken += 1f; 

            if (hitTaken >= hitTakable)
            {
                SwitchState(new EnemyImpactState(this));  
                hitTaken = 0f; 
            }
        }

        private void HandleDie()
        {
            SwitchState(new EnemyDeathState(this));
        }

        public void TriggerStartScream()
        {
            OnStartScream?.Invoke();
        }
        public void TriggerStopScream()
        {
            OnStopScream?.Invoke();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
        }
    }
}
