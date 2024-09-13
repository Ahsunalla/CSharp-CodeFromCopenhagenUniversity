using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Timers;


namespace Breakout.Blocks {

    /// <summary>
    /// A special type of block that is invisible until any block of that type is hit.
    /// </summary>
    public class InvisibleBlock : Block, IGameEventProcessor {

        // Common visibility status for all blocks of this type.
        public static bool areVisible = false;
        public InvisibleBlock(StationaryShape shape, String imageStr) : base(shape, imageStr) {
            UnbreakableBlock.AddBreakableBlock();
            health = 1;
            value = 15;
            BreakoutBus.GetBus().Subscribe(GameEventType.TimedEvent, this);
        }

        public override bool CanTakeDamage() {
            if (areVisible)
                return true;
            else return false;
        }

        public override void BlockIsHit() {
            areVisible = true;
            BreakoutBus.GetBus().AddOrResetTimedEvent(new GameEvent {
                EventType = GameEventType.TimedEvent, Id = 999, Message = "NO_LONGER_RECENT"}, TimePeriod.NewSeconds(10.0));
        }

        public override void Render() {
            if (!areVisible) return;
            RenderEntity();
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "NO_LONGER_RECENT") {
                areVisible = false;
            }
        }
    }
}