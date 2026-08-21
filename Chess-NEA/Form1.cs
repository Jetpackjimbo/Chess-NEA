namespace Chess_NEA
{
    public partial class MainView : Form
    {

        Point BoardPosition;
        ChessBoard CurrentBoard;
        Point[,] BoardPixelPositions = new Point[8, 8];

        Image Board;
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

            BoardPosition = new Point(50, 50);

            // this loads all of the image assets needed
            Board = Image.FromFile(@"assets\board.png");
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

            CurrentBoard = new();
            CurrentBoard.InitializeNewBoard();

            InitializeBoardPixelPositions();
        }

        private void InitializeBoardPixelPositions()
        {
            for (int fileIndex = 0; fileIndex < 8; fileIndex++)
            {
                for (int rankIndex = 0; rankIndex < 8; rankIndex++)
                {
                    BoardPixelPositions[fileIndex, rankIndex] = new Point(50 + (fileIndex * 100), 750 - (rankIndex * 100));
                }
            }
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
            e.Graphics.DrawImage(Board, BoardPosition);
            

            for (int fileIndex = 0; fileIndex < 8; fileIndex++)
            {
                for (int rankIndex = 0; rankIndex < 8; rankIndex++)
                {
                    string TileID = Convert.ToString(fileIndex+1) + Convert.ToString(rankIndex+1);
                    if (CurrentBoard.GetTile(TileID).ContainsPiece())
                    {
                        string pieceAbrriviation = CurrentBoard.GetTile(TileID).CurrentPiece.GetPieceTypeAbbreviated();
                        string filePath = (@"assets\" + pieceAbrriviation + ".png");
                        e.Graphics.DrawImage(Image.FromFile(filePath), BoardPixelPositions[fileIndex,rankIndex]);
                    }
                }
            }
        }
    }
}
