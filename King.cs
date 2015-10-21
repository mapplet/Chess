using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class King
    {
        private List<Tuple<int, int>> valid_destinations = new List<Tuple<int, int>>();
        Gameboard gameboard;
        private Piece current_piece;
        private Piece next_piece;
        private int min = 0;
        private int max = 7;

        public List<Tuple<int, int>> Rules(Piece current_piece, Gameboard gameboard)
        {
            this.gameboard = gameboard;
            this.current_piece = current_piece;

            if (current_piece.row != min && current_piece.column != min)
                up_left();
            if (current_piece.row != min && current_piece.column != max)
                up_right();
            if (current_piece.row != max && current_piece.column != min)
                down_left();
            if (current_piece.row != max && current_piece.column != max)
                down_right();
            if (current_piece.row != min)
                up();
            if (current_piece.row != max)
                down();
            if (current_piece.column != min)
                left();
            if (current_piece.column != max)
                right();


            return valid_destinations;
        }

        private void up()
        {
            next_piece = gameboard.getPiece(current_piece.row - 1, current_piece.column);
            if (next_piece.team != current_piece.team)
                valid_destinations.Add(new Tuple<int, int>(current_piece.row - 1, current_piece.column));
        }
        private void up_right()
        {
            next_piece = gameboard.getPiece(current_piece.row - 1, current_piece.column + 1);
            if (next_piece.team != current_piece.team)
                valid_destinations.Add(new Tuple<int, int>(current_piece.row - 1, current_piece.column + 1));
        }
        private void right()
        {
            next_piece = gameboard.getPiece(current_piece.row, current_piece.column + 1);
            if (next_piece.team != current_piece.team)
                valid_destinations.Add(new Tuple<int, int>(current_piece.row, current_piece.column + 1));
        }
        private void down_right()
        {
            next_piece = gameboard.getPiece(current_piece.row + 1, current_piece.column + 1);
            if (next_piece.team != current_piece.team)
                valid_destinations.Add(new Tuple<int, int>(current_piece.row + 1, current_piece.column + 1));
        }
        private void down()
        {
            next_piece = gameboard.getPiece(current_piece.row + 1, current_piece.column);
            if (next_piece.team != current_piece.team)
                valid_destinations.Add(new Tuple<int, int>(current_piece.row + 1, current_piece.column));
        }
        private void down_left()
        {
            next_piece = gameboard.getPiece(current_piece.row + 1, current_piece.column - 1);
            if (next_piece.team != current_piece.team)
                valid_destinations.Add(new Tuple<int, int>(current_piece.row + 1, current_piece.column - 1));
        }
        private void left()
        {
            next_piece = gameboard.getPiece(current_piece.row, current_piece.column - 1);
            if (next_piece.empty == true || next_piece.team != current_piece.team)
                valid_destinations.Add(new Tuple<int, int>(current_piece.row, current_piece.column - 1));
        }
        private void up_left()
        {
            next_piece = gameboard.getPiece(current_piece.row - 1, current_piece.column - 1);
            if (next_piece.empty == true || next_piece.team != current_piece.team)
            {
                valid_destinations.Add(new Tuple<int, int>(current_piece.row - 1, current_piece.column - 1));
            }
        }
    }
}
