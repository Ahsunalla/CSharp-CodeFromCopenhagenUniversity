namespace Breakout.ThePlayer {

    /// <summary>
    /// An interface for the player's firing behaviours.
    /// </summary>
    public abstract class FiringBehaviour {
        protected Player player;
        public abstract void Fire();
        public abstract void Render();
    }
}