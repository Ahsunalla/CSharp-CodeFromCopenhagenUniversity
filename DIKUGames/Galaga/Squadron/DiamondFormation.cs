using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;

namespace Galaga.Squadron {
    public class DiamondFormation : ISquadron {
        private EntityContainer<Enemy> enemies { get; }
        public EntityContainer<Enemy> Enemies { 
            get {return enemies;}
        }
        private int maxEnemies { get; }
        public int MaxEnemies {
            get {return maxEnemies;}
        }
        
        public DiamondFormation(EntityContainer<Enemy> Enemies, int MaxEnemies) {
            this.enemies = Enemies;
            this.maxEnemies = MaxEnemies; 
        }

        public void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemyStrides) {
            int n = 0;
            float posX = 0.45f;
            float posY = 1.0f;
            bool goToMiddle = false;
            while (n < MaxEnemies){
                if (0.4f < posX && posX < 0.5f) {
                    enemies.AddEntity(new Enemy(
                        new DynamicShape(new Vec2F(posX, posY), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStrides),
                        new ImageStride(80, alternativeEnemyStrides))); 
                        posX += 0.15f;
                        posY += 0.075f;
                    n++;
                } else if (0.5f < posX && posX < 0.7) {
                    enemies.AddEntity(new Enemy(
                        new DynamicShape(new Vec2F(posX, posY), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStrides),
                        new ImageStride(80, alternativeEnemyStrides))); 
                    enemies.AddEntity(new Enemy(
                        new DynamicShape(new Vec2F(0.9f-posX, posY), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStrides),
                        new ImageStride(80, alternativeEnemyStrides))); 
                    if (goToMiddle) {
                        posX -= 0.15f;
                        goToMiddle = false;
                    } else {
                        posX += 0.15f;
                        goToMiddle = true;
                    }    
                    posY += 0.075f;
                    n += 2;
                } else if (0.7f < posX) {
                    enemies.AddEntity(new Enemy(
                        new DynamicShape(new Vec2F(posX, posY), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStrides),
                        new ImageStride(80, alternativeEnemyStrides))); 
                    enemies.AddEntity(new Enemy(
                        new DynamicShape(new Vec2F(0.9f-posX, posY), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStrides),
                        new ImageStride(80, alternativeEnemyStrides)));
                    posX -= 0.15f;
                    posY += 0.075f;
                    n += 2;
                }
            }
        }    
    }
}
