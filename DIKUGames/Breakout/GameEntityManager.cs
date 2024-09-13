using Breakout.LevelLoading;
using DIKUArcade.Entities;

namespace Breakout {

    /// <summary>
    /// An abstract class providing the basic functionality for all game entity managing classes.
    /// </summary>
    public abstract class GameEntityManager<T> : ILevelChangeObserver where T: Entity {
        protected EntityContainer<T> entities;

        public void Add(T obj) {
            entities.AddEntity(obj);
        }

        public Iterator<T> GetIterator() {
            var iterator = new GEMIterator<T>(entities);
            return iterator;
        }

        public abstract void Update();

        public abstract void Render();

        public abstract void InstantiateNewLevel();
    }
}