using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

//using System.Threading;

namespace Chess
{
    public partial class game : Form
    {
        // Globals
        private Gameboard gameboard;
        private Rulebook rulebook = new Rulebook();
        private GameComponents info = new GameComponents();
        private Piece previousPiece = null;
        private State currentState;
        private List<Tuple<int, int>> validDestinations = new List<Tuple<int, int>>();
        private AI AIOpponent;
        private bool pawnChangable = false;
        private Storage storage = new Storage();
        delegate void updateBoardCallback();
        delegate void initializeGameCallback(int myTeam);

        public game(int myTeamIn)
        {
            Console.WriteLine("\n# Game Thread ID: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            InitializeComponent();
            initializeGame(myTeamIn);
        }

        private void initializeGame(int myTeam)
        {
            Console.WriteLine("# Init: Thread ID: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            // Resume game?
            if (myTeam == (int)team.none)
            {
                Console.WriteLine("# Resuming Game.");
                Tuple<Gameboard, State> loadedGame = storage.LoadGame();
                gameboard = loadedGame.Item1;
                currentState = loadedGame.Item2;
                gameboard.updateBoard();
                currentState.updateState(currentState.getMyTeam());
            }

            // Else initialize default
            else
            {
                Console.WriteLine("# Initializes new Game.");
                gameboard = new Gameboard();
                gameboard.initialize();
                currentState = new State(myTeam);
                storage.manuallyInitialize(gameboard, currentState);
            }

            AIOpponent = new AI(currentState.getOpponentTeam());

            storage.getWatcher().SynchronizingObject = this;

            // Update GUI when database is changed
            storage.propertyChanged += new PropertyChangedEventHandler(OnDatabaseChanged);

            if (currentState.getOpponentTeam() == (int)team.white)
            {
                Console.WriteLine("# Computer will make first move.");
                AIOpponent.Move(gameboard, currentState);
            }

            updateBoard();
        }

        private void updateBoard()
        {
            Console.WriteLine("# Will update visual gameboard.");
            if (this.gameboardPanel.InvokeRequired)
            {
                updateBoardCallback d = new updateBoardCallback(updateBoard);
                this.Invoke(d);
                Console.WriteLine("# Invoked updateBoardCallback delegate.");
            }

            else
            {
                Console.WriteLine("# Refreshes tableLayoutPanels.");
                Piece temp;
                this.gameboardPanel.Refresh();
                for (int row = 0; row < 8; ++row)
                {
                    for (int column = 0; column < 8; ++column)
                    {
                        temp = gameboard.getPiece(row, column);
                        PictureBox p = (PictureBox)this.gameboardPanel.GetControlFromPosition(column, row);
                        p.Image = GameComponents.images[temp.team, temp.type];
                    }
                }

                this.blackTeamPanel.Refresh();
                if (gameboard.getDead((int)team.black).Count() == 0)
                {
                    if ((PictureBox)this.blackTeamPanel.GetControlFromPosition(0, 0) != null)
                    {
                        for (int row = 0; row < 8; ++row)
                        {
                            for (int column = 0; column < 2; ++column)
                            {
                                PictureBox p = (PictureBox)this.blackTeamPanel.GetControlFromPosition(column, row);
                                p.Image = null;
                            }
                        }
                    }
                }
                foreach (Piece piece in gameboard.getDead((int)team.black))
                {
                    double index = gameboard.getDead((int)team.black).IndexOf(piece);
                    int row = (int)Math.Floor(index / 2);
                    int column = (int)index % 2;
                    PictureBox p = (PictureBox)this.blackTeamPanel.GetControlFromPosition(column, row);
                    p.Image = GameComponents.images[piece.team, piece.type];
                }

                this.whiteTeamPanel.Refresh();
                if (gameboard.getDead((int)team.white).Count() == 0)
                {
                    if ((PictureBox)this.whiteTeamPanel.GetControlFromPosition(0, 0) != null)
                    {
                        for (int row = 0; row < 8; ++row)
                        {
                            for (int column = 0; column < 2; ++column)
                            {
                                PictureBox p = (PictureBox)this.whiteTeamPanel.GetControlFromPosition(column, row);
                                p.Image = null;
                            }
                        }
                    }
                }
                foreach (Piece piece in gameboard.getDead((int)team.white))
                {
                    double index = gameboard.getDead((int)team.white).IndexOf(piece);
                    int row = (int)Math.Floor(index / 2);
                    int column = (int)index % 2;
                    PictureBox p = (PictureBox)this.whiteTeamPanel.GetControlFromPosition(column, row);
                    p.Image = GameComponents.images[piece.team, piece.type];
                }
            }
        }

        private void gameboardPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            gameboardPanel.SuspendLayout();

            // Paint cells on gameboard
            if ((e.Column % 2) != 0 && (e.Row % 2) != 0)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                g.FillRectangle(Brushes.Coral, r);
            }

            else if ((e.Column % 2) == 0 && (e.Row % 2) == 0)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                g.FillRectangle(Brushes.Coral, r);
            }

            // Mark valid moves
            if (validDestinations.Count != 0)
            {
                for (int i = 0; i < validDestinations.Count(); i++)
                {
                    if (e.Row == validDestinations[i].Item1 && e.Column == validDestinations[i].Item2)
                    {
                        Rectangle r = e.CellBounds;
                        //Graphics g = e.Graphics;
                        r.Inflate(-1, -1);
                        //g.FillRectangle(Brushes.LightGray, r);
                        ControlPaint.DrawBorder(e.Graphics, r, Color.Black, 2, ButtonBorderStyle.Solid, Color.Black, 2, ButtonBorderStyle.Solid, Color.Black, 2, ButtonBorderStyle.Solid, Color.Black, 2, ButtonBorderStyle.Solid);
                    }

                }
            }
            gameboardPanel.ResumeLayout();
        }

        private void blackTeamPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (pawnChangable && currentState.getMyTeam() == (int)team.black)
            {
                blackTeamPanel.SuspendLayout();

                foreach (Piece piece in gameboard.getDead((int)team.black))
                {
                    double index = gameboard.getDead((int)team.black).IndexOf(piece);
                    int row = (int)Math.Floor(index / 2);
                    int column = (int)index % 2;

                    if (e.Row == row && e.Column == column)
                    {
                        Rectangle r = e.CellBounds;
                        r.Inflate(-1, -1);
                        ControlPaint.DrawBorder(e.Graphics, r, Color.Black, 2, ButtonBorderStyle.Solid, Color.Black, 2, ButtonBorderStyle.Solid, Color.Black, 2, ButtonBorderStyle.Solid, Color.Black, 2, ButtonBorderStyle.Solid);
                    }
                }

                blackTeamPanel.ResumeLayout();
            }
        }

        private void whiteTeamPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (pawnChangable && currentState.getMyTeam() == (int)team.white)
            {
                whiteTeamPanel.SuspendLayout();

                foreach (Piece piece in gameboard.getDead((int)team.white))
                {
                    double index = gameboard.getDead((int)team.white).IndexOf(piece);
                    int row = (int)Math.Floor(index / 2);
                    int column = (int)index % 2;

                    if (e.Row == row && e.Column == column)
                    {
                        Rectangle r = e.CellBounds;
                        r.Inflate(-1, -1);
                        ControlPaint.DrawBorder(e.Graphics, r, Color.Black, 2, ButtonBorderStyle.Solid, Color.Black, 2, ButtonBorderStyle.Solid, Color.Black, 2, ButtonBorderStyle.Solid, Color.Black, 2, ButtonBorderStyle.Solid);
                    }
                }

                whiteTeamPanel.ResumeLayout();
            }
        }

        private void gameboard_MouseClick(object sender, MouseEventArgs e)
        {
            Control c = (Control)sender;
            Piece currentPiece = gameboard.getPiece(this.gameboardPanel.GetRow(c), this.gameboardPanel.GetColumn(c));

            if (!pawnChangable)
            {
                // If this is the piece we want to move
                if (currentPiece.team == currentState.getMyTeam()
                    && (previousPiece == null || (!validDestinations.Contains(new Tuple<int, int>(currentPiece.row, currentPiece.column)))))// && currentPiece != previousPiece))
                {
                    if (currentPiece == previousPiece)
                    {
                        previousPiece = null;
                        validDestinations = new List<Tuple<int, int>>();
                    }
                    else
                    {
                        validDestinations = rulebook.getValidMoves(currentPiece, gameboard);
                        if (validDestinations.Count() == 0)
                        {
                            validDestinations = new List<Tuple<int, int>>();
                            previousPiece = null;
                            return;
                        }
                        else
                        {
                            previousPiece = currentPiece;
                        }
                    }
                }
                // If we have marked a previous piece and this is the destination
                else if (validDestinations.Contains(new Tuple<int, int>(currentPiece.row, currentPiece.column)))
                {
                    gameboard.Move(previousPiece, currentPiece);
                    updateBoard();
                    validDestinations = new List<Tuple<int, int>>();

                    int resultOfMove = gameboard.checkChessMate(currentState.getMyTeam());

                    if (resultOfMove == (int)moveResult.chess)
                    {
                        MessageBox.Show("Chess - Nice!", "Chess - Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (resultOfMove == (int)moveResult.chessMate)
                    {
                        DialogResult result = MessageBox.Show("Chessmate! You win.\n\nPlay again?", "Chess - Message", MessageBoxButtons.YesNo);
                        promptNewGame(result);
                        return;
                    }

                    if (previousPiece.type == (int)type.pawn && (currentPiece.row == 0 || currentPiece.row == 7))
                    {
                        MessageBox.Show("You get to change your pawn to one of your dead pieces.", "Pawn has crossed the gameboard!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pawnChangable = true;
                    }
                    else
                    {
                        previousPiece = null;
                        AIOpponent.Move(gameboard, currentState);
                        updateBoard();

                        resultOfMove = gameboard.checkChessMate(currentState.getOpponentTeam());

                        if (resultOfMove == (int)moveResult.chess)
                        {
                            MessageBox.Show("Chess - Watch out!", "Chess - Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (resultOfMove == (int)moveResult.chessMate)
                        {
                            DialogResult result = MessageBox.Show("Chessmate! You loose.\n\nPlay again?", "Chess - Message", MessageBoxButtons.YesNo);
                            promptNewGame(result);
                            return;
                        }
                    }
                }

                else if (previousPiece == null)
                {
                    return;
                }
                else
                {
                    validDestinations = new List<Tuple<int, int>>();
                    previousPiece = null;
                }
                updateBoard();
            }
        }

        // Returns True if the promt shows which means that the game is over
        private void promptNewGame(System.Windows.Forms.DialogResult result)
        {
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                gameboard.initialize();
                updateBoard();
            }
            else if (result == System.Windows.Forms.DialogResult.No)
            {
                storage.getWatcher().EnableRaisingEvents = false;
                File.Delete(GameComponents.saveLocation);
                storage.getWatcher().EnableRaisingEvents = true;
                this.Close();
            }
        }

        private void killedPieces_MouseClick(object sender, MouseEventArgs e)
        {
            if (pawnChangable)
            {
                Control c = (Control)sender;
                TableLayoutPanel teamBox = new TableLayoutPanel();
                if (currentState.getMyTeam() == (int)team.black)
                    teamBox = this.blackTeamPanel;
                else if (currentState.getMyTeam() == (int)team.white)
                    teamBox = this.whiteTeamPanel;

                int index = (teamBox.GetRow(c) * 2) + teamBox.GetColumn(c);
                if (gameboard.getDead(currentState.getMyTeam()).Count() > index)
                {
                    Piece currentPiece = gameboard.getDead(currentState.getMyTeam())[index];
                    if (gameboard.tradePawn(previousPiece, currentPiece))
                    {
                        if (gameboard.checkChessMate(currentState.getMyTeam()) == (int)moveResult.chessMate)
                        {
                            DialogResult result = MessageBox.Show("Chessmate! You win.\n\nPlay again?", "Chess - Message", MessageBoxButtons.YesNo);
                            promptNewGame(result);
                            return;
                        }
                        pawnChangable = false;
                        previousPiece = null;
                        AIOpponent.Move(gameboard, currentState);
                        updateBoard();
                        if (gameboard.checkChessMate(currentState.getOpponentTeam()) == (int)moveResult.chessMate)
                        {
                            DialogResult result = MessageBox.Show("Chessmate! You loose.\n\nPlay again?", "Chess - Message", MessageBoxButtons.YesNo);
                            promptNewGame(result);
                            return;
                        }
                    }
                }
            }
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("# Will hide this game form.");
            this.DialogResult = DialogResult.None;
            this.Hide();
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you really want to quit?", "Quit Chess?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Console.WriteLine("# Will close this game form.");
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
        }

        private void debugButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("# Will init new game.");
            DialogResult result = MessageBox.Show("Are you sure?", "Chess - Restart Game", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                promptNewGame(result);
        }

        // Force GUI to update
        public void OnDatabaseChanged(object source, PropertyChangedEventArgs e)
        {
            updateBoard();
        }
    }
}
