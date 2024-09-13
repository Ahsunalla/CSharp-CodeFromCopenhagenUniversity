using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;

namespace Galaga.Squadron {
    public class VFormation : ISquadron {
        private EntityContainer<Enemy> enemies { get; }
        public EntityContainer<Enemy> Enemies { 
            get {return enemies;}
        }
        private int maxEnemies { get; }
        public int MaxEnemies {
            get {return maxEnemies;}
        }
        
        public VFormation(EntityContainer<Enemy> Enemies, int MaxEnemies) {
            this.enemies = Enemies;
            this.maxEnemies = MaxEnemies; 
        }
        
        public void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemyStrides) {
            int n = 0;
            float posRight = 0.5f;
            float posLeft = 0.4f;
            float posY = 1.0f;
            bool leftNext = true;
            while (n < MaxEnemies && posLeft > 0.1f && posRight <= 0.9f) {
                if (leftNext) {
                    enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(posLeft, posY), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides),
                    new ImageStride(80, alternativeEnemyStrides)));
                    leftNext = false;
                } else {
                    enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(posRight, posY), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides),
                    new ImageStride(80, alternativeEnemyStrides))); 
                    posLeft -= 0.1f;
                    posRight += 0.1f;
                    posY += 0.1f;
                    leftNext = true;
                }
                n++;
            }
        }
    }
}
