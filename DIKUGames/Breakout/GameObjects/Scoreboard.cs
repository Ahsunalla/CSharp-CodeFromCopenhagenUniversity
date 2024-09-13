using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Events;


namespace Breakout.GameObjects { 

    /// <summary>
    /// Player rewards presented as a scoreboard. Points are added to the 
    /// scoreboard everytime a block is destroyd or when extra points are  
    /// achieved as a power up.
    /// </summary>
    public class Scoreboard : IGameObject, IGameEventProcessor {
        private int score;        
        private Text display;
        public Entity board;
        public Scoreboard(Shape shape, IBaseImage image) {
            score = 0;
            display = new Text(score.ToString(), new Vec2F(shape.Position.X + 0.06f, shape.Position.Y - 0.2f), new Vec2F(0.25f, 0.25f));
            display.SetFontSize(500);
            display.SetColor(new Vec3I(255, 255, 255));
            board = new Entity(shape, image);
            BreakoutBus.GetBus().Subscribe(GameEventType.MovementEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        }

        /// <summary>
        ///
        /// </summary>
        private void AddPoint(int points) {
            score += points;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetScore() {
            score = 0;
        }

        /// <summary>
        ///
        /// </summary>
        public int GetScore() {
            return score;
        }

        /// <summary>
        ///
        /// </summary>
        public void Update() {
            display.SetText(score.ToString());
        }

        /// <summary>
        ///
        /// </summary>
        public void Render() {
            board.RenderEntity();
            display.RenderText();
        }

        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.Message == "BLOCK_DESTROYED") {
                AddPoint(gameEvent.IntArg1);
            }
            else if (gameEvent.Message == "START_GAME") {
                ResetScore();
            } else if (gameEvent.Message == "EXTRA_POINTS")
                AddPoint(100);
        }
    }
}