using Breakout.LevelLoading;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;


namespace Breakout.Blocks {

    /// <summary>
    /// A game entity manager for all blocks.
    /// </summary>
    public class BlockManager : GameEntityManager<Block>, IGameEventProcessor {
        private ILevelChangeSubject levelReader;
        private BlockDataParser dataParser;
        private BlockFactory blockFactory;
        public BlockManager(LevelReader levelReader) {
            this.levelReader = levelReader;
            levelReader.Add(this);
            BreakoutBus.GetBus().Subscribe(GameEventType.MovementEvent, this);
            entities = new EntityContainer<Block>();
            dataParser = new BlockDataParser();
            blockFactory = new BlockFactory();   
        }

        public override void Update() {
            entities.Iterate(block =>{});
            if (entities.CountEntities() == 0) {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent, Message = "SWITCH_LEVEL"});
            }
        }

        public override void Render() {
            entities.Iterate(block =>{
                block.Render();
            });
        }

        public override void InstantiateNewLevel() {
            var map = levelReader.GetMap();
            var meta = levelReader.GetMeta();
            var legend = levelReader.GetLegend();
            var blockData = dataParser.ExtractBlockData(map, meta, legend);
            entities = blockFactory.BuildLevelBlocks(blockData);
        }

        public virtual void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "EXPLOSION") {
                foreach(Block block in entities) {
                    if (gameEvent.ObjectArg1 != null) {
                        var position = gameEvent.ObjectArg1 as Vec2F;
                        if (position != null) {
                        var range = new Vec2F(0.1f, 0.05f);
                            if (Math.Abs(block.Shape.Position.X - position.X) <= range.X && Math.Abs(block.Shape.Position.Y - position.Y) <= range.Y) {
                                block.BlockHit(100);
                            }
                        }
                    }
                }
            }
        }
    }
}