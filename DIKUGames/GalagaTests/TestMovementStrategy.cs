using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.Graphics;
using Galaga.MovementStrategy;
using Galaga;

namespace GalagaTests;

public class TestsMovementStrategy {
    private Enemy enemy;
    private Down down;
    private ZigZagDown zigZagDown;
    [SetUp]
    public void Setup() {
        enemy = new Enemy(
            new DynamicShape(new Vec2F(0.3f, 0.3f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, Path.Combine("../Galaga", "Assets", "Images", "BlueMonster.png")),
            new ImageStride(80, Path.Combine("../Galaga", "Assets", "Images", "RedMonster.png")));
        
        down = new Down();
        zigZagDown = new ZigZagDown();
    }

    //Test that enemy moves downwards with MovementStragegy Down
    [Test]
    public void TestEnemyMovesDown() {
        float initPosY = enemy.Shape.Position.Y;
        down.MoveEnemy(enemy);
        float newPosY = enemy.Shape.Position.Y;
        Assert.Greater(initPosY, newPosY);
    }

    //Test that enemy moves downwards with MovementStragegy ZigZagDown
    [Test]
    public void TestEnemyMovesDownZigZag() {
        float initPosY = enemy.Shape.Position.Y;
        zigZagDown.MoveEnemy(enemy);
        float newPosY = enemy.Shape.Position.Y;
        Assert.Greater(initPosY, newPosY);
    }
}