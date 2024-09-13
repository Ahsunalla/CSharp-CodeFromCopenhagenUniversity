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
using DIKUArcade.State;
using System;


namespace Galaga.GalagaStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private DIKUArcade.Graphics.Text newGame;
        private DIKUArcade.Graphics.Text quit;
        private DIKUArcade.Graphics.Text pointerNewGame;
        private DIKUArcade.Graphics.Text pointerQuit;
        private MainMenuOptions pointerCurrentPosition;
        public MainMenu(){
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f,1.0f)), new Image(Path.Combine("Assets", "Images", "TitleImage.png")));
            menuButtons = new Text[]{
                newGame = new Text("New Game", new Vec2F (0.11f, 0.2f), new Vec2F (0.45f, 0.45f)),
                quit = new Text("Quit", new Vec2F (0.11f, 0.1f), new Vec2F (0.45f, 0.45f)),
                pointerNewGame = new Text(">", new Vec2F (0.065f, 0.2f), new Vec2F (0.45f, 0.45f)),
                pointerQuit = new Text(">", new Vec2F (0.065f, 0.1f), new Vec2F (0.45f, 0.45f))
            };
            newGame.SetColor(new Vec3F(0.0f,0.0f,1.0f));
            quit.SetColor(new Vec3F(1.0f, 0.0f, 0.0f));
            pointerNewGame.SetColor(new Vec3F(1.0f, 0.0f, 1.0f));
            pointerQuit.SetColor(new Vec3F(0.05f, 0.05f, 0.13f));
            pointerCurrentPosition = MainMenuOptions.StartGame;
            
        }
        public static MainMenu GetInstance() {
            if (MainMenu.instance == null) {
                MainMenu.instance = new MainMenu();
            }
            return MainMenu.instance;
        }

        public void ResetState(){}
        public void RenderState(){
            backGroundImage.RenderEntity();
            foreach(Text text in menuButtons){
                text.RenderText();
            }
        }
        
        public void UpdateState(){
            if(pointerCurrentPosition == MainMenuOptions.StartGame){
                pointerNewGame.SetColor(new Vec3F(1.0f, 0.0f, 1.0f));
                pointerQuit.SetColor(new Vec3F(0.05f, 0.05f, 0.13f));

            } else {
                pointerNewGame.SetColor(new Vec3F(0.05f, 0.05f, 0.13f));
                pointerQuit.SetColor(new Vec3F(1.0f, 0.0f, 1.0f));
            }
            
        }

        public void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Up:
                    pointerCurrentPosition = MainMenuOptions.StartGame;
                    break;
                case KeyboardKey.Down:
                    pointerCurrentPosition = MainMenuOptions.Quit;
                    break;
                case KeyboardKey.Enter:
                    if(pointerCurrentPosition==MainMenuOptions.StartGame){
                        GalagaBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.GameStateEvent, 
                            Message  = "START_NEW_GAME"
                        });
                    } else {
                        GalagaBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.WindowEvent, 
                            Message  = "CLOSE_WINDOW"
                        });
                    }
                    break;
                default:
                    break;
            }
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
            switch(action){
                case KeyboardAction.KeyPress:
                    this.KeyPress(key);
                    break;
            }
        }
    }
    public enum MainMenuOptions {
        StartGame,
        Quit
    }
}