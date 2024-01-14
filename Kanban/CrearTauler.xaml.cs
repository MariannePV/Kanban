using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kanban
{
    /// <summary>
    /// Lógica de interacción para CrearTauler.xaml
    /// </summary>
    public partial class CrearTauler : Window
    {
        private MainWindow mainWindow; // Agrega este campo
        string colorString;

        public CrearTauler(MainWindow mainWindow, Tauler taulerExistente = null)
        {
            InitializeComponent();
            this.mainWindow = mainWindow; // Asigna la referencia de MainWindow
            btnEliminar.Visibility = Visibility.Collapsed;

            // Inicializa los controles con los valores del tablero existente si se proporciona
            if (taulerExistente != null)
            {
                txtTitol.Text = taulerExistente.Titol;
                // ...

                // Puedes manejar las tareas asociadas al tablero si lo deseas
                // ...

                // Actualiza el color
                var colorWpf = (Color)ColorConverter.ConvertFromString(taulerExistente.Color);
                ColorRectangle.Fill = new SolidColorBrush(colorWpf);

                colorString = colorWpf.ToString();

                idTauler.Text = taulerExistente.Id.ToString();

                btnEliminar.Visibility = Visibility.Visible;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) //Event del propi Windows que és crida quant mentenim el mouse en la pestanya de windows
        {
            this.DragMove(); //Mètode per poder moure la finestra
        }

        private void SeleccionarColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color colorSeleccionado = colorDialog.Color;
                var colorWpf = Color.FromArgb(colorSeleccionado.A, colorSeleccionado.R, colorSeleccionado.G, colorSeleccionado.B);

                ColorRectangle.Fill = new SolidColorBrush(colorWpf);

                colorString = ConvertirColorAString(colorWpf);
            }
        }

        private void CerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DesarTauler(object sender, RoutedEventArgs e)
        {
            // Supongamos que tienes TextBoxes llamados txtTitol y txtColor en tu nueva ventana
            string titol = txtTitol.Text;
            string color = colorString;
            int id = 0;

            //En cas de que el tauler sí existeixi, li assignem la id que toca
            if (idTauler.Text != "")
            {
                id = int.Parse(idTauler.Text);
            }

            // Llama al método de la ventana principal para agregar un nuevo Tauler
            mainWindow.ActualitzarTauler(id, titol, color);

            // Cierra la ventana
            Close();
        }

        private void EliminarTauler(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(idTauler.Text);

            // Llama al método de la ventana principal para agregar un nuevo Tauler
            mainWindow.EliminarTauler(id);

            // Cierra la ventana
            Close();
        }

        private string ConvertirColorAString(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }
    }
}
