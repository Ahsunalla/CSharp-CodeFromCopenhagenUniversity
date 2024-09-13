using Breakout.GameObjects;
using Breakout.Blocks;
using Breakout.ThePlayer;
using Breakout.Balls;
using Breakout.Collisions;
using DIKUArcade.Graphics;

namespace Breakout.LevelLoading {

    /// <summary>
    /// A class responsible for holding all level components.
    /// </summary>
    public class LevelHolder {
        private LevelReader levelReader;

        public Player player;

        private ILevelChangeObserver levelSpecifics;

    
        private GameEntityManager<Block> blocks;
        private GameEntityManager<Ball> balls;




        public LevelHolder() {
            levelReader = new LevelReader();


            player = new Player(Constants.PlayerShape);
            
            balls = new BallManager(levelReader);
            blocks = new BlockManager(levelReader);

            levelSpecifics = new LevelSpecifics(levelReader);

        }

        public void Update() {
            CollisionHandler.CheckAllCollision(blocks, balls, player);
            player.Update();

            levelSpecifics.Update();


            balls.Update();
            blocks.Update();

        }

        public void Render() {
            player.Render();


            balls.Render();
            blocks.Render();

 
            levelSpecifics.Render();


        }
    }
}