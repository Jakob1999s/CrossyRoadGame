using CrossyRoad.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;


namespace CrossyRoad
{
    internal class Frog
    {
        //Auf welcher Bahn befindet sich der Frosch?
        private int idCurrentTrack;

        //Auf welcher ist der Frosch gestartet?
        private int idFinishTrack;

        public int TrackWidth;

        //Wo befindet sich der Frosch im Vergleich zur Spielbreite: 0.00 heißt ganz links, 1.00 heißt ganz rechts.
        //Man kann es auch wie eine Prozentrechnung betrachten.
        public double Scaler;

        private Rectangle edge;
        private Image image;

        public bool MoveLock = false;

        public Frog(Track track)
        {
            idCurrentTrack = track.IdTrack;

            idFinishTrack = track.IdTrack;

            TrackWidth = track.Width;

            //Frosch ist gleich und hoch.
            int height = track.Height;
            int width = track.Height;

            int xLocation = (track.Width / 2) - (width / 2);
            int yLocation = track.Y;

            edge = new Rectangle(xLocation, yLocation, width, height);

            image = Properties.Resources.frosch;
        }

        public int IdCurrentTrack
        {
            get
            {
                return idCurrentTrack;
            }
            set
            {
                idCurrentTrack = value;
            }
        }

        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
            }
        }

        public Rectangle Edge
        {
            get
            {
                return edge;
            }
            set
            {
                edge = value;
            }
        }

        public int X
        { get { return edge.X; } }

        public int Y
        { get { return edge.Y; } }

        public int Width
        { get { return edge.Width; } }

        public int Height
        { get { return edge.Height; } }

        public void Up()
        {
            //Der Frosch kann nicht höher als bahn[0] laufen.
            if (idCurrentTrack != 0)
            {
                edge.Location = new Point(edge.X, edge.Y - edge.Height);
                idCurrentTrack--;
            }
        }
        public void Down()
        {
            //Der Frosch kann nicht niederiger als die letzte Bahn laufen.
            if (idCurrentTrack != idFinishTrack)
            {
                edge.Location = new Point(edge.X, edge.Y + edge.Height);
                idCurrentTrack++;
            }
        }
        public void Left()
        {
            //Der Frosch kann nicht links aus dem Blickfeld laufen.
            if (edge.X > 0 + edge.Width / 2)
            {
                edge.Location = new Point(edge.X - edge.Width, edge.Y);
            }
        }
        public void Right()
        {
            //Der Frosch kann nicht rechgts aus dem Blickfeld laufen.
            if (edge.X < TrackWidth - edge.Width * 2)
            {
                edge.Location = new Point(edge.X + edge.Width, edge.Y);
            }
        }

        //Die Testpunkte sind nicht oben links und oben rechts. Sondern links Mitte und rechts Mitte.
        public Point[] CrashPoints
        {
            get
            {
                Point[] points = new Point[3];
                points[0] = new Point(X, Y + (Height / 2));
                points[1] = new Point(X + Width, Y + (Height / 2));
                return points;
            }
        }
    }
}
