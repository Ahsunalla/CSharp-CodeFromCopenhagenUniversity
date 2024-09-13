using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using NUnit.Framework;
using Breakout;
using DIKUArcade.Math;
using DIKUArcade.Utilities;

namespace BreakoutTests
{
    public class PlayerTests
    {
        public Player player;

        [SetUp]
        public void SetUp() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            player = new Player(
                new DynamicShape(new Vec2F(0.41f, 0.1f), new Vec2F(0.17f, 0.0225f)), null);
        }

        [Test]
        // Tjekker at spilleren ikke kan bevæge sig ud over den venstre kant
        public void MovesWithinBounderiesLeft() {
            for (int i = 0; i < 100; i++) {
                player.SetMoveLeft(true);
                player.Move();
            }
            Assert.AreEqual(0.01f, player.shape.Position.X);
        }

        [Test]
        // Tjekker at spilleren ikke kan bevæge sig ud over den højre kant
        public void MovesWithinBounderiesRight() {
            for (int i = 0; i < 100; i++) {
                player.SetMoveRight(true);
                player.Move();
            }
            Assert.AreEqual(0.82f, player.shape.Position.X);
        }

        [Test]
        // Tjekker at spilleren stater i midten i den nederste den af vinduet 
        public void SpawnsInMiddle() {
            Assert.AreEqual(0.41f, player.shape.Position.X);
            Assert.AreEqual(0.1f, player.shape.Position.Y);
        }
    }
}