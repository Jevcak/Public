
namespace PeXeSo
{
    public partial class Form1 : Form
    {
        Button Last = new Button();
        Button Current = new Button();
        int kolik;
        int i;
        int rnd = 12345;
        enum STAV { START, HRA, JEDNA, DVE, VYHRA };
        STAV stav;
        bool Kontrola(Button prvni, Button druhy)
        {
            if ((int)prvni.Tag == (int)druhy.Tag)
                return true;
            else
                return false;
        }
        void NastavSkore(int current, int kolik)
        {
            LSkore.Tag = current + kolik;
            LSkore.Text = (current + kolik).ToString();
        }
        int[] GeneratorPole(int velikost)
        {
            Random random = new Random(rnd);
            int[] pole = new int[velikost];
            for (int i = 0; i < velikost / 2; i++)
            {
                pole[i] = i + 1;
                pole[(velikost / 2) + i] = i + 1;
            }
            pole = pole.OrderBy(x => random.Next()).ToArray();
            return pole;
        }
        void VytvorKarticky()
        {
            int N = 6;
            int sx = (ClientRectangle.Width - 50) / N;
            int sy = (ClientRectangle.Height - 50) / N;
            int pocet = 0;
            int[] poleHodnot = GeneratorPole(N * N);
            kolik = N * N;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Button b = new Button();
                    b.Width = sx - 1;
                    b.Height = sy - 1;
                    b.Left = i * sx;
                    b.Top = j * sy;
                    b.Parent = this;
                    b.Click += Karticka_Klik;
                    b.Tag = poleHodnot[pocet];
                    b.Text = "PEXESO"; // + poleHodnot[pocet].ToString();
                    pocet++;
                }
            }
        }
        void NastavStav(STAV novy)
        {
            timerr.Enabled = false;
            switch (novy)
            {
                case STAV.START:
                    TStart.Visible = true;
                    TRestart.Visible = false;
                    LNavod.Visible = true;
                    LSkore.Visible = false;
                    LFinal.Visible = false;
                    break;
                case STAV.HRA:
                    TStart.Visible = false;
                    TRestart.Visible = false;
                    LNavod.Visible = false;
                    LSkore.Visible = true;
                    LFinal.Visible = false;
                    if (stav == STAV.START)
                    {
                        VytvorKarticky();
                        NastavSkore(0, 0);
                        i = 0;
                        rnd += 1234;
                        Current.Tag = -1;
                        Last.Tag = -1;
                    }
                    break;
                case STAV.JEDNA:
                    TStart.Visible = false;
                    TRestart.Visible = false;
                    LNavod.Visible = false;
                    LSkore.Visible = true;
                    LFinal.Visible = false;
                    break;
                case STAV.DVE:
                    TStart.Visible = false;
                    TRestart.Visible = false;
                    LNavod.Visible = false;
                    LSkore.Visible = true;
                    LFinal.Visible = false;

                    break;
                case STAV.VYHRA:
                    TStart.Visible = false;
                    TRestart.Visible = true;
                    LNavod.Visible = false;
                    LSkore.Visible = false;
                    LFinal.Visible = true;
                    LFinal.Text = LSkore.Text;
                    break;
            }
            stav = novy;
        }
        public Form1()
        {
            InitializeComponent();
            NastavStav(STAV.START);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void Karticka_Klik(object sender, EventArgs e)
        {
            if (stav == STAV.JEDNA)
                NastavStav(STAV.DVE);
            if ((int)Last.Tag == -1)
                NastavStav(STAV.JEDNA);
            if ((Button)sender != Last)
            {
                ((Button)sender).Text = ((Button)sender).Tag.ToString();
                if (stav == STAV.DVE)
                {
                    timerr.Enabled = true;
                    Current = ((Button)sender);
                }
                if (stav == STAV.JEDNA)
                { 
                    Last.Text = "PEXESO"; //+ Last.Tag.ToString();
                    (Current).Text = "PEXESO";//+ (Current).Tag.ToString();
                    Last = (Button)sender;
                }
            }
        }
        private void Start_Click(object sender, EventArgs e)
        {
            NastavStav(STAV.HRA);
        }
        private void Restart_Click(object sender, EventArgs e)
        {
            NastavStav(STAV.START);
        }

        private void timerr_Tick(object sender, EventArgs e)
        {
            if (Kontrola(Current, Last))
            {
                i += 2;
                this.Controls.Remove(Current);
                this.Controls.Remove(Last);
                Last = new Button();
                Last.Tag = -1;
            }
            else if (Kontrola(Current, Last) == false)
            {
                NastavSkore((int)LSkore.Tag, 1);
                Last.Text = "PEXESO"; //+ Last.Tag.ToString();
                (Current).Text = "PEXESO";//+ (Current).Tag.ToString();
                Last = new Button();
                Last.Tag = -1;
            }
            if (kolik == i)
            {
                NastavStav(STAV.VYHRA);
            }
            timerr.Enabled = false;
        }
    }
}