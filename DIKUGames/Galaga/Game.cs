using DIKUArcade;
using DIKUArcade.GUI;
using System.Collections.Generic;
using DIKUArcade.Events;
using Galaga.GalagaStates;

namespace Galaga {
    public class Game : DIKUGame, IGameEventProcessor {
        private StateMachine stateMachine;
        
        public Game(WindowArgs windowArgs) : base(windowArgs) {
            stateMachine = new StateMachine();
            window.SetKeyEventHandler(stateMachine.KeyHandler);

            //Initialize and subscribe to eventbus
            GalagaBus.GetBus().InitializeEventBus(new List<GameEventType> { GameEventType.PlayerEvent, 
                GameEventType.WindowEvent, GameEventType.GraphicsEvent, GameEventType.MovementEvent, GameEventType.GameStateEvent });
            GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        }

        public override void Render() {
            stateMachine.ActiveState.RenderState();   
        }

        public override void Update() {
            stateMachine.ActiveState.UpdateState();
            GalagaBus.GetBus().ProcessEventsSequentially();
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.WindowEvent) {
            switch (gameEvent.Message) {
                case "CLOSE_WINDOW":
                    window.CloseWindow();
                    break;
                }
            }
        }
    }   
}