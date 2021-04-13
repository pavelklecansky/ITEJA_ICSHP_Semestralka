using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
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

        private static Label FocusLabel = new();


        static SyntaxHighlighter()
        {
            //Keywords initialization
            Keywords.Add(new Regex("var", RegexOptions.Compiled));
            Keywords.Add(new Regex("while", RegexOptions.Compiled));
            Keywords.Add(new Regex("if", RegexOptions.Compiled));
            Keywords.Add(new Regex("else", RegexOptions.Compiled));
            Keywords.Add(new Regex("System", RegexOptions.Compiled));
            Keywords.Add(new Regex("Turtle", RegexOptions.Compiled));

            // Literals
            Literals.Add(new Regex("(\"[^\"\r\n]*\")", RegexOptions.Compiled));
        }

        public static void Highlight(RichTextBox textBox)
        {
            int originalIndex = textBox.SelectionStart;
            int originalLength = textBox.SelectionLength;
            Color originalColor = Color.Black;
            ClearColor(textBox, originalColor);
            ChangeTextColor(textBox, Keywords, Color.Red);
            ChangeTextColor(textBox, Operators, Color.Red);
            ChangeTextColor(textBox, Literals, Color.Orange);
            textBox.SelectionStart = originalIndex;
            textBox.SelectionLength = originalLength;
            textBox.SelectionColor = originalColor;
        }

        private static void ClearColor(RichTextBox textBox, Color color)
        {
            textBox.SelectionStart = 0;
            textBox.SelectionLength = textBox.Text.Length;
            textBox.SelectionColor = color;
        }

        private static void ChangeTextColor(RichTextBox textBox, IList<Regex> keywords, Color color)
        {
            var text = textBox.Text;
            var allIndexesAndSize = AllIndexOfAndSize(text, keywords);
            foreach (var indexLenghtPair in allIndexesAndSize)
            {
                textBox.SelectionStart = indexLenghtPair.Item1;
                textBox.SelectionLength = indexLenghtPair.Item2;
                textBox.SelectionColor = color;
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