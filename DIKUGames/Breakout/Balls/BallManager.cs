using System.IO;
using Breakout.LevelLoading;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Timers;



namespace Breakout.Balls {

    /// <summary>
    /// A game entity manager for all balls.
    /// </summary>
    public class BallManager : GameEntityManager<Ball> {

        public BallManager(LevelReader levelReader) {
            levelReader.Add(this);
            entities = new EntityContainer<Ball>();
        }

        public void ResetBalls() {
            entities.ClearContainer();
            entities.AddEntity(new Ball(Constants.BallShape, 
                new Image(Path.Combine("Assets", "Images", "ball2.png"))));
            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
                    EventType = GameEventType.MovementEvent, Message = "START_BALL"}, TimePeriod.NewSeconds(2.0));
        }

        public override void Update() {
            entities.Iterate(ball => {
                ball.Move();
            });
            if (entities.CountEntities() == 0) {
                ResetBalls();
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                    EventType = GameEventType.MovementEvent, Message = "LOSE_LIFE"});
            }
        }

        public override void Render() {
            entities.RenderEntities();
        }
        public override void InstantiateNewLevel() {
            ResetBalls();
        }
    }
}