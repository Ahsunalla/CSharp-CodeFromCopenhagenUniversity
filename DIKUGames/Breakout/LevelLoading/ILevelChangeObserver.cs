
namespace Breakout.LevelLoading {
    /// <summary>
    /// An interface for an observer in an observer pattern.
    /// </summary>
    public interface ILevelChangeObserver: IGameObject {
        void InstantiateNewLevel();
    }
}