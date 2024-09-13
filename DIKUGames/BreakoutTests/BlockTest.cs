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
    public class BlockTest
    {
        private HardenedBlock hardenedBlock;
        private NormalBlock testNormalBlock;
        [SetUp]
        public void setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            hardenedBlock = new HardenedBlock(
                    new StationaryShape(new Vec2F(0.08f, 0.03f), new Vec2F(0.08f, 0.03f)),
                    new Image(Path.Combine("Assets", "Images", "darkgreen-block.png")));
            testNormalBlock = new NormalBlock(
                    new StationaryShape(new Vec2F(0.08f, 0.03f), new Vec2F(0.08f, 0.03f)),
                    new Image(Path.Combine("Assets", "Images", "teal-block.png")));
        }

        //Test that a hardened block is not deleted after one hit
        [Test]
        public void HardenedBlockTest1() {
            hardenedBlock.TakeDamage();
            Assert.AreEqual(hardenedBlock.IsDeleted(), false);
        }

        //Test that a hardened block is deleted after two hit
        [Test]
        public void HardenedBlockTest2() {
            hardenedBlock.TakeDamage();
            hardenedBlock.TakeDamage();
            Assert.AreEqual(hardenedBlock.IsDeleted(), true);
        }

        //Test that a normal block is deleted after one hit
        [Test]
        public void NormalBlockTest() {
            testNormalBlock.TakeDamage();
            Assert.AreEqual(testNormalBlock.IsDeleted(), true);
        }
    }
}