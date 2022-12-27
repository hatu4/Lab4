using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FiguresLibrary;

namespace Lab4
{
    public partial class Form1 : Form
    {
        private Stack<Operator> operators = new Stack<Operator>();
        private Stack<Operand> operands = new Stack<Operand>();
        private List<Triangle_points> tri_points = new List<Triangle_points>();

        int tri_count = 0;
        public Form1()
        {
            InitializeComponent();
            Init.bitmap = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            Init.pen = new Pen(Color.Black, 3);
            Init.pictureBox = pictureBox1;
        }

        private void textBoxInputString_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                operators.Clear();
                operands.Clear();
                try
                {
                    string sourceExpression = textBoxInputString.Text.Replace(" ", "");
                    for (int i = 0; i < sourceExpression.Length; i++)
                    {
                        if (IsNotOperation(sourceExpression[i]))
                        {
                            if (!Char.IsDigit(sourceExpression[i]))
                            {
                                operands.Push(new Operand(sourceExpression[i]));
                                while (i < sourceExpression.Length - 1 && IsNotOperation(sourceExpression[i + 1]))
                                {
                                    string temp_str = operands.Pop().value.ToString() + sourceExpression[i + 1].ToString();
                                    operands.Push(new Operand(temp_str));
                                    i++;
                                }
                            }
                            else if (Char.IsDigit(sourceExpression[i]))
                            {
                                operands.Push(new Operand(sourceExpression[i].ToString()));
                                while (i < sourceExpression.Length - 1 && Char.IsDigit(sourceExpression[i + 1])
                                    && IsNotOperation(sourceExpression[i + 1]))
                                {
                                    int temp_num = Convert.ToInt32(operands.Pop().value.ToString()) * 10 +
                                        (int)Char.GetNumericValue(sourceExpression[i + 1]);
                                    operands.Push(new Operand(temp_num.ToString()));
                                    i++;
                                }
                            }
                        }

                        else if (sourceExpression[i] == 'A')
                        {
                            if (operators.Count == 0)
                            {
                                operators.Push(OperatorContainer.FindOperator(sourceExpression[i]));
                            }
                        }
                        else if (sourceExpression[i] == 'T')
                        {
                            if (operators.Count == 0)
                            {
                                operators.Push(OperatorContainer.FindOperator(sourceExpression[i]));
                            }
                        }
                        else if (sourceExpression[i] == 'M')
                        {
                            if (operators.Count == 0)
                            {
                                operators.Push(OperatorContainer.FindOperator(sourceExpression[i]));
                            }
                        }
                        else if (sourceExpression[i] == 'D')
                        {
                            if (operators.Count == 0)
                            {
                                operators.Push(OperatorContainer.FindOperator(sourceExpression[i]));
                            }
                        }
                        else if (sourceExpression[i] == '(')
                        {
                            operators.Push(OperatorContainer.FindOperator(sourceExpression[i]));
                        }
                        else if (sourceExpression[i] == ')')
                        {
                            do
                            {
                                if (operators.Peek().symbolOperator == '(')
                                {
                                    operators.Pop();
                                    break;
                                }
                                if (operators.Count == 0)
                                {
                                    break;
                                }
                            }
                            while (operators.Peek().symbolOperator != '(');
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Параметры введены некорректно.");
                    comboBox1.Items.Add("Параметры введены некорректно.");
                }
                try
                {
                    SelectingPerformingOperation(operators.Peek());
                }
                catch
                {
                    MessageBox.Show("Введенной операции не существует.");
                    comboBox1.Items.Add("Введенной операции не существует.");
                }
            }
        }
        private void SelectingPerformingOperation(Operator op)
        {
            if (op.symbolOperator == 'A')
            {
                if (operands.Count == 7)
                {
                    int y3 = Convert.ToInt32(operands.Pop().value.ToString());
                    int x3 = Convert.ToInt32(operands.Pop().value.ToString());
                    int y2 = Convert.ToInt32(operands.Pop().value.ToString());
                    int x2 = Convert.ToInt32(operands.Pop().value.ToString());
                    int y1 = Convert.ToInt32(operands.Pop().value.ToString());
                    int x1 = Convert.ToInt32(operands.Pop().value.ToString());
                    string name = operands.Pop().value.ToString();
                    Triangle_points points = new Triangle_points(name, x1, x2, x3, y1, y2, y3);
                    tri_points.Add(points);
                    comboBox1.Items.Add($"Массив точек {name} добавлен.");
                }
                else
                {
                    MessageBox.Show("Опертор A принимает 7 параметров.");
                    comboBox1.Items.Add("Неверное число параметров для оператора A.");
                }
            }
            if (op.symbolOperator == 'T')
            {
                if (operands.Count == 2)
                {
                    string nameA = operands.Pop().value.ToString();
                    string name = operands.Pop().value.ToString();
                    Triangle_points points = null;
                    foreach (Triangle_points t_p in tri_points)
                    {
                        if (t_p.name == nameA)
                        {
                            points = t_p;
                        }
                    }
                    if (points == null)
                    {
                        MessageBox.Show($"Массива точек {nameA} не существует.");
                        comboBox1.Items.Add($"Массива точек {nameA} не существует.");
                        return;
                    }
                    Triangle triangle = new Triangle(tri_count + 1, points.points, name);
                    if (!triangle.CoordsCheck(points.points, 0, 0))
                    {
                        MessageBox.Show("Фигура вышла за границы.");
                        comboBox1.Items.Add("Фигура вышла за границы.");
                        return;
                    }
                    tri_count += 1;
                    ShapeContainer.AddFigure(triangle);
                    triangle.Draw();
                    comboBox1.Items.Add($"Фигура {name} отрисована.");
                }
                else
                {
                    MessageBox.Show("Опертор A принимает 7 параметров.");
                    comboBox1.Items.Add("Неверное число параметров для оператора A.");
                }
            }
            else if (op.symbolOperator == 'M')
            {
                if (operands.Count == 3)
                {
                    Triangle triangle = null;
                    int y = Convert.ToInt32(operands.Pop().value.ToString());
                    int x = Convert.ToInt32(operands.Pop().value.ToString());
                    string name = operands.Pop().value.ToString();
                    foreach (Figure f in ShapeContainer.figureList)
                    {
                        if (f.unique_name == name)
                        {
                            triangle = (Triangle)f;
                        }
                    }
                    if (triangle == null)
                    {
                        MessageBox.Show($"Фигуры c именем {name} не существует.");
                        comboBox1.Items.Add($"Фигуры c именем {name} не существует.");
                        return;
                    }
                    if (triangle.CoordsCheck(triangle.points, x, y))
                    {
                        triangle.MoveTo(x, y);
                        comboBox1.Items.Add($"Фигура {name} перемещена.");
                    }
                    else
                    {
                        MessageBox.Show("Фигура вышла за границы.");
                        comboBox1.Items.Add("Фигура вышла за границы.");
                    }
                }
                else
                {
                    MessageBox.Show("Опертор M принимает 3 параметра.");
                    comboBox1.Items.Add("Неверное число параметров для оператора M.");
                }
            }
            else if (op.symbolOperator == 'D')
            {
                if (operands.Count == 1)
                {
                    Triangle triangle = null;
                    string name = operands.Pop().value.ToString();
                    foreach (Figure f in ShapeContainer.figureList)
                    {
                        if (f.unique_name == name)
                        {
                            triangle = (Triangle)f;
                        }
                    }
                    if (triangle == null)
                    {
                        MessageBox.Show($"Фигуры c именем {name} не существует.");
                        comboBox1.Items.Add($"Фигуры c именем {name} не существует.");
                        return;
                    }
                    triangle.DeleteF(triangle, true);
                    comboBox1.Items.Add($"Фигура {name}  удалена.");
                }
                else
                {
                    MessageBox.Show("Опертор D принимает 1 параметр.");
                    comboBox1.Items.Add("Неверное число параметров для оператора D.");
                }
            }
        }
        private bool IsNotOperation(char item)
        {
            if (!(item == 'D' || item == 'M' || item == 'T' || item == 'A' || item == ',' || item == '(' || item == ')'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
