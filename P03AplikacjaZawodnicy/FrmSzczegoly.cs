﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P03AplikacjaZawodnicy
{
    public partial class FrmSzczegoly : Form
    {
        private ZawodnikVM zawodnik;
        private FrmZawodnicy frmZawodnicy;

        private enum TrybOkienka
        {
            Edycja,
            Nowy,
        }

        private TrybOkienka tryb => zawodnik == null ? TrybOkienka.Nowy : TrybOkienka.Edycja;

        public FrmSzczegoly(FrmZawodnicy frmZawodnicy)
        {
            // to jest konstuktor, który wywoujemy gdy tworzymy nowego zawodnika
            this.frmZawodnicy = frmZawodnicy;
            InitializeComponent();
        }

        public FrmSzczegoly(FrmZawodnicy frmZawodnicy, ZawodnikVM zawodnik) : this(frmZawodnicy)
        {
            // to konsturktor gdy edytujemy zawodnika
            this.zawodnik = zawodnik;
            txtImie.Text = zawodnik.Imie;
            txtNazwisko.Text = zawodnik.Nazwisko;
            txtKraj.Text = zawodnik.Kraj;
            dtpDataUr.Value = zawodnik.DataUr;
            numWaga.Value = zawodnik.Waga;
            numWzrost.Value = zawodnik.Wzrost;
            txtMiasto.Text = zawodnik.Miasto;

            btnUsun.Visible = true;
            WczytajTemperature();
        }

        private void WczytajTemperature()
        {
            if (!string.IsNullOrWhiteSpace(txtMiasto.Text))
            {
                ManagerPogody mp = new ManagerPogody(Jednostka.Celcjusz);
                double temp = mp.PodajTemperature(txtMiasto.Text);
                lblTemperatura.Text = temp.ToString();

                TemperaturyRepository tr = new TemperaturyRepository();
                Temperatura temperatura = new Temperatura();
                temperatura.Data = DateTime.Now;
                temperatura.Wartosc = Convert.ToInt32(temp);
                temperatura.Miasto = txtMiasto.Text;

                tr.DodajTemperature(temperatura);
            }
        }

        private void btnZapisz_Click(object sender, EventArgs e)
        {
            ZawodnicyRepository zr = new ZawodnicyRepository();

            if (tryb == TrybOkienka.Edycja)
            {
                zczytajDaneZawodnika();
                zr.Edytuj(zawodnik);
            }
            else if (tryb == TrybOkienka.Nowy)
            {
                zawodnik = new ZawodnikVM();
                zczytajDaneZawodnika();
                zr.DodajZawodnika(zawodnik);
            }
            else
                throw new Exception("Nieznany tryb");

            frmZawodnicy.Odswiez();

            Close();
        }

        private void zczytajDaneZawodnika()
        {
            zawodnik.Imie = txtImie.Text;
            zawodnik.Nazwisko = txtNazwisko.Text;
            zawodnik.Kraj = txtKraj.Text;
            zawodnik.DataUr = dtpDataUr.Value;
            zawodnik.Waga = Convert.ToInt32(numWaga.Value);
            zawodnik.Wzrost = Convert.ToInt32(numWzrost.Value);
            zawodnik.Miasto = txtMiasto.Text;
        }

        private void uzupelnijPola()
        {
        }

        private void btnUsun_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Czy chcesz usnąc zawodnika?", "Usuwanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                ZawodnicyRepository zr = new ZawodnicyRepository();
                zr.UsunZawodnika(zawodnik);
                frmZawodnicy.Odswiez();
                Close();
            }
        }
    }
}