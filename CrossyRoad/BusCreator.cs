using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad
{
    class BusCreator
    {
        private int phase = 0;

        public int Phase
        {
            get
            {
                return phase;
            }
        }

        int open;
        int closed;

        Bus bus;

        int idTrack;


        public BusCreator()
        {
        }

        public void createBus(Track[] tracks)
        {
            switch (phase)
            {
                case 0:

                    int id = RandomNumber.GenerateSecureRandomNumber(1, tracks.Length - 3);

                    //Wenn eine der beiden Bahnen schon belegt wird mit einem Bus dann die Methode direkt beenden.
                    if (tracks[id].BusPlacement || tracks[id + 1].BusPlacement)
                    {
                        return;
                    }


                    tracks[id].BusPlacement = true;
                    tracks[id + 1].BusPlacement = true;

                    if (tracks[id].Speed >= tracks[id + 1].Speed)
                    {
                        closed = id;
                        open = id + 1;

                        tracks[closed].BusLocked = true;
                    }
                    else
                    {
                        closed = id + 1;
                        open = id;

                        tracks[closed].BusLocked = true;

                    }

                    phase++;

                    break;
                case 1:

                    //warten bis die geschlossene Bahn leer ist. Falls nicht wieder verlassen.
                    if (tracks[closed].Figures.Count > 0)
                    {
                        return;
                    }

                    //Die geschlossene Bahn nimmt die gleiche Geschwindifgkeit und Richtung der offenen Bahn an
                    //Dazu auch die gleiche Anzahl an hindernissenmax.
                    tracks[closed].DetermineDirection(tracks[open].LeftToRight);

                    tracks[closed].Speed = tracks[open].Speed;

                    //Verhindern dass andere Figuren erstellt werden. Lastwagen hat Vorrang!
                    tracks[closed].BusLocked = true;
                    tracks[open].BusLocked = true;

                    phase++;

                    break;
                case 2:


                    //Prüfen ob sich Figuren in den Startzonen befinden
                    foreach (GameFigure g in tracks[closed].Figures)
                    {
                        if (tracks[closed].StartZone.Contains(g.TestPoint))
                        {
                            return;
                        }
                    }

                    foreach (GameFigure g in tracks[open].Figures)
                    {
                        if (tracks[open].StartZone.Contains(g.TestPoint))
                        {
                            return;
                        }
                    }

                    //Bahn ermitteln auf der der Lastwagen erstellt wird.

                    if (tracks[closed].Y < tracks[open].Y)
                    {
                        idTrack = closed;
                    }
                    else
                    {
                        idTrack = open;
                    }

                    //Lastwagen wird gesetzt
                    bus = new Bus(tracks[idTrack]);
                    tracks[idTrack].Figures.Add(bus);

                    tracks[closed].FiguresMax = 3;
                    tracks[open].FiguresMax = 3;

                    tracks[closed].FCounter = 1;
                    tracks[open].FCounter = 1;

                    phase++;

                    break;
                case 3:

                    if (!tracks[idTrack].Edge.Contains(bus.TestPoint))
                    {
                        return;
                    }

                    tracks[closed].BusLocked = false;
                    tracks[open].BusLocked = false;

                    phase++;

                    break;
                case 4:

                    if (tracks[idTrack].Endone.Contains(bus.TestPoint))
                    {
                        tracks[closed].BusPlacement = false;
                        tracks[open].BusPlacement = false;
                        phase++;
                    }

                    break;
                case 5:
                    //Ende. Der Buscreator kann entfernt werden.
                    break;
            }
        }
    }
}
