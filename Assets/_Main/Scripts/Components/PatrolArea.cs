using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.Patrol
{
    public class PatrolArea : MonoBehaviour
    {
        [Header("Enemy Settings")]
        [SerializeField] private Vector2 enemySize = new Vector2(0, 0); // El tamaño del Enemigo

        [Header("Patrol Settings")]
        [SerializeField] private float movemetSpeed = 3.0f; // La velocidad que se desplaza al estar patrullando
        [SerializeField] private float waitTime = 1.0f; // El tiempo que espera hasta en un punto antes de empezar a moverse al siguiente
        private float timer = 0.0f; // Variable que usaremos para llevar el control del tiempo
        [SerializeField] private float minDistance = 0; // Distancia minima que tiene que haber entre al
        private GameObject patrolPosition = null; // Lo usaremos para asignar la posicion actual a la que debemos movernos

        [Header("Patrol Area Settings")]
        [SerializeField] private Transform patrolCenter = null; // Variable donde almacenaremos el centro del punto a patrullar
        [SerializeField] private Vector2 areaSize = new Vector2(0, 0); // Cuantas unidades se puede mover hacia la izquierda de patrolPoint
        private float minX = 0; // Al Activar el Componente o el Objeto se almacenará el valor Mínimo del Area para X
        private float maxX = 0; // Al Activar el Componente o el Objeto se almacenará el valor Máximo del Area para X
        private float minY = 0; // Al Activar el Componente o el Objeto se almacenará el valor Mínimo del Area para Y
        private float maxY = 0; // Al Activar el Componente o el Objeto se almacenará el valor Máximo del Area para Y

        [Header("Raycast Settings")]
        [SerializeField] private float rayDistance = 0f; // La distancia que vamos a comprobar si se choca con algo
        [SerializeField] private LayerMask rayLayerMask = 0; // Que Layers tiene que detectar
        [SerializeField] private float rayOffset = 0f; // Que Layers tiene que detectar

        private void Awake()
        {
            if (patrolCenter == null) patrolCenter = transform;
        }

        private void OnEnable() // Cada vez que se activa el Objeto o el Componente
        {
            patrolPosition = new GameObject("Patrol Position"); // Creamos un nuevo GameObject que usaremos para determinar los puntos a los que debemos movernos
            patrolPosition.SetActive(false); // Desactivamos el GameObject patroPosition

            minX = (areaSize.x / -2) + patrolCenter.position.x; // Al Activar el Componente o el Objeto se almacenamos el valor Mínimo para X
            maxX = (areaSize.x / 2) + patrolCenter.position.x; // Al Activar el Componente o el Objeto se almacenamos el valor Máximo para X
            minY = (areaSize.y / -2) + patrolCenter.position.y; // Al Activar el Componente o el Objeto se almacenamos el valor Mínimo para Y
            maxY = (areaSize.y / 2) + patrolCenter.position.y; // Al Activar el Componente o el Objeto se almacenamos el valor Máximo para Y

            patrolPosition.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)); // Asignamos el punto siguiente al que nos vamos a desplazar
            timer = waitTime; // Inicializamos el contador al tiempo de espera deseado
        }

        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPosition.transform.position, movemetSpeed * Time.deltaTime); // Nos movemos al punto indicado
            
            if (Vector2.Distance(transform.position, patrolPosition.transform.position) < 0.2f) // Nos fijamos si ya estamos cerca del punto indicado (randomPoint)
            {
                if (timer <= 0) // Comprobamos si ya paso el tiempo de espera deseado
                {
                    patrolPosition.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)); // Asignamos el punto siguiente al que nos vamos a desplazar

                    while (Vector2.Distance(transform.position, patrolPosition.transform.position) < minDistance) // Nos fijamos si la distancia del nuevo punto supera la Distancia Minima
                    {
                        patrolPosition.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)); // Asignamos el punto siguiente al que nos vamos a desplazar
                    }

                    timer = waitTime; // Reiniciamos el Contador
                }
                else
                {
                    timer -= Time.deltaTime; // Si el tiempo no paso, vamos restando Time.deltaTime
                }
            }
        }

        private void FixedUpdate()
        {
            Vector2 rayPosition = transform.position + (patrolPosition.transform.position - transform.position).normalized * rayOffset;
            Vector2 rayDirection = (patrolPosition.transform.position - transform.position).normalized;

            RaycastHit2D raycast = Physics2D.Raycast(rayPosition, rayDirection, rayDistance, rayLayerMask);
            Debug.DrawRay(rayPosition, rayDirection * rayDistance, Color.blue);

            if (raycast)
                patrolPosition.transform.position = transform.position;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null) rb.velocity = Vector2.zero;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (patrolCenter != null) Gizmos.DrawWireCube(patrolCenter.position, new Vector3(areaSize.x + enemySize.x, areaSize.y + enemySize.y, 0)); // Dibujamos un Gizmo para representar el Area donde va a patrullar
        }

        private void OnDisable()
        {
            Destroy(patrolPosition); // Al Desactivar el Objeto o el Componente destruimos el patrolPosition
        }
    }
}
