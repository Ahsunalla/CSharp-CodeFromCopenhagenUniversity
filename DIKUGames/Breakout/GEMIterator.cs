using System.Collections.Generic;
using DIKUArcade.Entities;

namespace Breakout {

    /// <summary>
    /// An iterator for iterating through a GameEntityManager.
    /// </summary>
    public class GEMIterator<T>: Iterator<T> where T: Entity {
        private IEnumerator<T> enumerator;

        public GEMIterator(EntityContainer<T> entities) {
            enumerator = entities.GetEnumerator() as IEnumerator<T>;
        }

        public bool MoveNext() {
            return enumerator.MoveNext();
        }

        public T Current() {
            var entity = enumerator.Current as T;
            return entity;
        }
    }
}