using MI_BIBLIOTECA;
using System.Linq.Expressions;
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        Class1 c1 = new Class1();
        public Form1()
        {
            InitializeComponent();
        }
        private void button_Click(object sender, EventArgs e)
        {
            string c = (sender as Button)?.Text;
            string nombre_boton = (sender as Button)?.Name;

            // Si se hace clic en el bot�n 19, realizamos una operaci�n especial y salimos del m�todo
            if (nombre_boton == "button19")
            {
                if (c != "")
                {
                    // Asigna el texto almacenado en button19 al TextBox y limpia button19
                    textBox1.Text = button19.Text;
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.Focus();
                    button19.Text = "";
                }
                return; // Aqu� salimos de la funci�n para evitar la ejecuci�n de m�s c�digo
            }

            // Limpia el TextBox si el bot�n "C" es clicado
            if (nombre_boton == "button1")
            {
                textBox1.Text = ""; // Aseg�rate de usar una cadena vac�a
               
                return;
            }
            if (c == "<--")
            {
                if (textBox1.Text.Length > 0) // Aseg�rate de que la cadena no est� vac�a
                {
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1); // Elimina el �ltimo car�cter
                }
                return;
            }
            // Si se hace clic en "=" se eval�a la expresi�n
            if (c == "=")
            {
                string expresion = textBox1.Text;

                expresion = c1.MultiplicacionImplicita(expresion);
                bool val = VerificarExpresion(expresion);

                if (val)
                {
                    try
                    {
                        string expresionPostfija = c1.ConvertirAPostfija(expresion);
                        double resultado = c1.EvaluarPostfija(expresionPostfija);
                        textBox1.Text = resultado.ToString();

                        // Guardar la expresi�n en button19
                        button19.Text = expresion;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
                textBox1.SelectionStart = expresion.Length;
                textBox1.Focus();
                return;
            }
            else { 
                // Si no es ninguno de los casos anteriores, insertamos el texto en la posici�n actual del cursor
                int cursorPosition = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(cursorPosition, c);
                textBox1.SelectionStart = cursorPosition + c.Length;
                textBox1.Focus();
            }   
        }



        public bool VerificarExpresion(string expresion)
        {

            // Verificar que los par�ntesis est�n balanceados
            if (!c1.VerificarParentesisBalanceados(expresion))
            {
                MessageBox.Show("Error: Los par�ntesis no est�n balanceados.");
                return false;
            }

            // Verificar la sintaxis general de la expresi�n
            if (!c1.VerificarSintaxis(expresion))
            {
                MessageBox.Show("Error: Sintaxis incorrecta.");
                return false;
            }

            return true; // La expresi�n es v�lida
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo n�meros, par�ntesis, y los operadores +, -, *, /
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '(' && e.KeyChar != ')' &&
                e.KeyChar != '+' && e.KeyChar != '-' &&
                e.KeyChar != '*' && e.KeyChar != '/')
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }


    }
}
