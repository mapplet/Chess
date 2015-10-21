using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Rulebook
    {
        private List<Tuple<int, int>> validDestinations = new List<Tuple<int, int>>();
        private Piece copyOfCurrentPiece;
        private Gameboard gameboard;
        private GameComponents info = new GameComponents();

        public List<Tuple<int, int>> getValidMoves(Piece currentPiece, Gameboard gameboard, bool doRecurse = true)
        {
            this.copyOfCurrentPiece = new Piece(currentPiece);
            this.gameboard = new Gameboard(gameboard);

            switch (currentPiece.type)
            {
                case (int)type.rock: // tower
                    Rock rock = new Rock();
                    this.validDestinations = rock.Rules(currentPiece, gameboard);
                    if (doRecurse)
                        cleanUp();
                    return validDestinations;
                case (int)type.knight: // horsie
                    Knight knight = new Knight();
                    this.validDestinations = knight.Rules(currentPiece, gameboard);
                    if (doRecurse)
                        cleanUp();
                    return validDestinations;
                case (int)type.bishop: // springare
                    Bishop bishop = new Bishop();
                    this.validDestinations = bishop.Rules(currentPiece, gameboard);
                    if (doRecurse)
                        cleanUp();
                    return validDestinations;
                case (int)type.queen: //los quuenos
                    Queen queen = new Queen();
                    this.validDestinations = queen.Rules(currentPiece, gameboard);
                    if (doRecurse)
                        cleanUp();
                    return validDestinations;
                case (int)type.king: // los kingos
                    King king = new King();
                    this.validDestinations = king.Rules(currentPiece, gameboard);
                    if (doRecurse)
                        cleanUp();
                    return validDestinations;
                case (int)type.pawn: // los farmeros
                    Pawn pawn = new Pawn();
                    this.validDestinations = pawn.Rules(currentPiece, gameboard);
                    if (doRecurse)
                        cleanUp();
                    return validDestinations;
            }
            return new List<Tuple<int,int>>();
        }

        private void cleanUp()
        {
            Gameboard copyOfGameboard = new Gameboard(gameboard);
            Rulebook rulebook = new Rulebook();
            List<Tuple<int, int>> copyOfValids = new List<Tuple<int, int>>();
            bool foundIllegalMove = false;

            Piece originalCurrentPiece;

            int currentTeam = copyOfCurrentPiece.team;
            int opponentTeam = info.getOpponent(currentTeam);

            foreach (Tuple<int, int> validMove in validDestinations) // För varje möjligt drag för markerad Piece
            {
                copyOfGameboard = new Gameboard(gameboard);
                originalCurrentPiece = copyOfGameboard.getPiece(copyOfCurrentPiece.row, copyOfCurrentPiece.column);
                Piece destination = copyOfGameboard.getPiece(validMove.Item1, validMove.Item2);

                copyOfGameboard.Move(originalCurrentPiece, destination); // Gör temporärt move
                foundIllegalMove = false;

                    foreach (Piece opponent in copyOfGameboard.getTeam(opponentTeam)) // För varje Piece i team OPPONENT
                    {
                        List<Tuple<int,int>> validDest = rulebook.getValidMoves(opponent, copyOfGameboard, false); // Hämta valid moves för Piece i OPPONENT (Utan hänsyn till möjliga drag som sätter kungen i schack..)
                        foreach (Tuple<int,int> coordinate in validDest) // För varje möjligt drag för Piece i OPPONENT
                        {
                            if (copyOfGameboard.getPiece(coordinate.Item1, coordinate.Item2).type == (int)type.king) // Om Piece i OPPONENT kan ta kungen i CURRENT
                            {
                                foundIllegalMove = true;
                                break;
                            }
                            if (foundIllegalMove)
                                break;
                        }
                        if (foundIllegalMove)
                            break;
                    }

                if (!foundIllegalMove)
                    copyOfValids.Add(new Tuple<int, int>(validMove.Item1, validMove.Item2));

                copyOfGameboard.setPiece(originalCurrentPiece);
                copyOfGameboard.setPiece(destination);
            }

            validDestinations = copyOfValids;
        }
    }

}
