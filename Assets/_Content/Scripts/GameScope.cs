/**
 *  LifeTime Scope для внедрения зависимостей
 */

using _Content.Scripts.Services;
using _Content.Scripts.Settings;
using MapGenerator.WorldObjects;
using UnityEngine;

using VContainer;
using VContainer.Unity;

namespace _Content.Scripts
{
    public class GameScope: LifetimeScope
    {
        [SerializeField] private PlayerEffectSettings _effectSettings;
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private Player _player;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_player);
            builder.RegisterComponent(_gameSettings);
            builder.RegisterComponent(_effectSettings);

            builder.Register<ObjectCache>(Lifetime.Transient);

            builder.UseEntryPoints(ep =>
            {
                ep.Add<ControllerService>().AsSelf();
                ep.Add<PlayerEffectService>().AsSelf();
                ep.Add<MapManager>();
            });
        }
    }
}