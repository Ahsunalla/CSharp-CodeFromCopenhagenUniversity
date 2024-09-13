using DIKUArcade.Events;
using DIKUArcade.Entities;

namespace Breakout.Blocks {

    /// <summary>
    /// A special type of block that will spawn a power up when out of health.
    /// </summary>
    public class PowerUpBlock : Block {
            public PowerUpBlock(StationaryShape shape, String imageStr) : base(shape, imageStr) {
            UnbreakableBlock.AddBreakableBlock();
            health = 1;
            value = 10;
        }

        public override void BlockIsDead() {
            UnbreakableBlock.RemoveBreakableBlock();
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.MovementEvent, ObjectArg1 = Shape.Position, Message = "ACTIVATE_POWERUP"});
        }
    }
}