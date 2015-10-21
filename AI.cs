using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class AI
    {
        private int thisTeam;
        private Dictionary<int, int> rewards = new Dictionary<int, int>();

        public AI(int team)
        {
            this.thisTeam = team;
            initializeRewards();
        }

        public void initializeRewards()
        {
            rewards.Add((int)type.none, 0);
            rewards.Add((int)type.pawn, 1);
            rewards.Add((int)type.knight, 2);
            rewards.Add((int)type.bishop, 3);
            rewards.Add((int)type.rock, 4);
            rewards.Add((int)type.queen, 5);
            rewards.Add((int)type.king, 6);
        }

        public void Move(Gameboard gameboard, State currentState)
        {
            Rulebook rulebook = new Rulebook();
            List<Tuple<Piece, Piece>> topValidMoves = new List<Tuple<Piece, Piece>>();
            foreach(Piece piece in gameboard.getTeam(thisTeam))
            {
                List<Tuple<int, int>> validDestinations = rulebook.getValidMoves(piece, gameboard);
                foreach (Tuple<int,int> coordinate in validDestinations)
                {
                    if (validDestinations.Count() == 0)
                        break;
                    else if (topValidMoves.Count() == 0 || rewards[topValidMoves[0].Item2.type] == rewards[gameboard.getPiece(coordinate.Item1, coordinate.Item2).type])
                        topValidMoves.Add(new Tuple<Piece, Piece>(piece, gameboard.getPiece(coordinate.Item1, coordinate.Item2)));
                    else if (rewards[topValidMoves[0].Item2.type] < rewards[gameboard.getPiece(coordinate.Item1, coordinate.Item2).type])
                    {
                        topValidMoves.Clear();
                        topValidMoves.Add(new Tuple<Piece, Piece>(piece, gameboard.getPiece(coordinate.Item1, coordinate.Item2)));
                    }
                }

            }
            if (topValidMoves.Count() != 0)
            {
                Random rand = new Random();
                int index = rand.Next(topValidMoves.Count());
                gameboard.Move(topValidMoves[index].Item1, topValidMoves[index].Item2);

                if (topValidMoves[index].Item1.type == (int)type.pawn && (topValidMoves[index].Item1.row == 0 || topValidMoves[index].Item1.row == 7))
                {
                    Piece bestPieceFromDead = new Piece();
                    foreach (Piece deadPiece in gameboard.getDead(thisTeam))
                    {
                        if (rewards[deadPiece.type] > rewards[bestPieceFromDead.type])
                            bestPieceFromDead = deadPiece;
                    }
                    gameboard.tradePawn(topValidMoves[index].Item1, bestPieceFromDead);
                }

                //gameboard.checkChessMate(currentState.getWhosTurn());
            }
            //currentState.swapTurn();
        }
    }
}
