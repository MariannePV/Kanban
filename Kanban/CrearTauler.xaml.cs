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
        private MainWindow mainWindow;
        string colorString;

        public CrearTauler(MainWindow mainWindow, Tauler taulerExistent = null)
        {
            InitializeComponent();
            this.mainWindow = mainWindow; //Assignem la referència del mainwindow
            btnEliminar.Visibility = Visibility.Collapsed;

            //Si hi ha un tauler existent, emplenem els valors amb els corresponents
            if (taulerExistent != null)
            {
                txtTitol.Text = taulerExistent.Titol;

                //Actualitzar el color
                var colorWpf = (Color)ColorConverter.ConvertFromString(taulerExistent.Color);
                ColorRectangle.Fill = new SolidColorBrush(colorWpf);

                colorString = colorWpf.ToString();

                idTauler.Text = taulerExistent.Id.ToString();

                //Fem visible el botó d'eliminar
                btnEliminar.Visibility = Visibility.Visible;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) //Event del propi Windows que és crida quant mentenim el mouse en la pestanya de windows
        {
            this.DragMove(); //Mètode per poder moure la finestra
        }

        //Selecció del color
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
            string titol = txtTitol.Text;
            string color = colorString;
            int id = 0;

            //En cas de que el tauler sí existeixi, li assignem la id que toca
            if (idTauler.Text != "")
            {
                id = int.Parse(idTauler.Text);
            }

            //Afegim un nou tauler mitjançant el mètode del mainWindow
            mainWindow.ActualitzarTauler(id, titol, color);

            Close();
        }

        private void EliminarTauler(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(idTauler.Text);

            //Eliminem el tauler mitjançant el mètode del mainWindow
            mainWindow.EliminarTauler(id);

            Close();
        }

        private string ConvertirColorAString(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }
    }
}
