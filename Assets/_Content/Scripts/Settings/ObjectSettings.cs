/**
 * Структура настройки игровых объектов
 */

using System;
using UnityEngine;

namespace _Content.Scripts.Settings
{
    [Serializable]
    public struct ObjectSettings
    {
        [Tooltip("Префаб")]
        public GameObject prefab;
        [Tooltip("Максимальное количество похожих объектов на одном сегменте карты")]
        public int maxCount;
        [Range(0, 1)]
        [Tooltip("Шанс того, что объект появится при обновлении сегмента")]
        public float spawnChance;

        [Tooltip("Начальная точка расположения объекта (от центра сегмента)")]
        public float startSpawnXPosition;
        [Tooltip("Конечная точка расположения объекта (от центра сегмента)")]
        public float endSpawnXPosition;
    }
}