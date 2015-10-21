using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chess
{
    [XmlRoot("State")]
    [Serializable]
    public class State
    {
        public State() {}
        public State(int myTeam)
        {
            this.myTeam = myTeam;
            this.opponentTeam = 3 - myTeam;
        }

        public void updateState(int myTeam)
        {
            this.myTeam = myTeam;
            this.opponentTeam = 3 - myTeam;
        }

        public int getOpponentTeam()
        {
            return this.opponentTeam;
        }

        public int getMyTeam()
        {
            return this.myTeam;
        }

        public void Print(Gameboard gameboard)
        {
            Console.WriteLine("############## CURRENT GAMEBOARD ##############");
            for (int row = 0; row < 8; ++row)
            {
                for (int column = 0; column < 8; ++column)
                {
                    Console.Write("[{0},{1}] ", gameboard.getPiece(row, column).team, gameboard.getPiece(row, column).type);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("###############################################");
        }

        public int myTeam;
        private int opponentTeam;
    }
}
