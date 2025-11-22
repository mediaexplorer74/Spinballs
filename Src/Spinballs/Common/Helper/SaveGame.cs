// Decompiled with JetBrains decompiler
// Type: Spinballs.Common.Helper.SaveGame
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using Spinballs.Document;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Windows.Storage;
using System.Threading.Tasks;
using System;

#nullable disable
namespace Spinballs.Common.Helper
{
    [DataContract]
    public class SaveGame
    {
        private int _points;
        private int _activeScreenId = -1;
        private List<BallSave> _balls = new List<BallSave>();
        private GameState _state;
        private GameState _prevState;
        private GameState _nextState;
        private List<int> _chainBalls = new List<int>();
        [DataMember]
        public Dictionary<string, ControllerSave> Controller = new Dictionary<string, ControllerSave>();
        private static readonly string filename = "spinballs.savegame";
        private static readonly string localFolder = ApplicationData.Current.LocalFolder.Path;

        [DataMember]
        public int ActiveScreenId
        {
            get => this._activeScreenId;
            set => this._activeScreenId = value;
        }

        [DataMember]
        public List<BallSave> Balls
        {
            get => this._balls;
            set => this._balls = value;
        }

        [DataMember]
        public int Points
        {
            get => this._points;
            set => this._points = value;
        }

        [DataMember]
        public GameState State
        {
            get => this._state;
            set => this._state = value;
        }

        [DataMember]
        public GameState NextState
        {
            get => this._nextState;
            set => this._nextState = value;
        }

        [DataMember]
        public GameState PrevState
        {
            get => this._prevState;
            set => this._prevState = value;
        }

        [DataMember]
        public List<int> ChainBalls
        {
            get => this._chainBalls;
            set => this._chainBalls = value;
        }

        public T GetController<T>(object obj) where T : ControllerSave
        {
            ControllerSave controllerSave;
            return this.Controller.TryGetValue(obj.GetType().FullName, out controllerSave) ? controllerSave as T : default(T);
        }

        public T NewController<T>(object obj) where T : ControllerSave, new()
        {
            T obj1 = new T();
            this.Controller.Add(obj.GetType().FullName, (ControllerSave)obj1);
            return obj1;
        }

        public void Save()
        {
            try
            {
                string saveDataPath = Path.Combine(localFolder, filename);
                using (FileStream file = new FileStream(saveDataPath, FileMode.Create))
                {
                    new DataContractSerializer(this.GetType()).WriteObject(file, this);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving game: {ex.Message}");
            }
        }

        public static bool SaveGameExists()
        {
            try
            {
                var task = ApplicationData.Current.LocalFolder.TryGetItemAsync(filename).AsTask();
                task.Wait();
                return task.Result != null;
            }
            catch
            {
                return false;
            }
        }

        public static void RemoveSaveGame()
        {
            try
            {
                var task = ApplicationData.Current.LocalFolder.TryGetItemAsync(filename).AsTask();
                task.Wait();
                if (task.Result != null)
                {
                    var deleteTask = ((StorageFile)task.Result).DeleteAsync().AsTask();
                    deleteTask.Wait();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error removing save game: {ex.Message}");
            }
        }

        public static SaveGame Load()
        {
            try
            {
                var task = ApplicationData.Current.LocalFolder.GetFileAsync(filename).AsTask();
                task.Wait();
                StorageFile file = task.Result;
                
                var openTask = file.OpenStreamForReadAsync();
                openTask.Wait();
                using (Stream stream = openTask.Result)
                {
                    SaveGame saveGame = new DataContractSerializer(typeof(SaveGame)).ReadObject(stream) as SaveGame;
                    return saveGame;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
