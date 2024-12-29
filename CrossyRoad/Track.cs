using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad
{
    public class Track
    {
        private int idTrack;

        private Rectangle startZone;
        private Rectangle endZone;
        private Rectangle edge;

        private int minSpeed;
        private int maxSpeed;

        private int figuresMax;
        public int FCounter = 0;
        private bool locked;
        private int speed;
        private bool leftToRight;


        private List<GameFigure> figures = new List<GameFigure>();

        //Boolean für die Lastwagen erstellung
        public bool BusPlacement = false;
        public bool BusLocked = false;

        public bool Red;

        public Track(int idTrack, int xLocation, int yLocation, int width, int height, int minSpeed, int maxSpeed)
        {
            this.idTrack = idTrack;

            edge = new Rectangle(xLocation, yLocation, width, height);

            this.minSpeed = minSpeed;
            this.maxSpeed = maxSpeed;

            speed = RandomNumber.GenerateSecureRandomNumber(minSpeed, maxSpeed);

            DetermineDirectionRandom();//funktioniert 

            //Start- und Endzone zuteilen 
            if (leftToRight)
            {
                startZone = new Rectangle(xLocation - width, yLocation, width, height);
                endZone = new Rectangle(width, yLocation, width, height);
            }
            else
            {
                endZone = new Rectangle(xLocation - width, yLocation, width, height);
                startZone = new Rectangle(width, yLocation, width, height);
            }

            figuresMax = GenerateFiguresMax(); ;

            locked = false;

        }

        public int IdTrack
        {
            get
            {
                return idTrack;
            }
        }


        //Gibt an wie viele Hindernisse insgesamt auf der Bahn sein können.
        public int FiguresMax
        {
            get
            {
                return figuresMax;
            }
            set
            {
                figuresMax = value;
            }
        }

        public Rectangle StartZone
        {
            get
            {
                return startZone;
            }
            set
            {
                startZone = value;
            }
        }

        public Rectangle Endone
        {
            get
            {
                return endZone;
            }
            set
            {
                endZone = value;
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

        public int Width
        {
            get { return edge.Width; }
        }

        public int Height
        {
            get { return edge.Height; }
        }

        public int X
        {
            get { return edge.X; }
        }

        public int Y
        {
            get { return edge.Y; }
        }

        public bool Locked
        {
            get
            {
                return locked;
            }
            set
            {
                locked = value;
            }
        }

        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }

        public bool LeftToRight
        {
            get
            {
                return leftToRight;
            }
        }

        public List<GameFigure> Figures
        {
            get
            {
                return figures;
            }
        }


        //Fahrtrichtung wird random bestimmt, beziehungsweise verändert.
        public void DetermineDirectionRandom()
        {
            //Anzahl der zugelassenen Hindernisse verändern.
            figuresMax = GenerateFiguresMax();

            //Richtung wird random zugewiesen.

            int lr = RandomNumber.GenerateSecureRandomNumber(0, 2);

            //Eine neue Geschwindigkeit wird zugewiesen
            speed = RandomNumber.GenerateSecureRandomNumber(minSpeed, maxSpeed);

            switch (lr)
            {
                case 0:

                    //End und Startzone wechseln, falls es nötig sein sollte.
                    if (!leftToRight)
                    {
                        Rectangle hilfe = startZone;
                        startZone = endZone;
                        endZone = hilfe;
                    }

                    leftToRight = true;

                    break;
                case 1:

                    //End und Startzone wechseln falls es nötig sein sollte.
                    if (leftToRight)
                    {
                        Rectangle hilfe = startZone;
                        startZone = endZone;
                        endZone = hilfe;
                    }

                    leftToRight = false;

                    break;
                default:
                    break;
            }

        }

        public void DetermineDirection(bool lr)
        {
            if (lr)
            {
                if (!leftToRight)
                {
                    Rectangle hilfe = startZone;
                    startZone = endZone;
                    endZone = hilfe;

                    leftToRight = true;
                }
            }
            else
            {
                if (leftToRight)
                {
                    Rectangle hilfe = startZone;
                    startZone = endZone;
                    endZone = hilfe;

                    leftToRight = false;
                }
            }
        }

        //Hindernisse werden nur entfernt sofern sie nicht mehr im Spielfeld zu sehen sind.
        public void RemoveFigures()
        {
            for (int i = 0; i < figures.Count; i++)
            {
                if (endZone.Contains(figures[i].TestPoint))
                {
                    figures.RemoveAt(i);
                }
            }
        }

        //Generiert wie viele Hindernisse auf einer Bahn insgesamt fahren können.
        public int GenerateFiguresMax()
        {

            return RandomNumber.GenerateSecureRandomNumber(1, 3);
        }

    }
}
