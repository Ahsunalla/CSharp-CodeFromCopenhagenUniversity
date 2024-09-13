using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Breakout.GameObjects {
    
    /// <summary>
    /// This class will transform raw level data, loaded from an ASCII file and separated
    /// into data structures into a text representing the name of a level.
    /// </summary>
    public class LevelName : IMetaHandler, IGameEventProcessor {
        private Text name;
        public LevelName() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            name = new Text("", new Vec2F(0.35f, 0.1f), new Vec2F(0.5f, 0.3f));
            name.SetColor(255, 255, 255, 255);
        }

        public void HandleMeta(string[] meta) {
            for (int i = 0; i < meta.Length; i++) {
                if (meta[i].Split(" ")[0] == "Name:") {
                    var spaceIndex = meta[i].IndexOf(" ");
                    var newName = meta[i].Substring(spaceIndex);
                    name.SetText(newName);
                }
            }
        }
        public void Update() {}
        public void Render() {
            name.RenderText();
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "START_LEVEL") {
                name.SetText("");
            }
        }
    }
}