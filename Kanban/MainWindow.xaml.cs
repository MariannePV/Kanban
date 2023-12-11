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

            // Obtén el tamaño del cuadro de diálogo
            double dialogAncho = DialogGrid.ActualWidth;
            double dialogAlto = DialogGrid.ActualHeight;

            // Calcula la posición para centrar el cuadro de diálogo
            double left = (ventanaAncho - dialogAncho) / 2;
            double top = (ventanaAlto - dialogAlto) / 2;

            // Posiciona el cuadro de diálogo en el centro
            Canvas.SetLeft(DialogGrid, left);
            Canvas.SetTop(DialogGrid, top);

            // Mostrar el cuadro de diálogo
            DialogGrid.Visibility = Visibility.Visible;
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            // Ocultar el cuadro de diálogo después de hacer algo con los datos
            DialogGrid.Visibility = Visibility.Collapsed;

            // Obtiene los datos desde el cuadro de diálogo
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;

            // Lógica para agregar el Post-it en la zona correspondiente
            AgregarPostItALaZona(nombre, descripcion);
        }
        private void AgregarPostItALaZona(string nombre, string descripcion)
        {
            // Lógica para agregar el Post-it en la zona correspondiente
            // ...
        }

    }
}
