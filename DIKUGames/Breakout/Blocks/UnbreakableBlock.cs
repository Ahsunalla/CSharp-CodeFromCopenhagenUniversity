using DIKUArcade.Entities;

namespace Breakout.Blocks {

    /// <summay>
    /// A special type of block that cannot be destroyed until it is the only type left.
    /// </summary>
    public class UnbreakableBlock : Block {
        private static int numOfBreakableBlocks = 0;

        public UnbreakableBlock(StationaryShape shape, String imageStr) : base(shape, imageStr) {
            health = 1;
            value = 30;
        }

        public override bool CanTakeDamage() {
            if (numOfBreakableBlocks <= 0)
                return true;
            else return false;
        }

        public static void AddBreakableBlock() {
            numOfBreakableBlocks++;
        }

        public static void RemoveBreakableBlock() {
            numOfBreakableBlocks--;
        }
    }
}