using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace P03AplikacjaZawodnicy
{
    public partial class FrmZawodnicy : Form
    {
        public FrmZawodnicy()
        {
            InitializeComponent();
        }

        public void Odswiez()
        {
            string sortowanie = null;
            if (rbImie.Checked)
                sortowanie = (string)rbImie.Tag;
            if (rbNazwisko.Checked)
                sortowanie = (string)rbNazwisko.Tag;
            if (rbWzrost.Checked)
                sortowanie = (string)rbWzrost.Tag;

            ZawodnicyRepository zr = new ZawodnicyRepository();
            ZawodnikVM[] zawodnicy = zr.WczytajZawodnikow(txtFiltr.Text, sortowanie);
            lbDane.DataSource = zawodnicy;
            lbDane.DisplayMember = "WidoczneDane";
            lblLicznaZaimportowanychDanych.Text = zawodnicy.Length.ToString();
        }

        private void btnWczytaj_Click(object sender, EventArgs e)
        {
            Odswiez();
            WygenerujWykres();
        }

        private void WygenerujWykres()
        {
            chartWykres.Series.Clear();

            Series seria = new Series("Wzrost");
            seria.ChartType = SeriesChartType.Column;

            var zawodnicy = (ZawodnikVM[])lbDane.DataSource;
            var grupy = zawodnicy.GroupBy(x => x.Kraj).Select(x => new
            {
                osX = x.Key,
                osY = x.Average(y => y.Wzrost)
            });

            string[] wartosci_X = grupy.Select(x => x.osX).ToArray();
            double[] wartosci_Y = grupy.Select(x => x.osY).ToArray();

            seria.Points.DataBindXY(wartosci_X, wartosci_Y);

            chartWykres.Series.Add(seria);
        }

        private void btnSzczegoly_Click(object sender, EventArgs e)
        {
            ZawodnikVM zaznaczony = (ZawodnikVM)lbDane.SelectedItem;
            FrmSzczegoly fs = new FrmSzczegoly(this, zaznaczony);
            fs.Show();
        }

        private void btnNowy_Click(object sender, EventArgs e)
        {
            FrmSzczegoly fs = new FrmSzczegoly(this);
            fs.Show();
        }

        private void rbKolumna_Click(object sender, EventArgs e)
        {
        }

        private void btnZapisz_Click(object sender, EventArgs e)
        {
        }
    }
}