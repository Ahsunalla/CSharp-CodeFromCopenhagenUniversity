using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout.ThePlayer {

    /// <summary>
    /// The player.
    /// </summary>
    public class Player : Entity, IGameEventProcessor {
        private FiringBehaviour firingBehaviour;
        private float moveRight = 0.0f;
        private float moveLeft = 0.0f;
        private float MOVEMENT_SPEED = 0.015f;
     
        public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
            BreakoutBus.GetBus().Subscribe(GameEventType.MovementEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            var imageStrides = ImageStride.CreateStrides(3,Path.Combine("..", "Breakout" , "Assets", "Images", "playerStride.png")); 
            firingBehaviour = new NoFiring(this);
        }

        public Player(DynamicShape shape) : this(shape, new ImageStride(50, ImageStride.CreateStrides(3,Path.Combine("..", "Breakout" , "Assets", "Images", "playerStride.png")))){}

        public void ResetPosition() {
            Shape.Position = Constants.PlayerStartPosition;
        }

        public Vec2F GetPosition() {
            return Shape.Position;
        }

        private void Fire() {
            firingBehaviour.Fire();
        }
        
        public void SetFiringBehaviour(FiringBehaviour firingBehaviour) {
            this.firingBehaviour = firingBehaviour;
        }

        public void KeyPress(string key) {
            switch (key) {
                case "KEY_RIGHT":
                    SetMoveRight(true);
                    break;
                case "KEY_LEFT":
                    SetMoveLeft(true);
                    break;
                case "KEY_UP":
                    Fire();
                    break;
                default:
                    break;
            }
        }

        public void KeyRelease(string key) {
            switch (key) {
                case "KEY_RIGHT":
                    SetMoveRight(false);
                    break;
                case "KEY_LEFT":
                    SetMoveLeft(false);
                    break;
                default:
                    break;
            }
        }
        public float GetExtentX(){
            return Shape.Extent.X;
        }
        public void Move() { 
            var moveTo = Shape.Position.X + moveLeft + moveRight;
            if (moveTo > 0 && moveTo + Shape.Extent.X < 1.0)
                Shape.Move();
        }
        private void SetMoveLeft(bool val) {
            if (val)
                moveLeft = -MOVEMENT_SPEED;
            else moveLeft = 0.0f;
            UpdateDirection();
        }
        private void SetMoveRight(bool val) {
            if (val)
                moveRight = +MOVEMENT_SPEED;
            else moveRight = 0.0f;
            UpdateDirection();
        }

        private void UpdateDirection(){
            Shape.AsDynamicShape().Direction = new Vec2F((moveLeft + moveRight),0.0f);
        }


        public void Update() {
            Move();
        }
        public void Render() {
            firingBehaviour.Render();
        }
        

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "KEY_PRESS") {
                KeyPress(gameEvent.StringArg1);
            } else if (gameEvent.Message == "KEY_RELEASE") {
                KeyRelease(gameEvent.StringArg1);
            } else if (gameEvent.Message == "NEW_LEVEL") {
                ResetPosition();
            }
        }
    }
}