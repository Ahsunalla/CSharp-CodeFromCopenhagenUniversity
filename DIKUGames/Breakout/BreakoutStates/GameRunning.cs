using System.IO;
using Breakout.LevelLoading;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;


namespace Breakout.States {

    /// <summary>
    /// A game state for when the game is running.
    /// </summary>
    public class GameRunning : IGameState, IGameEventProcessor {
        private Entity background;
        private LevelController levelController;
        private LevelHolder levelHolder;



        public GameRunning() {
            BreakoutBus.GetBus().Subscribe(GameEventType.MovementEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            background = new Entity(Constants.BackGroundShape, 
                new Image(Path.Combine("../", "Breakout","Assets", "Images", "SpaceBackground.png")));
            
            levelController = new LevelController();
            levelHolder = new LevelHolder();
            
            

        }
        public void ResetState() {
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.GameStateEvent, Message = "START_GAME"});
        }
        public void UpdateState() {
            levelHolder.Update();
        }
        public void RenderState() {
            background.RenderEntity();
            levelHolder.Render();
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                switch (key) {
                    case KeyboardKey.Escape:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.WindowEvent, Message = "CLOSE_WINDOW"});
                        break;
                    case KeyboardKey.Right:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.MovementEvent, StringArg1 = "KEY_RIGHT", Message = "KEY_PRESS"});
                        break;
                    case KeyboardKey.Left:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.MovementEvent, StringArg1 = "KEY_LEFT", Message = "KEY_PRESS"});
                        break;
                    case KeyboardKey.Up:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.MovementEvent, StringArg1 = "KEY_UP", Message = "KEY_PRESS"});
                        break;
                    case KeyboardKey.P:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.MovementEvent, StringArg1 = "KEY_RIGHT", Message = "KEY_RELEASE"});
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.MovementEvent, StringArg1 = "KEY_LEFT", Message = "KEY_RELEASE"});
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.GameStateEvent, Message = "GAME_PAUSED"});
                        StateMachine.GetStateMachine().SwitchGameState(GameStateType.GamePaused);
                        break;
                    case KeyboardKey.Z:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.GameStateEvent, Message = "SWITCH_LEVEL"});
                        break;
                    case KeyboardKey.Q:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.MovementEvent, StringArg1 = "DOUBLE_SIZE", Message = "ACTIVATE_POWERUP"});
                        break;
                    case KeyboardKey.W:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.MovementEvent, StringArg1 = "DOUBLE_SPEED", Message = "ACTIVATE_POWERUP"});
                        break;
                    case KeyboardKey.E:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.MovementEvent, StringArg1 = "EXTRA_LIFE", Message = "ACTIVATE_POWERUP"});
                        break;
                    case KeyboardKey.R:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.MovementEvent, StringArg1 = "EXTRA_POINTS", Message = "ACTIVATE_POWERUP"});
                        break;
                    case KeyboardKey.T:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.MovementEvent, StringArg1 = "FIRE_LASER", Message = "ACTIVATE_POWERUP"});
                        break;
                    case KeyboardKey.Y:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.MovementEvent, StringArg1 = "FIRE_ROCKET", Message = "ACTIVATE_POWERUP"});
                        break;
                    case KeyboardKey.U:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.MovementEvent, StringArg1 = "HALF_SPEED", Message = "ACTIVATE_POWERUP"});
                        break;
                    case KeyboardKey.I:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.MovementEvent, StringArg1 = "SPLIT", Message = "ACTIVATE_POWERUP"});
                        break;
                    case KeyboardKey.O:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.MovementEvent, StringArg1 = "WIDE", Message = "ACTIVATE_POWERUP"});
                        break;
                    default:
                        break;
                }
                
            }
            else if (action == KeyboardAction.KeyRelease) {
                switch (key) {
                    case KeyboardKey.Right:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.MovementEvent, StringArg1 = "KEY_RIGHT", Message = "KEY_RELEASE"});
                        break;
                    case KeyboardKey.Left:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.MovementEvent, StringArg1 = "KEY_LEFT", Message = "KEY_RELEASE"});
                        break;
                    default:
                        break;
                }
            }
        }

        public void ProcessEvent(GameEvent gameEvent) {
        }
    }
}