namespace Breakout {

    /// <summary>
    /// A basic interface that makes sure objects kan be updated and rendered.
    /// </summary>
    public interface IGameObject {
        void Update();
        void Render();
    }
}