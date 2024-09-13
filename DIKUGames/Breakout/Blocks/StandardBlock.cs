using DIKUArcade.Entities;


namespace Breakout.Blocks {

    /// </summary>
    /// A standard block with only basic functionality.
    /// </summary>
    public class StandardBlock : Block {
        public StandardBlock (StationaryShape shape, String imageStr) : base(shape, imageStr) {
            UnbreakableBlock.AddBreakableBlock();
            health = 1;
            value = 10;
        }
    }
}