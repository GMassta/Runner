/**
 *  Настройки эффектов действующих на игрока
 *  Т.к. все эффекты кэшируются в одном месте настройку также реализовал общую
 *  Это упростит настройку и облегчит CollidableObject,
 *  Но не позволит использовать один и тот же эффект с разными параметрами в одном забеге
 *  Для этого можно было вынести настройки отдельно и получать их с конкретного CollidableObject
 *  Или добавить сюда списки конфигов, и с них уже формировать конкретные эффекты
 */

using UnityEngine;

namespace _Content.Scripts.Settings
{
    [CreateAssetMenu(fileName = "PlayerEffectSettings", menuName = "Settings/PlayerEffectSettings", order = 0)]
    public class PlayerEffectSettings : ScriptableObject
    {
        [Header("Speed Boost Settings")] 
        [InspectorName("Duration")]      public float SpeedBoostDuration = 10;
        [InspectorName("Acceleration")]  public float SpeedBoostAcceleration = 2;
        [InspectorName("Speed")]         public float SpeedBoostValue = 3;
        [Space(10)]
        [Header("Slowdown Settings")] 
        [InspectorName("Duration")]      public float SlowdownDuration = 10;
        [InspectorName("Acceleration")]  public float SlowdownAcceleration = 1;
        [InspectorName("Speed")]         public float SlowdownValue = .6f;
        [Space(10)]
        [Header("Fly Settings")] 
        [InspectorName("Duration")]      public float FlyDuration = 10;
        [InspectorName("Speed")]         public float FlySpeed = 3;
        [InspectorName("Height")]        public float FlyHeight = 5;
    }
}