using System.Diagnostics;

namespace Chess_NEA
{
    public partial class MainView : Form
    {
        BoardUIClass BoardUI;
        bool MouseDragging;
        Point DraggedPieceLocation;
        string DraggedPiece;
        

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

            MouseDragging = false;

            BoardUI = new BoardUIClass();

            InitializeComponent();
        }

        private void FormMouseDown(object sender, MouseEventArgs e)
        {
            Point mousePosition = new(e.X, e.Y);
            for (int fileIndex = 0; fileIndex < 8; fileIndex++)
            {
                for (int rankIndex = 0; rankIndex < 8; rankIndex++)
                {
                    if (BoardUI.TileHitboxes[fileIndex, rankIndex].Contains(mousePosition))
                    {
                        if (BoardUI.CurrentBoard.GetTile(fileIndex, rankIndex).ContainsPiece())
                        {
                            if (BoardUI.CurrentBoard.GetTile(fileIndex, rankIndex).CurrentPiece.IsWhite == BoardUI.CurrentBoard.IsWhitesTurn)
                            {
                                DraggedPieceLocation = e.Location;
                                MouseDragging = true;
                                DraggedPiece = BoardUI.PieceSpriteLocations[fileIndex, rankIndex];
                                BoardUI.PieceSpriteLocations[fileIndex, rankIndex] = "empty";
                            }
                        }
                    }
                }
            }
        }

        private void FormMouseMove(object sender, MouseEventArgs e)
        {
            if (MouseDragging)
            {
                DraggedPieceLocation = e.Location;
            }
        }

        private void FormMouseUp(object sender, MouseEventArgs e)
        {
            if (MouseDragging)
            {
                MouseDragging = false;
                // add code for move
            }
        }

        private void FormTimerTick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void FormPaintEvent(object sender, PaintEventArgs e)
        {
            // draws the board 
            e.Graphics.DrawImage(Board, BoardUI.BoardLocation);
            if (!MouseDragging)
            {
                BoardUI.UpdatePieceSpriteLocations();
            }

            for (int fileIndex = 0; fileIndex < 8; fileIndex++)
            {
                for (int rankIndex = 0; rankIndex < 8; rankIndex++)
                {
                    if (BoardUI.PieceSpriteLocations[fileIndex,rankIndex] != "empty")
                    {
                        string filePath = (@"assets\" + BoardUI.PieceSpriteLocations[fileIndex, rankIndex] + ".png");
                        e.Graphics.DrawImage(Image.FromFile(filePath), BoardUI.BoardPixelPositions[fileIndex,rankIndex]);
                    }
                }
            }

            if (MouseDragging)
            {
                string filePath = (@"assets\" + DraggedPiece + ".png");
                int x = DraggedPieceLocation.X - 50;
                int y = DraggedPieceLocation.Y - 50;
                e.Graphics.DrawImage(Image.FromFile(filePath), x, y);
            }
        }
    }



    public class BoardUIClass
    {
        public Point[,] BoardPixelPositions = new Point[8, 8];
        public Rectangle[,] TileHitboxes = new Rectangle[8, 8];
        public string[,] PieceSpriteLocations = new string[8, 8];
        public ChessBoard CurrentBoard;
        public Point BoardLocation;

        public BoardUIClass()
        {
            CurrentBoard = new();
            BoardLocation = new Point(50, 50);
            InitializeBoardPixelPositions();
            InitializeTileHitboxes();
            UpdatePieceSpriteLocations();
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

        private void InitializeTileHitboxes()
        {
            for (int fileIndex = 0; fileIndex < 8; fileIndex++)
            {
                for (int rankIndex = 0; rankIndex < 8; rankIndex++)
                {
                    Size HitboxSize = new(100, 100);
                    TileHitboxes[fileIndex, rankIndex] = new Rectangle(BoardPixelPositions[fileIndex, rankIndex], HitboxSize);
                }
            }
        }

        public void UpdatePieceSpriteLocations()
        {
            for (int fileIndex = 0; fileIndex < 8; fileIndex++)
            {
                for (int rankIndex = 0; rankIndex < 8; rankIndex++)
                {
                    if (CurrentBoard.GetTile(fileIndex, rankIndex).CurrentPiece != null)
                    {
                        PieceSpriteLocations[fileIndex, rankIndex] = CurrentBoard.GetTile(fileIndex, rankIndex).CurrentPiece.GetPieceTypeAbbreviated();
                    }
                    else
                    {
                        PieceSpriteLocations[fileIndex, rankIndex] = "empty";
                    }
                }
            }
        }
    }
}
