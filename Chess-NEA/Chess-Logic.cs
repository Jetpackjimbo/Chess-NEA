using System;
using System.Collections.Generic;
using System.Text;

namespace Chess_NEA
{
    public class Tile
    {
        int File { get; }
        int Rank { get; }
        public Piece? CurrentPiece { get; set; }

        public Tile(int file, int rank)
        {
            this.File = file;
            this.Rank = rank;
        }

        public string GetTileID()
        {
            return Convert.ToString(File) + Convert.ToString(Rank);
        }

        public bool ContainsPiece()
        {
            if (CurrentPiece == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class ChessBoard
    {
        private Tile[,] boardGrid = new Tile[8, 8];

        public ChessBoard()
        {
            // ChessBoard constructor adds Tiles to a 2D array
            int file;
            int rank;
            for (int fileIndex = 0; fileIndex < 8; fileIndex++)
            {
                file = (1 + fileIndex);
                for (int rankIndex = 0; rankIndex < 8; rankIndex++)
                {
                    rank = (1 + rankIndex);
                    boardGrid[fileIndex, rankIndex] = new Tile(file, rank);
                }
            }
        }

        public Tile GetTile(string TileID)
        {
            // this method takes in the file and rank and returns the tile in that location
            int fileIndex = TileID[0] - 49;
            int rankIndex = TileID[1] - 49;

            return boardGrid[fileIndex, rankIndex];
        }

        public void InitializeNewBoard()
        {
            // adding the backrank white pieces
            boardGrid[0, 0].CurrentPiece = new Rook(true, false);
            boardGrid[1, 0].CurrentPiece = new Knight(true);
            boardGrid[2, 0].CurrentPiece = new Bishop(true);
            boardGrid[3, 0].CurrentPiece = new Queen(true);
            boardGrid[4, 0].CurrentPiece = new King(true, false);
            boardGrid[5, 0].CurrentPiece = new Bishop(true);
            boardGrid[6, 0].CurrentPiece = new Knight(true);
            boardGrid[7, 0].CurrentPiece = new Rook(true, false);

            // adding white's pawns
            for (int i = 0; i < 8; i++)
            {
                boardGrid[i, 1].CurrentPiece = new Pawn(true);
            }

            // adding the backrank black pieces
            boardGrid[0, 7].CurrentPiece = new Rook(false, false);
            boardGrid[1, 7].CurrentPiece = new Knight(false);
            boardGrid[2, 7].CurrentPiece = new Bishop(false);
            boardGrid[3, 7].CurrentPiece = new Queen(false);
            boardGrid[4, 7].CurrentPiece = new King(false, false);
            boardGrid[5, 7].CurrentPiece = new Bishop(false);
            boardGrid[6, 7].CurrentPiece = new Knight(false);
            boardGrid[7, 7].CurrentPiece = new Rook(false, false);

            // adding black's pawns
            for (int i = 0; i < 8; i++)
            {
                boardGrid[i, 6].CurrentPiece = new Pawn(false);
            }
        }
    }

    public abstract class Piece
    {
        protected string? PieceType = "blank";

        public bool IsWhite { get; }

        public Piece(bool isWhite)
        {
            this.IsWhite = isWhite;
        }

        public virtual string GetPieceType()
        {
            return PieceType;
        }

        public abstract string GetPieceTypeAbbreviated();
    }

    class King : Piece
    {
        // HasMoved is stored so 
        bool HasMoved {  get; set; }

        public King(bool isWhite, bool hasMoved) : base(isWhite)
        {
            this.PieceType = "King";
            this.HasMoved = hasMoved;
        }

        public override string GetPieceTypeAbbreviated()
        {
            if (IsWhite)
            {
                return "wk";
            }
            return "bk";
        }
    }

    class Queen : Piece
    {
        public Queen(bool isWhite) : base(isWhite)
        {
            this.PieceType = "Queen";
        }

        public override string GetPieceTypeAbbreviated()
        {
            if (IsWhite)
            {
                return "wq";
            }
            return "bq";
        }
    }

    class Rook : Piece
    {
        bool HasMoved { get; set; }
        public Rook(bool isWhite, bool hasMoved) : base(isWhite)
        {
            this.PieceType = "Rook";
            this.HasMoved = hasMoved;
        }

        public override string GetPieceTypeAbbreviated()
        {
            if (IsWhite)
            {
                return "wr";
            }
            return "br";
        }
    }

    class Bishop : Piece
    {
        public Bishop(bool isWhite) : base(isWhite)
        {
            this.PieceType = "Bishop";
        }

        public override string GetPieceTypeAbbreviated()
        {
            if (IsWhite)
            {
                return "wb";
            }
            return "bb";
        }
    }

    class Knight : Piece
    {
        public Knight(bool isWhite) : base(isWhite)
        {
            this.PieceType = "Knight";
        }

        public override string GetPieceTypeAbbreviated()
        {
            if (IsWhite)
            {
                return "wn";
            }
            return "bn";
        }
    }

    class Pawn : Piece
    {

        bool CanBeTakenByEnPassant { get; set; }

        public Pawn(bool isWhite) : base(isWhite)
        {
            this.PieceType = "Pawn";
            this.CanBeTakenByEnPassant = false;
        }

        public override string GetPieceTypeAbbreviated()
        {
            if (IsWhite)
            {
                return "wp";
            }
            return "bp";
        }
    }
}
