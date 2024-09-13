using Galaga;
using NUnit.Framework;

namespace GalagaTests {
    public class TestStateTransformer
    {
        [SetUp]
        public void Setup(){}
 
        [Test]
        public void TestTransformStringToState_GameRunning() {
            var value = StateTransformer.TransformStringToState("GAME_RUNNING");
            Assert.AreEqual(value, GameStateType.GameRunning);
        }
 
        [Test]
        public void TestTransformStringToState_GamePaused() {
            var value = StateTransformer.TransformStringToState("GAME_PAUSED");
            Assert.AreEqual(value, GameStateType.GamePaused);
        }
 
        [Test]
        public void TestTransformStringToState_MainMenu() {
            var value = StateTransformer.TransformStringToState("MAIN_MENU");
            Assert.AreEqual(value, GameStateType.MainMenu);
        }

        [Test]
        public void TestTransformStateToString_GameRunning() {
            var value = StateTransformer.TransformStateToString(GameStateType.GameRunning);
            Assert.AreEqual(value, "GAME_RUNNING");
        }

        [Test]
        public void TestTransformStateToString_GamePaused() {
            var value = StateTransformer.TransformStateToString(GameStateType.GamePaused);
            Assert.AreEqual(value, "GAME_PAUSED");
        }

        [Test]
        public void TestTransformStateToString_MainMenu() {
            var value = StateTransformer.TransformStateToString(GameStateType.MainMenu);
            Assert.AreEqual(value, "MAIN_MENU");
        }
    }
}