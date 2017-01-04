using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphX;

namespace Lab1
{
    public class Calculation
    {
        public List<HashSet<string>> mas;//Масив елементів


        private HashSet<String> setEl = new HashSet<string>(); //Множина для обрахунку унікальних елементів

        private int countUEl; //Кількість унікальних елементів

        public List<List<int>> resultMatrix;//Вихідна матриця подібності

        private List<HashSet<int>> groups;//Групи елементів

        public List<HashSet<int>> groupsAfterV;//Групи елементів після уточнення

        public List<HashSet<string>> setElAfterV;//Множини елементів групи


        public Dictionary<int, List<List<string>>> moduls = new Dictionary<int,List<List<string>>>();//Словник модулів для кожної крупи

        public List<List<string>> modulsAfterVerification = new List<List<string>>();//Уточнені модулі


        ////////////1 Лаба
        public int CountUEl
        {
            get { return countUEl; }
        }

        public void countUniqueEl(TextBox[][] valueEl)//Обчислення унікальних елементів (копіюю всі елементи в множину, результатом буде кількість елементів)
        {
            bool voidEl = false;
            foreach (TextBox[] i in valueEl)
            {
                foreach (TextBox j in i)
                {
                    setEl.Add(j.Text);
                    if (j.Text == "")
                        voidEl = true;
                }
            }

            countUEl = setEl.Count();
            if (voidEl) countUEl--;
        }

        public void calcResultMatrix(TextBox[][] valueEl, int sizeMatrix, NumericUpDown[] numUpDnArray)//Обчислення матриці подібності
        {
            mas = new List<HashSet<string>>();//Копіюю елементи з TextBox[][] в  List

            for (int i = 0; i < sizeMatrix; i++)
            {
                mas.Add(new HashSet<string>());

                for (int j = 0; j < (int)numUpDnArray[i].Value; j++)
                {
                    if (valueEl[i][j].Text == "") continue;


                    mas[i].Add(valueEl[i][j].Text);
                }
            }

            for (int i = 0; i < mas.Count; i++)//Видалення пустих рядків
            {
                if (mas.ElementAt(i).Count == 0)
                    mas.RemoveAt(i);
            }
            ////////////////////////////////////////////////////////////////////////////////////////
            resultMatrix = new List<List<int>>();
            for (int i = 0; i < mas.Count; i++)
            {
                resultMatrix.Add(new List<int>());
                for (int j = 0; j < mas.Count; j++)
                {
                    resultMatrix[i].Add(0);
                }
            }
            List<String> list; //List для злиття 2 рядків елементів
            for (int i = 0; i < mas.Count - 1; i++)
            {
                list = new List<String>();

                for (int j = 1; j < mas.Count; j++)
                {
                    if (i == j) continue;


                    list.AddRange(mas[i]);//Копіюю 1 рядок
                    list.AddRange(mas[j]);//Копіюю 2 рядок

                    /* list.Distinct() видаляє елементи, які повторюються, залишаючи перший з них.
                     * (list.Count - list.Distinct().Count()))*2 - дізнаюсь скільки спільних елементів
                     * (list.Count - (list.Count - list.Distinct().Count()) * 2) - дізнаюсь скільки унікальних елементів
                     * countUEl - (list.Count - (list.Count - list.Distinct().Count()) * 2) - від загальної кількості віднімаю унікальні в 2 рядках
                     */


                    resultMatrix[i][j] = countUEl - (list.Count - (list.Count - list.Distinct().Count()) * 2);

                    resultMatrix[j][i] = countUEl - (list.Count - (list.Count - list.Distinct().Count()) * 2);


                    list.Clear();//Очищаю для наступної пари рядків
                }
            }
        }

        public void outBooltMatrix(DataGridView data)//Виведення матриці наявності елементів у рядках
        {
            HashSet<string> distinct = setEl;
            distinct.Remove("");

            bool[,] operationBoolMatrix = new bool[mas.Count, distinct.Count];
            for (int i = 0; i < mas.Count; ++i)
            {
                for (int j = 0; j < mas[i].Count; ++j)
                {
                    for (int f = 0; f < distinct.Count; ++f)
                    {
                        if (mas[i].ElementAt(j) == distinct.ElementAt(f))
                            operationBoolMatrix[i, f] = true;
                    }
                }
            }

            int element_index = 0;
            foreach (string el in distinct)
            {
                data.Columns.Add(element_index.ToString(), el);
                    element_index++;
            }
                   
            data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            var rowCount = mas.Count;
            var rowLength = distinct.Count;
            data.ColumnCount = rowLength;

            for (int rowIndex = 0; rowIndex < rowCount; ++rowIndex)
            {
                var row = new DataGridViewRow();

                for (int columnIndex = 0; columnIndex < rowLength; ++columnIndex)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell()
                    {
                        Value = (operationBoolMatrix[rowIndex, columnIndex] == true) ? "1  " : "0  "
                    });
                }

                data.Rows.Add(row);
                data.Rows[rowIndex].HeaderCell.Value = (rowIndex + 1).ToString();
            }

            data.SelectionMode = DataGridViewSelectionMode.CellSelect;
            data.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            data.AllowUserToAddRows = false;
        }


        public void outResultMatrix(DataGridView data)//Виведення матриці подібності
        {
            data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            data.RowHeadersVisible = false;
            data.ColumnHeadersVisible = false;

            var rowCount = resultMatrix.Count();
            var rowLength = rowCount;
            data.ColumnCount = resultMatrix.Count();

            for (int rowIndex = 0; rowIndex < rowCount; ++rowIndex)
            {
                var row = new DataGridViewRow();

                for (int columnIndex = 0; columnIndex < rowLength; ++columnIndex)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell()
                    {
                        Value = resultMatrix[rowIndex][columnIndex]
                    });
                }

                data.Rows.Add(row);

                data.Rows[rowIndex].Cells[rowIndex].Style.BackColor = System.Drawing.Color.DeepSkyBlue;
            }

            data.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }
        ////////////////До 2 лаби
        public void createGroups()//Розбиття на групи
        {
            groups = new List<HashSet<int>>();
            int currentGroup = 0;
            do//Виконуємо поки всі елементи не розподілили по групам
            {
                int[] maxElIndex = maxEl(resultMatrix.Count, resultMatrix);
                if (maxElIndex == null && containsAllIndex() == false)//Якщо не можемо знайти максимальний елемент, який не входить в групи, але ще залишилися елементи
                {
                    createLastGroup();//Елементи, що залишилися об'єднуємо в одну групу
                    return;
                }
                groups.Add(new HashSet<int>());//Створюю нову групу
                groups.ElementAt(currentGroup).Add(maxElIndex[0]);//Записую в групу індекси максимального елемента (i)
                groups.ElementAt(currentGroup).Add(maxElIndex[1]);//(j)

                createGroupsNext(currentGroup, maxElIndex[0], maxElIndex[1]);
                
                currentGroup++;
            } while (containsAllIndex() == false); 

        }
        private void createGroupsNext(int currentGroup, int maxElIndex0, int maxElIndex1)//Пошук таких самих елементів по рядках і стовпцях
        {
            
            int i = maxElIndex0;
            for (int j = 0; j < resultMatrix.Count; j++)//Продивляюсь по рядку(i=const)
            {
                //if (i == j) break;
                if (resultMatrix[i][j] == resultMatrix[maxElIndex0][maxElIndex1] && containedInGroups(j) == false)
                {
                    groups.ElementAt(currentGroup).Add(j);
                    createGroupsNext(currentGroup, i, j);
                }
            }
            int x = maxElIndex1;
            for (int k = 0; k < resultMatrix.Count; k++)//Продивляюсь по стовпцю(j=const)
            {
                if (resultMatrix[k][x] == resultMatrix[maxElIndex0][maxElIndex1] && containedInGroups(k) == false)
                {
                    groups.ElementAt(currentGroup).Add(k);
                    createGroupsNext(currentGroup, k, x);
                }
            }
        }
        private int[] maxEl(int matrixSize, List<List<int>> resultMatrix)//Пошук максимального елемента, який ще не має групи
        {
            int max = -1;

            int[] maxIndex = new int[2];
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    if (i == j) continue;

                    if (resultMatrix[i][j] > max && containedInGroups(i) == false && containedInGroups(j) == false)
                    {
                        max = resultMatrix[i][j];
                        maxIndex[0] = i;
                        maxIndex[1] = j;
                    }
                }
            }
            if (max == -1)//Якщо не можна знайти не зайнятого елемента
                maxIndex = null;
            return maxIndex;
        }

        private bool containedInGroups(int index)//Перевіряє чи заданий елемент вже міститься в групах
        {
            foreach (HashSet<int> i in groups)
            {
                if (i.Contains(index))
                    return true;
            }
            return false;

        }

        private bool containsAllIndex()//Перевіряє чи всі елементи розбиті на групи
        {
            bool found = true;
            for (int i = 0; i < resultMatrix.Count; i++)//Шукаю індекси в усіх групах
            {
                foreach (HashSet<int> j in groups)
                {
                    if (j.Contains(i) == false)
                        found = false;
                    else
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                    return false;
            }
            return true;
        }

        private void createLastGroup()//Створю останню групу, в яку входять елементи, що залишились незгрупованими
        {
            groups.Add(new HashSet<int>());

            bool found = false;
            for (int i = 0; i < resultMatrix.Count; i++)//Шукаю індекси в усіх групах
            {
                foreach (HashSet<int> j in groups)
                {
                    if (j.Contains(i) == false)
                        found = false;
                    else
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                    groups.ElementAt(groups.Count - 1).Add(i);
            }
        }

        public void outGroups(Label l)//Виведення результату групування
        {
            int n = 1;
            l.Text += "\n";
            foreach(HashSet<int> i in groups)
            {
                l.Text += n +  " group {  ";
                foreach(int j in i)
                {
                    l.Text += (j+1) + "  ";
                }
                l.Text += "}\n";
                n++;
            }
        }

        public void outGroups(ListView ls_view)//Виведення результату групування
        {
            string[] columns = { "Group number", "Group elements" };
            foreach(string column_name in columns)          
                ls_view.Columns.Add(column_name,100);


            int index = 1;
            foreach(HashSet<int> group in groups)
            {
                string group_elements = String.Empty;

              foreach(int element in group)
                {
                    group_elements += (element + 1) + " ";
                }
                string[] row = {"Group " + index, group_elements };
                var listViewItem = new ListViewItem(row);
                ls_view.Items.Add(listViewItem);
                index++;       
            }
        }


        ////////////3 Лаба
        private List<HashSet<string>> createSet(List<HashSet<int>> groups)//Зливаю елементи в одну множину за результатами групування
        {
            List<HashSet<string>> setElOfGroups = new List<HashSet<string>>();

            for(int i = 0; i < groups.Count(); i++)
            {
                setElOfGroups.Add(new HashSet<string>());
                for(int j = 0; j < groups[i].Count(); j++)
                {
                    for (int k = 0; k < mas[groups[i].ElementAt(j)].Count; k++)
                    {
                        setElOfGroups[i].Add(mas[groups[i].ElementAt(j)].ElementAt(k));
                    }
                }
            }

            

            //setElOfGroups.Sort((a, b) => b.Count - a.Count);//Сортую за кількістю елементів (DESC)


            return setElOfGroups;
        }

        public void groupsAfterVerification()//Групування з знаходженням підмножин
        {
            groupsAfterV = new List<HashSet<int>>();
            setElAfterV= new List<HashSet<string>>();

            groupsAfterV.AddRange(groups);
            
            setElAfterV.AddRange(createSet(groups));

            sortV(groupsAfterV, setElAfterV);//Сортування груп за кількістю елементів

            sortWithEqualL(setElAfterV);//Сортування груп з однаковою кількістю елементів за перекриванням найбільшої кількості об'єктів

            for (int i = 0; i < setElAfterV.Count() - 1; i++)//Йду по найбільших групах
            {
                for (int k = i+1; k < groupsAfterV.Count(); k++)//Йду по підгрупах
                {
                    for (int j = 0; j < groupsAfterV[k].Count; j++)//Йду по елементах в групі
                    {
                        if (setElAfterV[i].IsSupersetOf(mas[groupsAfterV[k].ElementAt(j)]))//Являється підмножиною?
                        {
                            groupsAfterV[i].Add(groupsAfterV[k].ElementAt(j));//Додаю елемент групи в групу надмножину
                            groupsAfterV[k].Remove(groupsAfterV[k].ElementAt(j));//Видаляю елемент з підмножини
                            j--;
                        }
                    }
                }
                for (int ind = 0; ind < groupsAfterV.Count; ind++)//Видалення пустих рядків
                {
                    if (groupsAfterV.ElementAt(ind).Count == 0)
                        groupsAfterV.RemoveAt(ind);
                }

                setElAfterV.Clear();//Видалив всі групи
                setElAfterV.AddRange(createSet(groupsAfterV));//Переукомплектовую групи після уточнення
                sortWithEqualL(setElAfterV);//Сортування груп з однаковою кількістю елементів за перекриванням найбільшої кількості об'єктів
            }
        }

        public void outgroupsAfterVerification(Label l)//Виведення уточнених груп
        {
            int n = 1;
            l.Text += "\n";
            foreach (HashSet<int> i in groupsAfterV)
            {
                l.Text += n + " group {  ";
                foreach (int j in i)
                {
                    l.Text += (j + 1) + "  ";
                }
                l.Text += "}\n";
                n++;
            }
        }

        public void outgroupsAfterVerification(ListView ls_view)//Виведення уточнених груп
        {
            string[] columns = { "Group number", "Group elements" };
            foreach (string column_name in columns)
                ls_view.Columns.Add(column_name, 100);


            int index = 1;
            foreach (HashSet<int> group in groupsAfterV)
            {
                string group_elements = String.Empty;

                foreach (int element in group)
                {
                    group_elements += (element + 1) + " ";
                }
                string[] row = { "Group " + index, group_elements };
                var listViewItem = new ListViewItem(row);
                ls_view.Items.Add(listViewItem);
                index++;
            }
        }

        private void sortV(List<HashSet<int>> groupsAfterV, List<HashSet<string>> setElAfterV)//Сортую групи і множини елементів за кількістю елементів(разом щоб співпадали)
        {
            List<HashSet<int>> temp = new List<HashSet<int>>();
            List<HashSet<string>> temp2 = new List<HashSet<string>>();
            for (int i = 0; i < groupsAfterV.Count; i++)
            {
                for (int j = 0; j < groupsAfterV.Count - 1; j++)
                {
                    if (setElAfterV.ElementAt(j).Count < setElAfterV.ElementAt(j + 1).Count)
                    {
                        temp.Add(groupsAfterV.ElementAt(j));
                        groupsAfterV.Insert(j, groupsAfterV.ElementAt(j + 1));
                        groupsAfterV.RemoveAt(j + 1);
                        groupsAfterV.Insert(j + 1, temp.ElementAt(0));
                        groupsAfterV.RemoveAt(j + 2);
                        temp.Clear();

                        temp2.Add(setElAfterV.ElementAt(j));
                        setElAfterV.Insert(j, setElAfterV.ElementAt(j + 1));
                        setElAfterV.RemoveAt(j + 1);
                        setElAfterV.Insert(j + 1, temp2.ElementAt(0));
                        setElAfterV.RemoveAt(j + 2);
                        temp2.Clear();
                    }
                }
            }
        }

        private void sortWithEqualL(List<HashSet<string>> setElAfterV)//Сортування груп з однаковою кількістю елементів за перекриванням найбільшої кількості об'єктів
        {
            List<HashSet<string>> tempSet = new List<HashSet<string>>();
            tempSet.Add(setElAfterV[0]);
            for (int i = 0; i < setElAfterV.Count-1; i++ )
            {
                if (setElAfterV[i].Count == setElAfterV[i + 1].Count)
                {
                    tempSet.Add(setElAfterV[i + 1]);
                }
                else
                    break;
            }
            if(tempSet.Count > 1)
            {
                List<int> numberOverlapped = new List<int>();//Кількість перекритих об'єктів

                for (int i = 0; i < tempSet.Count(); i++)//Знаходжу скільки об'єктів перекриває кожна група
                {
                    numberOverlapped.Add(0);//Початкова кількість перекритих об'єктів
                    for (int k = 0; k < tempSet.Count(); k++)
                    {
                        if (i == k) continue;
                        for (int j = 0; j < groupsAfterV[k].Count; j++)
                        {
                            if (tempSet[i].IsSupersetOf(mas[groupsAfterV[k].ElementAt(j)]))//Являється підмножиною?
                            {
                                numberOverlapped[i]++;
                            }
                        }
                    }
                }

                for(int i = 0; i < tempSet.Count; i++)
                {
                    for (int j = i+1; j < tempSet.Count; j++)
                    {
                        if(numberOverlapped[i] < numberOverlapped[j])
                        {
                            int temp = numberOverlapped[i];
                            numberOverlapped[i] = numberOverlapped[j];
                            numberOverlapped[j] = temp;

                            List<HashSet<int>> tempGroup = new List<HashSet<int>>();
                            tempGroup.Add(groupsAfterV[i]);
                            groupsAfterV[i] = groupsAfterV[j];
                            groupsAfterV[j] = tempGroup[0];

                            List<HashSet<string>> tempEl = new List<HashSet<string>>();
                            tempEl.Add(setElAfterV[i]);
                            setElAfterV[i] = setElAfterV[j];
                            setElAfterV[j] = tempEl[0];
                        }
                    }
                }
            }
        }       

        

    }
}
