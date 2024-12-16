using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad
{
    public abstract class GameFigure
    {
        protected Rectangle edge;

        public double Scaler;

        protected int speed;

        protected bool leftToRight;

        protected Image image;
        public abstract void DrawEdge(Track track);
        protected abstract void AssignImage(Track track);

        public abstract int Damage(Track track);


        public Image Image
        { get { return image; } }

        public Rectangle Edge
        {
            get { return edge; }
            set { edge = value; }
        }

        public int Width
        {
            get { return edge.Width; }
            set { edge.Width = value; }
        }

        public int Height
        {
            get { return edge.Height; }
            set { edge.Height = value; }
        }

        public int X
        { get { return edge.X; } }

        public int Y
        { get { return edge.Y; } }

        public Point TestPoint
        {
            get
            {
                Point testPoint;

                if (leftToRight)
                {
                    return testPoint = new Point(X, Y);
                }
                else
                {
                    return testPoint = new Point(X + Width, Y);
                }
            }
        }

        public virtual void Move(Track t)
        {
            if (leftToRight)
            {
                edge.Location = new Point(X + t.Speed, Y);
            }
            else
            {
                edge.Location = new Point(X - t.Speed, Y);
            }
        }

        protected void AssignPosition(Track t)
        {
            if (t.LeftToRight)
            {
                edge.Location = new Point(t.X - Width, t.Y);
            }
            else
            {
                edge.Location = new Point(t.Width, t.Y);
            }
        }

        //Aus dem bool ein int machen, sodass verschieden Figuren verschieden Schädlich sind.
        public bool Crash(Point[] points)
        {
            if (edge.Contains(points[0]) || edge.Contains(points[1]) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}

