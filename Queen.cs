using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Queen
    {
        private Bishop get_diagonal = new Bishop();
        private Rock get_straight = new Rock();
        private List<Tuple<int, int>> moves_dia = new List<Tuple<int, int>>();
        private List<Tuple<int, int>> moves_str = new List<Tuple<int, int>>();

        public List<Tuple<int, int>> Rules(Piece currentPiece, Gameboard gameboard)
        {
            this.moves_dia = get_diagonal.Rules(currentPiece, gameboard);
            this.moves_str = get_straight.Rules(currentPiece, gameboard);

            moves_str = moves_str.Concat(moves_dia).ToList();
            
            return moves_str;
        }
    }
}
