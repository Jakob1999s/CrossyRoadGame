using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad
{
    internal class Train : GameFigure
    {
        public Train(Track track)
        {
            idTrack = track.IdTrack;
            idTrack2 = track.IdTrack;

            //Width und Height wird ermittelt
            DrawEdge(track);

            AssignPosition(track);

            AssignImage(track);

            //geschwindigkeit = bahn.Geschwindigkeit;

            leftToRight = track.LeftToRight;
        }

        public override void DrawEdge(Track track)
        {
            Width = track.Width / 3;
            Height = track.Height;
        }

        protected override void AssignImage(Track track)
        {
            image = Properties.Resources.Train;
        }

        public override int Damage(Track track)
        {
            return Convert.ToInt32(track.Speed * 0.7);
        }

    }
}
