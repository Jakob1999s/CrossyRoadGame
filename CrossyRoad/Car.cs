using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad
{
    class Car : GameFigure
    {
        public Car(Track track)
        {
            idTrack = track.IdTrack;
            idTrack2 = track.IdTrack;
            
            //Width und Height wird ermittelt
            DrawEdge(track);

            AssignPosition(track);

            AssignImage(track);

            leftToRight = track.LeftToRight;

        }

        public override void DrawEdge(Track track)
        {
            Width = track.Width / 11;
            Height = track.Height;
        }

        protected override void AssignImage(Track track)
        {
            int random = RandomNumber.GenerateSecureRandomNumber(0,2);

            if (track.LeftToRight)
            {
                if(track.Speed < 30)
                {
                    if(random == 0)
                    {
                        image = Properties.Resources.CarYellowRight;
                    }
                    else
                    {
                        image = Properties.Resources.CarBlueRight;
                    }
                }
                else
                {
                    image = Properties.Resources.Formel1Right;
                }
            }
            else
            {
                if (track.Speed < 30)
                {
                    if(random == 0)
                    {
                        image = Properties.Resources.CarYellowLeft;
                    }
                    else
                    {
                        image = Properties.Resources.CarBlueLeft;
                    }
                }
                else
                {
                    image = Properties.Resources.Formel1Left;
                }
            }
        }

        public override int Damage(Track track)
        {
            return Convert.ToInt32(track.Speed * 0.5);
        }
    }
}
