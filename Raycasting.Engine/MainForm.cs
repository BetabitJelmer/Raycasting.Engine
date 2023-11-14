using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Forms;

namespace Raycasting.Engine
{
    public partial class MainForm : Form
    {
        // These control the size of window and frame.
        private const int W_WIDTH = 1280;
        private const int W_HEIGHT = 960;

        private Thread logicThread;

        // Stopwatch is used to keep track of frame time for consistent movement.
        private Stopwatch frameTime = new Stopwatch();

        Domain.Raycasting RC = new Domain.Raycasting();

        private bool forward = false;
        private bool back = false;
        private bool turnLeft = false;
        private bool turnRight = false;
        private bool left = false;
        private bool right = false;

        // Create a new cancellation token and a token source to use
        CancellationTokenSource tokenSource = new CancellationTokenSource();

        public MainForm()
        {
            InitializeComponent();

            CancellationToken token = tokenSource.Token;

            pictureBoxMain.Size = new Size(W_WIDTH, W_HEIGHT);
            ClientSize = new Size(W_WIDTH, W_HEIGHT);

            // The logic thread will handle raycasting and movement.
            logicThread = new Thread(() => DrawMainScreen(token));
            logicThread.Start();
        }

        public void DrawMainScreen(CancellationToken token)
        {
            // FrameTimeDouble is the length of time between frames.
            double frameTimeDouble = 0;
            try
            {
                while (!token.IsCancellationRequested)
                {
                    // Start the stopwatch so we can time this frame.
                    frameTime.Restart();
                    // Move the player if a movement key is being pressed.
                    MovePlayer();
                    // Update the player move speeds based on the last frame length.
                    RC.UpdateFramerate(frameTimeDouble);

                    var img = RC.NewFrame(W_WIDTH, W_HEIGHT);

                    UpdatePictureBoxMainImage((Image)img.Clone());
                    img.Dispose();
                    // Stop the stopwatch so we can get the time the frame took.
                    frameTime.Stop();
                    frameTimeDouble = frameTime.ElapsedMilliseconds;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void UpdatePictureBoxMainImage(Image image)
        {
            if (pictureBoxMain.InvokeRequired)
            {
                pictureBoxMain.Invoke(new Action(() => UpdatePictureBoxMainImage(image)));
            }
            else
            {
                pictureBoxMain.Image?.Dispose();
                pictureBoxMain.Image = image;
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            tokenSource.Cancel();
            logicThread.Interrupt();
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // This function gets when a key is pressed and changes that key's bool so that
            // we know it's down and can be used when we move the player.
            if (e.KeyCode == Keys.W)
            {
                forward = true;
            }

            if (e.KeyCode == Keys.S)
            {
                back = true;
            }
            if (e.KeyCode == Keys.Q)
            {
                turnLeft = true;
            }
            if (e.KeyCode == Keys.E)
            {
                turnRight = true;
            }
            if (e.KeyCode == Keys.A)
            {
                left = true;
            }
            if (e.KeyCode == Keys.D)
            {
                right = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void MainWindow_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // This function gets when a key is released and changes that key's bool so that
            // we know it's up again and can stop moving the player.
            if (e.KeyCode == Keys.W)
            {
                forward = false;
            }

            if (e.KeyCode == Keys.S)
            {
                back = false;
            }
            if (e.KeyCode == Keys.Q)
            {
                turnLeft = false;
            }
            if (e.KeyCode == Keys.E)
            {
                turnRight = false;
            }
            if (e.KeyCode == Keys.A)
            {
                left = false;
            }
            if (e.KeyCode == Keys.D)
            {
                right = false;
            }
        }

        private void MovePlayer()
        {
            // Checks if any key bools are down, and performs the appropriate player move(s).
            if (forward)
            {
                RC.Move(true, null);
            }

            if (back)
            {
                RC.Move(false, null);
            }

            if (right)
            {
                RC.Move(null, false);
            }

            if (left)
            {
                RC.Move(null, true);
            }

            if (turnLeft)
            {
                RC.Turn(false);
            }

            if (turnRight)
            {
                RC.Turn(true);
            }
        }
    }
}