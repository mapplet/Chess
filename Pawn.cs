using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Pawn
    {
        public List<Tuple<int, int>> Rules(Piece piece, Gameboard gameboard)    
        {
            if (piece.row == 0 || piece.row == 7)
                return new List<Tuple<int, int>>();

            int max_row;
            int moving_factor;
            int step = 1;
            List<Tuple<int, int>> valid_destinations = new List<Tuple<int, int>>();
            if (piece.team == (int)team.black)
            {
                max_row = 7;
                moving_factor = 1;
                if (piece.row == 1)
                    ++step;
            }
            else
            {
                max_row = 0;
                moving_factor = -1;
                if (piece.row == 6)
                    ++step;
            }

            for (int i = 1; i < step + 1; i++)
            {
                if (gameboard.getPiece(piece.row + (i * moving_factor), piece.column).empty && piece.row != max_row)
                {
                    valid_destinations.Add(new Tuple<int, int>(piece.row + (i * moving_factor), piece.column));
                }
                if (i == 1 && piece.column != 0 && gameboard.getPiece(piece.row + (i * moving_factor), piece.column - 1).team != piece.team && !gameboard.getPiece(piece.row + (i * moving_factor), piece.column - 1).empty)
                {
                    valid_destinations.Add(new Tuple<int, int>(piece.row + (i * moving_factor), piece.column-1));
                }
                if (i == 1 && piece.column != 7 && gameboard.getPiece(piece.row + (i * moving_factor), piece.column + 1).team != piece.team && !gameboard.getPiece(piece.row + (i * moving_factor), piece.column + 1).empty)
                {
                    valid_destinations.Add(new Tuple<int, int>(piece.row + (i * moving_factor), piece.column+1));
                }
            }

            return valid_destinations;
        }
    }
}
