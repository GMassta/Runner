/**
 *  Эффект полета. поднимает персонажа на указанную высоту
 */

using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Content.Scripts.Game.PlayerEffects
{
    public class FlyEffect: AEffect
    {
        private readonly int _animationFly = Animator.StringToHash("Fly");
        
        private readonly float _height;
        private readonly float _speed;
        
        private Animator _animator;
        
        public FlyEffect(float height, float speed, float duration)
        {
            _height = height;
            _speed = speed;
            this.duration = duration;
        }

        protected override async UniTask OnExecute(Player player)
        {
            _animator = player.GetComponent<Animator>();
            player.transform.DOKill();
            
            var position = player.transform.localPosition;
            position.y = _height;
            position.z = _height;
            
            player.transform.DOLocalMove(position, _speed);
            _animator.SetBool(_animationFly, true);
        }

        protected override async UniTask OnRemove(Player player)
        {
            var position = player.transform.localPosition;
            position.y = 0;
            position.z = 0;
            
            player.transform.DOLocalMove(position, _speed)
                .OnComplete(() => _animator.SetBool(_animationFly, false));
        }
    }
}