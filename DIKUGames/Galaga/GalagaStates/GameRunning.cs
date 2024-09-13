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

namespace Galaga.GalagaStates {
    public class GameRunning : IGameState, IGameEventProcessor {
        private static GameRunning instance = null;
        private Player player;
        private EntityContainer<Enemy> enemies;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        private AnimationContainer enemyExplosions;
        private List<Image> blueMonstersImage;
        private List<Image> redMonstersImage;
        private List<Image> explosionStrides;
        private ISquadron formation;
        private Down down;
        private ZigZagDown zigZagDown;
        private Score score;
        private readonly int numEnemies;
        private int nextFormation;
        private bool zigZagMovement;
        private const int EXPLOSION_LENGTH_MS = 500;

        public GameRunning(){
            GalagaBus.GetBus().Subscribe(GameEventType.GraphicsEvent, this);
            
            //The player
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));

            //Enemies
            blueMonstersImage = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            redMonstersImage = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "RedMonster.png"));
            numEnemies = 8;
            enemies = new EntityContainer<Enemy>(numEnemies);

            //MovementStrategies
            down = new Down();
            zigZagDown = new ZigZagDown();
            nextFormation = 1;
            zigZagMovement = false;

            //Player shots and explosions
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            enemyExplosions = new AnimationContainer(numEnemies);
            explosionStrides = ImageStride.CreateStrides(8,Path.Combine("Assets", "Images", "Explosion.png"));

            //Score
            score = new Score(new Vec2F(0.0f, 0.5f), new Vec2F(0.5f, 0.5f));
        }

        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
            }
            return GameRunning.instance;
        }

        public void AddExplosion(Vec2F position, Vec2F extent) {
            StationaryShape stat_shape = new StationaryShape(position, extent);
            ImageStride image = new ImageStride(EXPLOSION_LENGTH_MS/8, explosionStrides);
            enemyExplosions.AddAnimation(stat_shape, EXPLOSION_LENGTH_MS, image);
        }

        private bool PlayerIsDead(EntityContainer<Enemy> enemies) {                      
            foreach (Enemy enemy in enemies) {
                if (enemy.KilledPlayer())
                    return true;
            }
            return false;
        }

        private void IterateShots() {
            playerShots.Iterate(shot => {
                shot.Shape.MoveY(0.021f);
                if (shot.Shape.Position.Y > 1.0f) {
                    shot.DeleteEntity();
                } else {
                    enemies.Iterate(enemy => {
                        CollisionData collisionData = CollisionDetection.Aabb(shot.Shape.AsDynamicShape(),enemy.Shape.AsStationaryShape());
                        if(collisionData.Collision) {
                            enemy.TakesDamage();
                            shot.DeleteEntity();
                            if(enemy.IsDeleted()){
                                AddExplosion(enemy.Shape.Position, new Vec2F(0.1f, 0.1f));
                                score.AddPoints();
                            }          
                        }
                    });
                }
            });
        }


        private void InstantiateNewEnemies() {
            if(nextFormation > 3) {
                if(zigZagMovement == false){
                    zigZagMovement = true;
                }
                else if (zigZagMovement == true){
                    GalagaBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.MovementEvent, 
                        Message  = "INCREASE_ENEMY_SPEED"
                    });
                    zigZagMovement = false;
                } 
                nextFormation = 1;
            }  

            if(nextFormation == 1) {
                formation = new HorizontalLinesFormation(enemies, numEnemies);
                formation.CreateEnemies(blueMonstersImage, redMonstersImage);
            }
            else if(nextFormation == 2) {
                formation = new VFormation(enemies, numEnemies);
                formation.CreateEnemies(blueMonstersImage, redMonstersImage);
            }
            else {
                formation = new DiamondFormation(enemies, numEnemies);
                formation.CreateEnemies(blueMonstersImage, redMonstersImage);
                }
            nextFormation++;

        }
        public void ResetState(){
            instance = null;
        }

        public void UpdateState(){
            if (enemies.CountEntities() == 0) {
                   InstantiateNewEnemies();
            }
            player.Move();
            if(zigZagMovement){
               zigZagDown.MoveEnemies(enemies);
            } else {
               down.MoveEnemies(enemies);
            }
            IterateShots();
        }

        public void RenderState(){
            if(!this.PlayerIsDead(enemies)){
                   player.Render();
                   enemies.RenderEntities();
                   playerShots.RenderEntities();
                   enemyExplosions.RenderAnimations();
                   score.RenderScore();
                } else {
                   score.RenderScore();
                }   
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GraphicsEvent) {
                switch (gameEvent.Message) {
                    case "NEW_SHOT_CREATED":
                        Vec2F shotPosition = new Vec2F (player.GetPosition().X+0.046f, player.GetPosition().Y+0.1f);
                        playerShots.AddEntity(new PlayerShot(shotPosition, playerShotImage));
                        break;
                }
            }
        }

        public void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent, 
                        Message  = "MOVE_LEFT"
                    });
                    break;
                case KeyboardKey.Right:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent, 
                        Message  = "MOVE_RIGHT"
                    });
                    break;
                case KeyboardKey.Down:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent,
                        Message = "MOVE_DOWN"
                    });
                    break;
                case KeyboardKey.Up:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent,
                        Message = "MOVE_UP"
                    });
                    break;
                case KeyboardKey.Space:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.GraphicsEvent,
                        Message = "NEW_SHOT_CREATED"
                    });
                    break;
                case KeyboardKey.Escape:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.GameStateEvent,
                        Message = "GAME_PAUSED"
                    });
                    break;
                default:
                    break;
            }
        }
        
        public void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent, 
                        Message  = "STOP_MOVE_LEFT"
                    });
                    break;
                case KeyboardKey.Right:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent, 
                        Message  = "STOP_MOVE_RIGHT"
                    });
                    break;
                case KeyboardKey.Down:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent, 
                        Message  = "STOP_MOVE_DOWN"
                    });
                    break;
                case KeyboardKey.Up:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent, 
                        Message  = "STOP_MOVE_UP"
                    });
                    break;
                case KeyboardKey.Escape:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.WindowEvent, 
                        Message  = "CLOSE_WINDOW"
                    });
                    break;
                default:
                    break;
            }
        }
        
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
            switch (action) {
                case KeyboardAction.KeyPress:
                    this.KeyPress(key);
                    break;
                case KeyboardAction.KeyRelease:
                    this.KeyRelease(key);
                    break;
            }
        }
    }
}