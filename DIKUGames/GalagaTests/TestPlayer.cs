using NUnit.Framework;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using Galaga;

namespace GalagaTests;

public class TestPlayer
{
    private Player player;
    private GameEventBus eventBus;
    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("../Galaga", "Assets", "Images", "Player.png")));
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.PlayerEvent, 
                GameEventType.WindowEvent, GameEventType.GraphicsEvent, GameEventType.MovementEvent, GameEventType.GameStateEvent });
            eventBus.Subscribe(GameEventType.PlayerEvent, player);       
    }

    //Test that the player does not exceed the physical boundaries of the game
    [Test]
    public void TestBoundaryLeft() {
        eventBus.RegisterEvent(new GameEvent{
            EventType = GameEventType.PlayerEvent, 
            Message  = "MOVE_LEFT"
        });
        eventBus.ProcessEventsSequentially();
        for (int i = 1; i <=100; i++){
            player.Move();
        }
        Assert.AreEqual(0.0f, player.GetPosition().X);
    }

    [Test]
    public void TestBoundaryRight() {
        eventBus.RegisterEvent(new GameEvent{
            EventType = GameEventType.PlayerEvent, 
            Message  = "MOVE_RIGHT"
        });
        eventBus.ProcessEventsSequentially();
        for (int i = 1; i <=100; i++){
            player.Move();
        }
        Assert.AreEqual(0.9f, player.GetPosition().X);
    }

    // Test if the player stop its movement when it's supposed to
    [Test]
    public void TestStopLeft() {
        eventBus.RegisterEvent(new GameEvent{
            EventType = GameEventType.PlayerEvent, 
            Message  = "MOVE_LEFT"
        });
        eventBus.ProcessEventsSequentially();

        eventBus.RegisterEvent(new GameEvent{
            EventType = GameEventType.PlayerEvent, 
            Message  = "STOP_MOVE_LEFT"
        });
        eventBus.ProcessEventsSequentially();
        for (int i = 1; i <=100; i++){
            player.Move();
        }
        Assert.AreEqual(0.45f, player.GetPosition().X);
    }
    [Test]
    public void TestStopRight() {
        eventBus.RegisterEvent(new GameEvent{
            EventType = GameEventType.PlayerEvent, 
            Message  = "MOVE_RIGHT"
        });
        eventBus.ProcessEventsSequentially();

        eventBus.RegisterEvent(new GameEvent{
            EventType = GameEventType.PlayerEvent, 
            Message  = "STOP_MOVE_RIGHT"
        });
        eventBus.ProcessEventsSequentially();
        for (int i = 1; i <=100; i++){
            player.Move();
        }
        Assert.AreEqual(0.45f, player.GetPosition().X);
    }
}