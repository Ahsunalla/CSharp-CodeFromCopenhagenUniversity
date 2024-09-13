
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Events;

namespace Galaga {
public class Player : IGameEventProcessor {
    private Entity entity;
    private DynamicShape shape;
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private float moveUp = 0.0f;
    private float moveDown = 0.0f;
    private const float MOVEMENT_SPEED = 0.012f;
    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
        GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
    }
    
    public void Move() {
        shape.Move();
        if (shape.Position.Y > 0.9f) {
                shape.Position.Y = 0.9f;
            }
            else if(shape.Position.Y < 0.1f) {
                shape.Position.Y = 0.1f;
            }
        
            if (shape.Position.X < 0.0f) {
                shape.Position.X = 0.0f;
            }
            else if(shape.Position.X > 0.9f) {
                shape.Position.X = 0.9f;
            }

    }
    private void SetMoveLeft(bool val) {
        if(val) {
            moveLeft =- MOVEMENT_SPEED; 
        }
        else {
            moveLeft = 0.0f;
        }
        UpdateDirection();
    }
    private void SetMoveRight(bool val) {
        if(val) {
            moveRight = MOVEMENT_SPEED; 
        }
        else {
            moveRight = 0.0f;
        }
        UpdateDirection();
    }
    private void SetMoveUp(bool val) {
        if(val) {
            moveUp = MOVEMENT_SPEED;
        }
        else {
            moveUp = 0.0f;
        }
        UpdateDirection();
    }
    private void SetMoveDown(bool val) {
        if(val) {
            moveDown =- MOVEMENT_SPEED;
        }
        else {
            moveDown = 0.0f;
        }
        UpdateDirection();
    }
    public void Render() {
        entity.RenderEntity();
    }
    private void UpdateDirection() {
        shape.Direction.X = moveLeft + moveRight;
        shape.Direction.Y = moveDown + moveUp;
    }
    public Vec2F GetPosition() {
        return this.shape.Position;
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.PlayerEvent) {
            switch (gameEvent.Message) {
                case "MOVE_LEFT":
                    this.SetMoveLeft(true);
                    break;
                case "STOP_MOVE_LEFT":
                    this.SetMoveLeft(false);
                    break;
                case "MOVE_RIGHT":
                    this.SetMoveRight(true);
                    break;
                case "STOP_MOVE_RIGHT":
                    this.SetMoveRight(false);
                    break;
                case "MOVE_DOWN":
                    this.SetMoveDown(true);
                    break;
                case "STOP_MOVE_DOWN":
                    this.SetMoveDown(false);
                    break;
                case "MOVE_UP":
                    this.SetMoveUp(true);
                    break;
                case "STOP_MOVE_UP":
                    this.SetMoveUp(false);
                    break;
                }
            }
        }
    }
}
