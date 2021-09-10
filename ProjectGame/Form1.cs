using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectGame
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, isGameOver;

        // Modos relacionados ao jogo
        int jumpSpeed;
        int force;
        int score = 0;

        // Modos relacionados ao Player
        int playerSpeed = 7;

        // Modos relacionados a plataformas
        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        // Modos relacionados aos Inimigos
        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;



        public Form1()
        {
            InitializeComponent();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            LBL_Pontos.Text = score.ToString();
            Player.Top += jumpSpeed;

            if (goLeft) { Player.Left -= playerSpeed;  }
            if (goRight) { Player.Left += playerSpeed; }
            if (jumping) { jumpSpeed = -8; force -= 1; } else { jumpSpeed = 10; }
            if (jumping && force < 0) { jumping = false;}

            foreach (Control x in this.Controls)
            {
                if ((string)x.Tag == "Plataforma") 
                {
                    if (Player.Bounds.IntersectsWith(x.Bounds)) 
                    {
                        force = 8;
                        Player.Top = x.Top - Player.Height;

                        if ((string)x.Name == "H_plataforma" && goLeft == false || (string)x.Name == "H_plataforma" && goRight == false) 
                        {
                            Player.Left -= horizontalSpeed;
                        }
                    }

                    x.BringToFront();
                }

                if ((string)x.Tag == "Moeda")
                {
                    
                    
                    if (Player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                    {
                        x.Visible = false;
                        score++;
                    }
                }

                if ((string)x.Tag == "Inimigo")
                {


                    if (Player.Bounds.IntersectsWith(x.Bounds))
                    {
                        GameTimer.Stop();
                        isGameOver = true;
                        LBL_Pontos.Text = score + Environment.NewLine + "Você morreu!"; 
                    }
                }
            }


            H_plataforma.Left -= horizontalSpeed;

            if (H_plataforma.Left < 0 || H_plataforma.Left + H_plataforma.Width > this.ClientSize.Width) 
            {
                horizontalSpeed = -horizontalSpeed;
            }

            V_plataforma.Top -= verticalSpeed;

            if (V_plataforma.Top < 180 || V_plataforma.Top > 580 || H_plataforma.Top + H_plataforma.Height > this.ClientSize.Height)
            {
                verticalSpeed = -verticalSpeed;
            }

            Inimigo_1.Left -= enemyOneSpeed;

            if (Inimigo_1.Left < pictureBox4.Left || Inimigo_1.Left + Inimigo_1.Width > pictureBox4.Left + pictureBox4.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            Inimigo_2.Left -= enemyTwoSpeed;

            if (Inimigo_2.Left < pictureBox3.Left || Inimigo_2.Left + Inimigo_2.Width > pictureBox3.Left + pictureBox3.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            if (Player.Top + Player.Height > this.ClientSize.Height + 50) 
            {
                GameTimer.Stop();
                isGameOver = true;
                LBL_Pontos.Text = score + Environment.NewLine + "Você caiu para sua morte!";

            }

            if (Player.Bounds.IntersectsWith(Porta.Bounds) && score > 15)
            {
                GameTimer.Stop();
                isGameOver = true;
                LBL_Pontos.Text = score + Environment.NewLine + "Você venceu!";

            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    goLeft = true;
                    break;

                case Keys.Right:
                    goRight = true;
                    break;

                case Keys.Space:
                    jumping = true;
                    break;

                case Keys.Enter:
                    isGameOver = true;
                    RestartGame();
                    break;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    goLeft = false;
                    break;

                case Keys.Right:
                    goRight = false;
                    break;

                case Keys.Space:
                    jumping = false;
                    break;
            }

        }

        private void RestartGame() 
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;

            LBL_Pontos.Text = "Pontos: " + score;
            foreach (Control x in this.Controls) 
            {
                if (x is PictureBox && x.Visible == false) { x.Visible = true; }
            }

            Player.Left = 72;
            Player.Top = 656;

            Inimigo_1.Left = 392;
            Inimigo_1.Top = 389;

            Inimigo_2.Left = 843;
            Inimigo_2.Top = 469;

            H_plataforma.Left = 268;
            V_plataforma.Left = 598;

            H_plataforma.Top = 209;
            V_plataforma.Top = 209;

            GameTimer.Start();


        }
    }
}
