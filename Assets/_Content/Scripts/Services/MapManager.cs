/**
 * Создание сегментов карты и обновление позиции последнего сегмента
 * Если сегмент удаляется от позиции персонажа на длину сегмента, то он перемещается вперед
 * И заново переставляет все объекты на карте
 */

using _Content.Scripts.Game;
using _Content.Scripts.Settings;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Content.Scripts.Services
{
    public class MapManager: IInitializable, ITickable
    {
        private readonly IObjectResolver _container;

        private readonly GameSettings _settings;
        private readonly Player _player;

        private Transform _mapTransform;
        private SegmentObjects[] _segments;

        private float _lastSegmentPosition;

        public MapManager(IObjectResolver container, Player player, GameSettings settings)
        {
            _player = player;
            _settings = settings;
            _container = container;
        }

        public void Initialize()
        {
            var count = _settings.segments.Length;
            
            _mapTransform = new GameObject("Map").transform;
            _segments = new SegmentObjects[count];
            
            
            //Спаун всех добавленных сегментов
            for (var i = 0; i < count; i++)
            {
                var go = _container.Instantiate(_settings.segments[i], _mapTransform);
                go.transform.position = Vector3.forward * _lastSegmentPosition;
                _lastSegmentPosition += _settings.segmentLength;
                
                var segment = go.GetComponent<SegmentObjects>();
                _segments[i] = segment;
                segment.Rebuild();
            }
        }

        //Проверка, вышел ли игрок с крайнего сегмента.
        //Если вышел, сегмент перемещается вперед
        public void Tick()
        {
            foreach (var segment in _segments)
            {
                if (segment.transform.position.z + _settings.segmentLength < _player.transform.position.z)
                {
                    segment.transform.position = Vector3.forward * _lastSegmentPosition;
                    _lastSegmentPosition += _settings.segmentLength;
                    
                    segment.Rebuild();
                }
            }
        }
    }
}