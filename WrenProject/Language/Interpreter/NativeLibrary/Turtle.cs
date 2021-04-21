using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Language.Interpreter.NativeLibrary
{
    internal static class Turtle
    {
        private static Control DrawControl { get; set; }
        private static Graphics DrawGraphics { get; set; }
        private static Pen DrawPen { get; set; }

        private static TurtleGraphicsPanel Forms { get; set; }

        private static float X { get; set; }
        private static float Y { get; set; }

        private static int PenWidth { get; set; }

        private static double Direction { get; set; }

        private static readonly List<Action> Functions = new();


        static Turtle()
        {
            Clear();
        }

        private static void Init()
        {
            Dispose();
            Direction = 0;
            PenWidth = 5;

            DrawControl = Forms;

            DrawPen = new Pen(Color.Black, PenWidth) {StartCap = LineCap.Round, EndCap = LineCap.Round};

            DrawGraphics = Forms.Graphics;
            Forms.Canvas.Paint += CanvasOnPaint;
            Forms.Canvas.ClientSizeChanged += ClientSizeChanged;
            X = Forms.Canvas.ClientSize.Width / 2;
            Y = Forms.Canvas.ClientSize.Height / 2;
            DrawGraphics.SmoothingMode = SmoothingMode.AntiAlias;
        }

        /// <summary>
        /// Move turtle forward one step
        /// </summary>
        public static void Forward()
        {
            Functions.Add(() => Forward(1));
        }

        /// <summary>
        /// Move turtle forward number of steps
        /// </summary>
        /// <param name="steps">Number of steps</param>
        public static void Forward(double steps)
        {
            Functions.Add(() => DrawLine(steps));
        }

        /// <summary>
        /// Rotate turtle to right number of degrees
        /// </summary>
        /// <param name="degrees">Number of degrees</param>
        public static void Right(double degrees)
        {
            Functions.Add(() => Direction += degrees);
        }

        /// <summary>
        /// Rotate turtle to left number of degrees
        /// </summary>
        /// <param name="degrees">Number of degrees</param>
        public static void Left(double degrees)
        {
            Right(-degrees);
        }

        /// <summary>
        /// Show turtle graphics
        /// </summary>
        public static void Done()
        {
            Forms.ShowDialog();
        }

        /// <summary>
        /// Clear all turtle graphics commands
        /// </summary>
        public static void Clear()
        {
            Functions.Clear();
            Forms = new TurtleGraphicsPanel();
            Init();
        }

        private static void ClientSizeChanged(object sender, EventArgs e)
        {
            X = Forms.Canvas.ClientSize.Width / 2;
            Y = Forms.Canvas.ClientSize.Height / 2;
            Forms.Canvas.Invalidate();
        }

        private static void Dispose()
        {
            if (DrawControl != null)
            {
                Forms.Canvas.Paint -= CanvasOnPaint;
                Forms.Canvas.ClientSizeChanged -= ClientSizeChanged;
                DrawPen.Dispose();
                DrawPen = null;
                DrawGraphics.Dispose();
                DrawGraphics = null;
                DrawControl.Invalidate();
                DrawControl = null;
            }
        }

        private static void DrawLine(double steps)
        {
            var radians = Direction * (Math.PI / 180);
            var oldX = X;
            var oldY = Y;
            X += (float) (steps * Math.Cos(radians));
            Y += (float) (steps * Math.Sin(radians));
            DrawGraphics.DrawLine(DrawPen, oldX, oldY, X, Y);
        }

        private static void CanvasOnPaint(object sender, PaintEventArgs e)
        {
            foreach (var func in Functions)
            {
                func();
            }
        }
    }
}