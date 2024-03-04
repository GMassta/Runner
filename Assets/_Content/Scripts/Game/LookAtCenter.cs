/**
 * Поворот камеры в центр экрана
 */
using UnityEngine;

namespace _Content.Scripts.Game
{
    public class LookAtCenter: MonoBehaviour
    {
        
        [SerializeField] private float depth = 300;
        private Camera camera;

        private void Awake()
        {
            camera = GetComponent<Camera>();
        }

        private void Update()
        {
            var target = camera.transform.position + camera.transform.forward * depth;
            target.x = 0;
            camera.transform.LookAt(target);
        }
    }
}