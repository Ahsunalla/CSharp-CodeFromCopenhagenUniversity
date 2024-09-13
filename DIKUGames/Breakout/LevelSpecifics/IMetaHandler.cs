namespace Breakout.GameObjects {
    /// <summary>
    /// An interface for object handling (only) meta data.
    /// </summary>
    public interface IMetaHandler : IGameObject {
        void HandleMeta(string[] meta);
    }
}