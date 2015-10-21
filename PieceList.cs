using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class PieceList
    {
        /*
        public PieceList() { }

        public PieceList(int _team)
        {
            if (_team == (int)team.black)
            {
                list.Add(new Piece(false, (int)team.black, (int)type.rock, 0, 0, global::chess_new.Properties.Resources.Tower1_black));
                list.Add(new Piece(false, (int)team.black, (int)type.knight, 0, 1, global::chess_new.Properties.Resources.horse_black));
                list.Add(new Piece(false, (int)team.black, (int)type.bishop, 0, 2, global::chess_new.Properties.Resources.sprinter_black));
                list.Add(new Piece(false, (int)team.black, (int)type.queen, 0, 3, global::chess_new.Properties.Resources.queen_black));
                list.Add(new Piece(false, (int)team.black, (int)type.king, 0, 4, global::chess_new.Properties.Resources.king_black));
                list.Add(new Piece(false, (int)team.black, (int)type.bishop, 0, 5, global::chess_new.Properties.Resources.sprinter_black));
                list.Add(new Piece(false, (int)team.black, (int)type.knight, 0, 6, global::chess_new.Properties.Resources.horse_black));
                list.Add(new Piece(false, (int)team.black, (int)type.rock, 0, 7, global::chess_new.Properties.Resources.Tower1_black));

                list.Add(new Piece(false, (int)team.black, (int)type.pawn, 1, 0, global::chess_new.Properties.Resources.farmer_black));
                list.Add(new Piece(false, (int)team.black, (int)type.pawn, 1, 1, global::chess_new.Properties.Resources.farmer_black));
                list.Add(new Piece(false, (int)team.black, (int)type.pawn, 1, 2, global::chess_new.Properties.Resources.farmer_black));
                list.Add(new Piece(false, (int)team.black, (int)type.pawn, 1, 3, global::chess_new.Properties.Resources.farmer_black));
                list.Add(new Piece(false, (int)team.black, (int)type.pawn, 1, 4, global::chess_new.Properties.Resources.farmer_black));
                list.Add(new Piece(false, (int)team.black, (int)type.pawn, 1, 5, global::chess_new.Properties.Resources.farmer_black));
                list.Add(new Piece(false, (int)team.black, (int)type.pawn, 1, 6, global::chess_new.Properties.Resources.farmer_black));
                list.Add(new Piece(false, (int)team.black, (int)type.pawn, 1, 7, global::chess_new.Properties.Resources.farmer_black));
            }
            
            else if (_team == (int)team.white)
            {
                list.Add(new Piece(false, (int)team.white, (int)type.rock, 7, 0, global::chess_new.Properties.Resources.Tower1_white));
                list.Add(new Piece(false, (int)team.white, (int)type.knight, 7, 1, global::chess_new.Properties.Resources.horse_white));
                list.Add(new Piece(false, (int)team.white, (int)type.bishop, 7, 2, global::chess_new.Properties.Resources.sprinter_white));
                list.Add(new Piece(false, (int)team.white, (int)type.queen, 7, 3, global::chess_new.Properties.Resources.queen_white));
                list.Add(new Piece(false, (int)team.white, (int)type.king, 7, 4, global::chess_new.Properties.Resources.king_white));
                list.Add(new Piece(false, (int)team.white, (int)type.bishop, 7, 5, global::chess_new.Properties.Resources.sprinter_white));
                list.Add(new Piece(false, (int)team.white, (int)type.knight, 7, 6, global::chess_new.Properties.Resources.horse_white));
                list.Add(new Piece(false, (int)team.white, (int)type.rock, 7, 7, global::chess_new.Properties.Resources.Tower1_white));

                list.Add(new Piece(false, (int)team.white, (int)type.pawn, 6, 0, global::chess_new.Properties.Resources.farmer_white));
                list.Add(new Piece(false, (int)team.white, (int)type.pawn, 6, 1, global::chess_new.Properties.Resources.farmer_white));
                list.Add(new Piece(false, (int)team.white, (int)type.pawn, 6, 2, global::chess_new.Properties.Resources.farmer_white));
                list.Add(new Piece(false, (int)team.white, (int)type.pawn, 6, 3, global::chess_new.Properties.Resources.farmer_white));
                list.Add(new Piece(false, (int)team.white, (int)type.pawn, 6, 4, global::chess_new.Properties.Resources.farmer_white));
                list.Add(new Piece(false, (int)team.white, (int)type.pawn, 6, 5, global::chess_new.Properties.Resources.farmer_white));
                list.Add(new Piece(false, (int)team.white, (int)type.pawn, 6, 6, global::chess_new.Properties.Resources.farmer_white));
                list.Add(new Piece(false, (int)team.white, (int)type.pawn, 6, 7, global::chess_new.Properties.Resources.farmer_white));
            }

                this.id = _team;
        }

        public void killPiece(Piece piece)
        {
            for (int i = 0; i < list.Count(); ++i)
            {
                if (list[i] == piece)
                {
                    list.RemoveAt(i);
                    break;
                }
            }
        }

        public int Count()
        {
            return list.Count();
        }

        public Piece At(int i)
        {
            return list[i];
        }

        public PieceList getCopy()
        {
            PieceList copy = new PieceList();
            foreach (Piece originalPiece in this.list)
            {
                copy.list.Add(new Piece(originalPiece));
            }

            copy.id = this.id;
            return copy;
        }
        
        public int getId()
        {
            return this.id;
        }

        public List<Piece> getList()
        {
            return this.list;
        }

        public Tuple<int, int> findKing()
        {
            int row=0, column=0;
            foreach (Piece piece in this.list)
            {
                if (piece.type == (int)type.king)
                    row = piece.row; column = piece.column; break;
            }
            return new Tuple<int, int>(row, column);
        }


        private List<Piece> list = new List<Piece>();
        private int id;
        */
    }
}
