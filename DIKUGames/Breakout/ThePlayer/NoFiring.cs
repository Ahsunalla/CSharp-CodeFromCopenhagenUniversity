using System.IO;
using DIKUArcade.Graphics;

namespace Breakout.ThePlayer {

    /// <summary>
    /// A behaviour that fires nothing.
    /// </summary>
    public class NoFiring : FiringBehaviour {
        public NoFiring(Player player) {
            this.player = player;
            player.Image = new ImageStride(50, ImageStride.CreateStrides(3,Path.Combine("..", "Breakout" , "Assets", "Images", "playerStride.png")));
        }

        public override void Fire() {}

        public override void Render() {
            player.RenderEntity();
        }
    }
}