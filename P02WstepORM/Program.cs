using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02WstepORM
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ModelBazyDanychDataContext db = new ModelBazyDanychDataContext();

            Zawodnik[] wynik1 =
                db.Zawodnik.Where(x => x.kraj == "pol").ToArray();

            foreach (var wynk in wynik1)
            {
                Console.WriteLine(wynk.imie + " " + wynk.nazwisko);
            }

            var zapytanie =
                db.Zawodnik.Where(x => x.kraj == "pol");

            // dodawanie nowych rekordow
            //Zawodnik nowy = new Zawodnik()
            //{
            //    imie = "jan",
            //    nazwisko = "kowalski",
            //    kraj = "pol",
            //    data_ur = DateTime.Now,
            //    wzrost = 180,
            //    waga = 80
            //};

            //db.Zawodnik.InsertOnSubmit(nowy);
            //db.SubmitChanges();

            // edycja
            // najpierw pobieramy tego zawodnika, korego chcemy edytowac
            // potem zmieniamy odpowiednie pola
            // zapisujemy zaminy

            //Zawodnik doEdycji =
            //    db.Zawodnik.Where(x => x.id_zawodnika == 39).ToArray()[0];

            //Zawodnik doEdycji =
            //   db.Zawodnik.Where(x => x.id_zawodnika == 39).FirstOrDefault();

            //Zawodnik doEdycji =
            //  db.Zawodnik.FirstOrDefault(x => x.id_zawodnika == 39);

            //doEdycji.nazwisko = "nowak";

            //db.SubmitChanges();

            // usuwanie
            Zawodnik doUsuniecia =
              db.Zawodnik.FirstOrDefault(x => x.id_zawodnika == 39);

            db.Zawodnik.DeleteOnSubmit(doUsuniecia);
            db.SubmitChanges();

            Console.ReadKey();
        }
    }
}