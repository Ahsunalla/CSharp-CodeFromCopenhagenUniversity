using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Breakout.Balls {
    /// <summary>
    /// Responsible for direction and collision with player and screen borders.
    /// When starting a new level or losing a life, the ball will have a semi-random
    /// direction in a slope line.
    /// </summary>
    public class Ball : Entity, IGameEventProcessor {

        private Random random;
        public Ball (DynamicShape shape, IBaseImage image) : base(shape, image) {
            BreakoutBus.GetBus().Subscribe(GameEventType.MovementEvent, this);
            random = new Random();
        }

        public void CollideWithPlayer() {
            GetDirection().Y = -GetDirection().Y;
        }

        public DynamicShape GetShape () {
            return Shape.AsDynamicShape();
        }

        public Vec2F GetDirection() {
            return Shape.AsDynamicShape().Direction;
        }

        public void ChangeDirection(Vec2F direction) {
            GetShape().Direction = direction;
        }

        public void Move() {
            var leftSide = Shape.Position.X + GetDirection().X;
            var rightSide = leftSide + Shape.Extent.X;
            var bottom = Shape.Position.Y + GetDirection().Y;
            var top = bottom + Shape.Extent.Y;
            if (leftSide <= 0.0f || rightSide >= 1.0f) {
                GetDirection().X = -GetDirection().X;
            } else if (top >= 1.0f) {
                GetDirection().Y = -GetDirection().Y;
            } else if (top <= 0.0f) {
                DeleteEntity();
            } Shape.Move();
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "START_BALL") {
                var rd = random.Next(0, 2);
                if (rd == 0)
                    ChangeDirection(new Vec2F(0.01f, 0.018f));
                else 
                    ChangeDirection(new Vec2F(-0.01f, 0.018f));
            }
        }
    }
}