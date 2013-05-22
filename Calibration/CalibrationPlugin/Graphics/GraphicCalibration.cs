using System;
using System.Drawing;
using System.Threading;
using System.Drawing.Drawing2D;

namespace Calibration
{
    /// <summary>
    /// Определяет что выводить на график
    /// </summary>
    public enum Draw
    {
        /// <summary>
        /// Только точку
        /// </summary>
        PointOnly, 
        
        /// <summary>
        /// Только график
        /// </summary>
        PointsOnly, 
        
        /// <summary>
        /// Точку и график
        /// </summary>
        PointAndPoints, 
        
        /// <summary>
        /// Ничего не рислвать
        /// </summary>
        Nothing
    }

    /// <summary>
    /// Выполняет отрисовку графика калибровки
    /// </summary>
    public class GraphicCalibration
    {
        private BufferedGraphics graphicBuffer = null;          // графический буфер, для двойной буферизации
        private BufferedGraphicsContext graphicContext = null;  // методы сознания графичечких буферов

        private Rectangle Frame;        // определяет область и положение, занимаемое графиком калибровки
        private Rectangle Axes;         // определяет область и положение, осей X,Y графика калибровки

        private string textX = "Сигнал";    // определяет надпись оси X
        private string textY = "Значение";  // определяет надпись оси Y

        private int logicalPixelX = 65535;   // логическое значение масштаба по X в пикселах
        private int logicalPixelY = 65535;   // логическое значение масштаба по Y в пикселах

        private Point point;                // точка значения калибровочного значения
        private Point[] points;             // массив точек, определяющих график калибровочной кривой

        float ptsScaleX = 0.0f;             // соотношение по оси X
        float ptsScaleY = 0.0f;             // соотношение по оси Y

        private Matrix matrixForInnerLines = null;  // матрица для афинных преобразований при отрисовке калибровочной кривой и калибровочного параметра
        private Draw whatDraw = Draw.Nothing;       // что рисовать

        private Sync sync = null;

        /// <summary>
        /// определяет что выводить на график
        /// </summary>
        public Draw Draw
        {
            get { return whatDraw; }
            set { whatDraw = value; }
        }

        /// <summary>
        /// Определяет логическое значение масштаба по X в пикселах
        /// </summary>
        public int LogicalPixelX
        {
            get { return logicalPixelX; }
            set { logicalPixelX = value; }
        }

        /// <summary>
        /// Определяет логическое значение масштаба по Y в пикселах
        /// </summary>
        public int LogicalPixelY
        {
            get { return logicalPixelY; }
            set { logicalPixelY = value; }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="g">Повехность на которой необходимо выполнять рисование</param>
        /// <param name="FrameToDraw">Область и положение, занимаемое графиком калибровки на форме</param>
        public GraphicCalibration(Graphics g, Rectangle FrameToDraw)
        {
            Frame = new Rectangle(FrameToDraw.Location, FrameToDraw.Size);
            Axes = new Rectangle(Frame.X + 30, Frame.Y + 30, Frame.Width - 60, Frame.Height - 60);

            graphicContext = BufferedGraphicsManager.Current;
            graphicBuffer = graphicContext.Allocate(g, Frame);

            graphicBuffer.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphicBuffer.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            graphicBuffer.Graphics.TextContrast = 1;
            graphicBuffer.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            sync = new Sync();
            points = null;
        }

        /// <summary>
        /// Отрисовывает линии калибровочной кривой
        /// </summary>
        private void DrawPoints()
        {
            if (points != null)
            {
                if (!sync.Blocked)
                {
                    sync.Block();
                    using (Pen pen = new Pen(Color.Red))
                    {
                        Matrix oldMatrix = graphicBuffer.Graphics.Transform;
                        graphicBuffer.Graphics.Transform = matrixForInnerLines;

                        graphicBuffer.Graphics.DrawLines(pen, points);
                        graphicBuffer.Graphics.Transform = oldMatrix;
                    }

                    for (int index = 1; index < points.Length - 1; index++)
                    {
                        DrawPointsLines(points[index]);
                    }

                    sync.Relese();
                }
            }
        }

        /// <summary>
        /// украшаем прорисовку точек на линии
        /// </summary>
        /// <param name="pt"></param>
        private void DrawPointsLines(Point pt)
        {
            using (Pen pen = new Pen(Color.Silver))
            {
                if (pt.X != 0 && pt.Y != 0)
                {
                    Point pt1 = new Point(pt.X, 0);
                    Point pt2 = new Point(pt.X, logicalPixelY);

                    Matrix oldMatrix = graphicBuffer.Graphics.Transform;
                    graphicBuffer.Graphics.Transform = matrixForInnerLines;

                    graphicBuffer.Graphics.DrawLine(pen, pt1, pt2);

                    pt1.X = 0; pt1.Y = pt.Y;
                    pt2.X = logicalPixelX; pt2.Y = pt.Y;

                    graphicBuffer.Graphics.DrawLine(pen, pt1, pt2);
                    //graphicBuffer.Graphics.FillEllipse(Brushes.Black, pt.X-200, pt.Y-200, 300, 300);//.DrawEllipse(p, new Rectangle(pt.X-30, pt.Y-30, 300, 300));
                
                    graphicBuffer.Graphics.Transform = oldMatrix;
                }
            }
        }

        /// <summary>
        /// украшаем прорисовку точек на линии. рисуем точку
        /// </summary>
        /// <param name="pt"></param>
        private void DrawPointinLinesR(Point pt)
        {
            using (Pen pen = new Pen(Color.Silver))
            {
                if (pt.X != 0 && pt.Y != 0)
                {
                    graphicBuffer.Graphics.DrawEllipse(pen, new Rectangle(pt.X - 3, pt.Y + 3, 3, 3));
                }
            }
        }

        /// <summary>
        /// Отрисовывает точку калибровочного параметра
        /// </summary>
        private void DrawPoint()
        {
            if (!sync.Blocked)
            {
                sync.Block();
                if (point != null)
                {
                    using (Pen pen = new Pen(Color.Green))
                    {
                        if (point.X != 0 && point.Y != 0)
                        {
                            Point pt1 = new Point(point.X, 0);
                            Point pt2 = new Point(point.X, logicalPixelY);

                            Matrix oldMatrix = graphicBuffer.Graphics.Transform;
                            graphicBuffer.Graphics.Transform = matrixForInnerLines;

                            graphicBuffer.Graphics.DrawLine(pen, pt1, pt2);

                            pt1.X = 0; pt1.Y = point.Y;
                            pt2.X = logicalPixelX; pt2.Y = point.Y;

                            graphicBuffer.Graphics.DrawLine(pen, pt1, pt2);
                            graphicBuffer.Graphics.Transform = oldMatrix;
                        }
                    }
                }
                sync.Relese();
            }
        }

        /// <summary>
        /// Пересчитать соотношения графика
        /// </summary>
        public void CalculateScale()
        {
            if (!sync.Blocked)
            {
                sync.Block();
                ptsScaleX = (float)Axes.Width / (float)logicalPixelX;
                ptsScaleY = (float)Axes.Height / (float)logicalPixelY;

                matrixForInnerLines = new Matrix(ptsScaleX, 0, 0, -ptsScaleY, Axes.Left - 12/*364*/, (float)Axes.Bottom - 38/*204*/);
                sync.Relese();
            }
        }

        /// <summary>
        /// Установить калибровочную точку
        /// </summary>
        /// <param name="pt">Точка калибровочного параметра</param>
        public void InsertPoint(Point pt)
        {
            if (!sync.Blocked)
            {
                sync.Block();
                point = new Point(pt.X, pt.Y);
                sync.Relese();
            }
        }

        public void InsertPoints(Point[] pts)
        {
            if (!sync.Blocked)
            {
                sync.Block();
                points = new Point[pts.Length];
                pts.CopyTo(points, 0);
                sync.Relese();
            }
        }

        /// <summary>
        /// сбросить точки рисования
        /// </summary>
        public void ResetPoints()
        {
            if (!sync.Blocked)
            {
                sync.Block();
                points = null;
                sync.Relese();
            }
        }

        /// <summary>
        /// сбросить точку рисования
        /// </summary>
        public void ResetPoint()
        {
            if (!sync.Blocked)
            {
                sync.Block();
                point = new Point(0, 0);
                sync.Relese();
            }
        }

        /// <summary>
        /// сбросить масштаб
        /// </summary>
        public void ResetScale()
        {
            logicalPixelX = 65535;   // логическое значение масштаба по X в пикселах
            logicalPixelY = 65535;   // логическое значение масштаба по Y в пикселах

            CalculateScale();
        }

        /// <summary>
        /// Нарисовать все
        /// </summary>
        public void PresentAll()
        {
            if (point != null && points != null) Draw = Draw.PointAndPoints;
            else
                if (point != null) Draw = Draw.PointOnly;
                else
                    if (points != null) Draw = Draw.PointsOnly;
            
            Present();
        }

        /// <summary>
        /// Осуществляет вывод графики на поверхность
        /// </summary>
        public void Present(Draw WhatDraw)
        {
            Draw oldDraw = Draw;
            Draw = WhatDraw;

            Present();
            Draw = oldDraw;
        }
        /// <summary>
        /// Осуществляет вывод графики на поверхность
        /// </summary>
        public void Present()
        {
            // Рисует рамку и оси
            graphicBuffer.Graphics.Clear(Color.White);
            using (Pen pen = new Pen(Brushes.Black))
            {
                graphicBuffer.Graphics.DrawRectangle(pen, Frame);
                using (Pen mPen = new Pen(Brushes.Gray))
                {
                    using (Pen xyPen = new Pen(Brushes.Navy))
                    {
                        Point[] xyPoints = new Point[3];
                        Point[] points = new Point[3];

                        xyPoints[0] = new Point(Axes.Right, Axes.Bottom);
                        xyPoints[1] = new Point(Axes.Left, Axes.Bottom);
                        xyPoints[2] = new Point(Axes.Left, Axes.Top);

                        points[0] = new Point(Axes.Left, Axes.Top);
                        points[1] = new Point(Axes.Right, Axes.Top);
                        points[2] = new Point(Axes.Right, Axes.Bottom);

                        graphicBuffer.Graphics.DrawLines(mPen, points);
                        graphicBuffer.Graphics.DrawLines(xyPen, xyPoints);
                    }
                }
            }
            
            // Осуществляет вывод текстовых меток на графике
            Font fnt = new Font("Times New Roman", 12.0f);

            SizeF signalStringSize = graphicBuffer.Graphics.MeasureString(textX, fnt);
            SizeF parameterStringSize = graphicBuffer.Graphics.MeasureString(textY, fnt);

            int signalX = (int)(Axes.Width / 2) - (int)(signalStringSize.Width / 2) + Axes.Left;
            int signalY = Axes.Bottom;

            StringFormat format = new StringFormat();
            format.FormatFlags = StringFormatFlags.DirectionVertical;

            Matrix transform = new Matrix(-1, 0, 0, -1, -12, -38);//-364, -204);

            int parameterX = -Axes.Left;
            int parameterY = -(Axes.Bottom - (int)(Axes.Height / 2)) - (int)(parameterStringSize.Width / 2);

            int zeroX = Axes.Left - (int)(graphicBuffer.Graphics.MeasureString("0", fnt).Width);

            graphicBuffer.Graphics.DrawString(textX, fnt, Brushes.Black, signalX, signalY);
            graphicBuffer.Graphics.DrawString("0", fnt, Brushes.Gray, zeroX, Axes.Bottom);

            int maxX = Axes.Right - (int)(graphicBuffer.Graphics.MeasureString(logicalPixelX.ToString(), fnt).Width);
            graphicBuffer.Graphics.DrawString(logicalPixelX.ToString(), fnt, Brushes.Gray, maxX, Axes.Bottom);

            Matrix oldTransform = graphicBuffer.Graphics.Transform;

            graphicBuffer.Graphics.Transform = transform;
            graphicBuffer.Graphics.DrawString(textY, fnt, Brushes.Black, parameterX, parameterY, format);

            int maxY = Axes.Top + (int)(graphicBuffer.Graphics.MeasureString(logicalPixelY.ToString(), fnt).Width);
            graphicBuffer.Graphics.DrawString(logicalPixelY.ToString(), fnt, Brushes.Gray, -Axes.Left, -maxY, format);

            graphicBuffer.Graphics.Transform = oldTransform;
            fnt.Dispose();

            switch (whatDraw)
            {
                case Draw.PointOnly:

                    DrawPoint();
                    break;

                case Draw.PointsOnly:

                    DrawPoints();
                    break;

                case Draw.PointAndPoints:

                    DrawPoints();
                    DrawPoint();
                    break;

                default: break;
            }

            graphicBuffer.Render();
        }
    }
}