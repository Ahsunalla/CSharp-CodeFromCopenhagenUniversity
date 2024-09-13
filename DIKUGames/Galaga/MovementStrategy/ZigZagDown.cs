using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System;
namespace Galaga.MovementStrategy {
    public class ZigZagDown : IMovementStrategy,  IGameEventProcessor{
        private float amplitude = 0.05f;
        private float period = 0.045f; 
        private float speed = 0.0008f;

        public ZigZagDown() {
            GalagaBus.GetBus().Subscribe(GameEventType.MovementEvent, this);
        }
        
        public void MoveEnemy(Enemy enemy) {
            Vec2F startPos = enemy.GetStartPosition();
            if (enemy.GetHitPoints() == 1) {
                float newY = enemy.Shape.Position.Y - speed * 2;
                float newX = startPos.X + amplitude * (float)(Math.Sin(2 * Math.PI * (startPos.Y - newY) / period));
                enemy.Shape.SetPosition(new Vec2F (newX, newY));
            } else {
                float newY = enemy.Shape.Position.Y - speed;
                float newX = startPos.X + amplitude * (float)(Math.Sin(2 * Math.PI * (startPos.Y - newY) / period));
                enemy.Shape.SetPosition(new Vec2F (newX, newY));
            }
        }
        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            enemies.Iterate(enemy => {
                this.MoveEnemy(enemy);
            });
        }
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.MovementEvent) {
                switch (gameEvent.Message) {
                    case "INCREASE_ENEMY_SPEED":
                        speed += 0.0002f;
                        break;
                }
            }
        }
    }
}