using System.Collections.Generic;
using Breakout.LevelLoading;

namespace Breakout.GameObjects {


    public class LevelSpecifics : ILevelChangeObserver {
        private List<IMetaHandler> levelSpecifics;
        private ILevelChangeSubject levelReader;

        public LevelSpecifics(ILevelChangeSubject levelReader) {
            this.levelReader = levelReader;
            levelReader.Add(this);
            levelSpecifics = new List<IMetaHandler>();
            levelSpecifics.Add(new LevelName());
        }

        public void InstantiateNewLevel() {
            var meta = levelReader.GetMeta();
            foreach(IMetaHandler levelSpecific in levelSpecifics) {
                levelSpecific.HandleMeta(meta);
            }
        }

        public void Update() {
            foreach(IMetaHandler levelSpecific in levelSpecifics) {
                levelSpecific.Update();
            }
        }

        public void Render() {
            foreach(IMetaHandler levelSpecific in levelSpecifics) {
                levelSpecific.Render();
            }
        }
    }
}