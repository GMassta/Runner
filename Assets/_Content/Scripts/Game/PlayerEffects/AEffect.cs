/**
 *  Общий класс эффекта, накладываемого на игрока
 *  Т.к. в задаче нет четкого описания поведения при повторном наложении эффекта,
 *  то эффект при повторе просто перезапускает таймер.
 *
 *  CancellationTokenSource является protected, чтобы можно было останавливать таски в конце эффекта
 *
 *  OnExecute и OnRemove асинхронные, чтобы можно было проигрывать анимации в рамках воздействия эффекта на игрока
 */

using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Content.Scripts.Game.PlayerEffects
{
    public abstract class AEffect
    {
        public bool IsRemoved { get; private set; }
        
        protected CancellationTokenSource _cts;
        protected float duration;

        // метод Execute асинхронный, чтобы можно было, например отобразить
        // какой-то дополнительный эффект, анимацию, еще что-то
        // и только после этого запускать таймер.
        public async UniTask Execute(Player player)
        {
            Cancel();
            IsRemoved = false;
            _cts = new CancellationTokenSource();
            
            await OnExecute(player);
            RemoveTimer(player).Forget();
        }

        //Таймер остановки действия эффекта
        private async UniTask RemoveTimer(Player player)
        {
            await UniTask.WaitForSeconds(duration, cancellationToken: _cts.Token);
            await OnRemove(player);
            IsRemoved = true;
            Cancel();
        }
        
        private void Cancel()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        //Функция выполняется в момент наложения эффекта
        protected abstract UniTask OnExecute(Player player);
        //Функция выполняется в момент удаления эффекта
        protected abstract UniTask OnRemove(Player player);
    }
}