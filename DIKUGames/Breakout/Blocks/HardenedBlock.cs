using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {

    /// <summary>
    /// A special type of block that is hard to destroy and will be visually damaged.
    /// </summary>
    public class HardenedBlock : Block {

        private string imageString;
        public HardenedBlock (StationaryShape shape, String imageStr) : base(shape, imageStr) {
            UnbreakableBlock.AddBreakableBlock();
            health = 2;
            value = 20;
            imageString = imageStr;
        }

        public override void BlockIsHit() {
            if (health <= 1) {
                var dotIndex = imageString.IndexOf(@".");
                var damagedImage = imageString.Substring(0, dotIndex) + "-damaged.png";
                this.Image = new Image(Path.Combine("../", "Breakout", "Assets", "Images",  damagedImage));
            }
        }
    }
}