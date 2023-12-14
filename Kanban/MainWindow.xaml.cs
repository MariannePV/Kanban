using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kanban
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double ventanaAncho = ActualWidth;
            double ventanaAlto = ActualHeight;

            // Pillem la mida del quadre de diàleg
            double dialogAncho = DialogGrid.ActualWidth;
            double dialogAlto = DialogGrid.ActualHeight;

            // Calcula la posició per centrar el quadre de diàleg
            double left = (ventanaAncho - dialogAncho) / 2;
            double top = (ventanaAlto - dialogAlto) / 2;

            // Posiciona el quadre de diàleg al centre
            Canvas.SetLeft(DialogGrid, left);
            Canvas.SetTop(DialogGrid, top);

            // Mostrar el cuadro de diálogo
            DialogGrid.Visibility = Visibility.Visible;

            CrearTasca tasca = new CrearTasca();
            tasca.ShowDialog();
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            
            DialogGrid.Visibility = Visibility.Collapsed;

            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;

            AgregarPostItALaZona(nombre, descripcion);
        }
        private void AgregarPostItALaZona(string nombre, string descripcion)
        {
            
        }

    }
}
