using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Language;
using Language.Lexer;

namespace DrWren
{
    public static class SyntaxHighlighter
    {
        private static IList<Regex> Keywords { get; } = new List<Regex>();
        private static IList<Regex> Operators { get; } = new List<Regex>();
        private static IList<Regex> Literals { get; } = new List<Regex>();

        static SyntaxHighlighter()
        {
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
            ClearColor(textBox);
            ChangeTextColor(textBox, Keywords, Color.Red);
            ChangeTextColor(textBox, Operators, Color.Red);
            ChangeTextColor(textBox, Literals, Color.Orange);
        }

        private static void ClearColor(RichTextBox textBox)
        {
            var position = textBox.SelectionStart + textBox.SelectionLength;
            textBox.SelectAll();
            textBox.SelectionColor = Color.Black;
            textBox.Select(position, 0);
        }

        private static void ChangeTextColor(RichTextBox textBox, IList<Regex> keywords, Color color)
        {
            var text = textBox.Text;
            var allIndexesAndSize = AllIndexOfAndSize(text, keywords);
            foreach (var indexLenghtPair in allIndexesAndSize)
            {
                var position = textBox.SelectionStart + textBox.SelectionLength;
                var oldColor = textBox.SelectionColor;
                textBox.Select(indexLenghtPair.Item1, indexLenghtPair.Item2);
                textBox.SelectionColor = color;
                textBox.Select(position, 0);
                textBox.SelectionColor = oldColor;
            }
        }

        private static List<Tuple<int, int>> AllIndexOfAndSize(string text, IList<Regex> keywords)
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