using DIKUArcade.Entities;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.Graphics;
using Galaga;
using NUnit.Framework;

namespace GalagaTests;

public class TestsEnemy
{
    private Enemy enemyInsideBoarders;
    private Enemy enemyOutsideBoarders;

    [SetUp]
    public void Setup() {
        enemyInsideBoarders = new Enemy(
            new DynamicShape(new Vec2F(0.3f, 0.3f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, Path.Combine("../Galaga", "Assets", "Images", "BlueMonster.png")),
            new ImageStride(80, Path.Combine("../Galaga", "Assets", "Images", "RedMonster.png")));
        
        enemyOutsideBoarders = new Enemy(
            new DynamicShape(new Vec2F(0.3f, 0.0f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, Path.Combine("../Galaga", "Assets", "Images", "BlueMonster.png")),
            new ImageStride(80, Path.Combine("../Galaga", "Assets", "Images", "RedMonster.png")));
    }

    [Test]
    public void TestEnemyGetsDeleted() {
        for (int i = 1; i <=3; i++){
            enemyInsideBoarders.TakesDamage();
        }
        Assert.AreEqual(true,enemyInsideBoarders.IsDeleted());
    }

    [Test]
    public void TestEnemyHasNotKilledPlayer() {
        Assert.AreEqual(false,enemyInsideBoarders.KilledPlayer());
    }

    //Test that an enemy, that has reach the bottom of the window, will kill the player
    [Test]
    public void TestEnemyHasKilledPlayer() {
        Assert.AreEqual(true,enemyOutsideBoarders.KilledPlayer());
    }
}