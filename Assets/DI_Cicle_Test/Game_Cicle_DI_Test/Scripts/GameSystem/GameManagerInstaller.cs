using UnityEngine;

namespace Lesson_4.Lesson4_GameSystem.Scripts.GameSystem
{
    [RequireComponent(typeof(GameManager))]
    public class GameManagerInstaller : MonoBehaviour
    {
        public void Awake()
        {
            var manager = GetComponent<GameManager>();
            var listeners = GetComponentsInChildren<Listeners.IGameListener>();

            foreach (var gameListener in listeners)
            {
                manager.AddListener(gameListener);
            }
        }
    }
}
