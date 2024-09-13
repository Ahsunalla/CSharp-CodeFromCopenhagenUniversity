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
    public class LevelCreatorTest {
        private LevelLoader levelLoader;
        private LevelCreator levelCreator;
        private EntityContainer<Block> blocks;
        private bool positionTest_retval = false;

        [SetUp]
        public void SetUp() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            levelLoader = new LevelLoader();
            var level1 = levelLoader.GetSubfiles(Path.Combine(FileIO.GetProjectPath(), "Assets", "Levels", "level1.txt"));

            levelCreator = new LevelCreator();
            blocks = levelCreator.LoadNewlevel(level1);

        }

        [Test]
        // Tester at EntityContainer "blocks" ikke er tom
        public void NotEmptyTest() {
            Assert.True(blocks.CountEntities() != 0);
        }

        [Test]
        // Tester at alle blocks er inden for rammerne i spil-vinduet
        public void PositionTest() {
            //private bool retval = false;
            blocks.Iterate(block => {
                if (block.Shape.Position.X < 0.0f || 
                    block.Shape.Position.X > 1.0f - block.Shape.Extent.X ||
                    block.Shape.Position.Y < 0.0f || 
                    block.Shape.Position.Y > 1.0f - block.Shape.Extent.Y) {
                    positionTest_retval = true;
                }
            });
            Assert.False(positionTest_retval);
        }
    }
}