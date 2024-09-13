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
    public class LevelLoaderTest {
        private LevelLoader levelLoader;
        bool retValMeta = true;
        bool retValLegend = true;

        [SetUp]
        public void SetUp() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            levelLoader = new LevelLoader();
            levelLoader.GetSubfiles(Path.Combine(FileIO.GetProjectPath(), "Assets", "Levels", "level1.txt"));
        }

        [Test]
        // Tester om map er 25 langt
        public void MapTest() {
            Assert.AreEqual(25 , levelLoader.map.Length);
        }

        [Test]
        // I denne test tjekker vi om string arrayet indeholder et ":" på hver linje da meta-delen
        // af .txt filen er den eneste der indeholder et sådan tegn på hver linje.
        public void MetaTest() {
            foreach (string line in levelLoader.meta) {
                if (!line.Contains(":")) {
                    retValMeta = false;
                }
                Assert.AreEqual(true, retValMeta);
            }
        }

        [Test]
        // Legend er den eneste del af tekstfilen som indeholder ")" på hver linje.
        public void LegendTest() {
            foreach (string line in levelLoader.legend) {
                if (!line.Contains(")")) {
                    retValLegend = false;
                }
                Assert.AreEqual(true, retValLegend);
            }
        }

    }
}