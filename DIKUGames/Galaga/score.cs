using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using Galaga.Squadron;
using Galaga.MovementStrategy;

namespace Galaga
{
    public class Score {
        private int score;
        private Text display;
        private bool updateDisplay;
        public int getScore{
            get{return score;}
        }
        public Score (Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text (score.ToString(), position, extent);
            display.SetFontSize(50000);
            display.SetColor(new Vec3I(255, 255, 255));
        }
        public void AddPoints () {
            score++;
            updateDisplay = true;
        }
        public void RenderScore () {
            if (updateDisplay) {
                display.SetText(score.ToString());
                updateDisplay = false;
            }
            display.RenderText();
        }
    }
}
