
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Text.Json;
using System.Globalization;

namespace SequoiaEngine
{

    /// <summary>
    /// The Resource Manager is a singleton that should be accesible from anywhere. Handles loading fonts, textures, songs, and sound effects
    /// </summary>
    /// This may change in the future for optimization, only loading in resources
    /// that are useful on the screens they are useful... However, that change will happen later if needed
    public static partial class ResourceManager
    {
       
        private static Dictionary<string, SpriteFont> fonts = new();
        private static Dictionary<string, Texture2D> textures = new();
        private static Dictionary<string, SoundEffect> soundEffects = new();
        private static Dictionary<string, Song> music = new();

        public static ContentManager Manager { get; set; }

        public static T Get<T>(string filepath) where T : class
        {
            if (typeof(T) == typeof(SpriteFont))
            {
                return GetFont(filepath) as T;
            }
            else if (typeof(T) == typeof(Texture2D))
            {
                return GetTexture(filepath) as T;
            }
            else if (typeof(T) == typeof(Song))
            {
                return GetSong(filepath) as T;
            }
            else if (typeof(T) == typeof(SoundEffect))
            {
                return GetSoundEffect(filepath) as T;
            }

            else
            {
                throw new NotSupportedException($"Type {typeof(T)} is not supported.");
            }
        }

        public static void Load<T>(string path, string name)
        {
            if (typeof(T) == typeof(SpriteFont))
            {
                RegisterFont(path, name);
            }
            else if (typeof(T) == typeof(Texture2D))
            {
                RegisterTexture(path, name);
            }
            else if (typeof(T) == typeof(Song))
            {
                RegisterSong(path, name);
            }
            else if (typeof(T) == typeof(SoundEffect))
            {
                RegisterSoundEffect(path, name);
            }
            else
            {
                throw new NotSupportedException($"Type {typeof(T)} is not supported.");
            }
        }

        private static SpriteFont GetFont(string fontName)
        {
            if (!fonts.ContainsKey(fontName))
            {
                throw new Exception($"{fontName} doesn't exist in the current resource manager");
            }

            return fonts[fontName];
        }

        private static Texture2D GetTexture(string textureName)
        {
            if (!textures.ContainsKey(textureName))
            {
                throw new Exception($"{textureName} doesn't exist in the current resource manager");
            }

            return textures[textureName];
        }

        private static Song GetSong(string songName)
        {
            if (!music.ContainsKey(songName))
            {
                throw new Exception($"{songName} doesn't exist in the current resource manager");
            }

            return music[songName];
        }

        private static SoundEffect GetSoundEffect(string soundEffectName)
        {
            if (!soundEffects.ContainsKey(soundEffectName))
            {
                throw new Exception($"{soundEffectName} doesn't exist in the current resource manager");
            }
            return soundEffects[soundEffectName];
        }

        private static void RegisterFont(string pathToFont, string fontName)
        {
            if (!fonts.ContainsKey(fontName))
            {
                fonts.Add(fontName, Manager.Load<SpriteFont>(pathToFont));
            }
        }

        private static void RegisterTexture(string pathToTexture, string textureName)
        {
            if (!textures.ContainsKey(textureName))
            {
                textures.Add(textureName, Manager.Load<Texture2D>(pathToTexture));
            }
        }

        public static void RegisterTexture(Texture2D texture, string textureName)
        {
            if (!textures.ContainsKey(textureName))
            {
                textures.Add(textureName, texture);
            }
        }

        private static void RegisterSoundEffect(string pathToSoundEffect, string soundEffectName)
        {
            if (!soundEffects.ContainsKey(pathToSoundEffect))
            {
                soundEffects.Add(soundEffectName, Manager.Load<SoundEffect>(pathToSoundEffect));
            }
        }

        private static void RegisterSong(string pathToSong, string songName)
        {
            if (!music.ContainsKey(songName))
            {
                music.Add(songName, Manager.Load<Song>(pathToSong));
            }
        }
    }
}
