using System;
using System.Collections.Generic;
using System.Text;

namespace Chess_NEA
{
    public class Square
    {
        char File { get; }
        int Rank { get; }
        public Piece? CurrentPiece { get; set; }

        public Square(char file, int rank)
        {
            this.File = file;
            this.Rank = rank;
        }
    }

    public class ChessBoard
    {
        private Square[,] boardGrid = new Square[8, 8];

        public ChessBoard()
        {
            // ChessBoard constructor adds Squares to a 2D array
            char file;
            int rank;
            for (int fileIndex = 0; fileIndex < 8; fileIndex++)
            {
                file = (char)('a' + fileIndex);
                for (int rankIndex = 0; rankIndex < 8; rankIndex++)
                {
                    rank = (1 + rankIndex);
                    boardGrid[fileIndex, rankIndex] = new Square(file, rank);
                }
            }
        }

        public Square getSquare(char file, int rank)
        {
            // this method takes in the file and rank and returns the square in that location
            int fileIndex = (int)(file - 'a');
            int rankIndex = (rank - 1);
            return boardGrid[fileIndex, rankIndex];
        }

        public void initiateNewBoard()
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
                boardGrid[i, 1].CurrentPiece = new Pawn(true, false);
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
                boardGrid[i, 6].CurrentPiece = new Pawn(false, false);
            }
        }
    }

    public class Piece
    {
        public string PieceType = "";
        public bool IsWhite { get; }

        public Piece(bool isWhite)
        {
            this.IsWhite = isWhite;
        }

        public string getPieceType()
        {
            return PieceType;
        }
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
    }

    class Queen : Piece
    {
        public Queen(bool isWhite) : base(isWhite)
        {
            this.PieceType = "Queen";
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
    }

    class Bishop : Piece
    {
        public Bishop(bool isWhite) : base(isWhite)
        {
            this.PieceType = "Bishop";
        }
    }

    class Knight : Piece
    {
        public Knight(bool isWhite) : base(isWhite)
        {
            this.PieceType = "Knight";
        }
    }

    class Pawn : Piece
    {
        bool HasMoved { get; set; }
        bool CanBeTakenByEnPassant { get; set; }

        public Pawn(bool isWhite, bool hasMoved) : base(isWhite)
        {
            this.PieceType = "Pawn";
            this.HasMoved = hasMoved;
            this.CanBeTakenByEnPassant = false;
        }
    }
}
