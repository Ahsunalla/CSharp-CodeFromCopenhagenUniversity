namespace Breakout.Blocks {
    public enum BlockType {
        Standard,
        Unbreakable,
        Explosive,
        Hardened,
        Invisible,
        Sequence,
        PowerUp
    }

    /// <summary>
    /// A transformer with the purpose of turning string into BlockTypes.
    /// </summary>
    public static class BlockTypeTransformer {
        public static BlockType StringToType (string blockTypeString) {
            BlockType blockType;
            switch (blockTypeString) {
                case "Unbreakable:":
                    blockType = BlockType.Unbreakable;
                    break;
                case "Explosive:":
                    blockType = BlockType.Explosive;
                    break;
                case "Invisible:":
                    blockType = BlockType.Invisible;
                    break;
                case "Hardened:":
                    blockType = BlockType.Hardened;
                    break;
                case "Sequence:":
                    blockType = BlockType.Sequence;
                    break;
                case "PowerUp:":
                    blockType = BlockType.PowerUp;
                    break;
                default:
                    blockType = BlockType.Standard;
                    break;
                
            }
            return blockType;
        }
    }
}