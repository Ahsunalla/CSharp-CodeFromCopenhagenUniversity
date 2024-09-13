using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;


namespace Breakout.Blocks {

    /// <summary>
    /// A special type of block that needs to be destroyed in a certain order.
    /// </summary>
    public class SequenceBlock : Block {
        private static int commonN = 0;
        private static int nextKillN = 1;
        private int ownN;
        private Text number;
        public SequenceBlock (StationaryShape shape, String imageStr) : base(shape, imageStr) {
            UnbreakableBlock.AddBreakableBlock();
            health = 1;
            value = Convert.ToInt32(Math.Floor(Math.Pow(2, commonN)));
            commonN++;
            ownN = commonN;
            number = new Text(ownN.ToString(), new Vec2F(Shape.Position.X  + 0.033f, Shape.Position.Y - 0.077f), new Vec2F(0.1f, 0.1f));
            number.SetColor(255, 255, 255, 255);
        }

        public override bool CanTakeDamage() {
            if (nextKillN == ownN)
                return true;
            else return false;
        }

        public override void BlockIsDead() {
            nextKillN++;
            UnbreakableBlock.RemoveBreakableBlock();
            number.SetText("");
        }

        public override void Render() {
            RenderEntity();
            number.RenderText();
        }
    }
}