using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace Language
{
    public static class Turtle
    {
        private static Control DrawControl { get; set; }
        private static Graphics DrawGraphics { get; set; }
        private static Pen DrawPen { get; set; }

        private static DrawingPanel forms;

        private static float X { get; set; }
        private static float Y { get; set; }

        private static int PenWidth { get; set; }

        public static double Direction { get; set; }

        static List<Action> functions = new();


        static Turtle()
        {
            forms = new DrawingPanel();
            Init();
        }

        static void Init()
        {
            Direction = 0;
            PenWidth = 4;

            DrawControl = forms;

            DrawPen = new Pen(Color.Black, PenWidth);


            DrawGraphics = forms.Graphics;
            forms.Canvas.Paint += CanvasOnPaint;
            X = forms.ClientSize.Width / 2;
            Y = forms.ClientSize.Height / 2;
            DrawGraphics.SmoothingMode = SmoothingMode.HighQuality;
        }

        private static void CanvasOnPaint(object sender, PaintEventArgs e)
        {
            foreach (Action func in functions)
            {
                Thread.Sleep(200);
                func();
            }
        }

        public static void Forward()
        {
            functions.Add(() => Forward(1));
        }

        public static void Forward(double steps)
        {
            functions.Add(() => DrawLine(steps));
        }

        public static void Right(double degrees)
        {
            functions.Add(() => Direction += degrees);
        }

        public static void Left(double degrees)
        {
            Right(-degrees);
        }


        public static void DrawLine(double steps)
        {
            var radians = Direction * (Math.PI / 180);
            var oldX = X;
            var oldY = Y;
            X += (float) (steps * Math.Cos(radians));
            Y += (float) (steps * Math.Sin(radians));
            DrawGraphics.DrawLine(DrawPen, oldX, oldY, X, Y);
        }

        public static void Done()
        {
            forms.ShowDialog();
        }
    }
}