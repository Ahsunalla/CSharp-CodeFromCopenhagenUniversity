namespace Breakout.LevelLoading {
    public enum LevelType {
        Level_1,
        Level_2,
        Level_3,
        Central_Mass,
        Columns,
        Wall
    }

    /// <summary>
    /// A transformer with the purpose of turning LevelTypes into strings.
    /// </summary>
    public static class LevelTransformer {
        public static string LevelTypeToString (LevelType level) {
            string stringLev = "";
            var path = @"Assets/Levels/";
            switch (level) {
                case LevelType.Level_1:
                    stringLev = path + "level1.txt";
                    break;
                case LevelType.Level_2:
                    stringLev = path + "level2.txt";
                    break;
                case LevelType.Level_3:
                    stringLev = path + "level3.txt";
                    break;
                case LevelType.Central_Mass:
                    stringLev = path + "central-mass.txt";
                    break;
                case LevelType.Columns:
                    stringLev = path + "columns.txt";
                    break;
                case LevelType.Wall:
                    stringLev = path + "wall.txt";
                    break;
                default:
                    break;
            }
            return stringLev;
        }

    }

} 