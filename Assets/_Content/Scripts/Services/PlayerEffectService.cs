/**
 *  Фабрика и кэш эффектов, чтобы не создавать эффекты для каждого Collidable объекта снова, а использовать уже ранее созданные.
 *  Фабрику можно передать игроку и, получая при столкновении с объектом тип объекта, накладывать нужный эффект
 *  Тогда в теории это станет частью игрока.
 *  Но в задаче сказано не изменять класс или классы игрока. Поэтому фабрика передается созданным Collidable объектам.
 */

using System.Collections.Generic;
using _Content.Scripts.Game.PlayerEffects;
using _Content.Scripts.Settings;

namespace _Content.Scripts.Services
{
    public class PlayerEffectService
    {
        private readonly Dictionary<EPlayerEffectType, AEffect> _effectCache;
        private readonly PlayerEffectSettings _settings;

        public PlayerEffectService(PlayerEffectSettings settings)
        {
            _settings = settings;
            _effectCache = new Dictionary<EPlayerEffectType, AEffect>();
        }

        //Получение нужного эффекта из списка
        //Если эффекта в списке нет, он создается
        public AEffect GetEffect(EPlayerEffectType type)
        {
            if (type == EPlayerEffectType.None) return null;
            
            if (_effectCache.ContainsKey(type))
                return _effectCache[type];

            var effect = CreateEffect(type);
            _effectCache.Add(type, effect);
            return effect;
        }

        //Создание класса эффекта по выбранному типу.
        private AEffect CreateEffect(EPlayerEffectType type) => type switch
        {
            EPlayerEffectType.BoostSpeed => 
                new SpeedBoostEffect(_settings.SpeedBoostValue, _settings.SpeedBoostAcceleration, _settings.SpeedBoostDuration),
            EPlayerEffectType.SlowDownSpeed => 
                new SpeedBoostEffect(_settings.SlowdownValue, _settings.SlowdownAcceleration, _settings.SlowdownDuration),
            EPlayerEffectType.Fly => 
                new FlyEffect(_settings.FlyHeight, _settings.FlySpeed, _settings.FlyDuration),
        };
    }
}