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

namespace Lab1
{
    public partial class GraphVisualizer : Form
    {

        ///////////4 Лаба
        public GraphVisualizer(Calculation calc)
        {
            InitializeComponent();
            this.calc = calc;
        }

        private ZoomControl _zoomctrl;
        private GraphArea _gArea;

        private Calculation calc;
        private List<Graph> graphs;//Лист графів для кожної групи
        private Graph totalGraph;//Загальний граф для відображення
        private List<List<DataVertex>> catalogCycles = new List<List<DataVertex>>();//Зберігає всі знайдуні цикли(для 4 і 5 умови)
        private List<Dictionary<string, List<string>>> nameVertexAndModuls = new List<Dictionary<string, List<string>>>();//Зберігає ім'я вершини(key) і відповідні їй модулі(value)

        private List<Graph> allGraphs = new List<Graph>();//Лист для зберігання проміжних графів(щоб можна було переглядати покроково)
        private int currentGraph;//Номер проміжного графа в allGraphs, який відображається
        private void GraphVisualizer_Load(object sender, EventArgs e)
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

            logic.Graph = GenerateGraph();

            logic.Graph = totalGraph;

            logic.Graph = GenerateGraph();

            logic.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.LinLog;
            logic.DefaultLayoutAlgorithmParams = logic.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.LinLog);
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


        private Graph GenerateGraph()//Створює графи на основі уточнених груп
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

                
                    for (int j = 0; j < calc.groupsAfterV[i].Count; j++)//Формування ребер
                    {
                        for(int t = 0; t < calc.mas[calc.groupsAfterV[i].ElementAt(j)].Count-1; t++)
                        {
                            var dataVertex1 = new DataVertex(calc.mas[calc.groupsAfterV[i].ElementAt(j)].ElementAt(t));
                            var dataVertex2 = new DataVertex(calc.mas[calc.groupsAfterV[i].ElementAt(j)].ElementAt(t+1));

                            var Edge = new DataEdge(vlist.Find(x => x.Text == dataVertex1.Text), vlist.Find(x => x.Text == dataVertex2.Text)) 
                            { Text = string.Format("{0} -> {1}", vlist.Find(x => x.Text == dataVertex1.Text), vlist.Find(x => x.Text == dataVertex2.Text)) };

                            //Перевіряю чи є вже таке ребро
                            DataEdge temp = new DataEdge();
                            if (Graph.TryGetEdge(Edge.Source, Edge.Target, out temp) == false)
                                Graph.AddEdge(Edge);
                            
                        }
                        
                    }
                    graphs.Add(Graph);
            }
                totalGraph = new Graph();//Граф для відображення(об'єднує всі графи)
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
                var vert =  graphs[i].Vertices.ToList();
                calc.moduls.Add(i, new List<List<string>>());
                
                foreach(DataVertex v in vert)
                {
                    var modul = new List<string>();
                    modul.Add(v.Text);

                    calc.moduls[i].Add(modul);

                    nameVertexAndModul.Add(v.Text, new List<string>(modul));//Зв'язую назву вершини графа з модулями, які вона містить
                }
                nameVertexAndModuls.Add(nameVertexAndModul);
            }
            //Test
            //mergeVertex(graphs[0], 0, graphs[0].Vertices.First(), graphs[0].Vertices.Last());
           //Повна згортка графів          
            for (int i = 0; i < graphs.Count; i++ )
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
                    
                } while(flag);
            }
            //////////////////////////////
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
            for (int i = 0; i < vertices.Count; i++ )//Йду по вершинах
            {
                var target =  edges.FindAll(x => x.Source == vertices[i]);//Всі сини для даної вершини
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
        private void DFScycle(int u, int endV, List<DataEdge> E,  List<DataVertex> V, int[] color, int unavailableEdge, List<int> cycle)//Пошук в глибину(http://vscode.ru/prog-lessons/poisk-elementarnyih-tsiklov-v-grafe.html)
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
                for (int i = 1; i < cycle.Count-1; i++)
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
                    for (int i = 1; i < cycle.Count-1; i++)
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
                else if (color[V.FindIndex(0, V.Count, x => x.Text == E[w].Source.Text)] == 1 && V.FindIndex(0, V.Count, x => x.Text == E[w].Target.Text) == u)
                {
                    List<int> cycleNEW = new List<int>(cycle);
                    cycleNEW.Add(V.FindIndex(0, V.Count, x => x.Text == E[w].Source.Text));
                    DFScycle(V.FindIndex(0, V.Count, x => x.Text == E[w].Source.Text), endV, E, V, color, w, cycleNEW);
                    color[V.FindIndex(0, V.Count, x => x.Text == E[w].Source.Text)] = 1;
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

                    DFScycle(i, endV, E, V, color, -1, cycle);//Шукаю так само як в 4 умові але кінцевою вершиною є не початкова вершина, а її син
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
            for(int i = 1; i < list.Length; i++)
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
                        nameVertexAndModuls[indexOfGraph].Add(str+list[i].Text, l);                       
                        //////
                        nameVertexAndModuls[indexOfGraph][str + list[i]] = calc.moduls[indexOfGraph].ElementAt(j);
                    }
                    if(str == list[i].Text)//Видаляю злитий
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
                foreach(DataEdge e in inE)
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
                if(i == list.Count())
                {
                    _gArea.LogicCore.Graph.RemoveVertex(list[0]);
                    graph.RemoveVertex(list[0]); 
                }
                _gArea.LogicCore.Graph.RemoveVertex(list[i]); 
                graph.RemoveVertex(list[i]); 
                
            }
            return true;
        }


        private void Generate_Click(object sender, EventArgs e)
        {
            GenerateGraph();
            wpfHost.Child = GenerateWpfVisuals();
        }


        private void Reload_Click(object sender, EventArgs e)
        {
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

            Generate_Click(sender, e);
            
            createModuls();
            outModuls();
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

        ////////////////5 Лаба
        public void createVerificationModuls()//Уточнення модулів
        {
            totalGraph = allGraphs.Last();
            //Зливаю назви вершин і модулі в один словник
            Dictionary<string, List<string>> nameVertexAndM = new Dictionary<string, List<string>>();
            
            
            for(int i = 0; i < nameVertexAndModuls.Count; i++)
            {
                for (int j = 0; j <  nameVertexAndModuls[i].Count; j++)
                {
                    try//Ключ, який вже є в словнику не додаємо
                    {
                        nameVertexAndM.Add(nameVertexAndModuls[i].ElementAt(j).Key, nameVertexAndModuls[i].ElementAt(j).Value);
                    }
                    catch(Exception)
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
            calc.modulsAfterVerification = calc.modulsAfterVerification.Distinct().ToList();
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
                for (int j = i+1; j < calc.modulsAfterVerification.Count; j++)
                {

                    if (setModuls[i].IsSubsetOf(setModuls[j]))//Являється підмножиною?
                    {
                        calc.modulsAfterVerification.RemoveAt(i);
                        setModuls.RemoveAt(i);
                        totalGraph.RemoveVertex(totalGraph.Vertices.First(x => x.Text == nameVertexAndM.ElementAt(i).Key));
                        nameVertexAndM.Remove(nameVertexAndM.ElementAt(i).Key);
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
                            calc.modulsAfterVerification[k].Remove(calc.modulsAfterVerification[i][j]);
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
            totalGraph.RemoveEdgeIf(x => x.Text != "");
        }

        private void modulsV_Click(object sender, EventArgs e)
        {
            calc.modulsAfterVerification.Clear();
            createM_Click(sender, e);
            createVerificationModuls();
            outVModuls();
            _gArea.GenerateGraph(true, true);//Перемальовую граф
        }

        private void outModuls()
        {
            label1.Text = "Модулі:\n";
            int n = 1;
            label1.Text += "\n";
            for (int i = 0; i < calc.moduls.Count; i++)
            {
                foreach (List<string> list in calc.moduls[i])
                {
                    label1.Text += n + " модуль {  ";
                    foreach (string j in list)
                    {
                        label1.Text += j + "  ";
                    }
                    label1.Text += "}\n";
                    n++;

                }
            }
        }

        private void outVModuls()
        {
            label2.Text = "Уточнені модулі:\n";
            int n = 1;
            label2.Text += "\n";
           
                foreach (List<string> list in calc.modulsAfterVerification)
                {
                    label2.Text += n + " модуль {  ";
                    foreach (string j in list)
                    {
                        label2.Text += j + "  ";
                    }
                    label2.Text += "}\n";
                    n++;

                }
            
        }

    }
}
