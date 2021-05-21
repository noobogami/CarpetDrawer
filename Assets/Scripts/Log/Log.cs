using System;
using System.Collections.Generic;
using System.Linq;
using Garaj.Prefs;
using Garaj.Utility;
using UnityEngine;
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedMember.Local
// ReSharper disable IdentifierTypo
namespace Garaj.GLog
{
    public static class Log
    {

        #region Debug

        public static void Debug(this string message, string prefixMessage, string section, Color color = Color.White) =>
            BaseLog(Level.Debug, message, prefixMessage, section, color);
        
        public static void Debug(this string message, string section) =>
            BaseLog(Level.Debug, message, section);
        
        public static void Debug(this int value, string prefixMessage, string section, Color color = Color.White) => 
            BaseLog(Level.Debug, value, prefixMessage, section, color);
        public static void Debug(this string message) =>
            BaseLog(Level.Debug, message, "");
        public static void Debug(this IEnumerable<string> messageList, bool oneLine, string prefixMessage = "", string section = "", Color color = Color.White) =>
            BaseLog(Level.Debug, messageList, oneLine, prefixMessage, section, color);
        public static void Debug(this bool value, string prefixMessage, string section, Color color = Color.White) => 
            BaseLog(Level.Debug, value, prefixMessage, section, color);

        #endregion



        #region Print

        public static void Print(this string message, string prefixMessage, string section, Color color = Color.White) =>
            BaseLog(Level.Print, prefixMessage + message, section,color);
        public static void Print(this string message, string section, Color color) =>
            BaseLog(Level.Print, message, section, color);
        public static void Print(this string message, string section) =>
            BaseLog(Level.Print, message, section);
        public static void Print(this string message) =>
            BaseLog(Level.Print, message, "");

        public static void Print(this float value, string prefixMessage, string section, Color color = Color.White) => 
            BaseLog(Level.Print, value, prefixMessage, section, color);
        public static void Print(this float value, string section) =>
            BaseLog(Level.Print, value, section);
        public static void Print(this bool value, string prefixMessage, string section, Color color = Color.White) => 
            BaseLog(Level.Print, value, prefixMessage, section, color);
        public static void Print(this bool value, string section) =>
            BaseLog(Level.Print, value, section);

        public static void Print(this int value, string prefixMessage, string section, Color color = Color.White) => 
            BaseLog(Level.Print, value, prefixMessage, section, color);
        
        public static void Print(this int value, string section) =>
            BaseLog(Level.Print, value, section);
        
        public static void Print(this long value, string section) =>
            BaseLog(Level.Print, value, section);
        public static void Print(this IEnumerable<string> messageList, bool oneLine, string prefixMessage,
            string section = "", Color color = Color.White) =>
            BaseLog(Level.Print, messageList, oneLine, prefixMessage, section, color);

        public static void Print(this Dictionary<string, string> dict, string prefixMessage,
            string section = "", Color color = Color.White) =>
            BaseLog(Level.Print, dict, prefixMessage, section, color);

        public static void Print(this Vector3 target, string additionalMessage, string objectNames,
            string section = "", bool oneLine = true, Color color = Color.White) =>
            BaseLog(Level.Print, target, additionalMessage, objectNames, section, oneLine, color);

        public static void Print(this Vector2 target, string additionalMessage, string objectNames,
            string section = "", bool oneLine = true, Color color = Color.White) =>
            BaseLog(Level.Print, target, additionalMessage, objectNames, section, oneLine, color);

        public static void Print(this Vector2Int target, string additionalMessage, string objectNames,
            string section = "", bool oneLine = true, Color color = Color.White) =>
            BaseLog(Level.Print, target, additionalMessage, objectNames, section, oneLine, color);

        public static void Print(this Transform target, string additionalMessage, string objectNames,
            string section = "", bool oneLine = true, Color color = Color.White) =>
            BaseLog(Level.Print, target, additionalMessage, objectNames, section, oneLine, color);

        #endregion

        #region Error

        public static void Error(this string message, string additionalMessage, string section) => 
            BaseLog(Level.Error, message + additionalMessage, section, Color.Red);
        public static void Error(this string message, string section = "") =>
            BaseLog(Level.Error, message, section, Color.Red);

        public static void Error(this IEnumerable<string> messageList, bool oneLine, string prefixMessage = "", string section = "") =>
            BaseLog(Level.Error, messageList, oneLine, prefixMessage, section, Color.Red);
        
        public static void Error(this Vector3 target, string additionalMessage = "", string objectNames = "",
            string section = "", bool oneLine = true) =>
            BaseLog(Level.Error, target, additionalMessage, objectNames, section, oneLine, Color.Red);

        public static void Error(this Vector2 target, string additionalMessage = "", string objectNames = "",
            string section = "", bool oneLine = true) =>
            BaseLog(Level.Error, target, additionalMessage, objectNames, section, oneLine, Color.Red);

        public static void Error(this Vector2Int target, string additionalMessage = "", string objectNames = "",
            string section = "", bool oneLine = true) =>
            BaseLog(Level.Error, target, additionalMessage, objectNames, section, oneLine, Color.Red);
        
        #endregion

        #region Useful Methods

        private static void BaseLog(Level level, float value, string prefixMessage, string section, Color color) =>
            BaseLog(level, value + "", prefixMessage, section, color);

        private static void BaseLog(Level level, bool value, string prefixMessage, string section, Color color) =>
            BaseLog(level, value.ToString(), prefixMessage, section, color);

        private static void BaseLog(Level level, int value, string prefixMessage, string section, Color color) =>
            BaseLog(level, value.ToString(), prefixMessage, section, color);

        private static void BaseLog(Level level, IEnumerable<string> messageList, bool oneLine, string prefixMessage,
            string section, Color color)
        {
            var text = "";
            foreach (var m in messageList)
            {
                if (oneLine)
                    text += m + ", ";
                else
                    BaseLog(level, m, prefixMessage, section, color);
            }
            if(oneLine)
                BaseLog(level, text, prefixMessage, section, color);
        }

        private static void BaseLog(Level level, Dictionary<string, string> dict, string prefixMessage,
            string section, Color color)
        {
            foreach (var keyValue in dict)
                BaseLog(level,
                    $"Key: {keyValue.Key.Bracketize().Boldize()}, Value: {keyValue.Value.Bracketize().Boldize()}",
                    prefixMessage, section, color);
        }

        private static void BaseLog(Level level, Vector3 target, string additionalMessage, string objectNames,
            string section, bool oneLine, Color color)
        {
            if (oneLine)
                BaseLog(level,
                    $"{additionalMessage.Bracketize()} {objectNames.Bracketize()} -> X: {target.x.Colorize(Color.Red, true)} | Y: {target.y.Colorize(Color.Green, true)} | Z: {target.z.Colorize(Color.Cyan, true)}",
                    section, color);
            else
            {
                BaseLog(level, $"{additionalMessage.Bracketize()} {objectNames.Bracketize()}", "", color);
                BaseLog(level, $"X: {target.x.Colorize(Color.Red, true)}", section, color);
                BaseLog(level, $"Y: {target.y.Colorize(Color.Green, true)}", section, color);
                BaseLog(level, $"Z: {target.z.Colorize(Color.Cyan, true)}", section, color);
            }
        }

        private static void BaseLog(Level level, Vector2 target, string additionalMessage, string objectNames,
            string section, bool oneLine, Color color) =>
            BaseLog(level, new Vector3(target.x, target.y, 0), additionalMessage, objectNames, section, oneLine, color);

        private static void BaseLog(Level level, Vector2Int target, string additionalMessage, string objectNames,
            string section, bool oneLine, Color color) =>
            BaseLog(level, new Vector3(target.x, target.y, 0), additionalMessage, objectNames, section, oneLine, color);

        private static void BaseLog(Level level, Transform target, string additionalMessage, string objectNames,
            string section, bool oneLine, Color color) =>
            BaseLog(level, target.position, additionalMessage, objectNames, section, oneLine, color);

        #endregion

        #region Logic

        private static void BaseLog(Level level, string message, string section, Color color)
        {
#if !UNITY_EDITOR
            if (!GPrefs.GetBool(PrefsKeys.IsDeveloper, false)) return;
#endif
            switch (level)
            {
                case Level.Print:
#if !Log_Print
                return;
#endif
                    break;
                case Level.Debug:
#if !Log_Debug
                return;
#endif
                    break;
                case Level.Error:
#if !Log_Error
                return;
#endif
                    break;
            }

            if (message == "" || message == " ") return;
            var prefix = $"[GLOG {level}]";
            var text =
                $"{prefix.Colorize(Color.Grey, false)}{section.Bracketize().Colorize(GetSectioncColor(section), false)} {message.Colorize(level == Level.Error ? Color.Red : color, false).Boldize()}";
            UnityEngine.Debug.Log(text);

        }

        private static void BaseLog(Level level, string message, string prefixMessage, string section, Color color) =>
            BaseLog(level, prefixMessage + message, section, color);
        private static void BaseLog(Level level, string message, string section) =>
            BaseLog(level,  message, section, Color.White);
        private static void BaseLog(Level level, float message, string section) =>
            BaseLog(level,  message.ToString(), section, Color.White);
        private static void BaseLog(Level level, int message, string section) =>
            BaseLog(level,  message.ToString(), section, Color.White);
        private static void BaseLog(Level level, long message, string section) =>
            BaseLog(level,  message.ToString(), section, Color.White);
        private static void BaseLog(Level level, bool message, string section) =>
            BaseLog(level,  message.ToString(), section, Color.White);
        private static Dictionary<string, Color> SectionsColor => _sectionsColor ?? (_sectionsColor = new Dictionary<string, Color>());


        #region Tools

        private static Dictionary<string, Color> _sectionsColor;
        private static string Bracketize(this string text) => text != "" ? $"[{text}]" : "";

        private static string Colorize(this string text, Color color, bool hex)
        {
            #if UNITY_EDITOR
            return $"<color={(hex ? Colors[color] : color.ToString())}>{text}</color>";
            #endif
            return hex ? $"<color={Colors[color]}>{text}</color>" : text;
        }

        private static string Colorize(this string text, string color) => $"<color={color}>{text}</color>";
        private static string Colorize(this int text, Color color, bool hex) => (text + "").Colorize(color, hex);
        private static string Colorize(this float text, Color color, bool hex) => (text + "").Colorize(color, hex);

        private static string Boldize(this string text)
        {
#if UNITY_EDITOR
            return $"<b>{text}</b>";
#endif
            return text;
        }

        private static Color GetSectioncColor(string section)
        { 
            if (!SectionsColor.ContainsKey(section))
                SectionsColor.Add(section, GetRandomColor());
            return SectionsColor[section];
        }

        private static Color GetRandomColor()
        {
            var colorCount =  Enum.GetNames(typeof(Color)).Length;
            var result = (Color) RandomEx.Random(colorCount);
            var counter = 0;
            while ((result == Color.Grey || result == Color.Black || result == Color.White || result == Color.Red ||
                    IsColorOccupied(result)) && _sectionsColor.Count < 15)
                result = (Color) RandomEx.Random(colorCount);
            
            return result;
        }

        private static bool IsColorOccupied(Color color) => SectionsColor.Any(s => s.Value == color);

        private static readonly Dictionary<Color, string> Colors = new Dictionary<Color, string>()
        {
            {Color.White, "#ffffffff"},
            {Color.Cyan, "#00ffffff"},
            {Color.Black, "#000000ff"},
            {Color.Blue, "#0000ffff"},
            {Color.Brown, "#a52a2aff"},
            {Color.Darkblue, "#00ffffff"},
            {Color.Magenta, "#ff00ffff"},
            {Color.Green, "#008000ff"},
            {Color.Grey, "#808080ff"},
            {Color.Lightblue, "#add8e6ff"},
            {Color.Lime, "#00ff00ff"},
            {Color.Maroon, "#800000ff"},
            {Color.Navy, "#000080ff"},
            {Color.Olive, "#808000ff"},
            {Color.Orange, "#ffa500ff"},
            {Color.Purple, "#800080ff"},
            {Color.Red, "#ff0000ff"},
            {Color.Silver, "#c0c0c0ff"},
            {Color.Teal, "#008080ff"},
            {Color.Yellow, "#ffff00ff"},
        };

        private enum Level
        {
            Print,
            Debug,
            Error
        }
        #endregion

        #endregion
    }

    public enum Color
    {
        White = 0,
        Red = 1,
        Blue = 2,
        Green = 3,
        Yellow = 4,
        Grey = 5,
        Cyan = 6,
        Orange = 7,
        Brown = 8,
        Black = 9,
        Darkblue = 10,
        Lightblue = 11,
        Lime = 12,
        Magenta = 13,
        Maroon = 14,
        Navy = 15,
        Olive = 16,
        Purple = 17,
        Silver = 18,
        Teal = 19
    }
}