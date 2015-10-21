using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Rock
    {
        private List<Tuple<int, int>> valid_destinations = new List<Tuple<int, int>>();
        private Piece current_piece;
        private Gameboard gameboard;
        private Piece next_piece;
        private int max = 7;
        private int min = 0;

        public List<Tuple<int,int>> Rules(Piece current_piece, Gameboard gameboard)
        {
            this.current_piece = current_piece;
            this.gameboard = gameboard;

            if (current_piece.column != min)
                check_left_function();
            if (current_piece.column != max)
                check_right_function();
            if (current_piece.row != min)
                check_up_function();
            if (current_piece.row != max)
                check_down_function();

            return valid_destinations;
        }

        private void check_right_function()
        {
            // Console.WriteLine("inne i check right");
            next_piece = gameboard.getPiece(current_piece.row, current_piece.column + 1);
            while (next_piece.team != current_piece.team)
            {
                valid_destinations.Add(new Tuple<int, int>(next_piece.row, next_piece.column));


                if (next_piece.column + 1 > max || next_piece.team != (int)team.none)
                    break;
                next_piece = gameboard.getPiece(next_piece.row, next_piece.column + 1);
            }
        }

        private void check_left_function()
        {
            // Console.WriteLine("inne i check left");
            next_piece = gameboard.getPiece(current_piece.row, current_piece.column - 1);
            while (next_piece.team != current_piece.team)
            {
                valid_destinations.Add(new Tuple<int, int>(next_piece.row, next_piece.column));

                if (next_piece.column - 1 < min || next_piece.team != (int)team.none)
                    break;
                next_piece = gameboard.getPiece(next_piece.row, next_piece.column - 1);

            }


        }

        private void check_down_function()
        {
            // Console.WriteLine("inne i check down");
            next_piece = gameboard.getPiece(current_piece.row + 1, current_piece.column);
            while (next_piece.team != current_piece.team)
            {
                valid_destinations.Add(new Tuple<int, int>(next_piece.row, next_piece.column));
                //Console.WriteLine("Added: {0},{1} as valid coordinate.", next_piece.row, next_piece.column);

                if (next_piece.row + 1 > max || next_piece.team != (int)team.none)
                    break;
                next_piece = gameboard.getPiece(next_piece.row + 1, next_piece.column);
            }
        }

        private void check_up_function()
        {
            // Console.WriteLine("inne i check up");
            next_piece = gameboard.getPiece(current_piece.row - 1, current_piece.column);
            while (next_piece.team != current_piece.team)
            {
                valid_destinations.Add(new Tuple<int, int>(next_piece.row, next_piece.column));
                //Console.WriteLine("Added: {0},{1} as valid coordinate.", next_piece.row, next_piece.column);

                if (next_piece.row - 1 < min || next_piece.team != (int)team.none)
                    break;
                next_piece = gameboard.getPiece(next_piece.row - 1, next_piece.column);
            }
        }
    }
}
