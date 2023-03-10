using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Controllo_Prodotti
{
    public partial class Form1 : Form
    {
        #region dichiarazioni variabili
        public struct Prodotto
        {
            public string[] prod;
            public string[] prezzo;
        }

        public static int dim;

        string filename;

        public static Prodotto prodotto = new Prodotto();



        public Form1()
        {
            InitializeComponent();
            dim = 0;
            filename = @"spesa.csv";
            prodotto.prod = new string[100];
            prodotto.prezzo = new string[100];
        }
        #endregion
        #region Bottoni
        private void button1_Click(object sender, EventArgs e)
        {
            //chiamata alla funzione di caricamento
            caricamento(textBox1.Text, textBox2.Text);

            //chiamata alla funzione stampa
            stampa();
            //stampa del messaggio
            MessageBox.Show("Elementi caricati e stampati");
            //svuotamento delle textbox
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //chiamata alla funzione di falsa ricerca
            falsaricerca(textBox3.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //variabile posizione
            int posizione = search(textBox3.Text);
            if (posizione != -1)
            {
                //chiamata alla funzione di cancellazione
                cancellazione(posizione);

                //chiamata alla funzione stampa
                stampa();
                //stampa del messaggio
                MessageBox.Show("Elemento cancellato correttamente");
            }
            else
            {
                //stampa del messaggio
                MessageBox.Show("L'elemento non esiste");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //variabile posizione
            int posizione = search(textBox3.Text);

            if (posizione != -1)
            {
                //chiamata alla funzione di modifica
                modifica(textBox4.Text, textBox5.Text, posizione);
                //chiamata alla funzione stampa
                stampa();

                //stampa del messaggio
                MessageBox.Show("Elemento modificato");
            }
            else
            {
                //stampa del messaggio

                MessageBox.Show("L'elemento non esiste");
            }
            //svuotamento delle textbox
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //chiamata alla funzione di calcolo del prezzo totale
            calcoloPrezzoTot();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //chiamata alla funzione di sconto
            sconto(int.Parse(textBox6.Text));

            //chiamata alla funzione stampa
            stampa();

            //svuotamento delle textbox
            textBox6.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //chimata alla funzione di calcolo dello sconto
            sconto(-int.Parse(textBox6.Text));

            //chiamata alla funzione stampa
            stampa();

            //svuotamento delle textbox
            textBox6.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //chimata alla funzione di creazione file
            creazioneFile();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            //chiamata alla funzione di lettura del file
            LetturaFile();
        }
        #endregion
        #region Funzioni
        //funzione di caricamento
        static void caricamento(string p, string pr)
        {

            prodotto.prod[dim] = p;
            prodotto.prezzo[dim] = pr;
            dim++;
        }

        //funzione di stampa
        void stampa()
        {
            listView1.Items.Clear();
            for (int i = 0; i < dim; i++)
            {
                listView1.Items.Add(prodotto.prod[i] + " €" + prodotto.prezzo[i]);
            }
        }

        //funzione di falsa ricerca
        void falsaricerca(string nome)
        {
            //ciclo di ricerca sequenziale
            for (int i = 0; i < dim; i++)
            {
                if (prodotto.prod[i] == nome)
                {
                    MessageBox.Show("Elemento trovato");
                    return;
                }
            }
            MessageBox.Show("Elemento non trovato");
        }

        //funzione di ricerca 
        int search(string nome)
        {
            //variabile che segna la posizione
            int pos;

            //ciclo di ricerca sequenziale
            for (int i = 0; i < dim; i++)
            {
                if (prodotto.prod[i] == nome)
                {
                    pos = i;
                    return pos;
                }
            }
            pos = -1;
            return pos;
        }

        //funzione di cancellamento
        void cancellazione(int pos)
        {
            for (int i = pos; i < dim; i++)
            {
                prodotto.prezzo[i] = prodotto.prezzo[i + 1];
                prodotto.prod[i] = prodotto.prod[i + 1];
            }
            dim--;
        }

        //funzione di modifica
        void modifica(string nome, string prez, int pos)
        {
            prodotto.prod[pos] = nome;
            prodotto.prezzo[pos] = prez;
        }

        //funzione di calcolo del prezzo totale
        void calcoloPrezzoTot()
        {

            float prezzo = 0;
            for (int i = 0; i < dim; i++)
            {
                prezzo += float.Parse(prodotto.prezzo[i]);
            }
            listView1.Items.Add("prezzo spesa: €" + prezzo);
        }

        //funzione di calcolo dei prezzi scontati o aumentati
        void sconto(int sconto)
        {
            for (int i = 0; i < dim; i++)
            {
                float nuovop = float.Parse(prodotto.prezzo[i]) + (float.Parse(prodotto.prezzo[i]) / 100 * sconto);
                prodotto.prezzo[i] = nuovop.ToString();
            }
        }

        //funzione di creazione del file
        void creazioneFile()
        {
            using (StreamWriter sw = new StreamWriter(filename, append: false))
            {
                for (int i = 0; i < dim; i++)
                {
                    sw.WriteLine(prodotto.prod[i] + " €" + prodotto.prezzo[i]);
                }
            }
        }

        //funzione di lettura del file
        void LetturaFile()
        {

            using (StreamReader sr = File.OpenText(filename))
            {
                string s;


                while ((s = sr.ReadLine()) != null)
                {
                    listView1.Items.Add(s);
                }
            }
        }

    }
}
#endregion
