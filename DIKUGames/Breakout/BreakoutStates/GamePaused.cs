using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;


namespace Breakout.States {

    /// <summary>
    /// A game state for when the game is paused. From this state one should be
    /// able to choose between unpausin or enter the main menu state.
    /// </summary>
    public class GamePaused : IGameState {
        private Text text;
        private Text mainMenuText;
        public GamePaused() {
            text = new Text("Press P to unpause", new Vec2F(0.3f, 0.3f), new Vec2F(0.3f, 0.2f));
            text.SetColor(255, 255, 255, 255);
            mainMenuText = new Text("Press M to enter main menu", new Vec2F(0.2f, 0.2f), new Vec2F(0.3f, 0.2f));
            mainMenuText.SetColor(255, 255, 255, 255);
        }
        public void ResetState() {}
        public void UpdateState() {}
        public void RenderState() {
            StateMachine.GetStateMachine().GetGameState(GameStateType.GameRunning).RenderState();
            text.RenderText();
        }
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                switch (key) {
                    case KeyboardKey.Escape:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.WindowEvent, Message = "CLOSE_WINDOW"});
                        break;
                    case KeyboardKey.P:
                        BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
                            EventType = GameEventType.GameStateEvent, Message = "GAME_UNPAUSED"}, TimePeriod.NewSeconds(1.0));
                        StateMachine.GetStateMachine().SwitchGameState(GameStateType.GameRunning);
                        break;
                    case KeyboardKey.M:
                        StateMachine.GetStateMachine().SwitchGameState(GameStateType.MainMenu);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}