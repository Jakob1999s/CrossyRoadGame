using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


//-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0
//-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0
//-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0
//-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0
//-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0
//-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0-------------5.0
/// <summary>
/// </summary>

namespace CrossyRoad
{
    public partial class FrmCrossyRoad : Form
    {
        private bool handleEvents = false;

        public FrmCrossyRoad()//
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.WindowState = FormWindowState.Maximized;
        }

        #region Variablen
        UserInterface u;

        Rectangle btnPausing;
        Image pauseImage = Properties.Resources.PauseWhite;


        Rectangle btnEnding;
        Image beendenImage = Properties.Resources.BeendenWhite;

        //Lebensanzeige
        int life;
        double lifeScaler;

        bool timerEnabled = false;

        int score = 0;

        int difficultyLevel = 1;

        bool showGameName = true;
        int showGameNameCounter = 0;

        bool gameOver = false;

        GameFigure gRedFlash;
        #endregion

        private void FrmCrossyRoad_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;

            //Benutezroberfläche erstellen 
            u = new UserInterface(difficultyLevel: difficultyLevel, this.ClientSize.Width, this.ClientSize.Height);

            tmrCrossyRoad.Start();

            //Leben wird voll geladen.
            life = GenerateLife();

            PositioningBtnTryAgain();

            handleEvents = true;
        }


        private void FrmCrossyRoad_Paint(object sender, PaintEventArgs e)
        {

            if (!handleEvents)
            {
                return;
            }

            Graphics g = e.Graphics;

            #region Game over
            if(gameOver)
            {
                string gameOver = "Game Over!";

                g.DrawString(gameOver,
                    new Font(new FontFamily("Cooper Black"), 120, FontStyle.Bold),
                    new SolidBrush(Color.Red),
                    new Point(Convert.ToInt32((this.Width * 0.5) - (gameOver.Length / 2) * 120), Convert.ToInt32(this.Height * 0.3)));

                return;
            }
            #endregion

            #region Startbildschirm und Zwischenbildschirm
            //Startbildschirm und Zwischenbildschirm: kündigt die kommende Schweirigkeitsstufe an. 
            if (showGameName)
            {
                string crossyRoad = "Crossy Road";

                g.DrawString(crossyRoad,
                    new Font(new FontFamily("Cooper Black"), 120, FontStyle.Bold),
                    new SolidBrush(Color.Red),
                    new Point(Convert.ToInt32((this.Width * 0.5) - (crossyRoad.Length / 2) * 120), Convert.ToInt32(this.Height * 0.3)));

                string level = "Level: " + u.DifficultiLevel;

                g.DrawString(level,
                    new Font(new FontFamily("Cooper Black"), 60, FontStyle.Bold),
                    new SolidBrush(Color.DeepSkyBlue),
                    new Point(Convert.ToInt32((this.Width * 0.5) - (level.Length / 2) * 60), Convert.ToInt32(this.Height * 0.58)));

                string info = "Steuerung: Pfeiltasten!";

                g.DrawString(info,
                    new Font(new FontFamily("Cooper Black"), 30, FontStyle.Bold),
                    new SolidBrush(Color.ForestGreen),
                    new Point(Convert.ToInt32((this.Width * 0.5) - (info.Length / 2) * 30), Convert.ToInt32(this.Height * 0.75)));

                return;
            }
            #endregion

            #region Bahnen zeichnen
            //Startbahn
            g.FillRectangle(u.BrGoalTrack, u.Tracks[0].Edge);

            //Zielbahn
            g.FillRectangle(u.BrStartTrack, u.Tracks[u.Tracks.Length - 1].Edge);


            //mittlere Bahnen
            for (int i = 1; i < u.Tracks.Length - 1; i += 2)
            {
                if (u.Tracks[i].Red)
                {
                    g.FillRectangle(new SolidBrush(Color.Red), u.Tracks[i].Edge);
                }
                else
                {
                    g.FillRectangle(u.BrTrackLight, u.Tracks[i].Edge);
                }
            }

            for (int i = 2; i < u.Tracks.Length - 1; i += 2)
            {
                if (u.Tracks[i].Red)
                {
                    g.FillRectangle(new SolidBrush(Color.Red), u.Tracks[i].Edge);
                }
                else
                {
                    g.FillRectangle(u.BrTrackDark, u.Tracks[i].Edge);
                }
            }

            //Blinken:
            //Falls nötig grün blinken
            if (u.BoolFlashing)
            {
                u.LevelUp(score, ref showGameName, ref difficultyLevel);

                if (u.GreenFlashing)
                {
                    g.FillRectangle(new SolidBrush(Color.Green), u.Tracks[0].Edge);
                }
            }
            //

            //Bahnränder zeichnen
            foreach (Track t in u.Tracks)
            {
                g.DrawRectangle(new Pen(Color.Black), t.Edge);
            }
            #endregion


            #region Hindernisse zeichnen
            foreach (Track t in u.Tracks)
            {
                foreach (GameFigure gf in t.Figures)
                {
                    g.DrawImage(gf.Image, gf.Edge);
                }
            }
            #endregion

            //Frosch zeichnen
            g.DrawImage(u.Frog.Image, u.Frog.Edge);

            #region Gameanzeige (Buttons, Lebensanzeige) etc.
            btnPausing = u.CeateBtnPausing(this);
            g.DrawImage(pauseImage, btnPausing);

            btnEnding = u.CreateBtnEnding(this);
            g.DrawImage(beendenImage, btnEnding);

            //Lebensanzeige (Rechteck)
            g.DrawRectangle(new Pen(Color.Black, 2), u.LifeDisplayEdge(this));
            g.FillRectangle(new SolidBrush(Color.Green), u.LifeDisplay(this, life));

            g.DrawImage(Properties.Resources.lblLife, u.LblLife(this));

            //Levelanzeige
            double fontSizeLevel = Convert.ToDouble(this.Height) * 0.0143;

            double fontSizeScore;

            //Scoreanzeige
            if (u.DifficultiLevel == 1)
            {
                fontSizeScore = Convert.ToDouble(this.Height) * 0.0572;
                fontSizeLevel = Convert.ToDouble(this.Height) * 0.0305;
            }
            else if (u.DifficultiLevel == 2)
            {
                fontSizeScore = Convert.ToDouble(this.Height) * 0.0477;
                fontSizeLevel = Convert.ToDouble(this.Height) * 0.0238;
            }
            else
            {
                fontSizeScore = Convert.ToDouble(this.Height) * 0.0381;
                fontSizeLevel = Convert.ToDouble(this.Height) * 0.0143;
            }


            g.DrawString("Score: " + score,
                new Font(new FontFamily("Cooper Black"), (float)fontSizeScore, FontStyle.Bold),
                new SolidBrush(Color.Red),
                new Point(Convert.ToInt32(this.Width * 0.44), Convert.ToInt32(u.Tracks[u.Tracks.Length - 1].Y + u.Tracks[0].Height * 1.25)));

            g.DrawString("Level: " + u.DifficultiLevel + "",
                new Font(new FontFamily("Cooper Black"), (float)fontSizeLevel, FontStyle.Bold),
                new SolidBrush(Color.DeepSkyBlue),
                new Point(Convert.ToInt32(this.Width * 0.476), Convert.ToInt32(u.Tracks[u.Tracks.Length - 1].Y + u.Tracks[0].Height * 3)));

            #endregion

        }

        private void FrmCrossyRoad_SizeChanged(object sender, EventArgs e)
        {
            if (!handleEvents)
            {
                return;
            }

            tmrCrossyRoad.Stop();

            this.ResizeRedraw = true;

            u.CustomizeAllSizes(this.ClientSize.Width, this.ClientSize.Height);

            //Life wird anders behandelt als die anderen Objekte, da es ein unabhängiges Verhalten zu der Benutzeroberfläche hat.
            life = Convert.ToInt32(Convert.ToDouble(this.Width) * lifeScaler);

            PositioningBtnTryAgain();

            this.Refresh();

            if (!timerEnabled)
            {
                tmrCrossyRoad.Start();
            }
        }

        private void tmrCrossyRoad_Tick(object sender, EventArgs e)
        {
            if (!handleEvents)
            {
                return;
            }

            //Start- Unterbechungsbildschirm
            if (showGameNameCounter <= 150 && showGameName)
            {
                if (showGameNameCounter == 0)
                {
                    u = new UserInterface(difficultyLevel: difficultyLevel, this.ClientSize.Width, this.ClientSize.Height);
                }

                showGameNameCounter++;

                this.Refresh();

                return;
            }
            else
            {
                showGameName = false;
                showGameNameCounter = 0;

            }

            #region Hinderniss erstellen


            //Wahrscheinlichkeit dass ein Hinderniss (Auto oder Zug) erstellt wird.
            int createFigure = RandomNumber.GenerateSecureRandomNumber(0, u.Probability);

            //Auto erstellen
            if (createFigure == 0)
            {
                int id = 0;

                //Die Schleife soll nur maximal "X" mal durchlaufen werden. Sodass sich im Falle dass keine Bahn frei ist
                //das Programm nicht aufhängt.

                for (int i = 0; i < 2; i++)
                {
                    id = RandomNumber.GenerateSecureRandomNumber(1, u.Tracks.Length - 1);

                    if (u.TrackOccupyable(id))
                    {
                        break;
                    }
                }

                if (u.TrackOccupyable(id))
                {
                    u.OccupyTrack(id);
                }
            }


            foreach (BusCreator l in u.BusCreator)
            {
                l.createBus(u.Tracks);
            }
            #endregion

            //Crash
            for (int i = 0; i < u.Tracks.Length; i++)
            {
                for (int a = 0; a < u.Tracks[i].Figures.Count; a++)
                {
                    if (u.Tracks[i].Figures[a].Crash(u.Frog.CrashPoints))
                    {
                        gRedFlash = u.Tracks[i].Figures[a];

                        if (life > 0)
                        {
                            life -= u.Tracks[i].Figures[a].Damage(u.Tracks[i]);
                        }
                        else if(life <= 0)
                        {
                            gameOver = true;
                            PositioningBtnTryAgain();
                            btnTryAgain.Visible = true;
                        }

                        u.Tracks[u.Frog.IdCurrentTrack].Red = true;
                    }
                }
            }

            if(gRedFlash != null)
            {
                //rotes Leuchten unterbinden
                if (!gRedFlash.Crash(u.Frog.CrashPoints))
                {
                    u.Tracks[gRedFlash.IdTrack].Red = false;
                    u.Tracks[gRedFlash.IdTrack2].Red = false;
                }
            }


            foreach (Track t in u.Tracks)
            {
                if (t.IdTrack != u.Frog.IdCurrentTrack)
                {
                    t.Red = false;
                }
            }

            //Falls der Frosch nicht auf der Bahn ist

            #region Ready
            for (int i = 0; i < u.BusCreator.Count; i++)
            {
                if (u.BusCreator[i].Phase == 5)
                {
                    u.BusCreator.RemoveAt(i);
                }
            }

            //Hinderniss entfernen, sofern es nicht mehr sichtbar ist 
            foreach (Track t in u.Tracks)
            {
                t.RemoveFigures();
            }

            //Hindernisse werden bewegt
            foreach (Track t in u.Tracks)
            {
                foreach (GameFigure f in t.Figures)
                {
                    f.Move(t);
                }
            }

            //Bahn wechselt automatisch die Richtung. Sobald sich keine Hindernisse mehr auf ihr befinden.
            u.TrackEmpty();
            #endregion

            //Wo befinden sich Objekte im Vergleich zu der Breite des Spielfelds.
            u.CalculateScaler(this.ClientSize.Width);

            //Lifeskallierer berechnen
            //Life wird anders behandelt als die anderen Objekte, da es ein unabhängiges Verhalten zu der Benutzeroberfläche hat.
            lifeScaler = Convert.ToDouble(life) / Convert.ToDouble(this.Width);

            this.Refresh();
        }

        private void FrmCrossyRoad_KeyDown(object sender, KeyEventArgs e)
        {
            if (!handleEvents)
            {
                return;
            }
            //Wenn die Bewegung des Frosches gesperrt ist, dann die Ereignisbehnadlungsmethode direkt verlsassen.
            //Das habe ich in jede "key." Bedingung mit eingefügt
            if (e.KeyCode == Keys.Up && !u.Frog.MoveLock)
            {
                u.Frog.Up();

                //Wenn der Frosch auf der Bahn "0" angekommen ist, blinkt die Bahn grün.
                if (u.Frog.IdCurrentTrack == 0)
                {
                    score++;
                    u.Frog.MoveLock = true;
                    u.LevelUp(score, ref showGameName, ref difficultyLevel);
                }
            }

            if (e.KeyCode == Keys.Down && !u.Frog.MoveLock)
            {
                u.Frog.Down();
            }

            if (e.KeyCode == Keys.Left && !u.Frog.MoveLock)
            {
                u.Frog.Left();
            }

            if (e.KeyCode == Keys.Right && !u.Frog.MoveLock)
            {
                u.Frog.Right();
            }

            if(e.KeyCode == Keys.Space)
            {
                Pause();
            }
        }

        private void FrmCrossyRoad_MouseClick(object sender, MouseEventArgs e)
        {
            if (!handleEvents)
            {
                return;
            }

            if (!btnPausing.Contains(e.X, e.Y) && !btnEnding.Contains(e.X, e.Y))
            {
                return;
            }

            //Pausieren
            if (btnPausing.Contains(e.X, e.Y))
            {
                Pause();
            }

            if (btnEnding.Contains(e.X, e.Y))
            {
                Application.Exit();
            }

        }

        private void FrmCrossyRoad_MouseMove(object sender, MouseEventArgs e)
        {
            if (!handleEvents)
            {
                return;
            }

            #region BtnPause
            if (btnPausing.Contains(e.X, e.Y) && !timerEnabled)
            {
                pauseImage = Properties.Resources.PauseBlue;
            }
            else if (!btnPausing.Contains(e.X, e.Y) && !timerEnabled)
            {
                pauseImage = Properties.Resources.PauseWhite;
            }
            else if (btnPausing.Contains(e.X, e.Y) && timerEnabled)
            {
                pauseImage = Properties.Resources.StartBlue;
            }
            else
            {
                pauseImage = Properties.Resources.StartWhite;
            }
            #endregion


            #region btnEnde
            if (btnEnding.Contains(e.X, e.Y) && !timerEnabled)
            {
                beendenImage = Properties.Resources.BeendenBlue;
            }
            else if (btnEnding.Contains(e.X, e.Y) && timerEnabled)
            {
                beendenImage = Properties.Resources.BeendenBlue;
            }
            else
            {
                beendenImage = Properties.Resources.BeendenWhite;
            }
            #endregion


            if (timerEnabled)
            {
                this.Refresh();
            }
        }

        void Pause()
        {
            if (!timerEnabled)
            {
                pauseImage = Properties.Resources.StartBlue;
                u.Frog.MoveLock = true;
                tmrCrossyRoad.Stop();
                timerEnabled = true;
                this.Refresh();
            }
            else
            {
                pauseImage = Properties.Resources.PauseWhite;
                u.Frog.MoveLock = false;
                tmrCrossyRoad.Start();
                timerEnabled = false;
                this.Refresh();
            }
        }

        private void btnTryAgain_Click(object sender, EventArgs e)
        {
            //Progamm wird erneut gestartet.
            RestartApplication();     
        }

        private int GenerateLife()
        {
            return this.Width / 4;
        }

        static void RestartApplication()
        {
            // Den aktuellen Prozess abrufen
            var currentProcess = Process.GetCurrentProcess();

            // Den Pfad zur ausführbaren Datei des aktuellen Prozesses abrufen
            string exePath = currentProcess.MainModule.FileName;

            // Einen neuen Prozess mit dem gleichen Programm starten
            Process.Start(exePath);

            // Den aktuellen Prozess beenden
            Environment.Exit(0);
        }


        void PositioningBtnTryAgain()
        {
            btnTryAgain.Location = new Point(Convert.ToInt32(this.Width * 0.5) - (btnTryAgain.Width / 2), Convert.ToInt32(this.Height * 0.7));
        }

    }
}
