using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DrWren
{
    public static class SyntaxHighlighter
    {
        private static IList<Regex> Keywords { get; } = new List<Regex>();
        private static IList<Regex> Operators { get; } = new List<Regex>();
        private static IList<Regex> Literals { get; } = new List<Regex>();

        private static ColorSettings.SyntaxColor _colors;

        private static RichTextBox _currentTextBox;
        private static readonly ColorSettings Settings;


        static SyntaxHighlighter()
        {
            Settings = ColorSettings.Instance;
            _colors = Settings.Load();
            Settings.ColorSettingsChanged += OnColorSettingChanged;

            //Keywords initialization
            Keywords.Add(new Regex("var", RegexOptions.Compiled));
            Keywords.Add(new Regex("while", RegexOptions.Compiled));
            Keywords.Add(new Regex("if", RegexOptions.Compiled));
            Keywords.Add(new Regex("else", RegexOptions.Compiled));
            Keywords.Add(new Regex("System", RegexOptions.Compiled));
            Keywords.Add(new Regex("Turtle", RegexOptions.Compiled));
            // Operators inicialization
            Operators.Add(new Regex("=", RegexOptions.Compiled));
            Operators.Add(new Regex("!=", RegexOptions.Compiled));
            Operators.Add(new Regex("==", RegexOptions.Compiled));
            Operators.Add(new Regex(">=", RegexOptions.Compiled));
            Operators.Add(new Regex(">", RegexOptions.Compiled));
            Operators.Add(new Regex("<=", RegexOptions.Compiled));
            Operators.Add(new Regex("-", RegexOptions.Compiled));
            Operators.Add(new Regex("/", RegexOptions.Compiled));
            Operators.Add(new Regex("%", RegexOptions.Compiled));

            // Literals
            Literals.Add(new Regex("(\"[^\"\r\n]*\")", RegexOptions.Compiled));
        }

        public static void Highlight(RichTextBox textBox)
        {
            _currentTextBox = textBox;
            var originalIndex = textBox.SelectionStart;
            var originalLength = textBox.SelectionLength;
            var originalColor = Color.Black;
            ClearColor(textBox, originalColor);
            ChangeTextColor(textBox, Keywords, _colors.Keywords);
            ChangeTextColor(textBox, Operators, _colors.Operators);
            ChangeTextColor(textBox, Literals, _colors.Literals);
            textBox.SelectionStart = originalIndex;
            textBox.SelectionLength = originalLength;
            textBox.SelectionColor = originalColor;
        }

        private static void OnColorSettingChanged(object source, ColorEventArgs args)
        {
            _colors = Settings.Load();
            Highlight(_currentTextBox);
        }

        private static void ClearColor(RichTextBox textBox, Color color)
        {
            textBox.SelectionStart = 0;
            textBox.SelectionLength = textBox.Text.Length;
            textBox.SelectionColor = color;
        }

        private static void ChangeTextColor(RichTextBox textBox, IEnumerable<Regex> keywords, Color color)
        {
            var text = textBox.Text;
            var allIndexesAndSize = AllIndexOfAndSize(text, keywords);
            foreach (var (index, length) in allIndexesAndSize)
            {
                textBox.SelectionStart = index;
                textBox.SelectionLength = length;
                textBox.SelectionColor = color;
            }
        }

        private static IEnumerable<Tuple<int, int>> AllIndexOfAndSize(string text, IEnumerable<Regex> keywords)
        {
            var allIndexesAndSize = new List<Tuple<int, int>>();
            foreach (var keyword in keywords)
            {
                foreach (Match match in keyword.Matches(text))
                {
                    allIndexesAndSize.Add(new Tuple<int, int>(match.Index, match.Length));
                }
            }

            return allIndexesAndSize;
        }
    }
}