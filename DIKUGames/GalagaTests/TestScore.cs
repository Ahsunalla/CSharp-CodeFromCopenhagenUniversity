using NUnit.Framework;
using DIKUArcade.Math;
using Galaga;

namespace GalagaTests;

public class TestScore
{
    private Score score;

    [SetUp]
    public void Setup()
    {
        score = new Score(new Vec2F(0.0f, 0.5f), new Vec2F(0.5f, 0.5f));
    }

    [Test] //Test that the correct amount of points are added to the score board
    public void TestThatPointsAreAddedCorrectly()
    {
        int pointsGained = 50;

        for (int i = 1; i <=pointsGained; i++){
            score.AddPoints();
        }
        Assert.AreEqual(pointsGained, score.getScore);
    }
}