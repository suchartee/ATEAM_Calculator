using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class FormResult : Form
    {
        Double resultValue = 0;
        String operationPerformed = "";
        String currentEquation = "";
        String equationAfterBackspace;
        Double currentValue = 0;
        bool isOperationPerformed = false;
        bool isOperatorChanged = false;
        
        public FormResult()
        {
            InitializeComponent();
        }

        // clicking number or decimal point
        private void number_Click(object sender, EventArgs e)
        {
            // clear the display for the first 0 (only 1 zero) or when the user enters the next number
            if ((textBoxInput.Text == "0") || (isOperationPerformed))
            {
                textBoxInput.Clear();
            }

            // starting the new number after the operator is clicked
            isOperationPerformed = false;

            // receive the clicked number and show it on the display
            Button number = (Button)sender;

            // only allow one decimal
            if (number.Text == ".")
            {
                if (!textBoxInput.Text.Contains("."))
                {
                    textBoxInput.Text = textBoxInput.Text + number.Text;
                }
            }
            else
            {
               textBoxInput.Text = textBoxInput.Text + number.Text;
            }
            
        }

        // clicking any operator (+, -, *, /)
        private void operator_Click(object sender, EventArgs e)
        {
            Button operation = (Button)sender;
            currentValue = Double.Parse(textBoxInput.Text);

            if (resultValue != 0)
            {
                // check that the operator can be changed
                if (!isOperatorChanged)
                {
                    buttonEqual.PerformClick();
                    operationPerformed = operation.Text;
                    resultValue = Double.Parse(textBoxInput.Text);
                    currentEquation += currentValue + " " + operationPerformed + " ";
                    currentOperation.Text = currentEquation;
                    isOperationPerformed = true;
                } 
            }
            else
            {
                operationPerformed = operation.Text;
                resultValue = Double.Parse(textBoxInput.Text);
                currentEquation += currentValue + " " + operationPerformed + " ";
                currentOperation.Text = currentEquation;
                isOperationPerformed = true;
            }
        }

        // clicking clear button
        private void allclear_Click(object sender, EventArgs e)
        {
            textBoxInput.Text = "0";
            resultValue = 0;
            currentOperation.Text = "";
            currentEquation = "";
        }

        private void clear_Click(object sender, EventArgs e)
        {
            textBoxInput.Text = "0";
        }

        // clicking = button
        private void equal_Click(object sender, EventArgs e)
        {
            switch (operationPerformed)
            {
                case "+":
                    textBoxInput.Text = (resultValue + Double.Parse(textBoxInput.Text)).ToString();
                    break;
                case "-":
                    textBoxInput.Text = (resultValue - Double.Parse(textBoxInput.Text)).ToString();
                    break;
                case "*":
                    textBoxInput.Text = (resultValue * Double.Parse(textBoxInput.Text)).ToString();
                    break;
                case "/":
                    textBoxInput.Text = (resultValue / Double.Parse(textBoxInput.Text)).ToString("#,0.00");     // the 2 decimal places
                    break;

                default:
                    break;
            }
            resultValue = Double.Parse(textBoxInput.Text);
            currentOperation.Text = "";
        }

        private void backspace_Click(object sender, EventArgs e)
        {
            equationAfterBackspace = textBoxInput.Text;

            if (equationAfterBackspace.Length > 1)
            {
                equationAfterBackspace = equationAfterBackspace.Substring(0, equationAfterBackspace.Length - 1);
            }
            else
            {
                equationAfterBackspace = "0";
            }

            textBoxInput.Text = equationAfterBackspace;
        }

        private void negative_Click(object sender, EventArgs e)
        {

        }
    }
}
