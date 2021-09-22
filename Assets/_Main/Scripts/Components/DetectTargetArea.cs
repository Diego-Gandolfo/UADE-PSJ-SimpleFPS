using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.Patrol
{
    public class DetectTargetArea : MonoBehaviour
    {
        [SerializeField] private Transform detectionCenterPoint = null; // Marcamos el Centro en torno al que va a Detectar
        [SerializeField] private float detectionRange = 1.0f; // El Rango del Area de Detección
        [SerializeField] private LayerMask targetsLayerMask = 0; // Que LayerMasks tiene que Detectar
        private Collider2D[] targetsDetected = null;

        public bool DetectTargets()
        {
            targetsDetected = Physics2D.OverlapCircleAll(detectionCenterPoint.position, detectionRange, targetsLayerMask); // Guardamos en un Array todos los Objetos que tienen el LayerMask asignado

            if (targetsDetected.Length > 0) return (true); // Si el tamaño del Array es MAYOR a 0, es que encontro algo y devuelve TRUE
            else return (false); // Sino es que no encontro nada y devuelve FALSE
        }

        private void OnDrawGizmosSelected()
        {
            if (detectionCenterPoint != null) Gizmos.DrawWireSphere(detectionCenterPoint.position, detectionRange); // Esto es para dibujar donde está el Overlap
        }
    }
}
