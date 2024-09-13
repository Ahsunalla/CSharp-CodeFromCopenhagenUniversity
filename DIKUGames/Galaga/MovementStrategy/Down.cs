using DIKUArcade.Entities;
using DIKUArcade.Events;

namespace Galaga.MovementStrategy {
    public class Down : IMovementStrategy, IGameEventProcessor {
        private float speed = 0.001f;

        public Down() {
            GalagaBus.GetBus().Subscribe(GameEventType.MovementEvent, this);
        }

        public void MoveEnemy(Enemy enemy) {
            if (enemy.GetHitPoints() == 1) {
                enemy.Shape.MoveY(-speed * 2);
            } else {
                enemy.Shape.MoveY(-speed);
            }
        }
        
        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            enemies.Iterate(enemy =>
            {
                this.MoveEnemy(enemy);
            });
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.MovementEvent) {
                switch (gameEvent.Message) {
                    case "INCREASE_ENEMY_SPEED":
                        speed += 0.0005f;
                        break;
                }
            }
        }
    }
}