// Decompiled with JetBrains decompiler
// Type: Spinballs.Common.Helper.Highscore
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Windows.Storage;
using System.Threading.Tasks;
using System;

#nullable disable
namespace Spinballs.Common.Helper
{
    public class Highscore
    {
        private const int _highscoreCount = 5;
        private static string filename = "highscores.xml";
        private List<int> _highscores;
        private static Highscore _instance;
        private static readonly string localFolder = ApplicationData.Current.LocalFolder.Path;

        public Highscore() => this._highscores = new List<int>();

        public static Highscore Instance
        {
            get
            {
                if (Highscore._instance == null)
                    Highscore._instance = Highscore.Load();
                return Highscore._instance;
            }
        }

        public List<int> Highscores
        {
            get => this._highscores;
            set => this._highscores = value;
        }

        public int Add(int score)
        {
            int index = 0;
            while (index < 5 && index < this.Highscores.Count && score <= this.Highscores[index])
                ++index;
            if (index >= 5)
                return -1;
            this.Highscores.Insert(index, score);
            if (this.Highscores.Count > 5)
                this.Highscores.RemoveRange(5, this.Highscores.Count - 5);
            this.Save();
            return index;
        }

        public void Clear()
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
                this.Highscores.Clear();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error clearing highscores: {ex.Message}");
            }
        }

        private void Save()
        {
            try
            {
                string highscorePath = Path.Combine(localFolder, filename);
                using (FileStream file = new FileStream(highscorePath, FileMode.Create))
                {
                    new XmlSerializer(this.GetType()).Serialize(file, this);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving highscores: {ex.Message}");
            }
        }

        public static Highscore Load()
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
                    Highscore highscore = new XmlSerializer(typeof(Highscore)).Deserialize(stream) as Highscore;
                    return highscore;
                }
            }
            catch
            {
                return new Highscore();
            }
        }
    }
}
