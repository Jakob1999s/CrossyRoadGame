using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrossyRoad
{
    internal class UserInterface
    {
        //-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0
        //-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0
        //-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0
        //-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0
        //-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0
        //-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0

        #region Eigenschaften
        public Frog Frog;

        public bool Pause = false;

        //Blinken (grün)
        public  int FlashCounter = 0;
        private bool boolFlashing = false;
        private bool greenFlashing = false;

        private int difficultyLevel;
        private Track[] tracks;
        private int minSpeed;
        private int maxSpeed;

        private SolidBrush brTrackLight = new SolidBrush(Color.LightGray);
        private SolidBrush brTrackDark = new SolidBrush(Color.Gray);
        private SolidBrush brStartTrack = new SolidBrush(Color.PaleTurquoise);
        private SolidBrush brGoalTrack = new SolidBrush(Color.PaleTurquoise);

        public List<BusCreator> BusCreator = new List<BusCreator>();

        public int Probability;
        #endregion


        public UserInterface(int difficultyLevel, int formWidth, int formHeight)
        {

            DifficultiLevel = difficultyLevel;

            switch (difficultyLevel)
            {
                case 1:
                    tracks = new Track[12];
                    minSpeed = 5;
                    maxSpeed = 30;
                    Probability = 5;
                    break;
                case 2:
                    tracks = new Track[16];
                    minSpeed = 5;
                    maxSpeed = 40;
                    Probability = 5;
                    break;
                case 3:
                    tracks = new Track[20];
                    minSpeed = 5;
                    maxSpeed = 60;
                    Probability = 8;
                    break;
                case 4:
                    tracks = new Track[24];
                    minSpeed = 5;
                    maxSpeed = 80;
                    Probability = 7;
                    break;
                case 5:
                    tracks = new Track[24];
                    minSpeed = 5;
                    maxSpeed = 100;
                    Probability = 7;
                    break;
                case 6:
                    tracks = new Track[24];
                    minSpeed = 8;
                    maxSpeed = 100;
                    Probability = 7;
                    break;
                case 7:
                    tracks = new Track[24];
                    minSpeed = 10;
                    maxSpeed = 100;
                    Probability = 4;
                    break;
                case 8:
                    tracks = new Track[24];
                    minSpeed = 15;
                    maxSpeed = 100;
                    Probability = 3;
                    break;
                case 9:
                    tracks = new Track[24];
                    minSpeed = 30;
                    maxSpeed = 100;
                    Probability = 3;
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
                case 16:
                    break;
                case 17:
                    break;
                case 18:
                    break;
                case 19:
                    break;
                case 20:
                    break;
                case 21:
                    break;
                case 22:
                    break;

                default:
                    break;
            }

            //Den unteren Bereich für die Scoreanzeige etc. schaffen.
            formHeight = Cutting(formHeight);

            //Bahnen erstellen
            for (int i = 0; i < tracks.Length; i++)
            {
                tracks[i] = new Track(i, 0, (formHeight / tracks.Length) * i, formWidth, (formHeight / tracks.Length), minSpeed, maxSpeed);
            }

            //Frosch auf die erste Bahn (im Array die letzte Bahn) platzieren.
            Frog = new Frog(tracks[tracks.Length - 1]);

        }


        //1. 
        public int DifficultiLevel
        {
            get
            {
                return difficultyLevel;
            }
            set
            {
                this.difficultyLevel = value;
            }
        }

        //2.

        public Track[] Tracks
        {
            get
            {
                return tracks;
            }
        }

        public SolidBrush BrStartTrack
        {
            get
            {
                return brStartTrack;
            }
        }

        public SolidBrush BrGoalTrack
        {
            get
            {
                return brGoalTrack;
            }
        }

        public SolidBrush BrTrackLight
        {
            get
            {
                return brTrackLight;
            }
        }

        public SolidBrush BrTrackDark
        {
            get
            {
                return brTrackDark;
            }
        }

        public bool BoolFlashing
        {
            get
            {
                return boolFlashing;
            }
        }

        public bool GreenFlashing
        {
            get
            {
                return greenFlashing;
            }
        }

        /// <summary>
        /// Passt die Größe von allen Objekten an.
        /// </summary>
        public void CustomizeAllSizes(int formWidth, int formHeight)
        {

            //Kürzen für den unteren Bereich.
            formHeight = Cutting(formHeight);

            //Bahngröße anpassen
            int tWidth = formWidth;
            int tHeight = formHeight / tracks.Length;

            for (int i = 0; i < tracks.Length; i++)
            {
                tracks[i].Edge = new Rectangle(0, tHeight * i, tWidth, tHeight);
            }


            //End- und Startzonen müssen auch verschoben werden!!!!
            for (int i = 0; i < tracks.Length; i++)
            {
                if (tracks[i].LeftToRight)
                {
                    tracks[i].StartZone = new Rectangle(0 - tWidth, tHeight * i, tWidth, tHeight);
                    tracks[i].Endone = new Rectangle(tWidth, tHeight * i, tWidth, tHeight);
                }
                else
                {
                    tracks[i].Endone = new Rectangle(0 - tWidth, tHeight * i, tWidth, tHeight);
                    tracks[i].StartZone = new Rectangle(tWidth, tHeight * i, tWidth, tHeight);
                }
            }


            //Rand zeichnen + X-Location skallieren
            foreach (Track t in tracks)
            {
                foreach (GameFigure g in t.Figures)
                {
                    g.DrawEdge(t);
                    g.Edge = new Rectangle(Scale(formWidth, g.Scaler), t.Y, g.Width, g.Height);
                }
            }

            //Die xLocation des Frosches muss angepasst werden.
            int xFLocation = Scale(formWidth, Frog.Scaler);

            int yFLoxation = tracks[Frog.IdCurrentTrack].Y;

            //Froschgröße anpassen --> Frosch ist so hoch und breit wie die Höhe einer Bahn.
            int fWidth = tracks[0].Height;
            int fHeight = tracks[0].Height;

            Frog.TrackWidth = formWidth;

            Frog.Edge = new Rectangle(xFLocation, yFLoxation, fWidth, fHeight);

        }

        /// <summary>
        /// (SkalliererBerechnen) Berechnet den Skallierer. 
        /// </summary>

        public void CalculateScaler(int formWidth)
        {
            //Froschskallierer:
            double scaler = Convert.ToDouble(Frog.Edge.X) / Convert.ToDouble(formWidth);
            Frog.Scaler = scaler;

            //Hindernissskallierer:
            foreach (Track t in tracks)
            {
                foreach (GameFigure g in t.Figures)
                {
                    scaler = Convert.ToDouble(g.X) / Convert.ToDouble(formWidth);
                    g.Scaler = scaler;
                }
            }

        }

        private int Scale(int formWidth, double scaler)
        {
            int xFLocation = Convert.ToInt32(Convert.ToDouble(formWidth) * scaler);
            return xFLocation;
        }

        //Den unteren Bereich für die Scoreanzeige etc. schaffen.

        public int Cutting(int formHeight)
        {
            return formHeight -= Convert.ToInt32((formHeight / tracks.Length) * 2.5);
        }

        public bool TrackOccupyable(int idTrack)
        {
            //Kann ein Hinderniss auf die Bahn gesetzt werden ohne dass sich Hindernisse sichtbar überschneiden?
            bool ready = true;

            //Ist die Bahn gesperrt? Wenn ja, dann die Methode direkt verlassen.
            if (tracks[idTrack].Locked || tracks[idTrack].BusLocked)
            {
                ready = false;
                return ready;
            }

            //Befindet sich ein Hinderniss in der Startzone?
            foreach (GameFigure g in tracks[idTrack].Figures)
            {
                if (tracks[idTrack].StartZone.Contains(g.TestPoint))
                {
                    ready = false;
                }
            }

            return ready;
        }

        public void OccupyTrack(int idTrack)
        {
            //Soll es ein Auto, ein Zug oder ein LKW werden?
            //Wahrscheinlichkeit:
            int number = RandomNumber.GenerateSecureRandomNumber(0, 15);

            GameFigure f;

            //Hinderniss erstellen
            if (number == 0)
            {
                //Der Ersteller sucht sich ein passendes Bahnpaar heraus und setzt dann den Lastwagen
                BusCreator.Add(new BusCreator());
            }
            else if (number == 1 || number == 2 || number == 3)
            {
                //Hinderniss wird der Liste hinzufügen 
                f = new Train(tracks[idTrack]);
                tracks[idTrack].Figures.Add(f);
                tracks[idTrack].FCounter++;
            }
            else
            {
                //Hinderniss wird der Liste hinzufügen 
                f = new Car(tracks[idTrack]);
                tracks[idTrack].Figures.Add(f);
                tracks[idTrack].FCounter++;
            }


            //Ist die maximale Anzahl von Hindernissen auf der Bahn erfolgt?
            if (tracks[idTrack].FiguresMax == tracks[idTrack].FCounter)
            {
                tracks[idTrack].Locked = true;
            }
        }

        public void TrackEmpty()
        {
            foreach (Track t in tracks)
            {
                if (!t.BusPlacement)
                {
                    //Wenn die Bahn gesperrt sein sollte und dazu sich keine Hindernisse mehr
                    //auf ihr befinden sollten.
                    if (t.Locked && t.Figures.Count == 0)
                    {
                        //Bahn wird eine random Richtung zugewiesen. Dazu verändern sich die 
                        //Start und Endzonen. Für mehr Infos geh auf die Methode.
                        t.DetermineDirectionRandom();

                        //Zähler wird zurückgesetzt.
                        t.FCounter = 0;

                        //Bahn wird entsperrt.
                        t.Locked = false;
                    }
                }
            }
        }

        public void LevelUp(int score, ref bool showGameName, ref int difficultyLevel)
        {
            boolFlashing = true;

            if (FlashCounter == 2 || FlashCounter == 3 || FlashCounter == 4
                || FlashCounter == 8 || FlashCounter == 9 || FlashCounter == 10
                || FlashCounter == 14 || FlashCounter == 15 || FlashCounter == 16)
            {
                greenFlashing = true;
            }
            else
            {
                greenFlashing = false;
            }

            FlashCounter++;

            if (FlashCounter == 16)
            {
                //Blinken hört auf 
                boolFlashing = false;
                FlashCounter = 0;

                //Level up
                if(score == 5)
                {
                    showGameName = true;
                    difficultyLevel++;
                }
                else if(score == 10 )
                {
                    showGameName = true;
                    difficultyLevel++;
                }
                else if (score == 15)
                {
                    showGameName = true;
                    difficultyLevel++;
                }
                else if (score == 20)
                {
                    showGameName = true;
                    difficultyLevel++;
                }
                else if (   score == 25)
                {
                    showGameName = true;
                    difficultyLevel++;
                }
                else if (score == 30)
                {
                    showGameName = true;
                    difficultyLevel++;
                }

                //Frosch an den Start setzen
                PlayerToStart();

                //Frosch kann sich wieder bewegen (kurz)
                Frog.MoveLock = false;

                //Frosch befindet sich wieder auf der Startbahn.
                Frog.IdCurrentTrack = tracks.Length - 1;

            }

        }

        public void PlayerToStart()
        {
            int width = tracks[0].Height;
            int height = tracks[0].Height;

            int xLocation = tracks[0].Width / 2 - Frog.Edge.Width / 2;
            int yLocation = tracks[tracks.Length - 1].Y;

            Frog.Edge = new Rectangle(xLocation, yLocation, width, height);
        }

        public Rectangle CeateBtnPausing(Form form)
        {
            int x = Convert.ToInt32(Convert.ToDouble(form.Width) * 0.8);
            int y = Convert.ToInt32(tracks[tracks.Length - 1].Y + tracks[0].Height * 2);


            int width = form.Width / 14;
            int height = Convert.ToInt32(tracks[1].Height * 1.5);

            return new Rectangle(x, y, width, height);
        }

        public Rectangle CreateBtnEnding(Form form)
        {
            int x = Convert.ToInt32(Convert.ToDouble(form.Width) * 0.89);
            int y = Convert.ToInt32(tracks[tracks.Length - 1].Y + tracks[0].Height * 2);

            int width = form.Width / 14;
            int height = Convert.ToInt32(tracks[1].Height * 1.5);

            return new Rectangle(x, y, width, height);
        }

        //Rand der Lebensanzeige
        public Rectangle LifeDisplayEdge(Form form)
        {
            int x = Convert.ToInt32(Convert.ToDouble(form.Width) * 0.04);
            int y = Convert.ToInt32(tracks[tracks.Length - 1].Y + tracks[0].Height * 2.25);

            int width = form.Width / 4;
            int height = tracks[1].Height;

            return new Rectangle(x, y, width, height);
        }

        //Füllung der Lebensanzeige
        //Ändern: Life in Benutzeroberflaeche schieben, mit einem boolean abfragen ob life schon bereits einen Wert erhalten hat.
        public Rectangle LifeDisplay(Form form, int life)
        {
            int x = Convert.ToInt32(Convert.ToDouble(form.Width) * 0.04);
            int y = Convert.ToInt32(tracks[tracks.Length - 1].Y + tracks[0].Height * 2.25);

            int width = life;
            int height = tracks[1].Height;

            return new Rectangle(x, y, width, height);
        }


        public Rectangle LblLife(Form form)
        {
            int x = Convert.ToInt32(Convert.ToDouble(form.Width) * 0.04);
            int y = Convert.ToInt32(tracks[tracks.Length - 1].Y + tracks[0].Height * 2.25);

            int width = form.Width / 22;
            int height = Convert.ToInt32(tracks[1].Height);

            return new Rectangle(x, y, width, height);            
        }
    }
}
