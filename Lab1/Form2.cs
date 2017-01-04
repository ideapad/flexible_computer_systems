using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form2 : Form
    {
        Calculation calc;
        public Form2(Calculation calc)
        {
            InitializeComponent();
            this.calc = calc;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            calc.outGroups(lGroups);

            calc.groupsAfterVerification();
            calc.outgroupsAfterVerification(lGroupsAfterV);
        }
    }
}
