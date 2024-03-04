/**
 * Класс создает все объекты для выбранного сегмента карты.
 */

using VContainer;
using UnityEngine;
using _Content.Scripts.Settings;
using System.Collections.Generic;
using System.Linq;
using MapGenerator.WorldObjects;
using Random = UnityEngine.Random;

namespace _Content.Scripts.Game
{
    public class SegmentObjects: MonoBehaviour
    {
        private const string PARENT_NAME = "Objects";
        [SerializeField] private ObjectSettings[] _objects;
        
        [Inject] private readonly GameSettings _settings;
        [Inject] private readonly ObjectCache _viewCache;

        private Transform _spawnParent;
        private IList<GameObject> _usedViews;

        public void Awake()
        {
            //Новый объект для группировки
            _spawnParent = new GameObject(PARENT_NAME).transform;
            _spawnParent.parent = transform;
            
            _viewCache.Parent = _spawnParent;
            _usedViews = new List<GameObject>();
        }

        public void Rebuild()
        {
            //Очистка сегментf от объектов
            Clear();
            
            //Расставление объектов в случайные места на сегменте
            foreach (var setting in _objects)
            {
                for (var i = 0; i < setting.maxCount; i++)
                {
                    var rnd = Random.Range(0f, 1f);
                    if(setting.spawnChance < rnd) continue;

                    var obj = _viewCache.GetOrCreateObject(setting.prefab);
                    _usedViews.Add(obj);

                    SetRandomPosition(obj, setting.startSpawnXPosition, setting.endSpawnXPosition);
                }
            }
        }

        private void Clear()
        {
            foreach (var obj in _usedViews)
                _viewCache.ReturnToCache(obj.name, obj);

            _usedViews.Clear();
        }

        //Случайная позиция для объекта
        private void SetRandomPosition(GameObject obj, float start, float end)
        {
            var xSide = Random.Range(0, 2) == 1 ? 1 : -1;
            var halfSegmentLength = _settings.segmentLength * .5f;
            obj.transform.localPosition = 
                new Vector3(Random.Range(start, end) * xSide, 0, 
                    Random.Range(-halfSegmentLength, halfSegmentLength));
        }
    }
}