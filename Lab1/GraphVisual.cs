using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Logic.Algorithms.OverlapRemoval;
using GraphX.PCL.Logic.Models;
using GraphX.Controls;
using QuickGraph;
using Combinatorics.Collections;

namespace Lab1
{
    public partial class GraphVisual : Form
    {
        ///////////4 Лаба
        public GraphVisual(Calculation calc, System.Windows.Forms.Integration.ElementHost host)
        {
            InitializeComponent();
            this.calc = calc;
            this.mainHost = host;
        }
        public void setPanels(Panel p1, Panel p2)
        {
            panel2 = p1;
            panel1 = p1;
        }

        private System.Windows.Forms.Integration.ElementHost mainHost;

        private ZoomControl _zoomctrl;
        private GraphArea _gArea;

        private Calculation calc;
        private Panel panel1, panel2;
        private List<Graph> graphs;//Лист графів для кожної групи
        private Graph totalGraph;//Загальний граф для відображення
        private List<List<DataVertex>> catalogCycles = new List<List<DataVertex>>();//Зберігає всі знайдуні цикли(для 4 і 5 умови)
        private List<Dictionary<string, List<string>>> nameVertexAndModuls = new List<Dictionary<string, List<string>>>();//Зберігає ім'я вершини(key) і відповідні їй модулі(value)
        private Dictionary<string, List<string>> nameVertexAndM;////Зберігає ім'я вершини(key) і відповідні їй модулі(value) (для всіх модулів разом)
        private List<Graph> allGraphs = new List<Graph>();//Лист для зберігання проміжних графів(щоб можна було переглядати покроково)
        private int currentGraph;//Номер проміжного графа в allGraphs, який відображається
        List<List<int>> links = new List<List<int>>();
        List<List<string>> operationMatrix = new List<List<string>>();
        List<Module> modules = new List<Module>();
        List<int> basicStructure = new List<int>();
        List<int> optimizedStructure = new List<int>();

        public List<Line> lines = new List<Line>();
        public List<Arc> arcs = new List<Arc>();
        public List<TextGraphics> textGraphics = new List<TextGraphics>();
        public List<Line> lines1 = new List<Line>();
        public List<Arc> arcs1 = new List<Arc>();
        public List<TextGraphics> textGraphics1 = new List<TextGraphics>();

        List<List<int>> inputStream = new List<List<int>>();
        List<List<int>> outputStream = new List<List<int>>();



        public void GraphVisual_Load(object sender, EventArgs e)
        {
            Generate_Click(sender, e);
            
        }
        private void gArea_RelayoutFinished(object sender, EventArgs e)
        {
            _zoomctrl.ZoomToFill();
        }

        private UIElement GenerateWpfVisuals()
        {
            _zoomctrl = new ZoomControl();
            ZoomControl.SetViewFinderVisibility(_zoomctrl, Visibility.Visible);
            /* ENABLES WINFORMS HOSTING MODE --- >*/
            var logic = new GXLogicCore<DataVertex, DataEdge, BidirectionalGraph<DataVertex, DataEdge>>();
            _gArea = new GraphArea() { EnableWinFormsHostingMode = true, LogicCore = logic };

            logic.Graph = totalGraph;

            logic.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.LinLog;
            logic.DefaultLayoutAlgorithmParams = logic.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.Tree);
            //((LinLogLayoutParameters)logic.DefaultLayoutAlgorithmParams). = 100;
            logic.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            logic.DefaultOverlapRemovalAlgorithmParams = logic.AlgorithmFactory.CreateOverlapRemovalParameters(OverlapRemovalAlgorithmTypeEnum.FSA);
            ((OverlapRemovalParameters)logic.DefaultOverlapRemovalAlgorithmParams).HorizontalGap = 50;
            ((OverlapRemovalParameters)logic.DefaultOverlapRemovalAlgorithmParams).VerticalGap = 50;
            logic.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None;
            logic.AsyncAlgorithmCompute = false;
            _zoomctrl.Content = _gArea;
            _gArea.RelayoutFinished += gArea_RelayoutFinished;


            var myResourceDictionary = new ResourceDictionary { Source = new Uri("Templates\\template.xaml", UriKind.Relative) };
            _zoomctrl.Resources.MergedDictionaries.Add(myResourceDictionary);

            return _zoomctrl;
        }

        public Graph GenerateGraph()//Створює графи на основі уточнених груп
        {
            graphs = new List<Graph>();//Лист графів для кожної групи
            for (int i = 0; i < calc.setElAfterV.Count; i++)
            {
                var Graph = new Graph();

                HashSet<string> tempSet = new HashSet<string>();

                foreach (string k in calc.setElAfterV[i])//Формування вершин
                {
                    var Vertex = new DataVertex(k);
                    Graph.AddVertex(Vertex);
                }

                var vlist = Graph.Vertices.ToList();


                //if (indicateByGroup)
                //{
                //    var datVertex1 = new DataVertex("Group " + (i + 1).ToString());
                //    var datVertex2 = new DataVertex(calc.mas[calc.groupsAfterV[i].ElementAt(0)].ElementAt(0));

                //    vlist.Add(datVertex1);
                //    Graph.AddVertex(datVertex1);

                //    var Edg = new DataEdge(vlist.Find(x => x.Text == datVertex1.Text), vlist.Find(x => x.Text == datVertex2.Text))
                //    { Text = string.Format("{0} -> {1}", datVertex1.Text, vlist.Find(x => x.Text == datVertex2.Text)) };
                //    Graph.AddEdge(Edg);
                //}


                for (int j = 0; j < calc.groupsAfterV[i].Count; j++)//Формування ребер
                {
                    for (int t = 0; t < calc.mas[calc.groupsAfterV[i].ElementAt(j)].Count - 1; t++)
                    {
                        var dataVertex1 = new DataVertex(calc.mas[calc.groupsAfterV[i].ElementAt(j)].ElementAt(t));
                        var dataVertex2 = new DataVertex(calc.mas[calc.groupsAfterV[i].ElementAt(j)].ElementAt(t + 1));

                        var Edge = new DataEdge(vlist.Find(x => x.Text == dataVertex1.Text), vlist.Find(x => x.Text == dataVertex2.Text))
                        { Text = string.Format("{0} -> {1}", vlist.Find(x => x.Text == dataVertex1.Text), vlist.Find(x => x.Text == dataVertex2.Text)) };

                        //Перевіряю чи є вже таке ребро
                        DataEdge temp = new DataEdge();
                        if (Graph.TryGetEdge(Edge.Source, Edge.Target, out temp) == false)
                        {
                            Graph.AddEdge(Edge);
                        }
                
                    }
                
                }              

                graphs.Add(Graph);
            }

            totalGraph = new Graph();//Граф для відображення(об'єднує всі графи)

            //totalGraph.AddVertexRange(graphs.First().Vertices.ToList());
            //totalGraph.AddEdgeRange(graphs.First().Edges.ToList());

            for (int i = 0; i < graphs.Count; i++)
            {
                totalGraph.AddVertexRange(graphs.ElementAt(i).Vertices.ToList());
                totalGraph.AddEdgeRange(graphs.ElementAt(i).Edges.ToList());
            }

            return totalGraph;
        }
           
        private void createModuls()//Формування модулів(повна згортка всіх графів)
        {
            //Роблю кожен елемент модулем
            for (int i = 0; i < graphs.Count; i++)
            {
                Dictionary<string, List<string>> nameVertexAndModul = new Dictionary<string, List<string>>();//Назва вершини в графі(key) і відповідні їй модулі(value)
                var vert = graphs[i].Vertices.ToList();
                calc.moduls.Add(i, new List<List<string>>());

                foreach (DataVertex v in vert)
                {
                    var modul = new List<string>();
                    modul.Add(v.Text);

                    calc.moduls[i].Add(modul);

                    nameVertexAndModul.Add(v.Text, new List<string>(modul));//Зв'язую назву вершини графа з модулями, які вона містить
                }
                nameVertexAndModuls.Add(nameVertexAndModul);
            }
            //Повна згортка графів          
            for (int i = 0; i < graphs.Count; i++)
            {
                ///////////////////////////////
                createStepGraph(i);
                //////////////////////////////
                bool flag;//Продовжую згортати поки true(false означає, що більше граф згорнути не можна)
                do
                {
                    flag = false;
                    if (thirdСondition(graphs[i]) != null)//Якщо знайдено вершини, які задовольняють умову
                    {
                        foreach (List<DataVertex> v in thirdСondition(graphs[i]))//Згортаю почерзі вершини до першого успішного згортання
                        {
                            if (mergeVertex(graphs[i], i, v.ToArray()))//Поверне true, якщо згортка відбулася
                            {
                                flag = true;//Отже згортка відбуваля і могли з'явитися нові вершини, які задовольняють умові
                                ///////////////////////////////////////
                                createStepGraph(i);
                                //////////////////////////////////////////
                                break;
                            }
                        }

                    }

                    if (fourthCondition(graphs[i]) != null)
                    {
                        foreach (List<DataVertex> v in fourthCondition(graphs[i]))
                        {
                            if (mergeVertex(graphs[i], i, v.ToArray()))
                            {
                                flag = true;
                                ////////////////////////////////////
                                createStepGraph(i);
                                ///////////////////////////////////
                                break;
                            }
                        }
                        catalogCycles.Clear();
                    }

                    if (fifthCondition(graphs[i]) != null)
                    {
                        foreach (List<DataVertex> v in fifthCondition(graphs[i]))
                        {
                            if (mergeVertex(graphs[i], i, v.ToArray()))
                            {
                                flag = true;
                                /////////////////////////////////////////////
                                createStepGraph(i);
                                ////////////////////////////
                                break;
                            }
                        }
                        catalogCycles.Clear();
                    }

                } while (flag);
            }
            //////////////////////////////
            //allGraphs.Add(graphs[0]);
            allGraphs.Add(totalGraph);
            _gArea.GenerateGraph(true, true);//Перемальовую граф
            
            currentGraph = allGraphs.Count - 1;//Поточний проміжний граф(повністю згорнутий)
        }

        private void createStepGraph(int i)//Записує проміжний граф(для покрокового перегляду)
        {
            //Копіюю вершини та зв'язки з поточного графа і записую в allGraphs
            Graph stepGraph = new Graph();
            foreach (DataVertex verStep in graphs[i].Vertices)
                stepGraph.AddVertex(new DataVertex(verStep.Text));
            foreach (DataEdge edgStep in graphs[i].Edges)
            {
                stepGraph.AddEdge(new DataEdge(stepGraph.Vertices.First(x => x.Text == edgStep.Source.Text), stepGraph.Vertices.First(x => x.Text == edgStep.Target.Text)) { Text = string.Format("{0} -> {1}", stepGraph.Vertices.First(x => x.Text == edgStep.Source.Text), stepGraph.Vertices.First(x => x.Text == edgStep.Target.Text)) });
            }

            allGraphs.Add(stepGraph);
        }

        private void firstAndSecondСondition(Graph graph, int indexOfGroup)//Не має сенсу застосовувати(поки залишаю)
        {
            List<DataVertex> vertexInGraph = graph.Vertices.ToList();

            foreach (DataVertex v in vertexInGraph)
            {
                var outE = graph.OutEdges(v);
                var inE = graph.InEdges(v);

                if (outE.Count() > 0 && inE.Count() == 0)//Вершина має тільки ребра, що виходять
                {
                    var modul = new List<string>();
                    modul.Add(v.Text);

                    calc.moduls[indexOfGroup].Add(modul);


                }
                if (outE.Count() == 0 && inE.Count() > 0)//Вершина має тільки ребра, що входять
                {
                    var modul = new List<string>();
                    modul.Add(v.Text);

                    calc.moduls[indexOfGroup].Add(modul);
                }
            }

        }

        private List<List<DataVertex>> thirdСondition(Graph graph)//Вершини, які мають взаємозв'язок
        {
            var vertices = graph.Vertices.ToList();
            var edges = graph.Edges.ToList();

            DataVertex v1 = new DataVertex();
            DataVertex v2 = new DataVertex();

            List<List<DataVertex>> listV = new List<List<DataVertex>>();
            for (int i = 0; i < vertices.Count; i++)//Йду по вершинах
            {
                var target = edges.FindAll(x => x.Source == vertices[i]);//Всі сини для даної вершини
                for (int j = 0; j < target.Count; j++)//Йду по всіх синах для поточної вершини
                {
                    var target2 = edges.FindAll(x => x.Source == target[j].Target);//Всі сини сина поточної вершини

                    for (int k = 0; k < target2.Count; k++)
                    {
                        if (target2[k].Target == vertices[i])//Якщо поточна вершина є сином її сина(тобто є взаємозв'язок)
                        {
                            v1 = vertices[i];
                            v2 = target2[k].Source;

                            List<DataVertex> list = new List<DataVertex>();
                            list.Add(v1);
                            list.Add(v2);

                            listV.Add(list);
                        }
                    }
                }
            }
            if (listV.Count == 0)
                return null;
            return listV;
        }

        //private void outGraph()
        //{
        //    foreach()

        //    bool[,] graph = CreateGraph(elements, group);
        //    textBox1.Text += "Graph: \r\n";
        //    for (int i = 0; i < elements.Count; i++)
        //    {
        //        for (int j = 0; j < elements.Count; j++)
        //        {
        //            textBox1.Text += ((graph[i, j] == true) ? "1" : "0") + " ";
        //        }
        //        textBox1.Text += "\r\n";
        //    }
        //}

        //bool[,] CreateGraph(List<Module> elements, List<int> group)
        //{
        //    bool[,] graph = new bool[elements.Count, elements.Count];
        //    string prev = operationMatrix[group[0]][0];
        //    foreach (int i in group)
        //    {
        //        prev = operationMatrix[i][0];
        //        foreach (string str in operationMatrix[i])
        //        {
        //            if (prev != str)
        //            {
        //                int y = elements.FindIndex(m => m.Name.Contains(prev));
        //                int x = elements.FindIndex(m => m.Name.Contains(str));
        //                graph[x, y] = true;
        //                prev = str;
        //            }
        //        }
        //    }
        //    return graph;
        //}

        private List<List<DataVertex>> fourthCondition(Graph graph)//Пошук циклів
        {
            var V = graph.Vertices.ToList();
            var E = graph.Edges.ToList();

            int[] color = new int[V.Count];//Масив, який містить кольори вершин
            for (int i = 0; i < V.Count; i++)
            {
                for (int k = 0; k < V.Count; k++)
                    color[k] = 1;
                List<int> cycle = new List<int>();
                cycle.Add(i);
                DFScycle(i, i, E, V, color, -1, cycle);//Пошук в глибину
            }

            catalogCycles.RemoveAll(x => x.Count == 2);//Видаляю ті, які не знайшли шляху до вершини
            catalogCycles.RemoveAll(x => x.Count > 5);//Видаляю ті, які мають більше 5 елементів(вони всеодно не згорнуться)
            if (catalogCycles.Count == 0)
                return null;
            return catalogCycles;
        }

        private void DFScycle(int u, int endV, List<DataEdge> E, List<DataVertex> V, int[] color, int unavailableEdge, List<int> cycle)//Пошук в глибину(http://vscode.ru/prog-lessons/poisk-elementarnyih-tsiklov-v-grafe.html)
        {
            //если u == endV, то эту вершину перекрашивать не нужно, иначе мы в нее не вернемся, а вернуться необходимо
            if (u != endV)
                color[u] = 2;
            else if (cycle.Count >= 2)
            {
                cycle.Reverse();
                string s = cycle[0].ToString();
                for (int i = 1; i < cycle.Count; i++)
                    s += "-" + cycle[i].ToString();
                ///////////////////////
                List<DataVertex> list = new List<DataVertex>();//Додаю вершину в цикл
                list.Add(V.ElementAt(cycle[0]));
                for (int i = 1; i < cycle.Count - 1; i++)
                    list.Add(V.ElementAt(cycle[i]));
                ///////////////////////
                bool flag = false; //есть ли палиндром для этого цикла графа в List<string> catalogCycles?
                for (int i = 0; i < catalogCycles.Count; i++)
                    if (catalogCycles[i].ToString() == s)
                    {
                        flag = true;
                        break;
                    }
                if (!flag)
                {
                    cycle.Reverse();
                    list = new List<DataVertex>();
                    list.Add(V.ElementAt(cycle[0]));
                    for (int i = 1; i < cycle.Count - 1; i++)
                        list.Add(V.ElementAt(cycle[i]));
                    catalogCycles.Add(list);
                }
                return;
            }
            for (int w = 0; w < E.Count; w++)
            {
                if (w == unavailableEdge)
                    continue;
                if (color[V.FindIndex(0, V.Count, x => x.Text == E[w].Target.Text)] == 1 && V.FindIndex(0, V.Count, x => x.Text == E[w].Source.Text) == u)
                {
                    List<int> cycleNEW = new List<int>(cycle);
                    cycleNEW.Add(V.FindIndex(0, V.Count, x => x.Text == E[w].Target.Text));
                    DFScycle(V.FindIndex(0, V.Count, x => x.Text == E[w].Target.Text), endV, E, V, color, w, cycleNEW);
                    color[V.FindIndex(0, V.Count, x => x.Text == E[w].Target.Text)] = 1;
                }
            }
        }

        private void DFScycleForFifth(int u, int endV, List<DataEdge> E, List<DataVertex> V, int[] color, int unavailableEdge, List<int> cycle, Graph graph)//Пошук в глибину(для 5 умови)
        {
            //если u == endV, то эту вершину перекрашивать не нужно, иначе мы в нее не вернемся, а вернуться необходимо
            if (u != endV)
                color[u] = 2;
            else if (cycle.Count >= 2)
            {
                cycle.Reverse();
                string s = cycle[0].ToString();
                for (int i = 1; i < cycle.Count; i++)
                    s += "-" + cycle[i].ToString();
                ///////////////////////
                List<DataVertex> list = new List<DataVertex>();//Додаю вершину в цикл
                list.Add(V.ElementAt(cycle[0]));
                for (int i = 1; i < cycle.Count - 1; i++)
                    list.Add(V.ElementAt(cycle[i]));
                ///////////////////////
                bool flag = false; //есть ли палиндром для этого цикла графа в List<string> catalogCycles?
                for (int i = 0; i < catalogCycles.Count; i++)
                    if (catalogCycles[i].ToString() == s)
                    {
                        flag = true;
                        break;
                    }
                if (!flag)
                {
                    cycle.Reverse();
                    list = new List<DataVertex>();
                    list.Add(V.ElementAt(cycle[0]));
                    for (int i = 1; i < cycle.Count - 1; i++)
                        list.Add(V.ElementAt(cycle[i]));
                    catalogCycles.Add(list);
                }
                return;
            }
            for (int w = 0; w < E.Count; w++)
            {
                var q = graph.OutEdges(V[V.FindIndex(0, V.Count, x => x.Text == E[w].Target.Text)]);
                if (w == unavailableEdge || (graph.InEdges(V[V.FindIndex(0, V.Count, x => x.Text == E[w].Target.Text)]).Count() > 1
                    || graph.OutEdges(V[V.FindIndex(0, V.Count, x => x.Text == E[w].Target.Text)]).Count() > 1) && V.FindIndex(x => x.Text == E[w].Target.Text) != endV)
                    
                    continue;
                if (color[V.FindIndex(0, V.Count, x => x.Text == E[w].Target.Text)] == 1 && V.FindIndex(0, V.Count, x => x.Text == E[w].Source.Text) == u)
                {
                    List<int> cycleNEW = new List<int>(cycle);
                    cycleNEW.Add(V.FindIndex(0, V.Count, x => x.Text == E[w].Target.Text));
                    DFScycleForFifth(V.FindIndex(0, V.Count, x => x.Text == E[w].Target.Text), endV, E, V, color, w, cycleNEW, graph);
                    color[V.FindIndex(0, V.Count, x => x.Text == E[w].Target.Text)] = 1;
                }
            }
        }

        private List<List<DataVertex>> fifthCondition(Graph graph)//Вершини, які мають інший шлях до сина
        {
            var V = graph.Vertices.ToList();
            var E = graph.Edges.ToList();

            int[] color = new int[V.Count];
            for (int i = 0; i < V.Count; i++)//Йду по всіх вершина
            {
                var eSonForV = E.FindAll(x => x.Source == V[i]);
                for (int j = 0; j < eSonForV.Count; j++)//Йду по всіх синах поточної вершини
                {
                    for (int k = 0; k < V.Count; k++)
                        color[k] = 1;
                    List<int> cycle = new List<int>();
                    int endV = V.FindIndex(0, V.Count, x => x == eSonForV[j].Target);
                    cycle.Add(i);
                    cycle.Add(endV);

                    DFScycleForFifth(i, endV, E, V, color, -1, cycle, graph);//Шукаю так само як в 4 умові але кінцевою вершиною є не початкова вершина, а її син
                    catalogCycles.RemoveAll(x => x.Count == 2);
                }
            }
            catalogCycles.RemoveAll(x => x.Count == 2);//Видаляю ті, які не знайшли шляху до вершини
            catalogCycles.RemoveAll(x => x.Count > 5);//Видаляю ті, які мають більше 5 елементів(вони всеодно не згорнуться)
            if (catalogCycles.Count == 0)
                return null;
            return catalogCycles;
        }

        private bool mergeVertex(Graph graph, int indexOfGraph, params DataVertex[] list)//Метод для злиття вершин(повертає true, якщо злиття відбулося)
        {
            //Перевірка чи кількість елементів модуля <=5
            int count = 0;
            for (int n = 0; n < calc.moduls[indexOfGraph].Count; n++)
            {
                string namev = "";
                foreach (string s in calc.moduls[indexOfGraph].ElementAt(n))
                {
                    namev += s;
                }
                if (list.Any(x => x.Text == namev))
                {
                    count += calc.moduls[indexOfGraph].ElementAt(n).Count;
                }
            }
            if (count > 5 || list == null) return false;//Якщо модуль буде містити більше 5 елементів, то злиття не проводимо
            /////////////////////////////////////////////
            for (int i = 1; i < list.Length; i++)
            {
                //Видаляю спільні зв'язки
                _gArea.LogicCore.Graph.RemoveEdgeIf(x => x.Source == list[0] && x.Target == list[i]);
                graph.RemoveEdgeIf(x => x.Source == list[0] && x.Target == list[i]);
                _gArea.LogicCore.Graph.RemoveEdgeIf(x => x.Target == list[0] && x.Source == list[i]);
                graph.RemoveEdgeIf(x => x.Target == list[0] && x.Source == list[i]);
                //Зливаю елементи модулей та видаляю злиті
                for (int j = 0; j < calc.moduls[indexOfGraph].Count; j++)
                {
                    string str = "";
                    foreach (string s in calc.moduls[indexOfGraph].ElementAt(j))//Формую ім'я модуля
                    {
                        str += s;
                    }
                    if (str == list[0].Text)//Знаходжу відповідні вершині модулі
                    {
                        //calc.moduls[indexOfGraph].ElementAt(j).Add(list[i].Text);
                        calc.moduls[indexOfGraph].ElementAt(j).AddRange(nameVertexAndModuls[indexOfGraph][list[i].Text]);
                        //Міняю назву ключа(Створюю новий, а старий видаляю)
                        List<string> l = new List<string>();
                        l.AddRange(nameVertexAndModuls[indexOfGraph][list[i].Text]);

                        nameVertexAndModuls[indexOfGraph].Remove(str);
                        nameVertexAndModuls[indexOfGraph].Add(str + list[i].Text, l);
                        //////
                        nameVertexAndModuls[indexOfGraph][str + list[i]] = calc.moduls[indexOfGraph].ElementAt(j);
                    }
                    if (str == list[i].Text)//Видаляю злитий
                    {
                        calc.moduls[indexOfGraph].RemoveAt(j);
                        nameVertexAndModuls[indexOfGraph].Remove(str);
                        //nameVertexAndModuls[indexOfGraph][list[i].Text].Clear();
                        //nameVertexAndModuls[indexOfGraph][list[i].Text].AddRange(calc.moduls[indexOfGraph].ElementAt(j));
                    }
                }
                //Додаю назву вершини до загальної
                list[0].Text += list[i].Text;
                //Перенаправляю всі зв'язки
                var inE = new List<DataEdge>();
                inE.AddRange(graph.InEdges(list[i]));
                foreach (DataEdge e in inE)
                {
                    //e.Target = list[0];
                    //e.Text = string.Format("{0} -> {1}", e.Source, list[0]);

                    var Edge = new DataEdge(e.Source, list[0]) { Text = string.Format("{0} -> {1}", e.Source, list[0]) };
                    _gArea.LogicCore.Graph.AddEdge(Edge);
                    graph.AddEdge(Edge);
                }

                var outE = new List<DataEdge>();
                outE.AddRange(graph.OutEdges(list[i]));
                foreach (DataEdge e in outE)
                {
                    //e.Source = list[0];
                    //e.Text = string.Format("{0} -> {1}", list[0], e.Target);
                    var Edge = new DataEdge(list[0], e.Target) { Text = string.Format("{0} -> {1}", list[0], e.Target) };
                    _gArea.LogicCore.Graph.AddEdge(Edge);
                    graph.AddEdge(Edge);
                }

                //Видаляю вершини
                if (i == list.Count())
                {
                    _gArea.LogicCore.Graph.RemoveVertex(list[0]);
                    graph.RemoveVertex(list[0]);
                }
                _gArea.LogicCore.Graph.RemoveVertex(list[i]);
                graph.RemoveVertex(list[i]);

            }
            return true;
        }

        public void Generate_Click(object sender, EventArgs e)
        {
            GenerateGraph();
            wpfHost.Child = GenerateWpfVisuals();
            mainHost.Child = GenerateWpfVisuals();
            _gArea.GenerateGraph(true, true);
            _gArea.RelayoutGraph();
        }
       
        private void Reload_Click(object sender, EventArgs e)
        {
            _gArea.RelayoutGraph();
        }
       
        public void showGivenGraph(int graphIndex)
        {
            if (graphIndex > calc.groupsAfterV.Count()) 
                return;

            totalGraph.Clear();

            totalGraph.AddVertexRange(graphs.ElementAt(graphIndex).Vertices.ToList());
            totalGraph.AddEdgeRange(graphs.ElementAt(graphIndex).Edges.ToList());

            _gArea.GenerateGraph(true, true);
            _gArea.RelayoutGraph();
        }

        private void createM_Click(object sender, EventArgs e)
        {
            calc.moduls.Clear();

            allGraphs.Clear();
            catalogCycles.Clear();
            nameVertexAndModuls.Clear();

            graphs.Clear();
            totalGraph.Clear();
            ///////////////////////////////
            Generate_Click(sender, e);

            createModuls();

            
            outModuls();

            _gArea.RelayoutGraph();
        }
       
        public void getModulesList(TreeView view)
        {
            if (calc.moduls.Count() == 0)
                createModuls();

            int moduleIndex = 1;
            for (int i = 0; i < calc.moduls.Count; i++)
            {
                view.Nodes.Add("Group " + (i + 1) );

                foreach (List<string> list in calc.moduls[i])
                {
                    string module = "Module " + moduleIndex + ": ";
                    module += String.Join(" ",list);                   
                    view.Nodes[i].Nodes.Add(module);

                    moduleIndex++;
                }
            }
        }

        public void getVModulesList(ListView list_box)
        {
            if (calc.modulsAfterVerification.Count() == 0)
                createVerificationModuls();

            string[] columns = { "Module number", "Module elements" };
            foreach (string column_name in columns)
                list_box.Columns.Add(column_name, 100);


            int index = 1;
            foreach (List<string> list in calc.modulsAfterVerification)
            {
                string module = String.Empty;

                foreach (string j in list)
                {
                    module += j + " ";
                }
                string[] row = { "Module " + index, module};
                var listViewItem = new ListViewItem(row);
                list_box.Items.Add(listViewItem);
                index++;
            }

        }

        private void backG_Click(object sender, EventArgs e)
        {
            if (currentGraph != 0 && calc.moduls.Count != 0)
            {
                currentGraph--;

                totalGraph = allGraphs.ElementAt(currentGraph);
                _gArea.LogicCore.Graph = allGraphs.ElementAt(currentGraph);
                _gArea.GenerateGraph(true, true);
            }
        }
       
        private void nextG_Click(object sender, EventArgs e)
        {
            if (currentGraph != allGraphs.Count - 1 && calc.moduls.Count != 0)
            {
                currentGraph++;

                totalGraph = allGraphs.ElementAt(currentGraph);
                _gArea.LogicCore.Graph = allGraphs.ElementAt(currentGraph);
                _gArea.GenerateGraph(true, true);
            }
        }

        private void outModuls()
        {

            textBox1.Text = "Modules:\n";
            int n = 1;
            textBox1.Text += "\r\n";
            for (int i = 0; i < calc.moduls.Count; i++)
            {
                foreach (List<string> list in calc.moduls[i])
                {
                    if (list.First().StartsWith("Group"))
                    {
                        textBox1.Text += list.First() + " ends" + "\r\n" + "\r\n";
                    }
                    else
                    {
                        textBox1.Text += n + " module {  ";
                        foreach (string j in list)
                        {
                            textBox1.Text += j + "  ";
                        }
                        textBox1.Text += "}\r\n";
                        n++;
                    }

                }
            }
        }
        ////////////////5 Лаба
        public void createVerificationModuls()//Уточнення модулів
        {
            totalGraph = allGraphs.Last();
            //Зливаю назви вершин і модулі в один словник
            nameVertexAndM = new Dictionary<string, List<string>>();


            for (int i = 0; i < nameVertexAndModuls.Count; i++)
            {
                for (int j = 0; j < nameVertexAndModuls[i].Count; j++)
                {
                    try//Ключ, який вже є в словнику не додаємо
                    {
                        nameVertexAndM.Add(nameVertexAndModuls[i].ElementAt(j).Key, nameVertexAndModuls[i].ElementAt(j).Value);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            nameVertexAndM = nameVertexAndM.OrderBy(a => a.Value.Count).ToDictionary(pair => pair.Key, pair => pair.Value);

            //Зливаю модулі для кожної групи в один List
            for (int i = 0; i < calc.moduls.Count; i++)
            {
                foreach (List<string> list in calc.moduls[i])
                {                 
                    calc.modulsAfterVerification.Add(list);
                }
            }
           
            calc.modulsAfterVerification.Sort((a, b) => a.Count - b.Count);//Сортую за кількістю елементів
            //Формую такі самі множини для перевірки підмножин
            List<HashSet<string>> setModuls = new List<HashSet<string>>();
            for (int i = 0; i < calc.modulsAfterVerification.Count; i++)
            {
                setModuls.Add(new HashSet<string>());
                for (int j = 0; j < calc.modulsAfterVerification[i].Count; j++)
                {

                    setModuls[i].Add(calc.modulsAfterVerification[i][j]);
                }
            }
            //Видаляю модулі, які є підмножиною інших модулів
            for (int i = 0; i < calc.modulsAfterVerification.Count - 1; i++)
            {
                for (int j = i + 1; j < calc.modulsAfterVerification.Count; j++)
                {

                    if (setModuls[i].IsSubsetOf(setModuls[j]))//Являється підмножиною?
                    {
                        calc.modulsAfterVerification.RemoveAt(i);
                        setModuls.RemoveAt(i);
                        //////
                        string temp = "";
                        foreach(string str in setModuls[i])
                        {
                            temp += str;
                        }
                        /////

                        totalGraph.RemoveVertex(totalGraph.Vertices.First(x => x.Text == temp));
                        nameVertexAndM.Remove(temp);
                        i--;
                        break;
                    }
                }
            }
            //Видаляю елементи, які є в інших модулях(з більшого)
            for (int i = 0; i < calc.modulsAfterVerification.Count - 1; i++)
            {
                for (int j = 0; j < calc.modulsAfterVerification[i].Count; j++)
                {
                    for (int k = 1; k < calc.modulsAfterVerification.Count; k++)
                    {
                        if (calc.modulsAfterVerification[k].Contains(calc.modulsAfterVerification[i][j]))
                        {
                            if (k == i) continue;
                            ///////////////////
                            if (calc.modulsAfterVerification[k].Count >= calc.modulsAfterVerification[i].Count)
                                calc.modulsAfterVerification[k].Remove(calc.modulsAfterVerification[i][j]);
                            else
                            {
                                calc.modulsAfterVerification[i].Remove(calc.modulsAfterVerification[i][j]);
                                j--;
                            }
                            ///////////////////
                            if (calc.modulsAfterVerification[i].Count == 0) calc.modulsAfterVerification.RemoveAt(i);
                            //////
                            string namev = "";
                            foreach (string s in calc.modulsAfterVerification[k])
                            {
                                namev += s;
                            }
                            totalGraph.Vertices.ElementAt(k).Text = namev;
                        }
                    }
                }
            }
             //Перезаписую назви вершин і відповідні їм модулі
            nameVertexAndM.Clear();

            foreach(List<string> list in calc.modulsAfterVerification)
            {
                string key = ""; 
                foreach(string name in list)
                   {
                       key += name;
                   }
                nameVertexAndM.Add(key, list);
            }
            /////////////////////////////////////////////
            totalGraph.RemoveEdgeIf(x => x.Text != "");//Видаляю всі зв'язки
            //Розтавляю нові вершини
            totalGraph.RemoveVertexIf(x => x.Text != "");

            foreach(string name in nameVertexAndM.Keys)
            {
                totalGraph.AddVertex(new DataVertex(name));
            }
            
        }

        private void modulsV_Click(object sender, EventArgs e)
        {
            calc.modulsAfterVerification.Clear();
            createM_Click(sender, e);
            createVerificationModuls();
            outVModuls();
            _gArea.GenerateGraph(true, true);//Перемальовую граф
            _gArea.RelayoutGraph();
        }
       
        private void outVModuls()
        {
            textBox2.Text = "Specified modules:\n";
            int n = 1;
            textBox2.Text += "\r\n";

            foreach (List<string> list in calc.modulsAfterVerification)
            {
                textBox2.Text += n + " module {  ";
                foreach (string j in list)
                {
                    textBox2.Text += j + "  ";
                }
                textBox2.Text += "}\r\n";
                n++;

            }
        }  
        ////////////6 Лаба
        private List<List<string>> findFirstAndLast()
        {
           
            List<string> listEl = new List<string>();
            for (int i = 0; i < calc.mas.Count; i++ )//Записую перші елементи кожного модуля
            {
                listEl.Add(calc.mas[i].First());
            }

            string elementFirst = listEl[0];//Елемент, який найчастіше є першим
            
            int elCount = 0;
            foreach(string el in listEl)
            {

                if (listEl.FindAll(x => x == el).Count > elCount)
                {
                    elCount = listEl.FindAll(x => x == el).Count;
                    elementFirst = el;
                }
            }
            List<string> first = calc.modulsAfterVerification.Find(x => x.Contains(elementFirst));
            /////////////////////////////////////////
            listEl.Clear();
            for (int i = 0; i < calc.mas.Count; i++ )//Записую останні елементи кожного модуля
            {
                listEl.Add(calc.mas[i].Last());
            }
            string elementLast = listEl[0];//Елемент, який найчастіше є останнім

            elCount = 0;
            foreach (string el in listEl)
            {

                if (listEl.FindAll(x => x == el).Count > elCount && first.Contains(el) == false)
                {
                    elCount = listEl.FindAll(x => x == el).Count;
                    elementLast = el;
                }
            }

            List<string> last = calc.modulsAfterVerification.Find(x => x.Contains(elementLast));
           /////////////////////
            
            List<List<string>> result = new List<List<string>>();
            result.Add(first);
            result.Add(last);

            return result;
        }

        private void genEdges(List<string> first, List<string> last)
        {
            ///////Поставлю модулі на відповідні місця 
            List<List<string>> firstOrder = new List<List<string>>();
            firstOrder.Add(first);
            firstOrder.Add(last);

            foreach (List<string> list in calc.modulsAfterVerification)
            {
                if (list != first && list != last)
                    firstOrder.Insert(1, list);
            }
            //Проставляю зв'язки
            createEdgesAndFindMinFeedBack(firstOrder);
            //Виводжу результати створення зв'язків і уточнені модулі з першою та останньою групою
            outVModulsFrom(firstOrder);
            textBox2.Text += "\r\nКількість зворотніх зв'язків: " + createEdgesAndFindMinFeedBack(firstOrder);
            _gArea.GenerateGraph(true, true);//Перемальовую граф
        }

        private void outVModulsFrom(List<List<string>> source)
        {
            textBox2.Text = "Уточнені модулі:\n";
            int n = 1;
            textBox2.Text += "\r\n";

            foreach (List<string> list in source)
            {
                textBox2.Text += n + " модуль {  ";
                foreach (string j in list)
                {
                    textBox2.Text += j + "  ";
                }
                textBox2.Text += "}\r\n";
                n++;

            }
        }

        private int createEdgesAndFindMinFeedBack(List<List<string>> list)
        {
            int feedback = 0;
            ///////////
            totalGraph.RemoveEdgeIf(x => x.Text != "");
            totalGraph.RemoveVertexIf(x => x.Text != "");
            ///////////////
            for (int i = list.Count - 1; i >= 0; i--)
            {
                string str = "";
                foreach (string name in list[i])
                {
                    str += name;
                }
                totalGraph.AddVertex(new DataVertex(str));
            }
            ///////////
            foreach(HashSet<string> row in calc.mas)
            {
                for (int i = 0; i < row.Count-1; i++)
                {
                    string nameV1 = nameVertexAndM.First(x => x.Value.Contains(row.ElementAt(i))).Key;
                    string nameV2 = nameVertexAndM.First(x => x.Value.Contains(row.ElementAt(i+1))).Key;

                    int indexV1 = list.FindIndex(x => x.Contains(row.ElementAt(i)));
                    int indexV2 = list.FindIndex(x => x.Contains(row.ElementAt(i + 1)));
                    if(nameV1 != nameV2)
                    {
                        var Edge = new DataEdge(totalGraph.Vertices.First(x => x.Text == nameV1), totalGraph.Vertices.First(x => x.Text == nameV2)) 
                        { Text = string.Format("{0} -> {1}", totalGraph.Vertices.First(x => x.Text == nameV1), totalGraph.Vertices.First(x => x.Text == nameV2)) };

                        //Перевіряю чи є вже таке ребро
                        DataEdge temp = new DataEdge();
                        if (totalGraph.TryGetEdge(Edge.Source, Edge.Target, out temp) == false)
                        {
                            totalGraph.AddEdge(Edge);
                            if (indexV1 > indexV2)
                            {
                                feedback++;
                            }
                        }
                    }
                }
            }
            return feedback;
        }

        private void relationsWithM_Click(object sender, EventArgs e)
        {
            if (calc.modulsAfterVerification.Count != 0)
            {
                findFirstAndLast();
                genEdges(findFirstAndLast()[0], findFirstAndLast()[1]);
                _gArea.RelayoutGraph();
            }
        }
        ///////////////////////////////////////////////
        private void verificationRelations_Click(object sender, EventArgs e)
        {
            if (calc.modulsAfterVerification.Count != 0)
            {
                relationsWithM_Click(sender, e);

                List<List<string>> temp = new List<List<string>>();
                temp.AddRange(calc.modulsAfterVerification);
                //temp.Remove(findFirstAndLast()[0]);
                //temp.Remove(findFirstAndLast()[1]);

                //Всі перестановки
                var permutationsP = new Variations<List<string>>(temp, temp.Count);//Всі перестановки


                int feedback = 1000;
                List<List<string>> permutatiOptimal = new List<List<string>>();
                List<List<string>> permutation = new List<List<string>>();

                foreach (var p in permutationsP)
                {
                    //permutation.Add(findFirstAndLast()[0]);
                    permutation.AddRange(p);
                    //permutation.Add(findFirstAndLast()[1]);

                    if (createEdgesAndFindMinFeedBack(permutation) < feedback)
                    {
                        feedback = createEdgesAndFindMinFeedBack(permutation);
                        permutatiOptimal.Clear();
                        permutatiOptimal.AddRange(permutation);
                    }
                    permutation.Clear();
                }
                /////////////////////
                createEdgesAndFindMinFeedBack(permutatiOptimal);
                outVModulsFrom(permutatiOptimal);
                textBox2.Text += "\r\nКількість зворотніх зв'язків: " + feedback;
                _gArea.RelayoutGraph();
            }
        }
        //private int findMinFeedBack(List<List<string>> list)
        //{
        //    int feedback = 0;
        //    foreach (HashSet<string> row in calc.mas)
        //    {
        //        for (int i = 0; i < row.Count - 1; i++)
        //        {
        //            int indexV1 = list.FindIndex(x => x.Contains(row.ElementAt(i)));
        //            int indexV2 = list.FindIndex(x => x.Contains(row.ElementAt(i + 1)));

        //            if (indexV1 > indexV2)
        //            {
        //                string nameV1 = nameVertexAndM.First(x => x.Value.Contains(row.ElementAt(i))).Key;
        //                string nameV2 = nameVertexAndM.First(x => x.Value.Contains(row.ElementAt(i + 1))).Key;

        //                if (nameV1 != nameV2)
        //                {
        //                    var Edge = new DataEdge(totalGraph.Vertices.First(x => x.Text == nameV1), totalGraph.Vertices.First(x => x.Text == nameV2)) { Text = string.Format("{0} -> {1}", totalGraph.Vertices.First(x => x.Text == nameV1), totalGraph.Vertices.First(x => x.Text == nameV2)) };

        //                    //Перевіряю чи є вже таке ребро
        //                    DataEdge temp = new DataEdge();
        //                    if (totalGraph.TryGetEdge(Edge.Source, Edge.Target, out temp) == false)
        //                    {
        //                        feedback++;                               
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    return feedback;
        //}

        private void stringToModule()
        {
            modules.Clear();

            foreach(List<string> module in calc.modulsAfterVerification)
            {
                string moduleName = string.Join(" ", module);
                modules.Add(new Module(moduleName));
            }


            operationMatrix.Clear();

            foreach(HashSet<string> set in calc.mas)
            {
                operationMatrix.Add(set.ToList());
            }
        }

        public void setSrtucture()
        {
            stringToModule();

            links.Clear();
            foreach (Module m in modules)
            {
                links.Add(new List<int>());
            }
            for (int i = 0; i < operationMatrix.Count; i++)
            {
                int start = modules.FindIndex(x => x.Name.Contains(operationMatrix[i][0]));
                for (int j = 1; j < operationMatrix[i].Count; j++)
                {
                    int next = modules.FindIndex(x => x.Name.Contains(operationMatrix[i][j]));
                    if (start != next && !links[start].Exists(x => x == next))
                    {
                        links[start].Add(next);
                    }
                }
            }
            List<List<int>> structuredModules = new List<List<int>>();
            for (int i = 0; i < modules.Count; i++)
            {
                if (links[i].Count > 0)
                    if (structuredModules.Count > 0)
                        for (int j = 0; j < structuredModules.Count; j++)
                        {
                            bool isLinked = false;
                            foreach (int k in links[i])
                                if (structuredModules.Exists(x => x.Exists(s => s == k)))
                                    isLinked = true;
                            if (structuredModules.Exists(x => x.Exists(s => s == i)))
                                isLinked = true;
                            if (!isLinked)
                            {
                                structuredModules.Add(new List<int>());
                                structuredModules.Last().Add(i);
                                foreach (int k in links[i])
                                    structuredModules.Last().Add(k);
                                break;
                            }
                            else
                            {
                                List<int> temp = new List<int>();
                                foreach (int k in links[i])
                                    for (int m = 0; m < structuredModules.Count; m++)
                                    {
                                        bool isConnected = false;
                                        for (int n = 0; n < structuredModules[m].Count; n++)
                                        {
                                            if (structuredModules[m][n] == k || structuredModules[m][n] == i)
                                            {
                                                isConnected = true;
                                            }
                                        }
                                        if (isConnected && !temp.Exists(x => x == m))
                                        {
                                            temp.Add(m);
                                        }
                                    }
                                if (temp.Count > 0)
                                {
                                    if (!structuredModules[temp.First()].Exists(x => x == i))
                                        structuredModules[temp.First()].Add(i);
                                    for (int m = 1; m < temp.Count; m++)
                                    {
                                        foreach (int item in structuredModules[temp[m]])
                                            if (!structuredModules[temp.First()].Exists(x => x == item))
                                                structuredModules[temp.First()].Add(item);
                                    }
                                    for (int m = temp.Count - 1; m >= 1; m--)
                                    {
                                        structuredModules.RemoveAt(temp[m]);
                                    }
                                    foreach (int k in links[i])
                                        if (!structuredModules[temp.First()].Exists(x => x == k))
                                            structuredModules[temp.First()].Add(k);
                                }
                            }
                        }
                    else
                    {
                        structuredModules.Add(new List<int>());
                        structuredModules.Last().Add(i);
                        foreach (int k in links[i])
                            structuredModules.Last().Add(k);
                    }
            }

            OptimizeStructure(structuredModules);
            drawStructures();
        }

        private void OptimizeStructure(List<List<int>> structuredModules)
        {
            int[] indexes = new int[modules.Count];
            List<Module> firsts = new List<Module>(), lasts = new List<Module>();
            FindFirstAndLastModules(ref firsts, ref lasts, structuredModules);
            int n = 0;
            for (int i = 0; i < structuredModules.Count; i++)
            {
                indexes[n] = modules.FindIndex(x => x == firsts[i]);
                n++;
                foreach (int j in structuredModules[i])
                {
                    if (j != modules.FindIndex(x => x == firsts[i]) && j != modules.FindIndex(x => x == lasts[i]))
                    {
                        indexes[n] = j;
                        n++;
                    }
                }
                indexes[n] = modules.FindIndex(x => x == lasts[i]);
                n++;
            }
            basicStructure = indexes.ToList();
            int[] optimizedIndexes = (int[])indexes.Clone();
            int feedbackCount = FindAmountOfFeedback(links, indexes);
            int start = 0, end = -1;
            int maxIter = 1000000;
            for (int i = 0; i < structuredModules.Count; i++)
            {
                start = end + 1;
                end += structuredModules[i].Count;
                int iter = 0;
                if (end - start > 1)
                    while (Next(ref indexes, start + 1, end - 1))
                    {
                        if (iter > maxIter)
                            break;
                        else
                            iter++;
                        int nextFeedbackCount = FindAmountOfFeedback(links, indexes);
                        if (nextFeedbackCount <= feedbackCount)
                        {
                            feedbackCount = nextFeedbackCount;
                            optimizedIndexes = (int[])indexes.Clone();
                        }
                    }
            }
            optimizedStructure = optimizedIndexes.ToList();
        }

        private void drawStructures()
        {
            List<Module> temp = new List<Module>();
            for (int i = 0; i < basicStructure.Count; i++)
                temp.Add(modules[basicStructure[i]]);
            getGraphics(temp, lines1, arcs1, textGraphics1);
            temp.Clear();
            for (int i = 0; i < optimizedStructure.Count; i++)
                temp.Add(modules[optimizedStructure[i]]);
            getGraphics(temp, lines, arcs, textGraphics);
        }

        private void FindFirstAndLastModules(ref List<Module> first, ref List<Module> last, List<List<int>> structuredModules)
        {
            List<int> repeatsFirst = new List<int>();
            List<int> repeatsLast = new List<int>();
            foreach (List<int> structure in structuredModules)
            {
                repeatsFirst.Clear();
                repeatsLast.Clear();
                foreach (int m in structure)
                {
                    repeatsFirst.Add(0);
                    repeatsLast.Add(0);
                }
                List<Module> temp = new List<Module>();
                for (int m = 0; m < structure.Count; m++)
                {
                    temp.Add(modules[structure[m]]);
                }
                for (int i = 0; i < operationMatrix.Count; i++)
                {
                    bool isIncluded = true;
                    foreach (string str in operationMatrix[i])
                    {
                        if (!temp.Exists(x => x.Name.Contains(str)))
                        {
                            isIncluded = false;
                            break;
                        }
                    }

                    if (isIncluded)
                    {
                        repeatsFirst[structure.FindIndex(s => s == modules.FindIndex(x => x.Name.Contains(operationMatrix[i][0])))]++;
                        repeatsLast[structure.FindIndex(s => s == modules.FindIndex(x => x.Name.Contains(operationMatrix[i][operationMatrix[i].Count - 1])))]++;
                    }
                }
                int firstId = repeatsFirst.FindIndex(x => x == repeatsFirst.Max());
                int lastId = repeatsLast.FindIndex(x => x == repeatsLast.Max());
                if (firstId == lastId)
                {
                    repeatsLast[repeatsLast.FindIndex(x => x == repeatsLast.Max())] = -1;
                    lastId = repeatsLast.FindIndex(x => x == repeatsLast.Max());
                }
                first.Add(temp[firstId]);
                last.Add(temp[lastId]);
            }
        }

        private int FindAmountOfFeedback(List<List<int>> arr, int[] indexes)
        {
            int feedbackCount = 0;
            if (indexes.Length != arr.Count)
                return -1;
            for (int i = 0; i < arr.Count; ++i)
            {
                for (int j = 0; j < arr[indexes[i]].Count; ++j)
                {
                    if (arr[indexes[i]][j] < indexes[i])
                    {
                        ++feedbackCount;
                    }
                }
            }
            return feedbackCount;
        }

        bool Next(ref int[] arr, int start, int end)
        {
            int k, j, l;
            for (j = end - 1; (j >= start) && (arr[j] >= arr[j + 1]); j--) { }
            if (j == start - 1)
            {
                arr = arr.OrderBy(c => c).ToArray();
                return false;
            }
            for (l = end; (arr[j] >= arr[l]) && (l >= start); l--) { }
            var tmp = arr[j];
            arr[j] = arr[l];
            arr[l] = tmp;
            for (k = j + 1, l = end; k < l; k++, l--)
            {
                tmp = arr[k];
                arr[k] = arr[l];
                arr[l] = tmp;
            }
            return true;
        }

        private void getGraphics(List<Module> structure, List<Line> lines, List<Arc> arcs, List<TextGraphics> textGraphics)
        {
            lines.Clear();
            textGraphics.Clear();
            arcs.Clear();
            SetInputAndOutputStreams();
            int panelWidth = (panel1.Width > panel2.Width) ? panel1.Width : panel2.Width;
            int panelHeight = (panel1.Height > panel2.Height) ? panel1.Height : panel2.Height;
            int step = (int)(panelWidth / (structure.Count * 2 + 1));
            for (int i = 1; i <= structure.Count; i++)
            {
                if (i == 1)
                {
                    setRectangle(i * step, panelHeight / 2, step, step, lines);
                    textGraphics.Add(new TextGraphics(structure[i - 1].Name, new Font("TimesNewRoman", step / structure[i - 1].Name.Length), new Vector(i * step, panelHeight / 2 + step / 2 - step / structure[i - 1].Name.Length / 2)));
                    for (int m = 0; m < links[modules.FindIndex(x => x == structure[i - 1])].Count; m++)
                    {
                        int next = structure.FindIndex(s => s == modules[links[modules.FindIndex(x => x == structure[i - 1])][m]]);
                        if ((next - (i - 1)) > 0)
                        {
                            int x = i * step + step / 2,
                                y = panelHeight / 2 - (m + 1) * step / 2,
                                width = 2 * step * (next - (i - 1)),
                                height = (m + 1) * step;
                            arcs.Add(new Arc(new Rectangle(x, y, width, height), 0, -180));
                        }
                        else
                        {
                            int x = i * step + step / 2 - ((i - 1) - next) * step * 2,
                                y = panelHeight / 2 - (m + 1) * step / 2 + step,
                                width = 2 * step * ((i - 1) - next),
                                height = (m + 1) * step;
                            arcs.Add(new Arc(new Rectangle(x, y, width, height), 0, 180));
                        }
                    }
                    if (inputStream[modules.FindIndex(x => x == structure[i - 1])].Count != 0)
                    {
                        string str = "";
                        for (int m = 0; m < inputStream[modules.FindIndex(x => x == structure[i - 1])].Count; m++)
                        {
                            str += inputStream[modules.FindIndex(x => x == structure[i - 1])][m] + ",";
                        }
                        textGraphics.Add(new TextGraphics(str, new Font("TimesNewRoman", 8), new Vector(i * step + step / 2, panelHeight / 8 + i * 9), Color.Red));
                        lines.Add(new Line(new Vector(i * step + step / 2, panelHeight / 8 + i * 9 + 9), new Vector(i * step + step / 2, panelHeight / 2), Color.DarkCyan));
                    }
                    if (outputStream[modules.FindIndex(x => x == structure[i - 1])].Count != 0)
                    {
                        string str = "";
                        for (int m = 0; m < outputStream[modules.FindIndex(x => x == structure[i - 1])].Count; m++)
                        {
                            str += outputStream[modules.FindIndex(x => x == structure[i - 1])][m] + ",";
                        }
                        textGraphics.Add(new TextGraphics(str, new Font("TimesNewRoman", 8), new Vector(i * step + step / 2, 6 * panelHeight / 8 + i * 9), Color.Red));
                        lines.Add(new Line(new Vector(i * step + step / 2, panelHeight / 2 + step), new Vector(i * step + step / 2, 6 * panelHeight / 8 + i * 9 - 9), Color.DarkCyan));
                    }
                }
                else
                {
                    setRectangle((i - 1) * step * 2 + step, panelHeight / 2, step, step, lines);
                    textGraphics.Add(new TextGraphics(structure[i - 1].Name, new Font("TimesNewRoman", step / structure[i - 1].Name.Length), new Vector((i - 1) * step * 2 + step, panelHeight / 2 + step / 2 - step / structure[i - 1].Name.Length / 2)));
                    for (int m = 0; m < links[modules.FindIndex(x => x == structure[i - 1])].Count; m++)
                    {
                        int next = structure.FindIndex(s => s == modules[links[modules.FindIndex(x => x == structure[i - 1])][m]]);
                        if ((next - (i - 1)) > 0)
                        {
                            int x = (i - 1) * step * 2 + step + step / 2,
                                y = panelHeight / 2 - (m + 1) * step / 2,
                                width = 2 * step * (next - (i - 1)),
                                height = (m + 1) * step;
                            arcs.Add(new Arc(new Rectangle(x, y, width, height), 0, -180));
                        }
                        else
                        {
                            int x = (i - 1) * step * 2 + step + step / 2 - ((i - 1) - next) * step * 2,
                                y = panelHeight / 2 - (m + 1) * step / 2 + step,
                                width = 2 * step * ((i - 1) - next),
                                height = (m + 1) * step;
                            arcs.Add(new Arc(new Rectangle(x, y, width, height), 0, 180));
                        }
                    }
                    if (inputStream[modules.FindIndex(x => x == structure[i - 1])].Count != 0)
                    {
                        string str = "";
                        for (int m = 0; m < inputStream[modules.FindIndex(x => x == structure[i - 1])].Count; m++)
                        {
                            str += inputStream[modules.FindIndex(x => x == structure[i - 1])][m] + ",";
                        }
                        textGraphics.Add(new TextGraphics(str, new Font("TimesNewRoman", 8), new Vector((i - 1) * step * 2 + step + step / 2, panelHeight / 8 + i * 9), Color.Red));
                        lines.Add(new Line(new Vector((i - 1) * step * 2 + step + step / 2, panelHeight / 8 + i * 9 + 9), new Vector((i - 1) * step * 2 + step + step / 2, panelHeight / 2), Color.DarkCyan));
                    }
                    if (outputStream[modules.FindIndex(x => x == structure[i - 1])].Count != 0)
                    {
                        string str = "";
                        for (int m = 0; m < outputStream[modules.FindIndex(x => x == structure[i - 1])].Count; m++)
                        {
                            str += outputStream[modules.FindIndex(x => x == structure[i - 1])][m] + ",";
                        }
                        textGraphics.Add(new TextGraphics(str, new Font("TimesNewRoman", 8), new Vector((i - 1) * step * 2 + step + step / 2, 6 * panelHeight / 8 + i * 9), Color.Red));
                        lines.Add(new Line(new Vector((i - 1) * step * 2 + step + step / 2, panelHeight / 2 + step), new Vector((i - 1) * step * 2 + step + step / 2, 6 * panelHeight / 8 + i * 9 - 9), Color.DarkCyan));
                    }
                }
            }
        }

        private void SetInputAndOutputStreams()
        {
            inputStream.Clear();
            outputStream.Clear();
            for (int i = 0; i < modules.Count; i++)
            {
                inputStream.Add(new List<int>());
                outputStream.Add(new List<int>());
            }
            for (int i = 0; i < operationMatrix.Count; i++)
            {
                inputStream[modules.FindIndex(x => x.Name.Contains(operationMatrix[i][0]))].Add(i + 1);
                outputStream[modules.FindIndex(x => x.Name.Contains(operationMatrix[i][operationMatrix[i].Count - 1]))].Add(i + 1);
            }
        }

        private void setRectangle(int v1, int v2, int s1, int s2, List<Line> lines)
        {
            lines.Add(new Line(new Vector(v1, v2), new Vector(v1 + s1, v2)));
            lines.Add(new Line(new Vector(v1 + s1, v2), new Vector(v1 + s1, v2 + s2)));
            lines.Add(new Line(new Vector(v1 + s1, v2 + s2), new Vector(v1, v2 + s2)));
            lines.Add(new Line(new Vector(v1, v2 + s2), new Vector(v1, v2)));
        }

    }
}

