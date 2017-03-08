using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class frmMain : Form
    {
        #region Enums
        enum Shape
        {
            X = 0,
            O = 1,
        }
        #endregion

        #region Properties
        private Shape NextShape { set; get; }
        private bool GameEnded { set; get; }
        #endregion

        #region Constructors
        public frmMain()
        {
            InitializeComponent();
            Init();
        } 
        #endregion

        #region Methods
        private bool Draw(Panel pnl)
        {
            if (this.NextShape == Shape.X)
            {
                if (!DrawShape(pnl, TicTacToe.Properties.Resources.X)) return false;
                this.NextShape = Shape.O;
            }
            else
            {
                if (!DrawShape(pnl, TicTacToe.Properties.Resources.O)) return false;
                this.NextShape = Shape.X;
            }
            return true;
        }
        private bool DrawShape(Panel pnl, Bitmap shape)
        {
            pnl.BackgroundImage = shape;
            pnl.BackgroundImageLayout = ImageLayout.Stretch;
            pnl.Tag = this.NextShape.ToString(); // because NextShape has not been changed yet, so the next shape is the current shape.
            return true;
        }
        private void Init()
        {
            this.NextShape = Shape.X;
            pnl_0_0.Click += new EventHandler(Panel_Click);
            pnl_0_1.Click += new EventHandler(Panel_Click);
            pnl_0_2.Click += new EventHandler(Panel_Click);
            pnl_1_0.Click += new EventHandler(Panel_Click);
            pnl_1_1.Click += new EventHandler(Panel_Click);
            pnl_1_2.Click += new EventHandler(Panel_Click);
            pnl_2_0.Click += new EventHandler(Panel_Click);
            pnl_2_1.Click += new EventHandler(Panel_Click);
            pnl_2_2.Click += new EventHandler(Panel_Click);
        }
        private bool CheckEndOfGame(string row, string column)
        {
            bool res = false;
            int rowNum = Convert.ToInt32(row);
            int colNum = Convert.ToInt32(column);

            res = ValidateRow(rowNum);
            res |= ValidateColumn(colNum);
            if ((rowNum == 0 && colNum == 0) ||
                (rowNum == 1 && colNum == 1) ||
                (rowNum == 2 && colNum == 2))
            {
                res |= ValidateCells(pnl_0_0, pnl_1_1, pnl_2_2);
            }
            if ((rowNum == 0 && colNum == 2) ||
                (rowNum == 1 && colNum == 1) ||
                (rowNum == 2 && colNum == 0))
            {
                res |= ValidateCells(pnl_0_2, pnl_1_1, pnl_2_0);
            }
            return res;
        }
        private bool ValidateRow(int rowNumber)
        {
            return this.ValidateCells(
                tableLayoutPanel1.Controls["pnl_" + rowNumber.ToString() + "_0"] as Panel,
                tableLayoutPanel1.Controls["pnl_" + rowNumber.ToString() + "_1"] as Panel,
                tableLayoutPanel1.Controls["pnl_" + rowNumber.ToString() + "_2"] as Panel);
        }
        private bool ValidateColumn(int columnNumber)
        {
            return this.ValidateCells(
                tableLayoutPanel1.Controls["pnl_0_" + columnNumber.ToString()] as Panel,
                tableLayoutPanel1.Controls["pnl_1_" + columnNumber.ToString()] as Panel,
                tableLayoutPanel1.Controls["pnl_2_" + columnNumber.ToString()] as Panel);
        }
        private bool ValidateCells(Panel pnl0, Panel pnl1, Panel pnl2)
        {
            if (pnl0 == null || pnl0.BackgroundImage == null) return false;
            if (pnl1 == null || pnl1.BackgroundImage == null) return false;
            if (pnl2 == null || pnl2.BackgroundImage == null) return false;

            if (pnl0.Tag == pnl1.Tag &&
                pnl1.Tag == pnl2.Tag) return true;
            return false;
        }
        private void ResetAll()
        {
            pnl_0_0.BackgroundImage = null;
            pnl_0_1.BackgroundImage = null;
            pnl_0_2.BackgroundImage = null;
            pnl_1_0.BackgroundImage = null;
            pnl_1_1.BackgroundImage = null;
            pnl_1_2.BackgroundImage = null;
            pnl_2_0.BackgroundImage = null;
            pnl_2_1.BackgroundImage = null;
            pnl_2_2.BackgroundImage = null;
        }
        #endregion

        #region Events
        private void Panel_Click(object sender, EventArgs e)
        {
            Panel pnl =  sender as Panel;
            if (pnl.BackgroundImage != null) return;
            if (GameEnded) return;
            this.Draw(pnl);
            if (CheckEndOfGame(pnl.Name.Split('_')[1], pnl.Name.Split('_')[2]))
            {
                this.GameEnded = true;
                MessageBox.Show(string.Format("The {0} player wins the game", (this.NextShape == Shape.X) ? "O" : "X"));
                ResetAll();
                return;
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ResetAll();
            this.GameEnded = false;
        }
        #endregion
    }
}
