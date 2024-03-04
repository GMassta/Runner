/**
 *  Конфиг карты
 */
using UnityEngine;

namespace _Content.Scripts.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [Tooltip("Длинна одного сегмента карты")]
        public float segmentLength = 500;
        [Tooltip("Ширина дороги (лимит перемещения для персонажа)")]
        public float roadWide = 8f;
        [Tooltip("Массив сегментов карты")]
        public GameObject[] segments;
    }
}