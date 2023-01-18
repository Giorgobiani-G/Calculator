using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double result;
        string operation = "";
        bool operationPressed;
        bool mathbuttonPressed;
        bool backspaceAllowed = true;
        bool numberButtonPressed;
        bool nth;
        bool nthPower;
        bool percentButton;
        bool lftClicked;
        bool rgtClicked;

        MenuItem menuItem1 = new MenuItem();
        MenuItem menuItem2 = new MenuItem();

        public MainWindow()
        {
            InitializeComponent();
            resultbox.Text = "0";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            backspaceAllowed = true;

            if (resultbox.Text == "0" || operationPressed == true || mathbuttonPressed == true && resultbox_Copy.Text != "")
                resultbox.Clear();

            if (mathbuttonPressed & nth == false & mathbuttonPressed & nthPower == false)
                resultbox_Copy.Text = "";

            var b = (Button)sender;
            resultbox.Text += b.Content;

            operationPressed = false;
            mathbuttonPressed = false;
            numberButtonPressed = true;
            rgtClicked = false;
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            operationPressed = true;

            if (nth)
            {
                Nthroot_Click(sender, e);
            }

            if (nthPower)
            {
                Npow_Click(sender, e);
            }

            if (numberButtonPressed || mathbuttonPressed)
            {
                var operation1 = "";

                if (resultbox_Copy.Text != "")
                {
                    if (rgtClicked == false)
                    {
                        var c = (Button)sender;

                        operation1 = (string)c.Content;

                        if (mathbuttonPressed)
                        {
                            resultbox_Copy.Text = resultbox_Copy.Text + " " + operation1;
                        }

                        if (percentButton)
                        {
                            resultbox_Copy.Text = resultbox_Copy.Text + " " + operation1;
                        }

                        if (mathbuttonPressed == false)
                        {
                            resultbox_Copy.Text = resultbox_Copy.Text + " " + resultbox.Text + " " + operation1;
                        }
                    }

                    string op;

                    if (result == 0 & operationPressed == false)
                    {
                        op = resultbox.Text;
                    }

                    string abc = resultbox_Copy.Text;

                    int count = 0;

                    foreach (var c in abc)
                    {
                        if (c == '(')
                        {
                            count++;
                        }
                    }

                    int l = count;

                    bool left = l % 2 == 0;

                    int count1 = 0;

                    foreach (var c in abc)
                    {
                        if (c == ')')
                        {
                            count1++;
                        }
                    }

                    int r = count1;

                    var right = r % 2 == 0;

                    var st = new Dictionary<string, string>()
                    {
                      {"sqrt",double.Parse(resultbox.Text).ToString()},
                      { "reciproc",(1/(double.Parse(resultbox.Text))).ToString()},
                    };

                    var b = false;

                    var currentMath = "";

                    for (int i = 0; i < st.Count; i++)
                    {
                        var item = st.ElementAt(i);
                        var itemKey = item.Key;
                        var itemValue = item.Value;
                        b = resultbox_Copy.Text.Contains(itemKey);

                        if (b)
                        {
                            currentMath = itemValue.ToString();
                            break;
                        }
                    }

                    if (mathbuttonPressed == false && (lftClicked && rgtClicked || lftClicked && rgtClicked == false && resultbox_Copy.Text.Contains(")") && left == true && right == true || lftClicked && rgtClicked == false && resultbox_Copy.Text.Contains(")") && left == false && right == false || left == false && right == false && mathbuttonPressed))
                    {
                        if (lftClicked && rgtClicked == false && resultbox_Copy.Text.Contains(")"))
                        {
                            var i = resultbox_Copy.Text.LastIndexOf("(");
                            var sub = resultbox_Copy.Text.Substring(i); /*right part*/
                            var sub1 = resultbox_Copy.Text.Substring(0, resultbox_Copy.Text.Length - sub.Length); /*Left part*/

                            string ss = sub.Remove(sub.Length - 2);
                            object ob = new System.Data.DataTable().Compute(ss, "");
                            op = sub1 + ob.ToString();
                        }
                        else if (b)
                        {
                            int i = resultbox_Copy.Text.LastIndexOf("(");
                            string sub = resultbox_Copy.Text.Substring(i);
                            string ss = sub.Remove(sub.Length - 2);
                            op = currentMath + operation + sub;

                            if (op.Contains(","))
                            {
                                op = op.Replace(",", ".");
                            }

                            var obj = new System.Data.DataTable().Compute(op, "");

                            obj.ToString();
                        }

                        else
                        {
                            var obj = new System.Data.DataTable().Compute(resultbox_Copy.Text, "");
                            op = obj.ToString();
                        }
                    }

                    else
                    {
                        op = result.ToString() + operation + resultbox.Text;
                    }

                    if (op.Contains(","))
                    {
                        op = op.Replace(",", ".");
                    }

                    var oper = new System.Data.DataTable().Compute(op, "");

                    resultbox.Text = oper.ToString();
                    result = double.Parse(resultbox.Text);
                    operation = operation1;
                }

                else
                {
                    var b = (Button)sender;
                    operation = (string)b.Content;
                    result = double.Parse(resultbox.Text);
                    resultbox_Copy.Text = result.ToString() + " " + operation;
                }
            }

            else
            {
                string operation2;
                var b = (Button)sender;
                operation2 = (string)b.Content;

                if (resultbox_Copy.Text.Length - 1 < 0)
                {
                    resultbox_Copy.Text = resultbox.Text + " " + operation2;
                    operation = operation2;
                }

                else if (resultbox_Copy.Text.Length - 1 > 0 && rgtClicked || resultbox_Copy.Text.Length - 1 > 0 && rgtClicked)
                {
                    resultbox_Copy.Text += operation2;
                    operation = operation2;
                }

                else
                {
                    resultbox_Copy.Text = resultbox_Copy.Text.Remove(resultbox_Copy.Text.Length - 1, 1) + operation2;
                    operation = operation2;
                }
            }

            mathbuttonPressed = false;
            numberButtonPressed = false;
            nth = false;
            nthPower = false;
            percentButton = false;
            #region
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var i = double.Parse(resultbox.Text) * -1;
            resultbox.Text = i.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!resultbox.Text.Contains(","))
            {
                var st = resultbox.Text;
                var st1 = ",";
                resultbox.Text = st + st1;

                if (operationPressed)
                {
                    resultbox.Text = "0" + st1;
                    operationPressed = false;
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            resultbox.Text = "0";
            result = 0;
            resultbox_Copy.Text = "";
            nth = false;
            nthPower = false;
            operation = "";
            rgtClicked = false;
            lftClicked = false;
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (backspaceAllowed)
            {
                if (resultbox.Text.Length >= 0)
                {
                    resultbox.Text = resultbox.Text.Remove(resultbox.Text.Length - 1, 1);
                }

                if (resultbox.Text == "" || resultbox.Text == "-")
                {
                    resultbox.Text = "0";
                }

                if (resultbox.Text.Length == 2 && resultbox.Text.StartsWith("-0"))
                {
                    resultbox.Text = "0";
                }
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            operationPressed = true;

            if (lftClicked)
            {
                string sub = "";

                if (rgtClicked)
                {
                    sub = resultbox_Copy.Text;
                }

                else
                {
                    sub = resultbox_Copy.Text + resultbox.Text; /*right part*/
                }

                var ob = new System.Data.DataTable().Compute(sub, "");

                if (ob.ToString() != "")
                {
                    resultbox.Text = ob.ToString();
                    resultbox_Copy.Text = string.Empty;
                    backspaceAllowed = false;
                    operation = "";
                }
            }

            else
            {
                switch (operation)
                {
                    case "+":
                        resultbox.Text = (result + double.Parse(resultbox.Text)).ToString();
                        break;
                    case "-":
                        resultbox.Text = (result - double.Parse(resultbox.Text)).ToString();
                        break;
                    case "/":
                        resultbox.Text = (result / double.Parse(resultbox.Text)).ToString();
                        break;
                    case "*":
                        resultbox.Text = (result * double.Parse(resultbox.Text)).ToString();
                        break;
                    default:
                        break;
                }

                if (nth)
                {
                    Nthroot_Click(sender, e);
                }

                if (nthPower)
                {
                    Npow_Click(sender, e);
                }

                resultbox_Copy.Text = string.Empty;
                backspaceAllowed = false;
                operation = "";

                result = 0;
            }
        }

        private void Clearentrance_Click(object sender, RoutedEventArgs e)
        {
            resultbox.Text = "0";
        }

        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "sqrt" + "(" + resultbox.Text + ")";
            resultbox.Text = Math.Sqrt(double.Parse(resultbox.Text)).ToString();
            backspaceAllowed = false;
            mathbuttonPressed = true;
        }

        private void Reciproc_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "reciproc" + "(" + resultbox.Text + ")";
            resultbox.Text = (1 / (double.Parse(resultbox.Text))).ToString();
            backspaceAllowed = false;
            mathbuttonPressed = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            menuItem1 = (MenuItem)sender;

            if (menuItem2.IsChecked)
            {
                menuItem2.IsChecked = false;
            }

            menuItem1.IsChecked = true;

            this.Width = 356;
            var m = resultbox.Margin;
            var n = resultbox_Copy.Margin;
            n.Right = 69;
            m.Right = 69;
            resultbox_Copy.Margin = n;
            resultbox.Margin = m;
            Grid.SetColumnSpan(resultbox, 6);
            Grid.SetColumnSpan(resultbox_Copy, 6);

            Grid.SetColumn(backspace, 0);
            Grid.SetColumn(clearentrance, 1);
            Grid.SetColumn(clear, 2);
            Grid.SetColumn(plmin, 3);
            Grid.SetColumn(sqrt, 4);

            Grid.SetColumn(_7, 0);
            Grid.SetColumn(_8, 1);
            Grid.SetColumn(_9, 2);
            Grid.SetColumn(div, 3);
            Grid.SetColumn(perc, 4);
            Grid.SetColumn(_4, 0);
            Grid.SetColumn(_5, 1);
            Grid.SetColumn(_6, 2);
            Grid.SetColumn(pow, 3);
            Grid.SetColumn(reciproc, 4);
            Grid.SetColumn(_1, 0);
            Grid.SetColumn(_2, 1);
            Grid.SetColumn(_3, 2);
            Grid.SetColumn(plus, 3);
            Grid.SetColumn(equals, 4);
            Grid.SetColumn(_0, 0);
            Grid.SetColumn(dec, 2);
            Grid.SetColumn(min, 3);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            menuItem2 = (MenuItem)sender;

            if (menuItem1.IsChecked)
            {
                menuItem1.IsChecked = false;
            }

            menuItem2.IsChecked = true;

            this.Width = 675;
            var m = resultbox.Margin;
            var n = resultbox_Copy.Margin;
            n.Right = 5;
            m.Right = 5;
            resultbox_Copy.Margin = n;
            resultbox.Margin = m;
            Grid.SetColumnSpan(resultbox, 10);
            Grid.SetColumnSpan(resultbox_Copy, 10);

            Grid.SetColumn(backspace, 5);
            Grid.SetColumn(clearentrance, 6);
            Grid.SetColumn(clear, 7);
            Grid.SetColumn(plmin, 8);
            Grid.SetColumn(sqrt, 9);
            Grid.SetColumn(_7, 5);
            Grid.SetColumn(_8, 6);
            Grid.SetColumn(_9, 7);
            Grid.SetColumn(div, 8);
            Grid.SetColumn(perc, 9);
            Grid.SetColumn(_4, 5);
            Grid.SetColumn(_5, 6);
            Grid.SetColumn(_6, 7);
            Grid.SetColumn(pow, 8);
            Grid.SetColumn(reciproc, 9);
            Grid.SetColumn(_1, 5);
            Grid.SetColumn(_2, 6);
            Grid.SetColumn(_3, 7);
            Grid.SetColumn(plus, 8);
            Grid.SetColumn(equals, 9);
            Grid.SetColumn(_0, 5);
            Grid.SetColumn(dec, 7);
            Grid.SetColumn(min, 8);
        }

        private void Fact_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                resultbox_Copy.Text = resultbox_Copy.Text + "fact" + "(" + resultbox.Text + ")";
                if (resultbox.Text.Contains(","))
                {
                    var b = MathNet.Numerics.SpecialFunctions.Gamma(double.Parse(resultbox.Text) + 1);
                    resultbox.Text = b.ToString();
                }
                else
                {
                    var d = MathNet.Numerics.SpecialFunctions.Factorial(int.Parse(resultbox.Text));
                    resultbox.Text = d.ToString();
                }
            }

            catch (Exception)
            {
                resultbox.Text = "invalid input";
            }

            backspaceAllowed = false;
            mathbuttonPressed = true;
        }

        private void Sin_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "sin" + "(" + resultbox.Text + ")";
            var sin = Math.PI * double.Parse(resultbox.Text) / 180;

            resultbox.Text = Math.Sin(sin).ToString();
            backspaceAllowed = false;
            mathbuttonPressed = true;
        }

        private void Cos_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "cos" + "(" + resultbox.Text + ")";
            var cos = Math.PI * double.Parse(resultbox.Text) / 180;

            resultbox.Text = Math.Cos(cos).ToString();
            backspaceAllowed = false;
            mathbuttonPressed = true;
        }

        private void Tan_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "tan" + "(" + resultbox.Text + ")";
            var tan = Math.PI * double.Parse(resultbox.Text) / 180;

            resultbox.Text = Math.Tan(tan).ToString();
            backspaceAllowed = false;
            mathbuttonPressed = true;
        }

        private void Sinh_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "sinh" + "(" + resultbox.Text + ")";
            var sinh = double.Parse(resultbox.Text);

            resultbox.Text = Math.Sinh(sinh).ToString();
            backspaceAllowed = false;
            mathbuttonPressed = true;
        }

        private void Cosh_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "cosh" + "(" + resultbox.Text + ")";
            var cosh = double.Parse(resultbox.Text);

            resultbox.Text = Math.Cosh(cosh).ToString();
            backspaceAllowed = false;
            mathbuttonPressed = true;
        }

        private void Tanh_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "tanh" + "(" + resultbox.Text + ")";
            var tanh = double.Parse(resultbox.Text);

            resultbox.Text = Math.Tanh(tanh).ToString();
            backspaceAllowed = false;
            mathbuttonPressed = true;
        }

        private void Exp_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "exp" + "(" + resultbox.Text + ")";
            var exp = double.Parse(resultbox.Text);

            resultbox.Text = Math.Exp(exp).ToString();
            backspaceAllowed = false;
            mathbuttonPressed = true;
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "log" + "(" + resultbox.Text + ")";
            var log10 = double.Parse(resultbox.Text);

            resultbox.Text = Math.Log10(log10).ToString();
            backspaceAllowed = false;
            mathbuttonPressed = true;
        }

        private void Ln_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "ln" + "(" + resultbox.Text + ")";
            var ln = double.Parse(resultbox.Text);

            resultbox.Text = Math.Log(ln).ToString();
            backspaceAllowed = false;
            mathbuttonPressed = true;
        }

        private void Powten_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "powten" + "(" + resultbox.Text + ")";
            var pow = double.Parse(resultbox.Text);

            resultbox.Text = Math.Pow(10, pow).ToString();
            backspaceAllowed = false;
            mathbuttonPressed = true;
        }

        private void Twopower_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "sqr" + "(" + resultbox.Text + ")";
            double sqr = double.Parse(resultbox.Text);

            resultbox.Text = Math.Pow(sqr, 2).ToString();
            backspaceAllowed = false;
            mathbuttonPressed = true;
        }

        private void Powthree_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "cube" + "(" + resultbox.Text + ")";
            double cube = double.Parse(resultbox.Text);

            resultbox.Text = Math.Pow(cube, 3).ToString();
            backspaceAllowed = false;
            mathbuttonPressed = true;
        }

        private void Pi_Click(object sender, RoutedEventArgs e)
        {
            resultbox.Text = Math.PI.ToString();
            backspaceAllowed = false;
            numberButtonPressed = true;
        }

        private void Cuberoot_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "cuberoot" + "(" + resultbox.Text + ")";
            double cbrt = double.Parse(resultbox.Text);
            double d = Math.Pow(cbrt, (1.0 / 3.0));
            resultbox.Text = d.ToString();
            backspaceAllowed = false;
            mathbuttonPressed = true;
        }

        private void Perc_Click(object sender, RoutedEventArgs e)
        {
            if (resultbox_Copy.Text != "0")
            {
                if (numberButtonPressed & resultbox_Copy.Text == "")
                {
                    resultbox_Copy.Text = "0";
                    resultbox.Text = "0";
                }

                else
                {
                    string substring = resultbox_Copy.Text.Substring(resultbox_Copy.Text.Length - 1, 1);
                    resultbox_Copy.Text = resultbox_Copy.Text.Remove(resultbox_Copy.Text.Length - 2, 2);

                    if (resultbox_Copy.Text.Contains(","))
                    {
                        resultbox_Copy.Text = resultbox_Copy.Text.Replace(",", ".");
                    }

                    var oper = new System.Data.DataTable().Compute(resultbox_Copy.Text, "");
                    string op = oper.ToString();
                    double bb = double.Parse(op);

                    resultbox.Text = (double.Parse(resultbox.Text).ToString());
                    double result = (bb * double.Parse(resultbox.Text)) / 100;

                    resultbox_Copy.Text += " " + substring + " " + result.ToString();

                    resultbox.Text = result.ToString();
                    if (operationPressed)
                    {
                        int num = resultbox_Copy.Text.IndexOf(operation);
                        resultbox_Copy.Text.Remove(num, num);
                    }
                }
            }

            percentButton = true;
        }

        double d;

        public void Nthroot_Click(object sender, RoutedEventArgs e)
        {
            mathbuttonPressed = true;

            if (numberButtonPressed || nth == false)
            {
                if (operationPressed)
                {
                    resultbox_Copy.Text = resultbox_Copy.Text + " " + (resultbox.Text);
                }

                if (mathbuttonPressed & operationPressed == false)
                {
                    resultbox_Copy.Text = resultbox_Copy.Text + " " + (resultbox.Text) + " " + "yroot";
                }

                else
                {
                    resultbox_Copy.Text = resultbox_Copy.Text;
                }

                if (nth)
                {
                    double n;
                    n = double.Parse(resultbox.Text);
                    double dd = Math.Pow(d, (1.0 / n));
                    resultbox.Text = dd.ToString();
                }

                d = double.Parse(resultbox.Text);
            }

            #endregion

            var sub = resultbox_Copy.Text.Substring(resultbox_Copy.Text.Length - 5, 5);

            if (operationPressed & sub == "yroot")
            {
                resultbox_Copy.Text = resultbox_Copy.Text.Remove(resultbox_Copy.Text.Length - 6, 6);
            }

            backspaceAllowed = false;
            nth = true;
            numberButtonPressed = false;
        }


        double p;
        private void Npow_Click(object sender, RoutedEventArgs e)
        {
            mathbuttonPressed = true;

            if (numberButtonPressed || nthPower == false)
            {
                if (operationPressed)
                {
                    resultbox_Copy.Text = resultbox_Copy.Text + " " + (resultbox.Text);
                }

                if (mathbuttonPressed & operationPressed == false)
                {
                    resultbox_Copy.Text = resultbox_Copy.Text + " " + (resultbox.Text) + " " + "^";
                }

                else
                {
                    resultbox_Copy.Text = resultbox_Copy.Text;
                }

                if (nthPower)
                {
                    double n;
                    n = double.Parse(resultbox.Text);
                    double dd = Math.Pow(p, n);
                    resultbox.Text = dd.ToString();
                }

                p = double.Parse(resultbox.Text);
            }

            var sub = resultbox_Copy.Text.Substring(resultbox_Copy.Text.Length - 1, 1);

            if (operationPressed & sub == "^")
            {
                resultbox_Copy.Text = resultbox_Copy.Text.Remove(resultbox_Copy.Text.Length - 2, 2);
            }

            backspaceAllowed = false;
            nthPower = true;
            numberButtonPressed = false;
        }

        private void Lft_Click(object sender, RoutedEventArgs e)
        {
            lftClicked = true;
            resultbox_Copy.Text += "(";

            if (mathbuttonPressed)
            {
                resultbox_Copy.Text = "(" + resultbox_Copy.Text;
            }
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            if (numberButtonPressed)
            {
                if (lftClicked & rgtClicked != true)
                {
                    resultbox_Copy.Text = resultbox_Copy.Text + resultbox.Text + ")";

                    rgtClicked = true;

                    Operation_Click(sender, e);
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
}

