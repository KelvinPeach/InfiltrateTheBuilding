using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Infiltrate
{
    public abstract class Enemy : MonoBehaviour
    {
        [Header("Speeds")]
        public float normalSpeed = 2f;
        public float chaseSpeed = 8f;
        [Header("Attributes")]
        public bool canBeDistracted = true;
        [Header("Field Of View")]
        public Material fovNormalMaterial;
        public Material fovCautionMaterial;
        public Material fovEvasionMaterial;
        public Material fovAlertMaterial;

        // Events
        public delegate void OnPlayerCaught();
        public static event OnPlayerCaught onPlayerCaught;

        // Core Enemy States
        #region State Machine
        public EnemyBaseState CurrentState { get; protected set; }
        public EnemyBaseState StartingEnemyState { get; protected set; }

        public readonly EnemyIdleState idleState = new EnemyIdleState();
        public readonly EnemyCombatState combatState = new EnemyCombatState();
        public readonly EnemySearchState searchState = new EnemySearchState(); // MGS Evasion: searches around the area (using invisible waypoints) for the player
        public readonly EnemyReturnState returnState = new EnemyReturnState();
        public readonly EnemyInvestigateState investigateState = new EnemyInvestigateState(); // E.g. investigation a noise

        // Vars to reset to initial state
        public Vector3 StartingPosition { get; private set; }
        public Quaternion StartingRotation { get; private set; }
        public Investigation CurrentInvestigation { get; private set; }
        #endregion

        // Cache
        public NavMeshAgent Agent { get; private set; }
        public FieldOfView Fov { get; private set; }
        public MeshRenderer FovRen { get; private set; }

        protected virtual void Awake()
        {
            // Cache
            Agent = GetComponent<NavMeshAgent>();
            Fov = GetComponent<FieldOfView>();
            FovRen = Fov.viewMeshFilter.GetComponent<MeshRenderer>();
        }

        protected virtual void Start()
        {
            StartingPosition = transform.position;
            StartingRotation = transform.rotation;
        }

        protected virtual void Update()
        {
            // If the guard sees the player, they should always transition to combat mode (unless they are already in combat mode)
            if (CurrentState != combatState)
            {
                // Has our field of vision cone seen the player?
                if (Fov.visibleTargets.Count > 0)
                {
                    // Is it the player?
                    PlayerDisguise playerDisguise = Fov.visibleTargets[0].GetComponent<PlayerDisguise>(); // Assumes there will only be one player

                    if (playerDisguise)
                    {
                        // Is the player correctly disguised?
                        if (playerDisguise.IsTresspassing()) // If the player doesn't have a disguise component, go to combat state
                        {
                            TransitionToState(combatState);
                        }
                        // Is the player performing an illegal action (for their current disguise)?
                        else
                        {
                            PlayerInteract playerInteract = playerDisguise.GetComponent<PlayerInteract>();

                            if (playerInteract && playerInteract.IsPerformingIllegalAction())
                            {
                                TransitionToState(combatState);
                            }
                        }
                    }
                }
            }

            CurrentState.Update(this);
        }

        void OnTriggerEnter(Collider other)
        {
            // Did we collide with the player?
            if (other.CompareTag("Player"))
            {
                if (onPlayerCaught != null)
                    onPlayerCaught();
            }

            // If the enemy is currently attacking the player, they shouldn't consider any other stimuli
            // Also if 'canBeDistracted' is set to false
            if (CurrentState != combatState && canBeDistracted)
            {
                // Heard a noise (e.g. the player whilsting)?
                Noise noise = other.GetComponent<Noise>();

                if (noise)
                {
                    CurrentInvestigation = new Investigation();
                    CurrentInvestigation.location = other.transform.position;
                    CurrentInvestigation.danger = other.GetComponent<Noise>().Danger; // Respond with different levels of urgency (e.g. gunshot vs smashed window)

                    TransitionToState(investigateState);
                }
            }
        }

        public void TransitionToState(EnemyBaseState newState)
        {
            CurrentState = newState;
            CurrentState.EnterState(this);
        }

        public void SetSpeed(float newSpeed)
        {
            Agent.speed = newSpeed;
        }
    }

    public enum AlertStatus { Normal, Caution, Evasion, Alert }

    public class Investigation
    {
        public Vector3 location;
        public AlertStatus danger;
    }
}