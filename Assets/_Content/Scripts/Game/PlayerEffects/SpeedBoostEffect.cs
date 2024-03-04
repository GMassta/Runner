/**
 *  Эффект изменяющий скорость игрока
 *  Можно получить с игрока публичные методы и использовать их,
 *  не меняя сам класс игрока
 *
 */

using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Content.Scripts.Game.PlayerEffects
{
    public class SpeedBoostEffect: AEffect
    {
        private readonly int _runSpeed = Animator.StringToHash("Speed");
        
        private readonly float _boostSpeed;
        private readonly float _acceleration;
        
        private Animator _animator;

        public SpeedBoostEffect(float boostSpeed, float acceleration, float duration)
        {
            _acceleration = acceleration;
            _boostSpeed = boostSpeed;
            this.duration = duration;
        }

        protected override async UniTask OnExecute(Player player)
        {
            _animator = player.GetComponent<Animator>();
            DOVirtual.Float(player.RunSpeedMultiply, _boostSpeed, _acceleration, 
                v =>
                {
                    _animator.SetFloat(_runSpeed, v);
                    player.RunSpeedMultiply = v;
                });
        }

        protected override async UniTask OnRemove(Player player)
        {
            DOVirtual.Float(player.RunSpeedMultiply, 1 , _acceleration, 
                v =>
                {
                    _animator.SetFloat(_runSpeed, v);
                    player.RunSpeedMultiply = v;
                });
        }
    }
}