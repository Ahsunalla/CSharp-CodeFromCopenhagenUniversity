using DIKUArcade.Events;
using DIKUArcade.Timers;

namespace Breakout.LevelLoading {

    /// <summary>
    /// A controller that switches between levels and keeps track of the current level.
    /// </summary>
    public class LevelController: IGameEventProcessor {
        private LevelType CurrentLevel;

        public LevelController() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        }

        private void StartGame() {
            CurrentLevel = LevelType.Level_1;
            SendNewLevel(LevelTransformer.LevelTypeToString(CurrentLevel));
        }

        private void SwitchLevel () {
            var nextLevelInt = (int)CurrentLevel + 1;
            if (Enum.IsDefined(typeof(LevelType), nextLevelInt)) {
                CurrentLevel = (LevelType)nextLevelInt;
                SendNewLevel(LevelTransformer.LevelTypeToString(CurrentLevel));
            }
            else
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent, Message = "GAME_COMPLETED"});
        }

        private void SendNewLevel(string nextLevel) {
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.GameStateEvent, StringArg1 = nextLevel, Message = "NEW_LEVEL"});
            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent, Message = "START_LEVEL"}, TimePeriod.NewSeconds(2.0));
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "START_GAME") {
                StartGame();
            } else if (gameEvent.Message == "SWITCH_LEVEL") {
                SwitchLevel();
            }
        }
    }
}