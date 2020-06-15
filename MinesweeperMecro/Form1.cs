using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;                              
                                  
namespace MinesweeperMecro
{
    public partial class Form1 : Form
    {
        MinesweeperMecro minesweeperMecro= new MinesweeperMecro();
        public Form1()
        {
            InitializeComponent();

            minesweeperMecro.UpdateEvent += UpdateEvent;
        }

        private void button1_Click(object sender, EventArgs e)
        {              
             
            minesweeperMecro.Start();
        }

        public void UpdateEvent()
        {
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = Util.ClopImage(Util.PrintScreenToImage(), minesweeperMecro.MinesweeperSpace);
        }
    }
}
