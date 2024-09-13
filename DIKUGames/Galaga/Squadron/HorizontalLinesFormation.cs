using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.Collections.Generic;

namespace Galaga.Squadron
{
    public class HorizontalLinesFormation : ISquadron
    {
        private EntityContainer<Enemy> enemies { get; }
        public EntityContainer<Enemy> Enemies { 
            get {return enemies;}
        }
        private int maxEnemies { get; }
        public int MaxEnemies {
            get {return maxEnemies;}
        }
        public HorizontalLinesFormation(EntityContainer<Enemy> Enemies, int MaxEnemies) {
            this.enemies = Enemies;
            this.maxEnemies = MaxEnemies;
        }
        public void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemyStrides) {
            float posY = 1.1f;
            int j = 0;
            for (int i = 0; i < maxEnemies; i++, j++) {
                if(j > 3) {
                    j = 0;
                    posY = posY - 0.1f;
                }
                enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.15f + (float)j * 0.2f, posY), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides), new ImageStride(80, alternativeEnemyStrides)));
            }
        }
    }
}