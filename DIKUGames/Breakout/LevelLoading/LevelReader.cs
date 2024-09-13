using System.Collections.Generic;
using DIKUArcade.Events;

namespace Breakout.LevelLoading {
    
    /// <summary>
    /// A reader of level files and subject in our level loading observer pattern.
    /// </summary>
    public class LevelReader : ILevelChangeSubject, IGameEventProcessor {
        private List<ILevelChangeObserver> observers;
        private string[] file;
        public string [] map;
        public string[] meta;
        public string[] legend;

        public static int i = 0;
        public LevelReader () {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            observers = new List<ILevelChangeObserver>();
        }

        public void Add(ILevelChangeObserver observer) {
            observers.Add(observer);
        }

        public String[] GetMap() {
            return map;
        }

        public String[] GetMeta() {
            return meta;
        }

        public String[] GetLegend() {
            return legend;
        }

        public void LoadNewlevel(string filename) {
            file = System.IO.File.ReadAllLines(filename);
            CreateAllSubFiles();
            NotifyObservers();
        }

        public void CreateAllSubFiles() {
            map = CreateSubFile("Map");
            meta = CreateSubFile("Meta");
            legend = CreateSubFile("Legend");
        }

        public string[] CreateSubFile(string subFileName) {
            var startWord = subFileName + ":";
            var stopWord = subFileName + "/";
            if (Array.Exists(file, element => element == startWord) && Array.Exists(file, element => element == stopWord)) {
                var subFileStarts = Array.IndexOf(file, startWord) + 1;
                var subFileEnds = Array.IndexOf(file, stopWord);
                string[] arr = new string[subFileEnds - subFileStarts]; 
                for (int i = subFileStarts; i < subFileEnds; i++)
                    arr[i - subFileStarts] = file[i];
                return arr;
            } else return new string[0]; 
        }

        public void NotifyObservers() {
            foreach(ILevelChangeObserver observer in observers) {
                observer.InstantiateNewLevel();
            }
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "NEW_LEVEL") {
                LoadNewlevel(gameEvent.StringArg1);
            }
        }
    }
}