using System;
using System.Drawing;
using System.IO;

namespace DrWren
{
    public class ColorEventArgs : EventArgs
    {
        public ColorSettings.SyntaxColor Colors { get; set; }
    }

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

        public SyntaxColor Colors { get; }

        public void Open()
        {
            var settings = new ColorSettingsForm(this);
            settings.ShowDialog();
        }

        public SyntaxColor Load()
        {
            if (!File.Exists(SettingsFilePath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(SettingsFilePath))
                {
                    sw.WriteLine("Literals-Yellow");
                    sw.WriteLine("Operators-Red");
                    sw.WriteLine("Keywords-Red");
                }

                return new SyntaxColor(Color.Yellow, Color.Red, Color.Red);
            }


            string[] lines = File.ReadAllLines(SettingsFilePath);
            var literals = Color.FromName(lines[0].Split("-")[1]);
            var operators = Color.FromName(lines[1].Split("-")[1]);
            var keywords = Color.FromName(lines[2].Split("-")[1]);
            return new SyntaxColor(literals, operators, keywords);
        }


        public void Save(Color literals, Color operators, Color keywords)
        {
            if (!File.Exists(SettingsFilePath))
            {
                using (StreamWriter sw = File.CreateText(SettingsFilePath))
                {
                    sw.WriteLine("Literals-Yellow");
                    sw.WriteLine("Operators-Red");
                    sw.WriteLine("Keywords-Red");
                }
            }
            
            using (var sw = File.CreateText(SettingsFilePath))
            {
                sw.WriteLine("Literals-" + literals.Name);
                sw.WriteLine("Operators-" + operators.Name);
                sw.WriteLine("Keywords-" + keywords.Name);
            }
            
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
            public Color Literals { get; set; }
            public Color Operators { get; set; }
            public Color Keywords { get; set; }

            public SyntaxColor(Color literals, Color operators, Color keywords)
            {
                Literals = literals;
                Operators = operators;
                Keywords = keywords;
            }
        }
    }
}