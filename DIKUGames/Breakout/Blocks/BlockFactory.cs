using System.Collections.Generic;
using Breakout.Blocks;
using DIKUArcade.Entities;


namespace Breakout.LevelLoading {

    /// <summary>
    /// A factory handling the instantiation of blocks.
    /// </summary>
    public class BlockFactory {
        
        public EntityContainer<Block> BuildLevelBlocks(List<BlockInfo> blockData) {
            var blocks = new EntityContainer<Block>(blockData.Count);
            foreach(BlockInfo blockInfo in blockData) {
                var block = CreateBlock(blockInfo);
                blocks.AddEntity(block);
            }
            return blocks;
        }

        public Block CreateBlock(BlockInfo blockInfo) {
            Block block = null;
            switch (blockInfo.type) {
                case BlockType.Unbreakable:
                    block = new UnbreakableBlock(blockInfo.shape, blockInfo.image);
                    break;
                case BlockType.Hardened:
                    block = new HardenedBlock(blockInfo.shape, blockInfo.image);
                    break;
                case BlockType.Invisible:
                    block = new InvisibleBlock(blockInfo.shape, blockInfo.image);
                    break;
                case BlockType.PowerUp:
                    block = new PowerUpBlock(blockInfo.shape, blockInfo.image);
                    break;
                case BlockType.Explosive:
                    block = new ExplosiveBlock(blockInfo.shape, blockInfo.image);
                    break;
                case BlockType.Sequence:
                    block = new SequenceBlock(blockInfo.shape, blockInfo.image);
                    break;
                default:
                    block = new StandardBlock(blockInfo.shape, blockInfo.image);
                    break;
            }
            return block;
        }
    }
}