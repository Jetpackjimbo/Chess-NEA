using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
            File = file;
            Rank = rank;
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
        private Tile[,] BoardGrid = new Tile[8, 8];
        private bool IsWhitesTurn;

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
                    BoardGrid[fileIndex, rankIndex] = new Tile(file, rank);
                }
            }

            IsWhitesTurn = true;
        }

        public Tile GetTileWithID(string TileID)
        {
            // this method takes in the file and rank and returns the tile in that location
            int fileIndex = TileID[0] - 49;
            int rankIndex = TileID[1] - 49;

            return BoardGrid[fileIndex, rankIndex];
        }

        public Tile GetTile(int file, int rank)
        {
            return BoardGrid[file + 1, rank + 1];
        }

        public void InitializeNewBoard()
        {
            // adding the backrank white pieces
            BoardGrid[0, 0].CurrentPiece = new Rook(true, false);
            BoardGrid[1, 0].CurrentPiece = new Knight(true);
            BoardGrid[2, 0].CurrentPiece = new Bishop(true);
            BoardGrid[3, 0].CurrentPiece = new Queen(true);
            BoardGrid[4, 0].CurrentPiece = new King(true, false);
            BoardGrid[5, 0].CurrentPiece = new Bishop(true);
            BoardGrid[6, 0].CurrentPiece = new Knight(true);
            BoardGrid[7, 0].CurrentPiece = new Rook(true, false);

            // adding white's pawns
            for (int i = 0; i < 8; i++)
            {
                BoardGrid[i, 1].CurrentPiece = new Pawn(true);
            }

            // adding the backrank black pieces
            BoardGrid[0, 7].CurrentPiece = new Rook(false, false);
            BoardGrid[1, 7].CurrentPiece = new Knight(false);
            BoardGrid[2, 7].CurrentPiece = new Bishop(false);
            BoardGrid[3, 7].CurrentPiece = new Queen(false);
            BoardGrid[4, 7].CurrentPiece = new King(false, false);
            BoardGrid[5, 7].CurrentPiece = new Bishop(false);
            BoardGrid[6, 7].CurrentPiece = new Knight(false);
            BoardGrid[7, 7].CurrentPiece = new Rook(false, false);

            // adding black's pawns
            for (int i = 0; i < 8; i++)
            {
                BoardGrid[i, 6].CurrentPiece = new Pawn(false);
            }
        }
    }

    public class Move
    {
        private ChessBoard BoardBeforeMove;
        private string StartingTileID;
        private string DestinationTileID;
        private bool WhitePlayersMove;
        private Piece PieceMoving;
        private bool IsCapture;
        private bool IsCheck;
        private bool IsCheckmate;
        public bool IsLegalMove { get; }

        public Move(ChessBoard boardBeforeMove, string startingTileID, string destinationTileID, bool whitePlayersMove)
        {
            BoardBeforeMove = boardBeforeMove;
            StartingTileID = startingTileID;
            DestinationTileID = destinationTileID;
            WhitePlayersMove = whitePlayersMove;
            PieceMoving = BoardBeforeMove.GetTileWithID(startingTileID).CurrentPiece;
            IsCapture = TestIfCapture();
            //IsCheck = 
            //IsCheckmate =
            IsLegalMove = LegalityCheck();
        }

        private bool TestIfCapture()
        {
            // this just checks if there is a piece on the destination tile, validity checks will be done later
            if (BoardBeforeMove.GetTileWithID(DestinationTileID).ContainsPiece()) return true; else return false;
        }

        private bool LegalityCheck()
        {
            if (IsCapture)
            {
                // a player can't take one of their own pieces, so the move is considered illegal if that occurs
                if (BoardBeforeMove.GetTileWithID(DestinationTileID).CurrentPiece.IsWhite == WhitePlayersMove) return false;
            }

            // add other legality checks

            return true;
        }

    }

    public class Direction
    {
        public readonly static Direction Up = new Direction(1, 0);
        public readonly static Direction Down = new Direction(-1, 0);
        public readonly static Direction Right = new Direction(0, 1);
        public readonly static Direction Left = new Direction(0, -1);
        public readonly static Direction Up_Right = new Direction(1, 1);
        public readonly static Direction Up_Left = new Direction(1, -1);
        public readonly static Direction Down_Right = new Direction(-1, 1);
        public readonly static Direction Down_Left = new Direction(-1, -1);


        public int FileChange { get; }
        public int RankChange { get; }

        public Direction(int fileChange, int rankChange)
        {
            FileChange = fileChange;
            RankChange = rankChange;
        }

        public static Direction operator +(Direction direction1, Direction direction2)
        {
            // adds up the changes in each direction
            return new Direction(direction1.FileChange + direction2.FileChange, direction1.RankChange + direction2.RankChange);
        }

        public static Direction operator *(int scalar, Direction direction1)
        {
            // multiplies direction by a scalar
            return new Direction(direction1.FileChange * scalar, direction1.RankChange * scalar);
        }
    }

    public abstract class Piece
    {
        protected string PieceType = "blank";
        public bool IsWhite { get; }

        public Piece(bool isWhite)
        {
            IsWhite = isWhite;
        }

        public virtual string GetPieceType()
        {
            return PieceType;
        }

        public abstract string GetPieceTypeAbbreviated();
    }

    class King : Piece
    {
        // HasMoved is stored to check if the player can castle
        bool HasMoved {  get; set; }

        public King(bool isWhite, bool hasMoved) : base(isWhite)
        {
            PieceType = "King";
            HasMoved = hasMoved;
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
            PieceType = "Queen";
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
        // HasMoved is stored to check if the player can castle
        bool HasMoved { get; set; }
        public Rook(bool isWhite, bool hasMoved) : base(isWhite)
        {
            PieceType = "Rook";
            HasMoved = hasMoved;
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
            PieceType = "Bishop";
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
            PieceType = "Knight";
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
            PieceType = "Pawn";
            CanBeTakenByEnPassant = false;
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
