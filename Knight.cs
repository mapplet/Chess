using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Knight
    {
        private List<Tuple<int, int>> valid_destinations = new List<Tuple<int, int>>();
        private Gameboard gameboard;
        private Piece current_piece;
        private Piece next_piece;
        private int max = 7;
        private int min = 0;

        public List<Tuple<int, int>> Rules(Piece current_piece, Gameboard gameboard)
        {
            this.gameboard = gameboard;
            this.current_piece = current_piece;

            if (current_piece.column > min && current_piece.row > min + 1)
            {
                next_piece = gameboard.getPiece(current_piece.row - 2, current_piece.column - 1);
                if (next_piece.empty == true || next_piece.team != current_piece.team)
                    valid_destinations.Add(new Tuple<int, int>(current_piece.row - 2, current_piece.column - 1));   // up left
            }
            if (current_piece.column < max && current_piece.row > min + 1)
            {
                Piece next_piece = gameboard.getPiece(current_piece.row - 2, current_piece.column + 1);
                if (next_piece.empty == true || next_piece.team != current_piece.team)
                    valid_destinations.Add(new Tuple<int, int>(current_piece.row -2, current_piece.column + 1)); // up right
            }
            if (current_piece.column < max - 1 && current_piece.row > min)
            {
                Piece next_piece = gameboard.getPiece(current_piece.row - 1, current_piece.column + 2);
                if (next_piece.empty == true || next_piece.team != current_piece.team)
                    valid_destinations.Add(new Tuple<int, int>(current_piece.row - 1, current_piece.column + 2)); // right up
            }
            if (current_piece.column < max - 1 && current_piece.row < max)
            {
                Piece next_piece = gameboard.getPiece(current_piece.row + 1, current_piece.column + 2);
                if (next_piece.empty == true || next_piece.team != current_piece.team)
                    valid_destinations.Add(new Tuple<int, int>(current_piece.row + 1, current_piece.column + 2));  // right down
            }
            if (current_piece.column < max && current_piece.row < max - 1)
            {
                Piece next_piece = gameboard.getPiece(current_piece.row + 2, current_piece.column + 1);
                if (next_piece.empty == true || next_piece.team != current_piece.team)
                    valid_destinations.Add(new Tuple<int, int>(current_piece.row + 2, current_piece.column + 1));  // down right
            }
            if (current_piece.column > min && current_piece.row < max - 1)
            {
                Piece next_piece = gameboard.getPiece(current_piece.row + 2, current_piece.column - 1);
                if (next_piece.empty == true || next_piece.team != current_piece.team)
                    valid_destinations.Add(new Tuple<int, int>(current_piece.row + 2, current_piece.column - 1));  // down left
            }
            if (current_piece.column > min + 1 && current_piece.row < max)
            {
                Piece next_piece = gameboard.getPiece(current_piece.row + 1, current_piece.column - 2);
                if (next_piece.empty == true || next_piece.team != current_piece.team)
                    valid_destinations.Add(new Tuple<int, int>(current_piece.row + 1, current_piece.column - 2));  // left down
            }
            if (current_piece.column > min + 1 && current_piece.row > min)
            {
                Piece next_piece = gameboard.getPiece(current_piece.row - 1, current_piece.column - 2);
                if (next_piece.empty == true || next_piece.team != current_piece.team)
                    valid_destinations.Add(new Tuple<int, int>(current_piece.row - 1, current_piece.column - 2)); // left up
            }
            return valid_destinations;
        }
    }
}
