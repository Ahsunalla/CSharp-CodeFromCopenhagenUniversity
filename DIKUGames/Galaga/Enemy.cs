using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Galaga {
    public class Enemy : Entity {
        private int hitpoints = 3;
        private IBaseImage enemyStridesRed;
        private readonly Vec2F startPos;

        public Enemy(DynamicShape shape, IBaseImage image, IBaseImage enemyStridesRed) : base(shape, image) {
            this.enemyStridesRed = enemyStridesRed;
            this.startPos = shape.Position;

        }
        
        public int GetHitPoints() {
            return hitpoints;
        }

        public Vec2F GetStartPosition() {
            return startPos;
        }

        public void TakesDamage() {
            hitpoints--;
            if (hitpoints == 1) {
                Image = enemyStridesRed;
            }
            if (hitpoints <= 0) {
                this.DeleteEntity();
            }
        }

        public bool KilledPlayer() {
            if (Shape.Position.Y <= 0.0f) {
                return true;
            }
            else if (Shape.Position == enemyStridesRed) {
                return true;
            }
            else { 
            return false;
            }
        }
    }   
}