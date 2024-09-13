using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Input;


namespace Breakout.States {

    /// <summary>
    /// A state machine that can switch between state and keeps track of the active state.
    /// </summary>
    public class StateMachine : IStateMachine, IGameState, IGameEventProcessor {
        private static StateMachine stateMachine;
        private IGameState ActiveState;
        private IGameState MainMenu;
        private IGameState GameRunning;
        private IGameState GamePaused;

        private StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            MainMenu = new MainMenu();
            GameRunning = new GameRunning();
            GamePaused = new GamePaused();

            ActiveState = MainMenu;
        }

        public static StateMachine GetStateMachine() {
            return stateMachine ?? (stateMachine = new StateMachine());
        }

        public IGameState GetGameState(GameStateType gameStateType) {
            IGameState gameState = null;
            switch (gameStateType) {
                case GameStateType.MainMenu:
                    gameState = MainMenu;
                    break;
                case GameStateType.GameRunning:
                    gameState = GameRunning;
                    break;
                case GameStateType.GamePaused:
                    gameState = GamePaused;
                    break;
                default:
                    break;
            }
            return gameState;
        }

        public void SwitchGameState(GameStateType gameStateType) {
            switch (gameStateType) {
                case GameStateType.MainMenu:
                    ActiveState = MainMenu;
                    break;
                case GameStateType.GameRunning:
                    ActiveState = GameRunning;
                    break;
                case GameStateType.GamePaused:
                    ActiveState = GamePaused;
                    break;
                default:
                    break;
            }
        }

        public void ResetState() {}
        public void UpdateState() {
            ActiveState.UpdateState();
        }

        public void RenderState() {
            ActiveState.RenderState();
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            ActiveState.HandleKeyEvent(action, key);
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "SWITCH_STATE") {
                switch (gameEvent.StringArg1) {
                    case "MAIN_MENU":
                        SwitchGameState(GameStateType.MainMenu);
                        break;
                    case "GAME_RUNNING":
                        SwitchGameState(GameStateType.GameRunning);
                        break;
                    case "GAME_PAUSED":
                        SwitchGameState(GameStateType.GamePaused);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}