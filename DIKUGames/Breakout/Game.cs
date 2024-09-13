using System.IO;
using System.Collections.Generic;
using Breakout.States;
using Breakout.GameObjects;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.State;

namespace Breakout {
    
    /// <summary>
    /// The game.
    /// </summary>
    public class Game : DIKUGame, IGameEventProcessor {
        
        private GameEventBus eventBus;
        private IGameState stateMachine;
        private Scoreboard scoreboard;
        private Entity overlay;
                
        public Game(WindowArgs windowArgs) : base(windowArgs) {
            window.SetKeyEventHandler(KeyHandler);
            window.SetClearColor(System.Drawing.Color.Black);

            eventBus = BreakoutBus.GetBus();
            eventBus.InitializeEventBus(new List <GameEventType> {GameEventType.WindowEvent, GameEventType.ControlEvent, GameEventType.GameStateEvent, 
                GameEventType.MovementEvent, GameEventType.PlayerEvent, GameEventType.StatusEvent, GameEventType.TimedEvent});
            eventBus.Subscribe(GameEventType.WindowEvent, this); 

            stateMachine = StateMachine.GetStateMachine();

            scoreboard = new Scoreboard(Constants.ScoreBoardShape, new Image(Path.Combine("..", "Breakout", "Assets", "Images", "emptyPoint.png")));
            
            overlay = new Entity(Constants.BackGroundShape,
                new Image(Path.Combine("../", "Breakout", "Assets", "Images", "Overlay.png")));
        }

        private void KeyHandler(KeyboardAction action, KeyboardKey key){
            stateMachine.HandleKeyEvent(action, key);
        }

        public override void Update(){
            eventBus.ProcessEventsSequentially();
            stateMachine.UpdateState();
            scoreboard.Update();
        }

        public override void Render(){
            stateMachine.RenderState();
            scoreboard.Render();
            overlay.RenderEntity();
            
        }

        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.Message == "CLOSE_WINDOW")
                window.CloseWindow();
        }


    }
}