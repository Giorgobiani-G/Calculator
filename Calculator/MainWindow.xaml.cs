using System;
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
        double result = 0;

        string operation = "";
        bool operationpressed = false;
        bool mathbuttonpressed = false;
        bool backspaceallowed = true;
        bool numburbuttonpressed = false;
        bool nth = false;
        bool nthpower = false;
        bool percentbutton = false;

        MenuItem menuItem1 = new MenuItem();
        MenuItem menuItem2 = new MenuItem();
        
        
        

        public MainWindow()
        {
            InitializeComponent();

            resultbox.Text = "0";
            

            


        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            backspaceallowed = true;


            if (resultbox.Text == "0" || operationpressed == true || mathbuttonpressed == true && resultbox_Copy.Text != "")


                resultbox.Clear();
            if (mathbuttonpressed & nth == false & mathbuttonpressed & nthpower == false)

                resultbox_Copy.Text = "";



            Button b = (Button)sender;
            resultbox.Text += b.Content;

            operationpressed = false;
            mathbuttonpressed = false;
            numburbuttonpressed = true;
            

        }




        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            
            operationpressed = true;
            if (nth)
            {
                Nthroot_Click(sender, e);


            }
            if (nthpower)
            {
                Npow_Click(sender, e);


            }

            if (numburbuttonpressed || mathbuttonpressed)
            {



                if (resultbox_Copy.Text != "")
                {

                    Button c = (Button)sender;
                    string operation1 = (string)c.Content;
                    if (mathbuttonpressed)
                    {
                        resultbox_Copy.Text = resultbox_Copy.Text + " " + operation1;


                    }
                    if (percentbutton)  /*percentbuttton*/
                    {
                        resultbox_Copy.Text = resultbox_Copy.Text + " " + operation1;
                    }

                    if (mathbuttonpressed==false) /*/*added*/ /*was written else */
                    {
                        resultbox_Copy.Text = resultbox_Copy.Text + " " + resultbox.Text + " " + operation1;
                    }

                    


                    string op;
                    if (result == 0 & operationpressed == false) /*davamate operationressed radgan 0 * 2 gamoived*/
                    {
                        op = resultbox.Text;
                    }
                    else
                    {
                        op = result.ToString() + operation + resultbox.Text;
                    }

                    if (op.Contains(","))
                    {
                        op = op.Replace(",", ".");
                    }


                    object oper = new System.Data.DataTable().Compute(op, "");

                    resultbox.Text = oper.ToString();
                    result = double.Parse(resultbox.Text);
                    operation = operation1;

                }
                else
                {
                    Button b = (Button)sender;
                    operation = (string)b.Content;
                    result = double.Parse(resultbox.Text);
                    resultbox_Copy.Text = result.ToString() + " " + operation;

                }

            }

            else
            {
                string operation2;
                Button b = (Button)sender;
                operation2 = (string)b.Content;
                if (resultbox_Copy.Text.Length - 1 < 0)
                {
                    resultbox_Copy.Text = resultbox.Text + " " + operation2;
                    operation = operation2;

                }
                else
                { 
                resultbox_Copy.Text = resultbox_Copy.Text.Remove(resultbox_Copy.Text.Length - 1, 1) + operation2;
                operation = operation2;
                }
                //resultbox_Copy.Text = resultbox_Copy.Text + " " + operation2 + " ";
            }

            mathbuttonpressed = false;
            
            numburbuttonpressed = false;
            nth = false;
            nthpower = false;
            percentbutton = false;
            
       #region


        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            double i = double.Parse(resultbox.Text) * -1;
            resultbox.Text = i.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!resultbox.Text.Contains(","))
            {
                string st = resultbox.Text;
                string st1 = ",";
                resultbox.Text = st + st1;
                if (operationpressed)
                {
                    resultbox.Text = "0" + st1;
                    operationpressed = false;
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            resultbox.Text = "0";
            result = 0;
            resultbox_Copy.Text = "";
            nth = false;
            nthpower = false;
            operation = "";
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (backspaceallowed)
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

            operationpressed = true;
            //resultbox_Copy.Text = string.Empty;
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
            if (nthpower)
            {
                Npow_Click(sender, e);
            }
            resultbox_Copy.Text = string.Empty;
            backspaceallowed = false;
            operation = "";

            result = 0;

        }



        private void Clearentrance_Click(object sender, RoutedEventArgs e)
        {
            resultbox.Text = "0";
        }

        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "sqrt" + "(" + resultbox.Text + ")";
            resultbox.Text = Math.Sqrt(Double.Parse(resultbox.Text)).ToString();

            backspaceallowed = false;
            mathbuttonpressed = true;
        }

        private void Reciproc_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "reciproc" + "(" + resultbox.Text + ")";
            resultbox.Text = (1 / (Double.Parse(resultbox.Text))).ToString();
            backspaceallowed = false;
            mathbuttonpressed = true;

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
            Thickness m = resultbox.Margin;
            Thickness n = resultbox_Copy.Margin;
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
            Thickness m = resultbox.Margin;
            Thickness n = resultbox_Copy.Margin;
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
                    double b = MathNet.Numerics.SpecialFunctions.Gamma(double.Parse(resultbox.Text) + 1);
                    resultbox.Text = b.ToString();

                }
                else
                {
                    double d = MathNet.Numerics.SpecialFunctions.Factorial(int.Parse(resultbox.Text));
                    resultbox.Text = d.ToString();
                }

            }

            catch (Exception)
            {
                resultbox.Text = "invalid input";

            }



            backspaceallowed = false;
            mathbuttonpressed = true;

        }

        private void Sin_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "sin" + "(" + resultbox.Text + ")";
            double sin = Math.PI * Double.Parse(resultbox.Text) / 180;

            resultbox.Text = Math.Sin(sin).ToString();
            backspaceallowed = false;
            mathbuttonpressed = true;
        }

        private void Cos_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "cos" + "(" + resultbox.Text + ")";
            double cos = Math.PI * Double.Parse(resultbox.Text) / 180;

            resultbox.Text = Math.Cos(cos).ToString();
            backspaceallowed = false;
            mathbuttonpressed = true;

        }

        private void Tan_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "tan" + "(" + resultbox.Text + ")";
            double tan = Math.PI * Double.Parse(resultbox.Text) / 180;

            resultbox.Text = Math.Tan(tan).ToString();
            backspaceallowed = false;
            mathbuttonpressed = true;

        }

        private void Sinh_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "sinh" + "(" + resultbox.Text + ")";
            double sinh = Double.Parse(resultbox.Text);

            resultbox.Text = Math.Sinh(sinh).ToString();
            backspaceallowed = false;
            mathbuttonpressed = true;
        }

        private void Cosh_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "cosh" + "(" + resultbox.Text + ")";
            double cosh = Double.Parse(resultbox.Text);

            resultbox.Text = Math.Cosh(cosh).ToString();
            backspaceallowed = false;
            mathbuttonpressed = true;

        }

        private void Tanh_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "tanh" + "(" + resultbox.Text + ")";
            double tanh = Double.Parse(resultbox.Text);

            resultbox.Text = Math.Tanh(tanh).ToString();
            backspaceallowed = false;
            mathbuttonpressed = true;


        }

        private void Exp_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "exp" + "(" + resultbox.Text + ")";
            double exp = Double.Parse(resultbox.Text);

            resultbox.Text = Math.Exp(exp).ToString();
            backspaceallowed = false;
            mathbuttonpressed = true;

        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "log" + "(" + resultbox.Text + ")";
            double log10 = Double.Parse(resultbox.Text);

            resultbox.Text = Math.Log10(log10).ToString();
            backspaceallowed = false;
            mathbuttonpressed = true;

        }

        private void Ln_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "ln" + "(" + resultbox.Text + ")";
            double ln = Double.Parse(resultbox.Text);

            resultbox.Text = Math.Log(ln).ToString();
            backspaceallowed = false;
            mathbuttonpressed = true;

        }

        private void Powten_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "powten" + "(" + resultbox.Text + ")";
            double pow = Double.Parse(resultbox.Text);

            resultbox.Text = Math.Pow(10, pow).ToString();
            backspaceallowed = false;
            mathbuttonpressed = true;

        }

        private void Twopower_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "sqr" + "(" + resultbox.Text + ")";
            double sqr = Double.Parse(resultbox.Text);

            resultbox.Text = Math.Pow(sqr, 2).ToString();
            backspaceallowed = false;
            mathbuttonpressed = true;

        }

        private void Powthree_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = resultbox_Copy.Text + "cube" + "(" + resultbox.Text + ")";
            double cube = Double.Parse(resultbox.Text);

            resultbox.Text = Math.Pow(cube, 3).ToString();
            backspaceallowed = false;
            mathbuttonpressed = true;

        }

        private void Pi_Click(object sender, RoutedEventArgs e)
        {


            resultbox.Text = Math.PI.ToString();
            backspaceallowed = false;
            numburbuttonpressed = true;





        }

        private void Cuberoot_Click(object sender, RoutedEventArgs e)
        {

            resultbox_Copy.Text = resultbox_Copy.Text + "cuberoot" + "(" + resultbox.Text + ")";
            double cbrt = double.Parse(resultbox.Text);
            double d = Math.Pow(cbrt, (1.0 / 3.0));
            resultbox.Text = d.ToString();
            backspaceallowed = false;
            mathbuttonpressed = true;

        }

        private void Perc_Click(object sender, RoutedEventArgs e)
        {

            if (resultbox_Copy.Text != "0")
            {

            
            

            if (numburbuttonpressed& resultbox_Copy.Text == "")
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
                object oper = new System.Data.DataTable().Compute(resultbox_Copy.Text, "");
                string op = oper.ToString();
                double bb = double.Parse(op);
                //double d = double.Parse(resultbox_Copy.Text);

                resultbox.Text = (double.Parse(resultbox.Text).ToString());
                double result = (bb * double.Parse(resultbox.Text)) / 100;

                resultbox_Copy.Text += " " + substring + " " + result.ToString();

                resultbox.Text = result.ToString();
                if (operationpressed)
                {
                    int num = resultbox_Copy.Text.IndexOf(operation);
                    resultbox_Copy.Text.Remove(num, num);
                }
                
            }

            }

            percentbutton = true;
        }



        double d;
        
        public void Nthroot_Click(object sender, RoutedEventArgs e)
        {
            
            
            mathbuttonpressed = true;
            //if (nthpower&numburbuttonpressed==false)
            //{
            //    string st = resultbox_Copy.Text.Remove(resultbox_Copy.Text.Length - 1);
            //    resultbox_Copy.Text = st + "yroot";
            //}
            
            //if (numburbuttonpressed == false & operationpressed)  /*correction*/
            //{
            //    resultbox_Copy.Text.Remove(resultbox_Copy.Text.Length - 2, 2);

            //    resultbox_Copy.Text = "(" + resultbox_Copy.Text + ")" + "yroot";
            //}  comentarebi davamatet test 
            //comments has been added

            if (numburbuttonpressed||nth==false)
            {

                


                if (operationpressed)
                {

                    resultbox_Copy.Text = resultbox_Copy.Text + " " + (resultbox.Text);


                }




                if (mathbuttonpressed & operationpressed == false)
                {

                    resultbox_Copy.Text = resultbox_Copy.Text + " " + (resultbox.Text) + " " + "yroot";
                }
                else
                {
                    resultbox_Copy.Text = resultbox_Copy.Text;

                }
                






                if (nth)
            {
                
                double n = 0;
                n = double.Parse(resultbox.Text);
                double dd = Math.Pow(d, (1.0 /  n));
                resultbox.Text = dd.ToString();
                
            }
            d = double.Parse(resultbox.Text);

            }

            #endregion

            string sub = resultbox_Copy.Text.Substring(resultbox_Copy.Text.Length - 5, 5);
            
            if (operationpressed & sub == "yroot")
            {
                resultbox_Copy.Text = resultbox_Copy.Text.Remove(resultbox_Copy.Text.Length-6, 6);
            }

            backspaceallowed = false;

            nth = true;
            numburbuttonpressed = false;
                

            
        }


        double p;
        private void Npow_Click(object sender, RoutedEventArgs e)
        {
            mathbuttonpressed = true;
            if (numburbuttonpressed||nthpower==false)
            {

                if (operationpressed)
                {

                    resultbox_Copy.Text = resultbox_Copy.Text + " " + (resultbox.Text);


                }



                if (mathbuttonpressed & operationpressed == false)
                {

                    resultbox_Copy.Text = resultbox_Copy.Text + " " + (resultbox.Text) + " " + "^";
                }
                else
                {
                    resultbox_Copy.Text = resultbox_Copy.Text;

                }

                //if (nthpower)
                //{

                //    resultbox_Copy.Text = resultbox_Copy.Text + " " + (resultbox.Text) + " ";
                //}
                //else
                //{

                //    resultbox_Copy.Text = resultbox_Copy.Text + " " + (resultbox.Text) + " " + "^";
                //}



                if (nthpower)
            {
                
                double n = 0;
                n = double.Parse(resultbox.Text);
                double dd = Math.Pow(p,n);
                resultbox.Text = dd.ToString();
            }
            
                p = double.Parse(resultbox.Text);

            }

            string sub = resultbox_Copy.Text.Substring(resultbox_Copy.Text.Length - 1, 1);

            if (operationpressed & sub == "^")
            {
                resultbox_Copy.Text = resultbox_Copy.Text.Remove(resultbox_Copy.Text.Length - 2, 2);
            }

            


            backspaceallowed = false;
            nthpower = true;
            numburbuttonpressed = false;
        }

        private void Lft_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text = "(";
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            resultbox_Copy.Text =  resultbox_Copy.Text + resultbox.Text + ")";
        }

       

        
    }

    
}

