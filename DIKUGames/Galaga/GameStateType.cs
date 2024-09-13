
using System;

namespace Galaga{
    public class StateTransformer {
        public static GameStateType TransformStringToState (string state) {
            switch(state) {
                case "GAME_RUNNING":
                    return GameStateType.GameRunning;
                case "MAIN_MENU":
                    return GameStateType.MainMenu;
                case "GAME_PAUSED":
                    return GameStateType.GamePaused;
                default:
                    throw new ArgumentException("no matching game state type");
            }
        }

        public static string TransformStateToString(GameStateType state){
            switch(state){
                case GameStateType.GameRunning:
                    return "GAME_RUNNING";
                case GameStateType.MainMenu:
                    return "MAIN_MENU";
                case GameStateType.GamePaused:
                    return "GAME_PAUSED";
                default:
                    throw new ArgumentException("No matching gametype");
            }
        }
    }

    public enum GameStateType {
        MainMenu,
        GameRunning,
        GamePaused
    }
}