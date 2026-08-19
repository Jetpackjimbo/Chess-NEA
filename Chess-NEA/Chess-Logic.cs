using System;
using System.Collections.Generic;
using System.Text;

namespace Chess_NEA
{
    class Square
    {
        public char File { get; }
        int Rank { get; }
        public Square(char file, int rank)
        {
            this.File = file;
            this.Rank = rank;
        }
    }

    class ChessBoard
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
    }
}
