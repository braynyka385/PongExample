using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongExample
{
    public partial class Form1 : Form
    {
        int paddle1X = 10;
        int paddle1Y = 170;
        int player1Score = 0;

        int paddle2X = 50;
        int paddle2Y = 170;
        int player2Score = 0;

        int paddleWidth = 10;
        int paddleHeight = 60;
        int paddleSpeed = 4;

        int ballX = 295;
        int ballY = 195;
        float ballXSpeed = 0;
        float ballYSpeed = 0;
        int ballWidth = 10;
        int ballHeight = 10;

        bool playStart = true;

        bool[] pressedKeys = new bool[10];

        float accel = 1.05f;

        bool activePlayer = true; //true == p1, false == p2

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Pen bluePen = new Pen(Color.Blue, 2);
        Font screenFont = new Font("Consolas", 12);


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    pressedKeys[0] = true;
                    break;
                case Keys.S:
                    pressedKeys[1] = true;
                    break;
                case Keys.A:
                    pressedKeys[2] = true;
                    break;
                case Keys.D:
                    pressedKeys[3] = true;
                    break;
                case Keys.Up:
                    pressedKeys[4] = true;
                    break;
                case Keys.Down:
                    pressedKeys[5] = true;
                    break;
                case Keys.Left:
                    pressedKeys[6] = true;
                    break;
                case Keys.Right:
                    pressedKeys[7] = true;
                    break;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    pressedKeys[0] = false;
                    break;
                case Keys.S:
                    pressedKeys[1] = false;
                    break;
                case Keys.A:
                    pressedKeys[2] = false;
                    break;
                case Keys.D:
                    pressedKeys[3] = false;
                    break;
                case Keys.Up:
                    pressedKeys[4] = false;
                    break;
                case Keys.Down:
                    pressedKeys[5] = false;
                    break;
                case Keys.Left:
                    pressedKeys[6] = false;
                    break;
                case Keys.Right:
                    pressedKeys[7] = false;
                    break;
            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball
            ballX += Convert.ToInt32(ballXSpeed); 
            ballY += Convert.ToInt32(ballYSpeed);

            //move player 1
            if (pressedKeys[0] == true && paddle1Y > 0)
            {
                paddle1Y -= paddleSpeed;
            }

            if (pressedKeys[1] == true && paddle1Y < this.Height - paddleHeight)
            {
                paddle1Y += paddleSpeed;
            }

            if (pressedKeys[2] == true && paddle1X > 0 + paddleWidth) // && paddle1X > this.Width + paddleWidth
            {
                paddle1X -= paddleSpeed;
            }

            if (pressedKeys[3] == true && paddle1X < this.Width - paddleWidth)
            {
                paddle1X += paddleSpeed;
            }

            //move player 2
            if (pressedKeys[4] == true && paddle2Y > 0)
            {
                paddle2Y -= paddleSpeed;
            }

            if (pressedKeys[5] == true && paddle2Y < this.Height - paddleHeight)
            {
                paddle2Y += paddleSpeed;
            }
            if (pressedKeys[6] == true && paddle2X > 0 + paddleWidth) // && paddle1X > this.Width + paddleWidth
            {
                paddle2X -= paddleSpeed;
            }

            if (pressedKeys[7] == true && paddle2X < this.Width - paddleWidth)
            {
                paddle2X += paddleSpeed;
            }

            //top and bottom wall collision
            if (ballY < 0 || ballY > this.Height - ballHeight)
            {
                ballYSpeed *= -1 * accel;  // or: ballYSpeed = -ballYSpeed;
            }

            //back wall collision
            if (ballX > this.Width - ballWidth)
            {
                ballXSpeed *= -1 * accel;
            }

            //create Rectangles of objects on screen to be used for collision detection
            Rectangle player1Rec = new Rectangle(paddle1X, paddle1Y, paddleWidth, paddleHeight);
            Rectangle player2Rec = new Rectangle(paddle2X, paddle2Y, paddleWidth, paddleHeight);
            Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);

            //check if ball hits either paddle. If it does change the direction
            //and place the ball in front of the paddle hit
            if (player1Rec.IntersectsWith(ballRec) && activePlayer == true)
            {
                if (playStart == true)
                {
                    if (pressedKeys[0] == true)
                    {
                        ballXSpeed = 6;
                        ballYSpeed = -6;
                    }
                    else if(pressedKeys[1] == true)
                    {
                        ballXSpeed = 6;
                        ballYSpeed = 6;
                    }
                    else
                    {
                        ballXSpeed = 6;
                    }
                    playStart = false;
                }
                else
                {
                    ballXSpeed *= -1 * accel;
                    ballX = paddle1X + paddleWidth + 1;
                    if (pressedKeys[0] == true)
                    {
                        ballYSpeed -= 0.15f;
                    }
                    else if (pressedKeys[1] == true)
                    {
                        ballYSpeed += 0.15f;
                    }
                }
                
                
                activePlayer = false;
            }
            else if (player2Rec.IntersectsWith(ballRec) && activePlayer == false)
            {
                if (playStart == true)
                {
                    if (pressedKeys[4] == true)
                    {
                        ballXSpeed = 6;
                        ballYSpeed = -6;
                    }
                    else if (pressedKeys[5] == true)
                    {
                        ballXSpeed = 6;
                        ballYSpeed = 6;
                    }
                    else
                    {
                        ballXSpeed = 6;
                    }
                    playStart = false;
                }
                else
                {
                    ballXSpeed *= -1 * accel;
                    ballX = paddle2X - ballWidth - 1;

                    if (pressedKeys[4] == true)
                    {
                        ballYSpeed -= 0.15f;
                    }
                    else if (pressedKeys[5] == true)
                    {
                        ballYSpeed += 0.15f;
                    }
                }
                
                activePlayer = true;
            }

            if (ballX < 0)
            {
                if (activePlayer == true)
                {
                    player1Score++;
                }
                else
                {
                    player2Score++;
                }
                
                ballX = 295;
                ballY = 195;

                ballXSpeed = 0;
                ballYSpeed = 0;

                paddle1Y = 170;
                paddle2Y = 170;
                playStart = true;
            }

            if (player1Score == 3 || player2Score == 3)
            {
                gameTimer.Enabled = false;
            }


            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(whiteBrush, ballX, ballY, ballWidth, ballHeight);

            e.Graphics.FillRectangle(blueBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillRectangle(blueBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);

            if (activePlayer == true)
            {
                e.Graphics.DrawRectangle(bluePen, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            }
            else
            {
                e.Graphics.DrawRectangle(bluePen, paddle2X, paddle2Y, paddleWidth, paddleHeight);
            }

            e.Graphics.DrawString($"{player1Score}", screenFont, whiteBrush, 280, 10);
            e.Graphics.DrawString($"{player2Score}", screenFont, whiteBrush, 310, 10);

        }
    }
}
