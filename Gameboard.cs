using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Chess
{
    [XmlRoot("Gameboard")]
    [Serializable]
    public class Gameboard : INotifyPropertyChanged
    {
        private List<List<Piece>> gameboard = new List<List<Piece>>();
        public List<Piece> blackTeam = new List<Piece>();
        public List<Piece> whiteTeam = new List<Piece>();
        public List<Piece> blackDead = new List<Piece>();
        public List<Piece> whiteDead = new List<Piece>();
        private GameComponents info = new GameComponents();
        private Rulebook rulebook = new Rulebook();
        public event PropertyChangedEventHandler propertyChanged;

        private void notifyGameboardChanged([CallerMemberName] String propertyName = "")
        {
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Creates a gameboard with pieces in init-position
        public Gameboard()
        {}

        public void Clear()
        {
            this.blackTeam.Clear();
            this.whiteTeam.Clear();
            this.blackDead.Clear();
            this.whiteDead.Clear();
            this.gameboard.Clear();
            for (int row = 0; row < 8; ++row)
            {
                List<Piece> columnWithEmptyObjects = new List<Piece>();
                this.gameboard.Add(columnWithEmptyObjects);
                for (int column = 0; column < 8; ++column)
                {
                    this.gameboard[row].Add(new Piece(row, column));
                }
            }
        }

        public void initialize()
        {
            this.blackTeam.Clear();
            this.whiteTeam.Clear();
            this.blackDead.Clear();
            this.whiteDead.Clear();

            this.blackTeam.Add(new Piece(false, (int)team.black, (int)type.rock, 0, 0));
            this.blackTeam.Add(new Piece(false, (int)team.black, (int)type.knight, 0, 1));
            this.blackTeam.Add(new Piece(false, (int)team.black, (int)type.bishop, 0, 2));
            this.blackTeam.Add(new Piece(false, (int)team.black, (int)type.queen, 0, 3));
            this.blackTeam.Add(new Piece(false, (int)team.black, (int)type.king, 0, 4));
            this.blackTeam.Add(new Piece(false, (int)team.black, (int)type.bishop, 0, 5));
            this.blackTeam.Add(new Piece(false, (int)team.black, (int)type.knight, 0, 6));
            this.blackTeam.Add(new Piece(false, (int)team.black, (int)type.rock, 0, 7));

            this.blackTeam.Add(new Piece(false, (int)team.black, (int)type.pawn, 1, 0));
            this.blackTeam.Add(new Piece(false, (int)team.black, (int)type.pawn, 1, 1));
            this.blackTeam.Add(new Piece(false, (int)team.black, (int)type.pawn, 1, 2));
            this.blackTeam.Add(new Piece(false, (int)team.black, (int)type.pawn, 1, 3));
            this.blackTeam.Add(new Piece(false, (int)team.black, (int)type.pawn, 1, 4));
            this.blackTeam.Add(new Piece(false, (int)team.black, (int)type.pawn, 1, 5));
            this.blackTeam.Add(new Piece(false, (int)team.black, (int)type.pawn, 1, 6));
            this.blackTeam.Add(new Piece(false, (int)team.black, (int)type.pawn, 1, 7));

            this.whiteTeam.Add(new Piece(false, (int)team.white, (int)type.rock, 7, 0));
            this.whiteTeam.Add(new Piece(false, (int)team.white, (int)type.knight, 7, 1));
            this.whiteTeam.Add(new Piece(false, (int)team.white, (int)type.bishop, 7, 2));
            this.whiteTeam.Add(new Piece(false, (int)team.white, (int)type.queen, 7, 3));
            this.whiteTeam.Add(new Piece(false, (int)team.white, (int)type.king, 7, 4));
            this.whiteTeam.Add(new Piece(false, (int)team.white, (int)type.bishop, 7, 5));
            this.whiteTeam.Add(new Piece(false, (int)team.white, (int)type.knight, 7, 6));
            this.whiteTeam.Add(new Piece(false, (int)team.white, (int)type.rock, 7, 7));

            this.whiteTeam.Add(new Piece(false, (int)team.white, (int)type.pawn, 6, 0));
            this.whiteTeam.Add(new Piece(false, (int)team.white, (int)type.pawn, 6, 1));
            this.whiteTeam.Add(new Piece(false, (int)team.white, (int)type.pawn, 6, 2));
            this.whiteTeam.Add(new Piece(false, (int)team.white, (int)type.pawn, 6, 3));
            this.whiteTeam.Add(new Piece(false, (int)team.white, (int)type.pawn, 6, 4));
            this.whiteTeam.Add(new Piece(false, (int)team.white, (int)type.pawn, 6, 5));
            this.whiteTeam.Add(new Piece(false, (int)team.white, (int)type.pawn, 6, 6));
            this.whiteTeam.Add(new Piece(false, (int)team.white, (int)type.pawn, 6, 7));

            updateBoard();
        }

        // Adds pieces from existing blackTeam and whiteTeam to gameboard
        public void updateBoard()
        {
            gameboard.Clear();
            for (int row = 0; row < 8; ++row)
            {
                List<Piece> columnWithEmptyObjects = new List<Piece>();
                this.gameboard.Add(columnWithEmptyObjects);
                for (int column = 0; column < 8; ++column)
                {
                    this.gameboard[row].Add(new Piece(row, column));
                }
            }

            foreach (Piece piece in this.blackTeam)
                this.gameboard[piece.row][piece.column] = piece;

            foreach (Piece piece in this.whiteTeam)
                this.gameboard[piece.row][piece.column] = piece;
        }

        public Gameboard(Gameboard originalGameboard)
        {
            for (int row = 0; row<8; ++row)
            {
                List<Piece> copyOfRow = new List<Piece>();
                foreach (Piece piece in originalGameboard.getRow(row))
                {
                    Piece copyOfPiece = new Piece(piece);
                    copyOfRow.Add(copyOfPiece);
                    if (copyOfPiece.team == (int)team.black)
                        this.blackTeam.Add(copyOfPiece);
                    else if (copyOfPiece.team == (int)team.white)
                        this.whiteTeam.Add(copyOfPiece);
                }
                this.gameboard.Add(copyOfRow);
            }
        }

        public void Move(Piece origin, Piece destination)
        {
            int originRow = origin.row;
            int originColumn = origin.column;
            int destinationRow = destination.row;
            int destinationColumn = destination.column;

            if (destination.team == (int)team.black)
            {
                this.blackDead.Add(destination);
                this.blackTeam.Remove(destination);
            }
            else if (destination.team == (int)team.white)
            {
                this.whiteDead.Add(destination);
                this.whiteTeam.Remove(destination);
            }

            this.gameboard[destinationRow][destinationColumn] = origin;
            origin.row = destinationRow; origin.column = destinationColumn;

            this.gameboard[originRow][originColumn] = new Piece(originRow, originColumn);

            notifyGameboardChanged();
        }

        public void setPiece(Piece piece)
        {
            this.gameboard[piece.row][piece.column] = new Piece(piece);
        }

        public Piece getPiece(int row, int column)
        {
            if (row < 0 || row > 7 || column < 0 || column > 7)
                Console.WriteLine("# ERROR: {0},{1}", row, column);
            return gameboard[row][column];
        }

        public List<Piece> getTeam(int _team)
        {
            if (_team == (int)team.black)
                return this.blackTeam;
            else if (_team == (int)team.white)
                return this.whiteTeam;
            else return null;
        }

        public List<Piece> getDead(int _team)
        {
            if (_team == (int)team.black)
                return this.blackDead;
            else if (_team == (int)team.white)
                return this.whiteDead;
            else return null;
        }

        public bool tradePawn(Piece pawn, Piece deadPiece)
        {
            if (pawn.team == (int)team.black && this.blackDead.Contains(deadPiece) || pawn.team == (int)team.white && this.whiteDead.Contains(deadPiece))
            {
                Piece copyOfPawn = new Piece(pawn);
                pawn.inherit(deadPiece);
                deadPiece.inherit(copyOfPawn);
                notifyGameboardChanged();

                return true;
            }
            return false;
        }

        public List<Piece> getRow(int row)
        {
            return this.gameboard[row];
        }

        //public DialogResult checkChessMate(int _team, bool checkChess = true)
        public int checkChessMate(int _team, bool checkChess = true)
        {
            int currentTeam = _team;
            int opponentTeam = info.getOpponent(currentTeam);
            
            // Check for chessmate
            bool validMovesExist = false;
            foreach (Piece opponentPiece in this.getTeam(opponentTeam))
            {
                if (rulebook.getValidMoves(opponentPiece, this).Count() > 0)
                {
                    validMovesExist = true;
                    break;
                }
            }

            if (!validMovesExist)
            {
                Console.WriteLine("# CHESSMATE!");
                return (int)moveResult.chessMate;
                /*
                string teamString = "Black";
                if (currentTeam == (int)team.white)
                    teamString = "White";
                return MessageBox.Show("Chessmate! "+teamString+" team wins.\n\nPlay again?", "Chess - Message", MessageBoxButtons.YesNo);
                */
            }

            else if (checkChess)
            {
                // Check for chess
                foreach (Piece piece in this.getTeam(currentTeam)) // For every PIECE in CURRENT team..
                {
                    List<Tuple<int, int>> validMovesOfOpponent = rulebook.getValidMoves(piece, this); // Get valid moves for PIECE
                    foreach (Tuple<int, int> coordinate in validMovesOfOpponent) // For every possible move of CURRENT..
                    {
                        if (this.getPiece(coordinate.Item1, coordinate.Item2).type == (int)type.king) // If PIECE is able to kill the King
                        {
                            Console.WriteLine("# CHESS!");
                            return (int)moveResult.chess;
                            //return MessageBox.Show("Chess!", "Chess - Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }

            return (int)moveResult.none;
            //return DialogResult.None;
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }
    }
}
