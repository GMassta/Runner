/**
 * Объект столкновения. Содержит в себе тип эффекта, накладываемого на персонажа
 */

using _Content.Scripts.Game.PlayerEffects;
using _Content.Scripts.Services;
using UnityEngine;
using VContainer;

namespace _Content.Scripts.Game
{
    public class CollidableObject: MonoBehaviour
    {
        [SerializeField] private EPlayerEffectType _effectType;
        [Inject] private PlayerEffectService _effectService;

        public AEffect GetEffect() => _effectService.GetEffect(_effectType);
    }
}