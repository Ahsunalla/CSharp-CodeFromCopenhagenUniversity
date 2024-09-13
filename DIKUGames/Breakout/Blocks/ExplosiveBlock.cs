using DIKUArcade.Events;
using DIKUArcade.Entities;

namespace Breakout.Blocks {

    /// <summary>
    /// A special type of block that will explode when out of health.
    /// </summary>
    public class ExplosiveBlock : Block {

        public ExplosiveBlock (StationaryShape shape, String imageStr) : base(shape, imageStr) {
            UnbreakableBlock.AddBreakableBlock();
            health = 1;
            value = 20;
        }

        public override void BlockIsDead() {
            UnbreakableBlock.RemoveBreakableBlock();;
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.MovementEvent, ObjectArg1 = Shape.Position, Message = "EXPLOSION"});
        }
    }
}