using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Xml.Serialization;

namespace Chess
{
    [XmlRoot("Piece")]
    [Serializable]
    public class Piece
    {
        // Contructor, set default values
        public Piece()
        {
            this.clear();
        }

        // Creates a Piece with default values but with specific coordinates
        public Piece(int row, int column)
        {
            this.clear();
            this.row = row;
            this.column = column;
        }

        // Full constructor
        public Piece(bool empty, int team, int type, int row, int column)
        {
            this.empty = empty;
            this.team = team;
            this.type = type;
            this.row = row;
            this.column = column;
        }

        // Copy-constructor
        public Piece(Piece origin)
        {
            this.empty = origin.empty;
            this.team = origin.team;
            this.type = origin.type;
            this.row = origin.row;
            this.column = origin.column;
        }

        // Inherit all but coordinates
        public void inherit(Piece piece)
        {
            this.empty = piece.empty;
            this.team = piece.team;
            this.type = piece.type;
        }

        // Inherit just coordinates
        public void move(Piece destination)
        {
            this.row = destination.row;
            this.column = destination.column;
        }

        // Set default values
        public void clear()
        {
            this.team = 0;
            this.type = 0;
            this.row = -1;
            this.column = -1;
            this.empty = true;
        }

        // Members
        public bool empty;
        public int team;
        public int type;
        public int row;
        public int column;
    }
}
