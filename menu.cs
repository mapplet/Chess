using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Chess
{
    public partial class menu : Form
    {
        private int myTeam = (int)team.none;
        private Form form;
        DialogResult gameResult;

        public menu()
        {
            Console.WriteLine("\n# Menu Thread ID: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            InitializeComponent();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (this.radioButtonBlack.Checked)
                myTeam = (int)team.black;
            else if (this.radioButtonWhite.Checked)
                myTeam = (int)team.white;
            else if (this.radioButtonResume.Checked)
                myTeam = (int)team.none;

            if (this.form == null || myTeam != (int)team.none)
            {
                this.form = new game(myTeam);
            }
            this.Hide();
            gameResult = form.ShowDialog();
            this.Show();
        }

        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonWhite.Checked || this.radioButtonBlack.Checked)
            {
                this.playButton.Text = "Start New Game!";
                this.playButton.Enabled = true;
            }
            else if (this.radioButtonResume.Checked)
                this.playButton.Text = "Resume Game!";
        }

        private void menu_Load(object sender, EventArgs e)
        {
            if (File.Exists(GameComponents.saveLocation))
            {
                this.playButton.Enabled = true;
                this.radioButtonResume.Enabled = true;
            }
        }

        private void menu_VisibleChanged(object sender, EventArgs e)
        {
            if (gameResult == DialogResult.Abort)
                Application.Exit();

            else
            {
                if (File.Exists(GameComponents.saveLocation))
                {
                    this.playButton.Enabled = true;
                    this.radioButtonResume.Enabled = true;
                    this.radioButtonResume.Checked = true;
                }
                else if (this.radioButtonResume.Checked)
                {
                    this.playButton.Enabled = false;
                    this.radioButtonResume.Enabled = false;
                }
            }

        }

    }
}
