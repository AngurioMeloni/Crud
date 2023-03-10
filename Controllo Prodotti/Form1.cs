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
            int posizione = ricerca(textBox3.Text);
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
            int posizione = ricerca(textBox3.Text);

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
            //aggiunta del nome del prodotto 
            prodotto.prod[dim] = p;
            //aggiunta del prezzo del prodotto
            prodotto.prezzo[dim] = pr;
            //incremento della dimensione
            dim++;
        }

        //funzione di stampa
        void stampa()
        {
            //pulizia della list view
            listView1.Items.Clear();
            //ciclo per la stampa 
            for (int i = 0; i < dim; i++)
            {
                //aggiunta degli elementi nella listview
                listView1.Items.Add(prodotto.prod[i] + " €" + prodotto.prezzo[i]);
            }
        }

        //funzione di falsa ricerca
        void falsaricerca(string nome)
        {
            //ciclo di ricerca sequenziale
            for (int i = 0; i < dim; i++)
            {
                //if che stabilisce che se la ricerca è positiva ovvero quando nel carrello c'è il nome ricercato stampare l'apposito messaggio altrimenti dire prodotto non trovato
                if (prodotto.prod[i] == nome)
                {
                    //stampa del messaggio
                    MessageBox.Show("Elemento trovato");
                    return;
                }
            }
            //stampa del messaggio
            MessageBox.Show("Elemento non trovato");
        }

        //funzione di ricerca 
        int ricerca(string nome)
        {
            //variabile che segna la posizione
            int posizione;

            //ciclo di ricerca sequenziale
            for (int i = 0; i < dim; i++)
            {
                //if che verifica la prensenza del nome ricercato all'interno del carrello
                if (prodotto.prod[i] == nome)
                {
                    //pongo la posizione del prodotto uguale a i
                    posizione = i;
                    //ritorno della posizione
                    return posizione;
                }
            }
            posizione = -1;
            //ritorno della posizione
            return posizione;
        }

        //funzione di cancellamento
        void cancellazione(int pos)
        {
            //ciclo per la cancellazione di un prodotto
            for (int i = pos; i < dim; i++)
            {
                prodotto.prezzo[i] = prodotto.prezzo[i + 1];
                prodotto.prod[i] = prodotto.prod[i + 1];
            }
            //diminuzione della dimensione
            dim--;
        }

        //funzione di modifica
        void modifica(string nome, string prezzo, int posizione)
        {
            //pongo il nuovo prodotto inserito uguale alla stringa nome
            prodotto.prod[posizione] = nome;
            //pongo il nuovo prodotto inserito uguale alla stringa prez
            prodotto.prezzo[posizione] = prezzo;
        }

        //funzione di calcolo del prezzo totale
        void calcoloPrezzoTot()
        {
            //dichiarazione della variabile prezzo
            float prezzo = 0;
            //ciclo per il calcolo del prezzo totale
            for (int i = 0; i < dim; i++)
            { 
                prezzo += float.Parse(prodotto.prezzo[i]);
            }
            //aggiunta del valore prezzo della spesa
            listView1.Items.Add("prezzo spesa: €" + prezzo);
        }

        //funzione di calcolo dei prezzi scontati o aumentati
        void sconto(int sconto)
        {
            //ciclo per il calcolo dello sconto
            for (int i = 0; i < dim; i++)
            {
                //calcolo sconto con variabile float
                float nuovoprodotto = float.Parse(prodotto.prezzo[i]) + (float.Parse(prodotto.prezzo[i]) / 100 * sconto);
                prodotto.prezzo[i] = nuovoprodotto.ToString();
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
