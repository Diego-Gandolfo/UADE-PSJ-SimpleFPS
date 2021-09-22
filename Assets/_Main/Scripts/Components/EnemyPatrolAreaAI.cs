using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.Patrol
{
    public class EnemyPatrolAreaAI : MonoBehaviour
    {
        private PatrolArea patrolArea = null; // Componente de Patrullaje
        private FollowTarget followEnemy = null; // Componente de Persecusión
        private DetectTargetArea detectTargetArea = null; // Componente de Detección
        [SerializeField] private bool keepFollowing = false; // Si debe seguir persiguiendo al Objetivo una vez que se va del Area de Detección o si debe de volver al Patrullaje

        private void Start()
        {
            patrolArea = GetComponent<PatrolArea>(); // Detección del Componente PatrolPoints
            if (patrolArea == null) Debug.LogError("A " + gameObject.name + " le falta el Componente PatrolArea y el EnemyPatrolPointsAI no funcionara correctamente");
            followEnemy = GetComponent<FollowTarget>(); // Detección del Componente FollowEnemy
            if (patrolArea == null) Debug.LogError("A " + gameObject.name + " le falta el Componente FollowEnemy y el EnemyPatrolPointsAI no funcionara correctamente");
            detectTargetArea = GetComponent<DetectTargetArea>(); // Detección del Componente DetectTargetArea
            if (patrolArea == null) Debug.LogError("A " + gameObject.name + " le falta el Componente DetectTargetArea y el EnemyPatrolPointsAI no funcionara correctamente");

            patrolArea.enabled = true; // Inicializamos patrolPoints en TRUE para que arranque Patrullando
            followEnemy.enabled = false; // Inicializamos followEnemy en FALSE porque empieza Patrullando
        }

        private void FixedUpdate()
        {
            bool check = detectTargetArea.DetectTargets();

            if (check) // Si detecto un Objetivo
            {
                patrolArea.enabled = false; // Deja de Patrullar
                followEnemy.enabled = true; // Empieza a Perseguir
            }
            else if (!check && !keepFollowing) // Si ya no detecta enemigos en su campo y no debe seguir persiguiendo
            {
                followEnemy.enabled = false; // Deja de Perseguir
                patrolArea.enabled = true; // Vuelve a Patrullar
            }
        }
    }
}
