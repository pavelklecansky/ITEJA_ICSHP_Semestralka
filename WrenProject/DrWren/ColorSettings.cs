using System;
using System.Drawing;
using System.IO;

namespace DrWren
{
    public class ColorEventArgs : EventArgs
    {
        public ColorSettings.SyntaxColor Colors { get; set; }
    }

    /// <summary>
    /// Class for working with color settings.
    /// </summary>
    public class ColorSettings
    {
        private static readonly string SettingsFilePath = @"./ColorSettings.txt";

        private static ColorSettings instance;

        private ColorSettings()
        {
        }

        public static ColorSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ColorSettings();
                }

                return instance;
            }
        }

        public delegate void ColorSettingsEventHandler(object source, ColorEventArgs args);

        public event ColorSettingsEventHandler ColorSettingsChanged;

        private SyntaxColor Colors { get; set; }

        /// <summary>
        /// Open color settings
        /// </summary>
        public void Open()
        {
            var settings = new ColorSettingsForm(this);
            settings.ShowDialog();
        }

        /// <summary>
        /// Load color settings from file.
        /// </summary>
        /// <returns></returns>
        public SyntaxColor Load()
        {
            if (!File.Exists(SettingsFilePath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(SettingsFilePath))
                {
                    sw.WriteLine("Literals|" + Color.Orange.ToArgb());
                    sw.WriteLine("Operators|" + Color.Red.ToArgb());
                    sw.WriteLine("Keywords|" + Color.Red.ToArgb());
                }

                return new SyntaxColor(Color.Orange, Color.Red, Color.Red);
            }


            string[] lines = File.ReadAllLines(SettingsFilePath);
            var literals = Color.FromArgb(int.Parse(lines[0].Split("|")[1]));
            var operators = Color.FromArgb(int.Parse(lines[1].Split("|")[1]));
            var keywords = Color.FromArgb(int.Parse(lines[2].Split("|")[1]));
            return new SyntaxColor(literals, operators, keywords);
        }


        /// <summary>
        /// Save color settings to file.
        /// </summary>
        public void Save(Color literals, Color operators, Color keywords)
        {
            if (!File.Exists(SettingsFilePath))
            {
                using (StreamWriter sw = File.CreateText(SettingsFilePath))
                {
                    sw.WriteLine("Literals|" + Color.Orange.ToArgb());
                    sw.WriteLine("Operators|" + Color.Red.ToArgb());
                    sw.WriteLine("Keywords|" + Color.Red.ToArgb());
                }
            }

            using (var sw = File.CreateText(SettingsFilePath))
            {
                sw.WriteLine("Literals|" + literals.ToArgb());
                sw.WriteLine("Operators|" + operators.ToArgb());
                sw.WriteLine("Keywords|" + keywords.ToArgb());
            }

            Colors = new SyntaxColor(literals, operators, keywords);

            OnColorSettingChanged();
        }

        protected virtual void OnColorSettingChanged()
        {
            if (ColorSettingsChanged != null)
            {
                ColorSettingsChanged(this, new ColorEventArgs {Colors = Colors});
            }
        }

        public class SyntaxColor
        {
            public Color Literals { get; }
            public Color Operators { get; }
            public Color Keywords { get; }

            public SyntaxColor(Color literals, Color operators, Color keywords)
            {
                Literals = literals;
                Operators = operators;
                Keywords = keywords;
            }
        }
    }
}