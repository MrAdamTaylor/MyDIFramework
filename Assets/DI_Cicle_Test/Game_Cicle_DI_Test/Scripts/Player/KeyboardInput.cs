using System;
using Lesson_4.Lesson4_GameSystem.Scripts.GameSystem;
using UnityEngine;

namespace Lesson_4.Lesson4_GameSystem.Scripts
{

    public interface IInputSystem
    {
        event Action<Vector2> OnMove;
    }

    public sealed class KeyboardInput : MonoBehaviour,
        Listeners.IGameStartListener,
        Listeners.IGameFinishListener,
        Listeners.IGamePauseListener,
        Listeners.IGameResumeListener,
        Listeners.IGameUpdateListener,
        IInputSystem
    {
        public event Action<Vector2> OnMove;

        public void OnFinish()
        {
            enabled = false;
        }

        public void OnPause()
        {
            this.enabled = false;
        }

        public void OnResume()
        {
            this.enabled = true;
        }

        public void OnStart()
        {
            enabled = true;
        }

        private void HandleKeyboard()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.Move(Vector2.up);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                this.Move(Vector2.down);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.Move(Vector2.left);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.Move(Vector2.right);
            }
        }

        private void Move(Vector2 direction)
        {
            this.OnMove?.Invoke(direction);
        }

        public void OnUpdate(float timeDelta)
        {
            this.HandleKeyboard();
        }
        

    }
}