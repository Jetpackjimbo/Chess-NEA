namespace Chess_NEA
{
    public partial class MainView : Form
    {

        Point boardPosition;
        Image board;
        Image wk;
        Image wq;
        Image wr;
        Image wb;
        Image wn;
        Image wp;
        Image bk;
        Image bq;
        Image br;
        Image bb;
        Image bn;
        Image bp;


        public MainView()
        {
            InitializeComponent();

            boardPosition = new Point(50, 50);

            // this loads all of the image assets needed
            board = Image.FromFile(@"assets\board.png");
            wk = Image.FromFile(@"assets\wk.png");
            wq = Image.FromFile(@"assets\wq.png");
            wr = Image.FromFile(@"assets\wr.png");
            wb = Image.FromFile(@"assets\wb.png");
            wn = Image.FromFile(@"assets\wn.png");
            wp = Image.FromFile(@"assets\wp.png");
            bk = Image.FromFile(@"assets\bk.png");
            bq = Image.FromFile(@"assets\bq.png");
            br = Image.FromFile(@"assets\br.png");
            bb = Image.FromFile(@"assets\bb.png");
            bn = Image.FromFile(@"assets\bn.png");
            bp = Image.FromFile(@"assets\bp.png");
        }

        private void FormMouseDown(object sender, MouseEventArgs e)
        {

        }

        private void FormMouseMove(object sender, MouseEventArgs e)
        {

        }

        private void FormMouseUp(object sender, MouseEventArgs e)
        {

        }

        private void FormTimerTick(object sender, EventArgs e)
        {

        }

        private void FormPaintEvent(object sender, PaintEventArgs e)
        {
            // draws the board 
            e.Graphics.DrawImage(board, boardPosition);
        }
    }
}
