using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.State;

namespace Galaga.GalagaStates {
    public class GamePaused : IGameState {
        private static GamePaused instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private DIKUArcade.Graphics.Text continueGame;
        private DIKUArcade.Graphics.Text mainMenu;
        private DIKUArcade.Graphics.Text pointerContinueGame;
        private DIKUArcade.Graphics.Text pointerMainMenu;
        private PausedMenuOptions pointerCurrentPosition;
        public GamePaused(){
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f,1.0f)), new Image(Path.Combine("Assets", "Images", "TitleImage.png")));
            menuButtons = new Text[]{
                continueGame = new Text("Continue", new Vec2F (0.11f, 0.2f), new Vec2F (0.45f, 0.45f)),
                mainMenu = new Text("Main menu", new Vec2F (0.11f, 0.1f), new Vec2F (0.45f, 0.45f)),
                pointerContinueGame = new Text(">", new Vec2F (0.065f, 0.2f), new Vec2F (0.45f, 0.45f)),
                pointerMainMenu = new Text(">", new Vec2F (0.065f, 0.1f), new Vec2F (0.45f, 0.45f))
            };
            continueGame.SetColor(new Vec3F(0.0f,0.0f,1.0f));
            mainMenu.SetColor(new Vec3F(1.0f, 0.0f, 0.0f));
            pointerContinueGame.SetColor(new Vec3F(1.0f, 0.0f, 1.0f));
            pointerMainMenu.SetColor(new Vec3F(0.05f, 0.05f, 0.13f));
            pointerCurrentPosition = PausedMenuOptions.Continue;
            
        }
        
        public static GamePaused GetInstance() {
            if (GamePaused.instance == null) {
                GamePaused.instance = new GamePaused();
            }
            return GamePaused.instance;
        }

        public void RenderState(){
            backGroundImage.RenderEntity();
            foreach(Text text in menuButtons){
                text.RenderText();
            }
        }

        public void UpdateState(){
            if(pointerCurrentPosition == PausedMenuOptions.Continue){
                pointerContinueGame.SetColor(new Vec3F(1.0f, 0.0f, 1.0f));
                pointerMainMenu.SetColor(new Vec3F(0.05f, 0.05f, 0.13f));

            } else {
                pointerContinueGame.SetColor(new Vec3F(0.05f, 0.05f, 0.13f));
                pointerMainMenu.SetColor(new Vec3F(1.0f, 0.0f, 1.0f));
            }
        }

        public void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Up:
                    pointerCurrentPosition = PausedMenuOptions.Continue;
                    break;
                case KeyboardKey.Down:
                    pointerCurrentPosition = PausedMenuOptions.MainMenu;
                    break;
                case KeyboardKey.Enter:
                    if(pointerCurrentPosition==PausedMenuOptions.Continue){
                        GalagaBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.GameStateEvent, 
                            Message  = "CONTINUE"
                        });
                    } else {
                        GalagaBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = GameEventType.GameStateEvent, 
                            Message  = "MAIN_MENU"
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

        public void ResetState(){}
    }
    public enum PausedMenuOptions {
        Continue,
        MainMenu
    }
}