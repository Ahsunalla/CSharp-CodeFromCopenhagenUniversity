using System.IO;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {

    /// <summary>
    /// An abstract class providing the basic funcionality for blocks.
    /// </summary>
    public abstract class Block : Entity {
        
        protected int health;
        protected int value;
        private Block (StationaryShape shape, IBaseImage image) : base(shape, image) {}
        public Block (StationaryShape shape, String imageStr) : this(shape, new Image(Path.Combine("..", "Breakout", "Assets", "Images", imageStr))) {}

        public void BlockHit(int hitPoints) {
            if (CanTakeDamage() || hitPoints > 50) {
                health -= hitPoints;
            }    
            BlockIsHit();
            
            if (health <= 0) {
                BlockIsDead();

                
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.MovementEvent, IntArg1 = value, Message = "BLOCK_DESTROYED"});
                DeleteEntity();
            }
        }

        public virtual bool CanTakeDamage() {
            return true;
        }
        public virtual void BlockIsHit() {}
        public virtual void BlockIsDead() {
            UnbreakableBlock.RemoveBreakableBlock();
        }

        public virtual void Render() {
            RenderEntity();
        }
    }
}