using Breakout.Blocks;
using Breakout.Balls;
using Breakout.ThePlayer;
using DIKUArcade.Physics;
using DIKUArcade.Math;

namespace Breakout.Collisions {

    /// <summary>
    /// A static class handling all collisions between game entities.
    /// </summary>
    public static class CollisionHandler {
        public static void CheckAllCollision(GameEntityManager<Block> blocks, GameEntityManager<Ball> balls, Player player) {
            AllBallCollisions(balls, blocks, player);
        }

        private static void AllBallCollisions(GameEntityManager<Ball> balls, GameEntityManager<Block> blocks, Player player) {
            var iterator = balls.GetIterator();
            while (iterator.MoveNext()) {
                BallPlayerCollision(player, iterator.Current());
                BallBlockCollision(iterator.Current(), blocks);
            }
        }
        private static void BallBlockCollision(Ball ball, GameEntityManager<Block> blocks) {
            var iterator = blocks.GetIterator();
            while (iterator.MoveNext()) {
                var collisionData = CollisionDetection.Aabb(ball.GetShape(), iterator.Current().Shape);
                if (collisionData.Collision) {
                    if (collisionData.CollisionDir == CollisionDirection.CollisionDirLeft || collisionData.CollisionDir == CollisionDirection.CollisionDirRight) {
                        ball.GetDirection().X = -ball.GetDirection().X;
                    } else {
                        ball.GetDirection().Y = -ball.GetDirection().Y;
                    }
                    iterator.Current().BlockHit(1);
                }
            }
        }

        private static void BallPlayerCollision(Player player, Ball ball) {
            if (CollisionDetection.Aabb(ball.GetShape(), player.Shape).Collision) {
                var middleOfPlayer = player.Shape.Position.X + player.Shape.Extent.X / 2;
                var newXDirection = (ball.Shape.Position.X - middleOfPlayer) * 0.1f;
                var currentVelocity = Convert.ToSingle(Math.Sqrt(Math.Pow(ball.GetDirection().X, 2.0f) + Math.Pow(ball.GetDirection().Y, 2.0f)));
                var newYDirection = Convert.ToSingle(Math.Sqrt(Math.Pow(currentVelocity, 2.0f) - Math.Pow(newXDirection, 2.0f)));
                ball.ChangeDirection(new Vec2F(newXDirection, newYDirection));
            }
        }
    }
}