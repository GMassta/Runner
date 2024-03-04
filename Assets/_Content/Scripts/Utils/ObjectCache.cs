/**
 * Кэш объектов, чтобы не инстансить их заново каждый раз
 */

using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MapGenerator.WorldObjects
{
    public class ObjectCache
    {
        public Transform Parent { private get; set; }
        
        private readonly IObjectResolver _container;
        private readonly Dictionary<string, List<GameObject>> _cache;

        public ObjectCache(IObjectResolver container)
        {
            _cache = new Dictionary<string, List<GameObject>>();
            _container = container;
        }

        //Возвращает объект из кэша или создает новый, если в кэше нет свободных объектов
        //Объект автоматически активируется
        public GameObject GetOrCreateObject(GameObject prefab)
        {
            var name = prefab.name;
            GameObject temp = null;
            if (_cache.ContainsKey(name) && _cache[name] != null && _cache[name].Count > 0)
            {
                temp = _cache[name][0];
                _cache[name].RemoveAt(0);
            }
            else
            {
                temp = _container.Instantiate(prefab, Parent);
            }

            if (temp == null) return temp;
            temp.name = name;
            temp.SetActive(true);

            return temp;
        }

        //Возврат объекта в кэш. Возвращенный объект деактивируется.
        public void ReturnToCache(string name, GameObject target)
        {
            target.SetActive(false);
            if(!_cache.ContainsKey(name)) _cache.Add(name, new List<GameObject>());
            _cache[name].Add(target);
        }
    }
}