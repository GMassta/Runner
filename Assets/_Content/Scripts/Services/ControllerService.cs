/**
 * Вынес управление персонажем отдельно, чтобы можно было изменить для разных устройств не изменяя поведение персонажа
 */

using UnityEngine;
using VContainer.Unity;
using Input = UnityEngine.Input;

namespace _Content.Scripts.Services
{
    public class ControllerService: IStartable, ITickable
    {
        private float _screenCenter;
        private float _horizontal;
        
        public float Horizontal => _horizontal;
        
        public void Start()
        {
            //Центр экрана для определения, с какой стороны игрок нажал на экран
            _screenCenter = Screen.width * .5f;
        }
        
        public void Tick()
        {
            //Управление для мобильной версии и игры с редактора 
#if UNITY_ANDROID && !UNITY_EDITOR
            _horizontal = 0f;
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase is TouchPhase.Stationary or TouchPhase.Moved)
                    _horizontal = Input.touches[0].position.x > _screenCenter? 1f: -1f;
            }
#else
            _horizontal = Input.GetAxis("Horizontal");
#endif
        }
    }
}