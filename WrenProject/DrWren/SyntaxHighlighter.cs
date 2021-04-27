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

        private static ColorSettings.SyntaxColor Colors;

        private static RichTextBox CurrentTextBox;
        private static readonly ColorSettings _settings;


        static SyntaxHighlighter()
        {
            _settings = ColorSettings.Instance;
            Colors = _settings.Load();
            _settings.ColorSettingsChanged += OnColorSettingChanged;

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
            CurrentTextBox = textBox;
            var originalIndex = textBox.SelectionStart;
            var originalLength = textBox.SelectionLength;
            var originalColor = Color.Black;
            ClearColor(textBox, originalColor);
            ChangeTextColor(textBox, Keywords, Colors.Keywords);
            ChangeTextColor(textBox, Operators, Colors.Operators);
            ChangeTextColor(textBox, Literals, Colors.Literals);
            textBox.SelectionStart = originalIndex;
            textBox.SelectionLength = originalLength;
            textBox.SelectionColor = originalColor;
        }

        private static void OnColorSettingChanged(object source, ColorEventArgs args)
        {
            Colors = _settings.Load();
            Highlight(CurrentTextBox);
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