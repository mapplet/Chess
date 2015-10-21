using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Bishop
    {
        List<Tuple<int, int>> valid_destinations = new List<Tuple<int, int>>();
        Gameboard gameboard;
        Piece current_piece;
        Piece next_piece;
        int max = 7;
        int min = 0;

        public List<Tuple<int, int>> Rules(Piece current_piece, Gameboard gameboard)
        {
            this.gameboard = gameboard;
            this.current_piece = current_piece;

            if (current_piece.column != min && current_piece.row != min) // NOT max up or left
                up_left_function();
            if (current_piece.column != max && current_piece.row != min) // NOT max or right
                up_right_function();
            if (current_piece.column != min && current_piece.row != max) // NOT max down or left
                down_left_function();
            if (current_piece.column != max && current_piece.row != max) // NOT max down or right
                down_right_function();

            return valid_destinations;
        }

        private void up_right_function()
        {
            next_piece = gameboard.getPiece(current_piece.row - 1, current_piece.column + 1);
            while (next_piece.team != current_piece.team)
            {
                valid_destinations.Add(new Tuple<int, int>(next_piece.row, next_piece.column));


                if (next_piece.row - 1 < min || next_piece.column + 1 > max || next_piece.team != (int)team.none)
                    break;
                next_piece = gameboard.getPiece(next_piece.row - 1, next_piece.column + 1);
            }
        }

        private void up_left_function()
        {
            next_piece = gameboard.getPiece(current_piece.row - 1, current_piece.column - 1);
            while (next_piece.team != current_piece.team)
            {
                valid_destinations.Add(new Tuple<int, int>(next_piece.row, next_piece.column));


                if (next_piece.row - 1 < min || next_piece.column - 1 < min || next_piece.team != (int)team.none)
                    break;
                next_piece = gameboard.getPiece(next_piece.row - 1, next_piece.column - 1);
            }
        }

        private void down_right_function()
        {
            next_piece = gameboard.getPiece(current_piece.row + 1, current_piece.column + 1);
            while (next_piece.team != current_piece.team)
            {
                valid_destinations.Add(new Tuple<int, int>(next_piece.row, next_piece.column));


                if (next_piece.row + 1 > max || next_piece.column + 1 > max || next_piece.team != (int)team.none)
                    break;
                next_piece = gameboard.getPiece(next_piece.row + 1, next_piece.column + 1);
            }
        }

        private void down_left_function()
        {
            next_piece = gameboard.getPiece(current_piece.row + 1, current_piece.column - 1);
            while (next_piece.team != current_piece.team)
            {
                valid_destinations.Add(new Tuple<int, int>(next_piece.row, next_piece.column));


                if (next_piece.row + 1 > max || next_piece.column - 1 < min || next_piece.team != (int)team.none)
                    break;
                next_piece = gameboard.getPiece(next_piece.row + 1, next_piece.column - 1);
            }
        }
    }
}
