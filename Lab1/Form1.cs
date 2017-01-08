using System;
using System.Collections;
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
    public partial class Form1 : Form
    {
        private Calculation calc;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            numericRows.Value = 14;
            numericCols.Value = 8;
            button1_Click(sender, e);
            string line = "";

            System.IO.StreamReader file =
                new System.IO.StreamReader("source_16.txt", Encoding.Default);
            int index = 0;
            while ((line = file.ReadLine()) != null)
            {
                string[] list = line.Split(' ');

                for (int i = 0; i < list.Length; i++)
                {
                    valueEl[index][i].Text = list[i];
                }
                index++;
            }

            file.Close();

            
        }
        private void clearEl()
        {
            Controls.Clear();

            //button1.Location = new System.Drawing.Point(223, 30);
            //button1.Size = new System.Drawing.Size(85, 23);
            //button1.Text = "Accept";
            //this.Controls.Add(button1);

            //button3.Location = new System.Drawing.Point(321, 30);
            //button3.Size = new System.Drawing.Size(87, 23);
            //button3.Text = "File";
            //this.Controls.Add(button3);

            //lNumberRows.Location = new System.Drawing.Point(13, 13);
            //lNumberRows.Size = new System.Drawing.Size(137, 13);
            //lNumberRows.Text = "Choose number of rows:";
            //this.Controls.Add(lNumberRows);

            //lNumberCols.Location = new System.Drawing.Point(16, 40);
            //lNumberCols.Size = new System.Drawing.Size(148, 13);
            //lNumberCols.Text = "Choose number of columns:";
            //this.Controls.Add(lNumberCols);

            //numericRows.Location = new System.Drawing.Point(168, 13);
            //numericRows.Size = new System.Drawing.Size(35, 20);
            ////this.Controls.Add(numericRows);

            //numericCols.Location = new System.Drawing.Point(167, 38);
            //numericCols.Size = new System.Drawing.Size(35, 20);
            ////this.Controls.Add(numericCols);

            //numberCol.Location = new System.Drawing.Point(13, 68);
            //numberCol.Size = new System.Drawing.Size(110, 13);
            //this.Controls.Add(numberCol);

            //lResult.Location = new System.Drawing.Point(503, 13);
            //lResult.Size = new System.Drawing.Size(365, 406);
            //this.Controls.Add(lResult);

            //lResult.Text = "";

        } //Очищає форму перед наступним обрахунком

        private GraphVisual GraphVisual;
        NumericUpDown[] numUpDnArray; //Масив, який містить довжину рядків вхідної матриці
        TextBox[][] valueEl; //Масив, який містить значення вхідної матриці
        private void button1_Click(object sender, EventArgs e)//Розміщення NumericUpDown на формі(для кількості елементів в рядку) та TextBox
        {
            //clearEl();
            valueEl = null;
           // numberCol.Visible = true; 


            numUpDnArray = new NumericUpDown[(int)numericRows.Value];//Кількість рядків
            //////////////////////////////////////////////////////////
            for (int i = 0; i < (int)numericRows.Value; i++) 
            {
                NumericUpDown numUpDn = new NumericUpDown();
                numUpDn.Location = new System.Drawing.Point(20, 90 + i * 22);
                numUpDn.Size = new System.Drawing.Size(40, 20);
                numUpDn.Minimum = 1;
                numUpDn.Maximum = 14;
                
               // this.Controls.Add(numUpDn);
                numUpDnArray[i] = numUpDn;
            }
            ////////////////////////////////////
            //button2.Location = new System.Drawing.Point(20, 100 + (int)numericRows.Value * 22); 
            //button2.Size = new System.Drawing.Size(85, 23);
            //button2.Text = "Accept";
            //this.Controls.Add(button2);
            //button2.Show();

            ////////////////////////////////////////
            foreach (NumericUpDown i in numUpDnArray)
            {
                i.Value = numericCols.Value;
            }
           
            ////////////////////////////////
            for (int i = 0; i < numUpDnArray.Length; i++)// Задаю подію на зміну значення
                numUpDnArray[i].ValueChanged += new EventHandler(ctr_ValueChanged);
            
            ////////////////////////////////////////////////////////
            ctr_ValueChanged(sender, e);

            button2.Text = "Calculate";

        }

        private void button2_Click(object sender, EventArgs e)//Обрахунок
        {
            resultMatrix.Rows.Clear();
            resultMatrix.Refresh();

            boolMatrix.Rows.Clear();
            boolMatrix.Refresh();    

            calc = new Calculation();

            calc.countUniqueEl(richTextBox1.Text);
          //  calc.countUniqueEl(valueEl); //Обчислюю кількість унікальних елементів
            lResult.Text = "Результат: ";
            lResult.Text += "\nКількість унікальних елементів: " + calc.CountUEl;
            lResult.Text += "\nМатриця: \n";

          //  calc.calcResultMatrix(valueEl, (int)numericRows.Value, numUpDnArray);// Обчислюю матрицю подібності
            calc.calcResultMatrix(richTextBox1.Text);

            calc.outBooltMatrix(boolMatrix);//Виведення результатів
            calc.outResultMatrix(resultMatrix);

            fitDataGridView(boolMatrix,23);
            fitDataGridView(resultMatrix,3);
            // 23 & 3 are hand-picked numbers to DataGridViews to fit it's height to cells          
        }

        private void fitDataGridView(DataGridView dgv, int margin)
        {
            var height = margin;
            foreach (DataGridViewRow dr in dgv.Rows)
            {
                height += dr.Height;
            }

            dgv.Height = height;
        }

        void ctr_ValueChanged(object sender, EventArgs e)//Подія при зміні довжини рядка
        {
            /////////////////////////////
            string[][] temp = new string[(int)numericRows.Value][];//Проміжний масив для зберігання елементів
            bool first = true;
            if (valueEl != null)
            {
                int a = -1, b = 0;
                first=false;
                foreach (TextBox[] i in valueEl)//Заповнюю temp і видаляю елементи TextBox
                {
                    a++;
                    temp[a] = new string[(int)numUpDnArray[a].Value];
                    b = 0;
                    foreach (TextBox j in i)
                    {
                        try
                        {
                            temp[a][b] = j.Text;
                        }
                        catch(IndexOutOfRangeException)
                        { }
                        b++;
                        j.Dispose();
                    }
                }
            }
            ///////////////////////////////
            valueEl = new TextBox[(int)numericRows.Value][];

            for (int i = 0; i < (int)numericRows.Value; i++)
            {
                valueEl[i] = new TextBox[(int)numUpDnArray[i].Value];

                for (int j = 0; j < (int)numUpDnArray[i].Value; j++) //Розміщення TextBox(для значень елементів)
                {
                    valueEl[i][j] = new TextBox();
                    valueEl[i][j].Location = new System.Drawing.Point(70 + 31 * j, 90 + 22 * i);
                    valueEl[i][j].Size = new System.Drawing.Size(30, 20);
                  //  this.Controls.Add(valueEl[i][j]);
                }

            }
            /////////////////////////////////
            if (first==false)//Якщо була змінена розмірність вже створеного TextBox
            {
                for (int i = 0; i < (int)numericRows.Value; i++)
                {
                    for (int j = 0; j < (int)numUpDnArray[i].Value; j++)
                    {
                        valueEl[i][j].Text = temp[i][j];
                    }
                }
            }
            ////////////////////////////////
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileName == "")
                    return;

                System.IO.StreamReader file =
               new System.IO.StreamReader(openFileDialog.FileName, Encoding.Default);

                string line = "";
                int index = 0;

                numericRows.Value = 14;
                numericCols.Value = 8;
                button1_Click(sender, e);

                while ((line = file.ReadLine()) != null)
                {
                    string[] list = line.Split(' ');

                    for (int i = 0; i < list.Length; i++)
                    {
                        valueEl[index][i].Text = list[i];
                    }
                    index++;

                    richTextBox1.Text += line + "\n";
                    
                }
            }
        }

        private void groupList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void calc_groups_btn_Click(object sender, EventArgs e)
        {
            if (calc.resultMatrix[0].Count == 1) //Достатьньо елементів для утворення груп?
                return;

            groupList.Clear();
            prec_groupList.Clear();

            calc.createGroups();
            calc.outGroups(groupList);

            calc.groupsAfterVerification();
            calc.outgroupsAfterVerification(prec_groupList);

            groupList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            prec_groupList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            GraphVisual = new GraphVisual(calc,wpfHost);
            GraphVisual.GraphVisual_Load(sender,e);

            GraphVisual.showGivenGraph(0);

        }

        private void groupLis_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            if(e.Column == 0)
                groupList.ListViewItemSorter = new ListViewItemComparer(e.Column,false);
            else
                groupList.ListViewItemSorter = new ListViewItemComparer(e.Column, true);
            groupList.Sort();
        }

        private void prec_groupList_MouseDown(object sender, MouseEventArgs e)
        {       
            ListViewHitTestInfo info = prec_groupList.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            GraphVisual.showGivenGraph(item.Index);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            modules_view.Nodes.Clear();
            vmodules_list.Items.Clear();
            vmodules_list.Columns.Clear();
             
            GraphVisual.Generate_Click(sender,e);

            GraphVisual.getModulesList(modules_view);
            GraphVisual.getVModulesList(vmodules_list);

            modules_view.ExpandAll();
            vmodules_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text != string.Empty)
                button1.Enabled = true;
            else button1.Enabled = false;
        }
    }
}
