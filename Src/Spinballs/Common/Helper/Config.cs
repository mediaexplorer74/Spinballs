// Decompiled with JetBrains decompiler
// Type: Spinballs.Common.Helper.Config
// Assembly: Spinballs, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9580170E-8A3B-4A32-8410-C21344CE25F7
// Assembly location: C:\Users\Admin\Desktop\RE\Spinballs\Spinballs.dll

using System.IO;
using System.Runtime.Serialization;
using Windows.Storage;
using System;
using System.Threading.Tasks;

#nullable disable
namespace Spinballs.Common.Helper
{
    [DataContract]
    public class Config
    {
        private static Config _instance;
        private static bool _inited;
        private float _soundVolume = 0.6f;
        private float _musicVolume = 0.6f;
        public float? OrigSoundVolume = new float?();
        public float? OrigMusicVolume = new float?();
        private int _lastPlayLevel;
        private bool _firstGameStart = true;
        private static readonly string filename = "spinballs.config";
        private static readonly string localFolder = ApplicationData.Current.LocalFolder.Path;

        public static Config Instance
        {
            get
            {
                if (Config._instance == null)
                {
                    Config._inited = false;
                    Config._instance = Config.Load();
                    if (Config._instance == null)
                        Config._instance = new Config();
                    Config._inited = true;
                }
                return Config._instance;
            }
        }

        [DataMember]
        public float SoundVolume
        {
            get => this._soundVolume;
            set => this._soundVolume = value;
        }

        [DataMember]
        public float MusicVolume
        {
            get => this._musicVolume;
            set
            {
                if (Config._inited && !Res.CanUseMusic)
                    return;
                this._musicVolume = value;
                AudioManager.SetMusicVolume(this._musicVolume);
            }
        }

        public float AdminMusicVolume
        {
            get => this._musicVolume;
            set
            {
                this._musicVolume = value;
                AudioManager.AdminSetMusicVolume(this._musicVolume);
            }
        }

        [DataMember]
        public int LastPlayLevel
        {
            get => this._lastPlayLevel;
            set => this._lastPlayLevel = value;
        }

        [DataMember]
        public bool FirstGameStart
        {
            get => this._firstGameStart;
            set => this._firstGameStart = value;
        }

        public void Save()
        {
            if (this.OrigSoundVolume.HasValue)
                this._soundVolume = this.OrigSoundVolume.Value;
            if (this.OrigMusicVolume.HasValue)
                this._musicVolume = this.OrigMusicVolume.Value;
            try
            {
                string configPath = Path.Combine(localFolder, filename);
                using (FileStream file = new FileStream(configPath, FileMode.Create))
                {
                    new DataContractSerializer(this.GetType()).WriteObject(file, this);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving config: {ex.Message}");
            }
        }

        public static void RemoveSave()
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
                System.Diagnostics.Debug.WriteLine($"Error removing config: {ex.Message}");
            }
        }

        private static Config Load()
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
                    Config config = new DataContractSerializer(typeof(Config)).ReadObject(stream) as Config;
                    return config;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
