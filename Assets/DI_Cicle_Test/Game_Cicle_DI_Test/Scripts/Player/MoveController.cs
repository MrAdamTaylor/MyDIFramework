using Lesson_4.Lesson4_GameSystem.Scripts.DIFramework;
using Lesson_4.Lesson4_GameSystem.Scripts.GameSystem;
using UnityEngine;

namespace Lesson_4.Lesson4_GameSystem.Scripts
{
    public sealed class MoveController : MonoBehaviour,
        Listeners.IGameStartListener,
        Listeners.IGameFinishListener
    {

        private IInputSystem _inputSystem;
        private Player _player;

        [Inject]
        public void Construct(IInputSystem inputSystem, Player player)
        {
            _inputSystem = inputSystem;
            _player = player;
        }

        private void OnMove(Vector2 direction)
        {
            var offset = new Vector3(direction.x, 0, direction.y) * Time.deltaTime;
            _player.Move(offset);
        }

        public void OnStart()
        {
            _inputSystem.OnMove += this.OnMove;
        }

        public void OnFinish()
        {
            _inputSystem.OnMove -= this.OnMove;
        }
    }
}