using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Data;


namespace UbatSpill
{

    public class Form1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.IContainer components;
        private const int AntallRad = 1;
        private const int AntallSpill = 3;
        private const int AntallKloser = 0;
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int WM_APPCOMMAND = 0x319;                
        
        private int AntallFartoy = 3;
        private long TimerCounter = 0;
        private long Timercntr = 0;
        private int Fart = 6;

        private int TheLevel = 0;

        private bool AktivKule = false;
        private LivIndikator _livesIndicator;

        private Fartoy VarFartoy = null;
        private Storbat VarStorbat = null;
        private bool StorbatStart = false;
        private bool SpillPagar = true;
        private Kule Kulen = new Kule(20, 30);
        private Score TheScore = null;
        private HighScore TheHighScore = null;
        private FremedFartoy[] FremmedFartoyer = new FremedFartoy[6];
        private Klosse[] Klosser = new Klosse[5];
        private FremedFartoy FremmedFartoyene = null;

        private int StorbatIntervall = 150;

        private string CurrentKeyDown = "";
        private string LastKeyDown = "";
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.Timer timer2;
        private MenuItem menuItem6;
        private MenuItem menuItem7;
        private MenuItem menuItem8;
        private MenuItem menuItem4;
        private MenuItem menuItem5;
        private MenuItem menuItem9;
        private MenuItem menuItem10;

        private Thread oThread = null;

        [DllImport("winmm.dll")]
        public static extern long PlaySound(String lpszName, long hModule, long dwFlags);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);


        public Form1()
        {

            InitializeComponent();
            // reduce flicker

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            InitializeAllGameObjects(true);

            timer1.Start();
            timer2.Start();

        }

        private void InitializeAllGameObjects(bool bScore)
        {
            InitializeKlosser();

            InitializeVarFartoy();

            InitializeLivesIndicator();


            if (bScore)
                InitializeScore();

            InitializeFremedFartoyer(TheLevel);

            VarStorbat = new Storbat("storbat0.gif", "storbat1.gif", "storbat2.gif");


            TheScore.GameOver = false;
            SpillPagar = true;
            Fart = 6;
        }

        private void InitializeStorbat()
        {
            VarStorbat.Reset();
            StorbatStart = true;
        }

        private void InitializeVarFartoy()
        {
            VarFartoy = new Fartoy();
            VarFartoy.Posisjon.Y = ClientRectangle.Bottom - 50;
            AntallFartoy = 4;
        }

        private void InitializeLivesIndicator()
        {
            _livesIndicator = new LivIndikator(ClientRectangle.Right - 250, 50);
        }
        private void InitializeScore()
        {
            TheScore = new Score(ClientRectangle.Right - 400, 50);
            TheHighScore = new HighScore(ClientRectangle.Left + 25, 50);
            TheHighScore.Les();
        }

        private void InitializeKlosser()
        {
            for (int i = 0; i < AntallKloser; i++)
            {
                Klosser[i] = new Klosse();
                Klosser[i].UpdateBounds();
                Klosser[i].Posisjon.X = (Klosser[i].GetBounds().Width + 75) * i + 25;
                Klosser[i].Posisjon.Y = ClientRectangle.Bottom -
                              (Klosser[i].GetBounds().Height + 75);
            }
        }

        void InitializeFremedFartoyer(int level)
        {
            FremmedFartoyer[0] = new FremedFartoy("fremmed1.gif", "fremmed1c.gif", 2 + level);
            FremmedFartoyer[1] = new FremedFartoy("fremmed2.gif", "fremmed2c.gif", 3 + level);
            FremmedFartoyer[2] = new FremedFartoy("fremmed2.gif", "fremmed2c.gif", 4 + level);
            FremmedFartoyer[3] = new FremedFartoy("fremmed3.gif", "fremmed3c.gif", 5 + level);
            FremmedFartoyer[4] = new FremedFartoy("fremmed3.gif", "fremmed3c.gif", 6 + level);
            //FremmedFartoyer[4] = new FremedFartoy("I.gif", "I.gif", 6 + level);
        }

        private string m_strCurrentSoundFile = "1.wav";
        public void PlayASound()
        {
            if (m_strCurrentSoundFile.Length > 0)
            {
                PlaySound(Application.StartupPath + "\\" + m_strCurrentSoundFile, 0, 0);
            }
            m_strCurrentSoundFile = "";
            m_nCurrentPriority = 3;
            oThread.Abort();
        }

        int m_nCurrentPriority = 3;
        public void PlaySoundInThread(string wavefile, int priority)
        {
            if (priority <= m_nCurrentPriority)
            {
                m_nCurrentPriority = priority;
                if (oThread != null)
                    oThread.Abort();

                m_strCurrentSoundFile = wavefile;
                oThread = new Thread(new ThreadStart(PlayASound));
                oThread.Start();

            }

        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem6,
            this.menuItem5});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3});
            this.menuItem1.Text = "Fil";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "Nyt Spill";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "Avslutt";
            this.menuItem3.Click += new System.EventHandler(this.Menu_Exit);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 1;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem7,
            this.menuItem8,
            this.menuItem4});
            this.menuItem6.Text = "Kontroll";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 0;
            this.menuItem7.Text = "Pause";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 1;
            this.menuItem8.Text = "Gjenoppta";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "Lyd/Lydløs";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 2;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem9,
            this.menuItem10});
            this.menuItem5.Text = "Hjelp";
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 0;
            this.menuItem9.Text = "Hjelp";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 1;
            this.menuItem10.Text = "Om Ubåt Spillet";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 50;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(930, 580);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Space Fremmed Game";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);

        }
        #endregion


        [STAThread]
        static void Main()
        {
            Application.Run(new welcomescreen());
        }

        private void HandtereTast()
        {
            switch (CurrentKeyDown)
            {
                case "Space":
                    if (AktivKule == false)
                    {
                        Kulen.Posisjon = VarFartoy.HentKuleStart();
                        AktivKule = true;
                        PlaySoundInThread("3.wav", 2);
                    }
                    CurrentKeyDown = LastKeyDown;
                    break;
                case "Left":
                    VarFartoy.FlyttVenstre();
                    Invalidate(VarFartoy.GetBounds());
                    if (timer1.Enabled == false)
                        timer1.Start();
                    break;
                case "Right":
                    VarFartoy.FlyttHoyre(ClientRectangle.Right);
                    Invalidate(VarFartoy.GetBounds());
                    if (timer1.Enabled == false)
                        timer1.Start();
                    break;
                
                default:
                    break;
            }


        }

        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            string result = e.KeyData.ToString();
            CurrentKeyDown = result;
            if (result == "Left" || result == "Right")
            {
                LastKeyDown = result;
            }

            //			Invalidate(VarFartoy.GetBounds());

        }

        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int i = 0; i < AntallKloser; i++)
            {
                Klosser[i].Draw(g);
            }

            //			g.FillRectangle(Brushes.Black, 0, 0, ClientRectangle.Width, ClientRectangle.Height);
            VarFartoy.Draw(g);
            TheScore.Draw(g);
            TheHighScore.Draw(g);
            _livesIndicator.Draw(g);
            if (AktivKule)
            {
                Kulen.Draw(g);
            }

            if (StorbatStart)
            {
                VarStorbat.Draw(g);
            }

            for (int i = 0; i < AntallRad; i++)
            {
                FremmedFartoyene = FremmedFartoyer[i];
                FremmedFartoyene.Draw(g);
            }

        }

        private int BeregnStorsteSistePosisjon()
        {
            int max = 0;
            for (int i = 0; i < AntallRad; i++)
            {
                FremmedFartoyene = FremmedFartoyer[i];
                int nLastPos = FremmedFartoyene.HentSisteFremmed().Posisjon.X;
                if (nLastPos > max)
                    max = nLastPos;
            }

            return max;
        }

        private int BeregnMinsteForstePosisjon()
        {
            int min = 50000;

            try
            {
                for (int i = 0; i < AntallRad; i++)
                {
                    FremmedFartoyene = FremmedFartoyer[i];
                    int nFirstPos = FremmedFartoyene.HentForsteFremmed().Posisjon.X;
                    if (nFirstPos < min)
                        min = nFirstPos;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            return min;

        }

        private void FlyttFremmedFartoyer()
        {
            bool bMoveDown = false;

            for (int i = 0; i < AntallRad; i++)
            {

                FremmedFartoyene = FremmedFartoyer[i];
                FremmedFartoyene.Flytt();

            }

            //			if (FremmedLydCounter % 5)
            PlaySoundInThread("4.wav", 3);


            if ((BeregnStorsteSistePosisjon()) > ClientRectangle.Width - FremmedFartoyer[4][0].GetWidth())
            {
                FremmedFartoyene.HoyreRetning = false;
                SettAlleRetninger(false);
            }

            if ((BeregnMinsteForstePosisjon()) < FremmedFartoyer[4][0].Width / 3)
            {
                FremmedFartoyene.HoyreRetning = true;
                SettAlleRetninger(true);
                for (int i = 0; i < AntallRad; i++)
                {
                    bMoveDown = true;
                }
            }

            if (bMoveDown)
            {
                for (int i = 0; i < AntallRad; i++)
                {

                    FremmedFartoyene = FremmedFartoyer[i];
                    FremmedFartoyene.FlyttNed();

                }
            }
        }

        private int TotaltAntallFremmede()
        {
            int sum = 0;
            for (int i = 0; i < AntallRad; i++)
            {
                FremmedFartoyene = FremmedFartoyer[i];
                sum += FremmedFartoyene.AntallLivFremmede();
            }

            return sum;
        }

        private void FlyttFremmedeIPlass()
        {
            for (int i = 0; i < AntallRad; i++)
            {
                FremmedFartoyene = FremmedFartoyer[i];
                FremmedFartoyene.FlyttIPlass();
            }

        }

        private void SettAlleRetninger(bool bDirection)
        {
            for (int i = 0; i < AntallRad; i++)
            {
                FremmedFartoyene = FremmedFartoyer[i];
                FremmedFartoyene.HoyreRetning = bDirection;
            }

        }

        public int BeregnPoengsumFraRad(int num)
        {
            int nScore = 10;
            switch (num)
            {
                case 0:
                    nScore = 30;
                    break;
                case 1:
                    nScore = 20;
                    break;
                case 2:
                    nScore = 20;
                    break;
                case 3:
                    nScore = 10;
                    break;
                case 4:
                    nScore = 10;
                    break;
            }

            return nScore;
        }

        void TestKuleKollisjon()
        {
            int rowNum = 0;
            for (int i = 0; i < AntallRad; i++)
            {
                FremmedFartoyene = FremmedFartoyer[i];
                rowNum = i;
                int kollisjonIndex = FremmedFartoyene.KollisjonTest(Kulen.GetBounds());

                if ((kollisjonIndex >= 0) && AktivKule)
                {
                    FremmedFartoyene.Fremmede2[kollisjonIndex].Truffet = true;
                    TheScore.AddScore(BeregnPoengsumFraRad(rowNum));
                    PlaySoundInThread("1.wav", 1);

                    AktivKule = false;
                    Kulen.Reset();
                }

                if (StorbatStart && AktivKule && VarStorbat.GetBounds().IntersectsWith(Kulen.GetBounds()))
                {
                    VarStorbat.Truffet = true;
                    if (VarStorbat.PoengsumRegnet == false)
                    {
                        TheScore.AddScore(VarStorbat.RegnPoengsum());
                        VarStorbat.PoengsumRegnet = true;
                        PlaySoundInThread("3.wav", 1);
                    }

                }
            }
        }

        void TestLanding()
        {
            for (int i = 0; i < AntallRad; i++)
            {
                FremmedFartoyene = FremmedFartoyer[i];
                if (FremmedFartoyene.FremmedHarLandet(ClientRectangle.Bottom))
                {
                    VarFartoy.Truffet = true;
                    PlaySoundInThread("2.wav", 1);
                    TheScore.GameOver = true;
                    TheHighScore.Skriv(TheScore.Count);
                    SpillPagar = false;
                }
            }

        }

        void NullstillAlleBombeCounter()
        {
            for (int i = 0; i < AntallRad; i++)
            {
                FremmedFartoyene = FremmedFartoyer[i];
                FremmedFartoyene.NullstillBombeCounter();
            }
        }

        void TestBombeKollisjon()
        {
            if (VarFartoy.Dod)
            {
                AntallFartoy--;
                _livesIndicator.DecrementLives(); // also show on lives indicator
                Invalidate();
                if (AntallFartoy == 0)
                {
                    TheHighScore.Skriv(TheScore.Count);
                    TheScore.GameOver = true;
                    SpillPagar = false;
                }
                else
                {
                    VarFartoy.Nullstill();
                    NullstillAlleBombeCounter();
                }
            }

            if (VarFartoy.Truffet == true)
                return;

            for (int i = 0; i < AntallRad; i++)
            {
                FremmedFartoyene = FremmedFartoyer[i];
                for (int j = 0; j < FremmedFartoyene.Fremmede2.Length; j++)
                {
                    for (int k = 0; k < AntallKloser; k++)
                    {
                        bool kuleHull = false;
                        if (Klosser[k].TestKollisjon(FremmedFartoyene.Fremmede2[j].GetBombBounds(), true, out kuleHull))
                        {
                            FremmedFartoyene.Fremmede2[j].NullstillBombePosisjon();
                            Invalidate(Klosser[k].GetBounds());
                        }


                        if (Klosser[k].TestKollisjon(Kulen.GetBounds(), false, out kuleHull))
                        {
                            AktivKule = false;
                            Invalidate(Klosser[k].GetBounds());
                            Kulen.Reset();
                        }
                    }

                    if (FremmedFartoyene.Fremmede2[j].BombeKolliderer(VarFartoy.GetBounds()))
                    {
                        VarFartoy.Truffet = true;
                        PlaySoundInThread("2.wav", 1);
                    }
                }
            }
        }


        private int nTotaltFremmede = 0;
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            HandtereTast();

            TimerCounter++;

            if (SpillPagar == false)
            {
                if (TimerCounter % 6 == 0)
                    FlyttFremmedeIPlass();
                Invalidate();
                return;
            }


            if (Kulen.Posisjon.Y < 0)
            {
                AktivKule = false;
            }

            if (TimerCounter % StorbatIntervall == 0)
            {
                InitializeStorbat();
                PlaySoundInThread("8.wav", 1);
                StorbatStart = true;
            }

            if (StorbatStart == true)
            {
                VarStorbat.Flytt();
                if (VarStorbat.GetBounds().Left > ClientRectangle.Right)
                {
                    StorbatStart = false;
                }
            }


            if (TimerCounter % Fart == 0)
            {
                FlyttFremmedFartoyer();

                nTotaltFremmede = TotaltAntallFremmede();

                if (nTotaltFremmede <= 20)
                {
                    Fart = 5;
                }

                if (nTotaltFremmede <= 10)
                {
                    Fart = 4;
                }


                if (nTotaltFremmede <= 5)
                {
                    Fart = 3;
                }

                if (nTotaltFremmede <= 3)
                {
                    Fart = 2;
                }

                if (nTotaltFremmede <= 1)
                {
                    Fart = 1;
                }

                if (nTotaltFremmede == 0)
                {
                    InitializeAllGameObjects(false); // don't initialize score					
                    TheLevel++;
                }


            }

            TestKuleKollisjon();
            TestBombeKollisjon();


            Invalidate();

        }

        private void Form1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //			string result = e.KeyChar.ToString();
            //			Invalidate(VarFartoy.GetBounds());

        }

        private void Form1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            string result = e.KeyData.ToString();
            if (result == "Left" || result == "Right")
            {
                LastKeyDown = "";
            }

        }

        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            this.InitializeAllGameObjects(true);
            TheLevel = 0;
        }

        private void Menu_Exit(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

       

        private void timer2_Tick(object sender, System.EventArgs e)
        {

            Timercntr++;


        }

      

        private void menuItem7_Click(object sender, EventArgs e)
        {

            timer1.Stop();
           
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void menuItem4_Click(object sender, EventArgs e)
        {

            SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_MUTE);
        }

        private void btn_Play_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void btn_Mute_Click(object sender, EventArgs e)
        {
            SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_MUTE);
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "file://C:\\Users\\Franknaros\\Dropbox\\UBAT SPILL\\bin\\Debug\\Bruker Manual.chm");
        }
        
        private void menuItem10_Click(object sender, EventArgs e)
        {
            About abt = new About();
            abt.Show();
            
            
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            welcomescreen ws = new welcomescreen();
            ws.Show();
                       
        }
               
    }

}
        
            