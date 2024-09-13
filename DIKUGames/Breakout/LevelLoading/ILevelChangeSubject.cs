namespace Breakout.LevelLoading {

    /// <summary>
    /// An interface for a subject in an observer pattern.
    /// </summary>
    public interface ILevelChangeSubject {
        void Add(ILevelChangeObserver observer);
        string[] GetMap();
        string[] GetMeta();
        string[] GetLegend();
        void NotifyObservers();
    }
}