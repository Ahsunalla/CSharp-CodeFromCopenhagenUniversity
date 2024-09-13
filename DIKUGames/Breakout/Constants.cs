using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Breakout {
    /// <summary>
    /// 
    /// </summary>
    public static class Constants {

        public static Vec2F BackGroundExtent => new Vec2F(1.0f, 1.0f);
        public static Vec2F BackGroundPosition => new Vec2F(0.0f, 0.0f);
        public static Shape BackGroundShape => new StationaryShape(BackGroundPosition, BackGroundExtent);


        public static Vec2F ScoreBoardPosition => new Vec2F(0.78f, 0.92f);
        public static Vec2F ScoreBoardExtent => new Vec2F(0.2f, 0.06f);
        public static Shape ScoreBoardShape => new StationaryShape(ScoreBoardPosition, ScoreBoardExtent);

        public static Vec2F BallExtent => new Vec2F(0.03f, 0.03f);

        public static Vec2F BallStartPosition => new Vec2F(0.5f - BallExtent.X / 2, 0.15f);
        public static DynamicShape BallShape => new DynamicShape(BallStartPosition, BallExtent);

        public static Vec2F BlockExtent => new Vec2F(0.08f, 0.03f);

    
        public static Vec2F PlayerExtent => new Vec2F(0.20f, 0.025f);

        public static Vec2F PlayerStartPosition => new Vec2F(0.5f - PlayerExtent.X / 2, 0.1f);
        public static DynamicShape PlayerShape => new DynamicShape(PlayerStartPosition, PlayerExtent);
    }
}