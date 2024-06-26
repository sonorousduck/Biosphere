using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using SequoiaEngine;


namespace SequoiaEngine
{
    public class SystemManager
    {
        public event Action<GameObject> AddGameObject;
        public event Action<uint> RemoveGameObject;
        public event Action<GameTime> UpdateSystem;
        private Queue<GameObject> toAddObjects = new Queue<GameObject>();
        private Queue<uint> toRemoveObjects = new Queue<uint>();

        public Dictionary<uint, GameObject> gameObjectsDictionary = new Dictionary<uint, GameObject>();

        /// <summary>
        /// Adds a new gameobject to all systems.
        /// </summary>
        /// <param name="gameObject"></param>
        public void Add(GameObject gameObject)
        {
            toAddObjects.Enqueue(gameObject);
        }

        /// <summary>
        /// Adds to a deletion queue to be removed at the end of a Update Loop
        /// </summary>
        public void Remove(GameObject gameObject)
        {
            toRemoveObjects.Enqueue(gameObject.id);
        }

        public void Update(GameTime gameTime)
        {
            UpdateSystem?.Invoke(gameTime);

            while (toAddObjects.Count > 0)
            {
                AddObject(toAddObjects.Dequeue());
            }
            while (toRemoveObjects.Count > 0)
            {
                uint idToRemove = toRemoveObjects.Dequeue();
                gameObjectsDictionary.Remove(idToRemove);
                RemoveObject(idToRemove);
            }
        }

        private void AddObject(GameObject gameObject)
        {
            gameObjectsDictionary.Add(gameObject.id, gameObject);
            AddGameObject?.Invoke(gameObject);
        }

        private void RemoveObject(uint id)
        {
            RemoveGameObject?.Invoke(id);
            gameObjectsDictionary.Remove(id);
        }

    }
}