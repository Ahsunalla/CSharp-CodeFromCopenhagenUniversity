using NUnit.Framework;
using DIKUArcade.Entities;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using Galaga.Squadron;
using Galaga;

namespace GalagaTests;

public class TestsSquadron {
    private ISquadron formation;
    private int numEnemies;
    private EntityContainer<Enemy> enemies;
    private List<Image> blueMonstersImage;
    private List<Image> redMonstersImage;

    [SetUp]
    public void Setup() {
        numEnemies = 8;
        enemies = new EntityContainer<Enemy>(numEnemies);
        blueMonstersImage = ImageStride.CreateStrides(4, Path.Combine("../Galaga", "Assets", "Images", "BlueMonster.png"));
        redMonstersImage = ImageStride.CreateStrides(2, Path.Combine("../Galaga", "Assets", "Images", "RedMonster.png"));
    }

    //Test that the correct amount of enemies are created in HorizontalLinesFormation
    //Note that HorizontalLinesFormation can have any number of enemies
    [Test] 
    public void TestAmountOfEnemies_HorizontalLinesFormation() {
        for (int i = 1; i <=100; i++) { 
            numEnemies = i;
            formation = new HorizontalLinesFormation(enemies, numEnemies);
            formation.CreateEnemies(blueMonstersImage, redMonstersImage);
            Assert.AreEqual(numEnemies, formation.Enemies.CountEntities());
            enemies.ClearContainer();
        }
    }

    //Test that the correct amount of enemies are created in VFormation
    //Note that VFormation can have maximum eight enemies
    [Test] 
    public void TestAmountOfEnemies_VFormation() {
        for (int i = 1; i <= 8; i++) {
            numEnemies = i;
            formation = new VFormation(enemies, numEnemies);
            formation.CreateEnemies(blueMonstersImage, redMonstersImage);
            Assert.AreEqual(numEnemies, formation.Enemies.CountEntities());
            enemies.ClearContainer();
        }
    }
    
    //Test that the correct amount of enemies are created in DiamondFormation
    //Note that DiamondFormation can only have eight enemies
    [Test] 
    public void TestAmountOfEnemies_DiamondFormation() {
            numEnemies = 8;
            formation = new DiamondFormation(enemies, numEnemies);
            formation.CreateEnemies(blueMonstersImage, redMonstersImage);
            Assert.AreEqual(numEnemies, formation.Enemies.CountEntities());
    }

    //Test that no more than the maximum amount of eight enemies are created with VFormation
    [Test] 
    public void TestMaxNotExceed_VFormation() {
        for (int i = 9; i <= 20; i++) {
            numEnemies = i;
            formation = new VFormation(enemies, numEnemies);
            formation.CreateEnemies(blueMonstersImage, redMonstersImage);
            Assert.AreEqual(8, formation.Enemies.CountEntities());
            enemies.ClearContainer();
        }
    }
}