using DIKUArcade.Events;
using Galaga.GalagaStates;
using NUnit.Framework;
using DIKUArcade.GUI;
using System.Collections.Generic;

namespace GalagaTests {
    [TestFixture]
    public class StateMachineTesting{

        private GameEventBus eventBus;
        private StateMachine stateMachine;
        
        [SetUp]
        public void InitiateStateMachine() {
            Window.CreateOpenGLContext();

            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.GameStateEvent });
            
            stateMachine = new StateMachine();

            eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
        }

        //Check that the initial Activestate is main menu
        [Test]
        public void TestInitialState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }

        //Check that the Activestate changes to GAME_PAUSED when corrosponding event i registered
        [Test]
        public void TestEventGamePaused() {
            eventBus.RegisterEvent(
                new GameEvent{
                    EventType = GameEventType.GameStateEvent,
                    Message = "GAME_PAUSED",
                }
            );
            eventBus.ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
        }

        //Check that the Activestate changes to GAME_RUNNING when corrosponding event i registered
        [Test]
        public void TestEventStartNewGame() {
            eventBus.RegisterEvent(
                new GameEvent{
                    EventType = GameEventType.GameStateEvent,
                    Message = "START_NEW_GAME",
                }
            );
            eventBus.ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
        }

        //Check that the Activestate changes to GAME_RUNNING when corrosponding event i registered
        [Test]
        public void TestEventContinue() {
            eventBus.RegisterEvent(
                new GameEvent{
                    EventType = GameEventType.GameStateEvent,
                    Message = "CONTINUE",
                }
            );
            eventBus.ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
        }
    }
}