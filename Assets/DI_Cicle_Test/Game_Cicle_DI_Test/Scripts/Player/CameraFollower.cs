using Lesson_4.Lesson4_GameSystem.Scripts.DIFramework;
using Lesson_4.Lesson4_GameSystem.Scripts.GameSystem;
using UnityEngine;

namespace Lesson_4.Lesson4_GameSystem.Scripts
{
    public sealed class CameraFollower : MonoBehaviour,
        Listeners.IGameFinishListener,
        Listeners.IGamePauseListener,
        Listeners.IGameResumeListener,
        Listeners.IGameStartListener,
        Listeners.IGameLateUpdateListener
    {
        [SerializeField]
        private Camera targetCamera;

        [SerializeField]
        private Vector3 offset;

        private Player _player;

        private void Awake()
        {
            enabled = false;
        }

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
        }

        public void OnStart()
        {
            enabled = true;
        }

        public void OnPause()
        {
            enabled = false;
        }

        public void OnResume()
        {
            enabled = true;
        }      

        public void OnFinish()
        {
            enabled = false;
        }

        public void OnLateUpdate(float timeDelta)
        {
            this.targetCamera.transform.position = _player.GetPosition() + this.offset;
        }
    }
}