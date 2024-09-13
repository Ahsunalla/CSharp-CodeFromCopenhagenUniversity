using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Math;


namespace Breakout.Blocks {
    public class BlockInfo {

        public BlockInfo(BlockType type, StationaryShape shape, string image) {
            this.type = type;
            this.shape = shape;
            this.image = image;
        }
        public BlockType type;
        public StationaryShape shape;
        public string image;
    }

    /// <summary>
    /// This class will transform raw level data, loaded from an ASCII file and separated
    /// into data structures; and blabblalbla... to block data.
    /// </summary>
    public class BlockDataParser {
        
        public List<BlockInfo> ExtractBlockData (string[] map, string[] meta, string[] legend) {
            var blockChars = CreateBlockChars(legend);
            var blockData = new List<BlockInfo>();
            for (int i = 0; i < map.Length; i ++) {
                for (int j = 0; j < map[i].Length; j++) {

                    // Check that we should construct a block at position map[i, j]
                    if (Array.Exists(blockChars, element => element == map[i][j])) {
                        var index = Array.IndexOf(blockChars, map[i][j]);
                        var image = legend[index].Split(" ")[1]; 


                        blockData.Add(CreateBlockInfo(new Vec2F(0.02f + j * 0.08f, 0.88f - i * 0.03f), image, meta, map[i][j]));
                    }
                }
            }
            return blockData;
        }
        public char[] CreateBlockChars(String[] legend) {
            var arr = new char[legend.Length];
            for (int i = 0; i < legend.Length; i++){
                if (!string.IsNullOrEmpty(legend[i])) 
                    arr[i] = char.Parse(legend[i].Substring(0, 1));
            }
            return arr;
        }

        public BlockInfo CreateBlockInfo (Vec2F position, string image, string[] meta, char blockChar) {
            BlockInfo blockInfo = blockInfo = new BlockInfo(BlockType.Standard, new StationaryShape(position, Constants.BlockExtent), image);
            for (int i = 0; i < meta.Length - 1; i++) {
                if (meta[i].Split(" ")[1] == blockChar.ToString()) {
                    blockInfo = new BlockInfo(BlockTypeTransformer.StringToType(meta[i].Split(" ")[0]), new StationaryShape(position, Constants.BlockExtent), image);
                } 
            }
            return blockInfo;
        }
    }
}