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
        private static IList<string> Keywords { get; } = new List<string>();
        private static IList<string> Operators { get; } = new List<string>();
        private static IList<string> Literals { get; } = new List<string>();

        static SyntaxHighlighter()
        {
            //Keywords initialization
            Keywords.Add("var");
            Keywords.Add("while");
            Keywords.Add("if");
            Keywords.Add("else");
            Keywords.Add("System");
            Keywords.Add("Turtle");
            // Operators inicialization
            Operators.Add("=");
            Operators.Add("!=");
            Operators.Add("==");
            Operators.Add(">=");
            Operators.Add(">");
            Operators.Add("<=");
            Operators.Add("-");
            Operators.Add("/");
            Operators.Add("%");

            // Literals
            Literals.Add("(\"[^\"\r\n]*\")");
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

        private static void ChangeTextColor(RichTextBox textBox, IList<string> keywords, Color color)
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

        private static List<Tuple<int, int>> AllIndexOfAndSize(string text, IList<string> keywords)
        {
            var allIndexesAndSize = new List<Tuple<int, int>>();
            foreach (var keyword in keywords)
            {
                foreach (Match match in Regex.Matches(text, keyword))
                {
                    allIndexesAndSize.Add(new Tuple<int, int>(match.Index, match.Length));
                }
            }

            return allIndexesAndSize;
        }
    }
}