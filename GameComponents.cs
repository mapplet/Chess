using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Chess
{
    enum team { none, black, white };
    enum type { none, rock, knight, bishop, queen, king, pawn };
    enum moveResult { none, chess, chessMate };

    public class GameComponents
    {
        static public string saveLocation = "SavedGame.xml";
        static public Image[,] images = new Image[3, 7] {{null, null, null, null, null, null, null}, // Riktig fullösning..
                                                {null, global::chess_new.Properties.Resources.Tower1_black,
                                                global::chess_new.Properties.Resources.horse_black,
                                                global::chess_new.Properties.Resources.sprinter_black,
                                                global::chess_new.Properties.Resources.queen_black,
                                                global::chess_new.Properties.Resources.king_black,
                                                global::chess_new.Properties.Resources.farmer_black}, {
                                                null, global::chess_new.Properties.Resources.Tower1_white,
                                                global::chess_new.Properties.Resources.horse_white,
                                                global::chess_new.Properties.Resources.sprinter_white,
                                                global::chess_new.Properties.Resources.queen_white,
                                                global::chess_new.Properties.Resources.king_white,
                                                global::chess_new.Properties.Resources.farmer_white}};

        public int getOpponent(int _team)
        {
            if (_team == (int)team.black)
                return (int)team.white;
            else return (int)team.black;
        }
    }
}
