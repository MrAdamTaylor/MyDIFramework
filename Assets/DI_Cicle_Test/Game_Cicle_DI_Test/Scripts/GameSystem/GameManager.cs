using System.Collections.Generic;
using UnityEngine;

namespace Lesson_4.Lesson4_GameSystem.Scripts.GameSystem
{
    public enum GameState
    {
        Off,
        Playing,
        Finish,
        Pause
    }

    public class GameManager : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private GameState _gameStatr = GameState.Off;

        private List<Listeners.IGameListener> listeners = new List<Listeners.IGameListener>();

        private List<Listeners.IGameUpdateListener> updateListeners = new List<Listeners.IGameUpdateListener>();
        private List<Listeners.IGameFixedUpdateListener> fixedUpdateListeners = new List<Listeners.IGameFixedUpdateListener>();
        private List<Listeners.IGameLateUpdateListener> lateUpdateListeners = new List<Listeners.IGameLateUpdateListener>();

    
        [EasyButtons.Button]
        private void OnStart()
        {
            foreach (var gameListener in listeners)
            {
                if (gameListener is Listeners.IGameStartListener startListener)
                {
                    startListener.OnStart();
                }
            }
            _gameStatr = GameState.Playing;
        }

        //TODO - здесь можно сделать проверку на null
        internal void AddListener(Listeners.IGameListener gameListener)
        {
            if (gameListener == null)
            {
                return;
            }

            listeners.Add(gameListener);

            if (gameListener is Listeners.IGameUpdateListener updateListener)
            {
                updateListeners.Add(updateListener);
            }

            if (gameListener is Listeners.IGameFixedUpdateListener fixedupdateListener)
            {
                fixedUpdateListeners.Add(fixedupdateListener);
            }

            if (gameListener is Listeners.IGameLateUpdateListener lateUpdateListener)
            {
                lateUpdateListeners.Add(lateUpdateListener);
            }
        }
    
        public void RemoveListener(Listeners.IGameListener listener)
        {
            if (listener == null)
            {
                return;
            }
            
            this.listeners.Remove(listener);

            if (listener is Listeners.IGameUpdateListener updateListener)
            {
                this.updateListeners.Remove(updateListener);
            }

            if (listener is Listeners.IGameFixedUpdateListener fixedUpdateListener)
            {
                this.fixedUpdateListeners.Remove(fixedUpdateListener);
            }

            if (listener is Listeners.IGameLateUpdateListener lateUpdateListener)
            {
                this.lateUpdateListeners.Remove(lateUpdateListener);
            }
        }

        [EasyButtons.Button]
        private void Finish()
        {
            foreach (var gameListener in listeners)
            {
                if (gameListener is Listeners.IGameFinishListener finishListener)
                {
                    finishListener.OnFinish();
                }
            }
            _gameStatr = GameState.Finish;
        }

        [EasyButtons.Button]
        private void Pause()
        {
            foreach (var gameListener in listeners)
            {
                if (gameListener is Listeners.IGamePauseListener pauseListener)
                {
                    pauseListener.OnPause();
                }
            }
            _gameStatr = GameState.Pause;
        }

        [EasyButtons.Button]
        private void Resume()
        {
            foreach (var gameListener in listeners)
            {
                if (gameListener is Listeners.IGameResumeListener resumeListener)
                {
                    resumeListener.OnResume();
                }
            }
            _gameStatr = GameState.Playing;
        }

        private void Update()
        {
            if (_gameStatr != GameState.Playing)
            {
                return;
            }
        
            var deltaTime = Time.deltaTime;
            for (int i = 0, count = this.updateListeners.Count; i < count; i++)
            {
                var listener = this.updateListeners[i];
                listener.OnUpdate(deltaTime);
            }
        }
    
        private void FixedUpdate()
        {
            if (_gameStatr != GameState.Playing)
            {
                return;
            }
            var deltaTime = Time.deltaTime;
            for (int i = 0, count = this.fixedUpdateListeners.Count; i < count; i++)
            {
                var listener = this.fixedUpdateListeners[i];
                listener.FixedUpdate(deltaTime);
            }
        }
    
        private void LateUpdate()
        {
            if (_gameStatr != GameState.Playing)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
        
            for (int i = 0, count = this.lateUpdateListeners.Count; i < count; i++)
            {
                var listener = this.lateUpdateListeners[i];
                listener.OnLateUpdate(deltaTime);
            }
        }
    }
}