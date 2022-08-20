using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01WstepLINQ
{
    internal class ManagerZawodnikow
    {
        private string kraj;

        public ManagerZawodnikow()
        {
        }

        public ManagerZawodnikow(string kraj)
        {
            this.kraj = kraj;
        }

        public Zawodnik[] Wczytaj(string[] wiersze)
        {
            // Zawodnik[] zawodnicy = new Zawodnik[wiersze.Length-1];
            List<Zawodnik> zawodnicy = new List<Zawodnik>();

            for (int i = 1; i < wiersze.Length; i++)
            {
                string[] komorki = wiersze[i].Split(';');

                Zawodnik z = new Zawodnik(komorki);
                //z.Id_zawodnika = Convert.ToInt32(komorki[0]);
                //z.Id_trenera = Convert.ToInt32(komorki[1]);
                //z.Imie = komorki[2];
                //z.Nazwisko = komorki[3];
                //z.Kraj = komorki[4];
                //z.DataUr = Convert.ToDateTime(komorki[5]);
                //z.Wzrost = Convert.ToInt32(komorki[6]);
                //z.Waga = Convert.ToInt32(komorki[7]);

                //zawodnicy[i - 1] = z;

                if (kraj == null || (kraj.ToLower() == z.Kraj.ToLower()))
                    zawodnicy.Add(z);
            }

            return zawodnicy.ToArray();
        }
    }
}