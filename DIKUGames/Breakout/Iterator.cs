namespace Breakout {

    /// <summary>
    /// A generic iterator interface.
    /// </summary>
    public interface Iterator<T> {
        bool MoveNext();
        T Current();
    }
}