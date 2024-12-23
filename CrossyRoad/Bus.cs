using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad
{
    class Bus : GameFigure
    {


        public Bus(Track track)
        {
            idTrack = track.IdTrack;
            idTrack2 = idTrack + 1;

            DrawEdge(track);

            AssignPosition(track);

            AssignImage(track);

            leftToRight = track.LeftToRight;

        }

        public override void DrawEdge(Track track)
        {
            Width = track.Width / 5;
            Height = track.Height * 2;
        }

        protected override void AssignImage(Track track)
        {
            int x = RandomNumber.GenerateSecureRandomNumber(0,2);

            if (track.LeftToRight)
            {
                if(x == 0)
                {
                    image = Properties.Resources.BusBlueRight;
                }
                else
                {
                    image = Properties.Resources.BusYellowRight;
                }
            }
            else
            {
                if(x == 0)
                {
                    image = Properties.Resources.BusBlueLeft;
                }
                else
                {
                    image = Properties.Resources.BusYellowLeft;
                }
            }
        }

        public override int Damage(Track track)
        {
            return Convert.ToInt32(track.Speed * 0.8);
        }

    }
}
