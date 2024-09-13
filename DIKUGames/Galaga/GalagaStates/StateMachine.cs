using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Input;

using System.Collections.Generic;

namespace Galaga.GalagaStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        public StateMachine() {
            ActiveState = MainMenu.GetInstance();
        }

        private Dictionary<GameStateType, IGameState> dict;

        public void AddState(GameStateType gst, IGameState state) {
            dict.Add(gst, state);
        }
        
        private void SwitchState(GameStateType stateType){
            ActiveState = dict[stateType];
            switch(stateType){
                case GameStateType.GameRunning:
                   ActiveState = GameRunning.GetInstance();
                   break;
                case GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance();
                    break;
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
                    break;
                default:
                    throw new System.Exception("GameStateType not implemented");
            }
        }

        public void KeyHandler(KeyboardAction action, KeyboardKey key) {
            ActiveState.HandleKeyEvent(action, key);
        } 
        public void ProcessEvent(GameEvent gameEvent)
        {
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                switch (gameEvent.Message) {
                    case "START_NEW_GAME":
                        SwitchState(GameStateType.GameRunning);
                        ActiveState.ResetState();
                        SwitchState(GameStateType.GameRunning);
                        break;
                    case "CONTINUE":
                        SwitchState(GameStateType.GameRunning);
                        break;
                    case "GAME_PAUSED":
                        SwitchState(GameStateType.GamePaused);
                        break;
                    case "MAIN_MENU":
                        SwitchState(GameStateType.MainMenu);
                        break;
                }
            }            
        }
    }
}