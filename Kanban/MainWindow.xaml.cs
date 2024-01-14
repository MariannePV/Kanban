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

        //Declarem un llistat de taulers
        public List<Tauler> taulers = new List<Tauler>();

        public MainWindow()
        {
            InitializeComponent();

            //Creem els primers taulers només d'iniciar
            taulers.Add(new Tauler()
            {
                Titol = "To Do",
                Color = "Magenta"
            });
            taulers.Add(new Tauler()
            {
                Titol = "Doing",
                Color = "Blue"
            });
            taulers.Add(new Tauler()
            {
                Titol = "Done",
                Color = "Green"
            });

            llistaTaulers.ItemsSource = taulers;
        }

        private void CrearTasca(object sender, RoutedEventArgs e)
        {
            CrearTasca tasca = new CrearTasca(this);
            tasca.ShowDialog();
            llistaTaulers.ItemsSource = null;
            llistaTaulers.ItemsSource = taulers;
        }

        private void EditarTasca_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender; // al clicar el boto de editar

            // Pillem la tasca que te associada el menuItem
            Tasca tascaSeleccionada = (Tasca)menuItem.DataContext;

            // Obrim la mateixa finestra que crear la tasca, fem servi el 2 cpnstructor de la classe CrearTasca
            CrearTasca crearTasca = new CrearTasca(this, tascaSeleccionada);
            crearTasca.ShowDialog();

            // Actualitzem la la llista de taualers
            llistaTaulers.ItemsSource = null;
            llistaTaulers.ItemsSource = taulers;
        }

        private void EliminarTasca_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender; // al clicar el boto de eliminar

            // tasca associada al menú contextual
            Tasca tascaSeleccionada = (Tasca)menuItem.DataContext;

            // Troba el tauler que conté aquesta tasca 
            Tauler taulerContenedor = taulers.FirstOrDefault(tauler => tauler.Tasques.Contains(tascaSeleccionada));

            if (taulerContenedor != null)
            {
                // Elimina la tasca del tauler
                taulerContenedor.Tasques.Remove(tascaSeleccionada);

                // Actualiza la llista de taulers
                llistaTaulers.ItemsSource = null;
                llistaTaulers.ItemsSource = taulers;
            }
        }

        private void CrearTauler(object sender, RoutedEventArgs e)
        {
            CrearTauler tauler = new CrearTauler(this);
            tauler.ShowDialog();
        }

        private void EditarTauler(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;

            Tauler taulerSeleccionat = (Tauler)boton.DataContext;

            CrearTauler tauler = new CrearTauler(this, taulerSeleccionat);
            tauler.ShowDialog();
        }

        //public void AgregarTauler(string titol, string color)
        //{
        //    taulers.Add(new Tauler()
        //    {
        //        Titol = titol,
        //        Color = color
        //    });

        //    llistaTaulers.ItemsSource = null;
        //    llistaTaulers.ItemsSource = taulers;
        //}

        public void ActualitzarTauler(int id, string nouTitol, string color)
        {
            // Busca un Tauler existente con el mismo Id
            Tauler taulerExistent = taulers.FirstOrDefault(t => t.Id == id);

            if (taulerExistent != null)
            {
                // Actualiza el Tauler existente
                taulerExistent.Titol = nouTitol;
                taulerExistent.Color = color;
            }
            else
            {
                if (nouTitol != "" && color != "")
                {
                    // Si no se encuentra un Tauler con el mismo Id, agrega uno nuevo
                    taulers.Add(new Tauler()
                    {
                        Titol = nouTitol,
                        Color = color
                    });
                }
                else
                {
                    taulers.Add(new Tauler());
                }
            }

            // Actualiza la llista de taulers
            llistaTaulers.ItemsSource = null;
            llistaTaulers.ItemsSource = taulers;
        }

        public void EliminarTauler(int id)
        {
            if (taulers.Count() == 1)   //Si només hi ha un tauler
            {
                MessageBox.Show("No pots esborrar l'últim tauler", "Error d'esborrat", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // Busca un Tauler existente con el mismo Id
                Tauler taulerExistent = taulers.FirstOrDefault(t => t.Id == id);

                if (taulerExistent.Tasques.Count != 0)     //Si la llista de tasques del tauler no està buida
                {
                    MessageBox.Show("El tauler té tasques associades. canvia-les de tauler abans d'esborrar-lo.", "Error d'esborrat", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    taulers.RemoveAll(t => t.Id == taulerExistent.Id);

                    // Actualiza la llista de taulers
                    llistaTaulers.ItemsSource = null;
                    llistaTaulers.ItemsSource = taulers;
                }
            }
        }

        private void GestionarResponsables(object sender, MouseButtonEventArgs e)
        {
            GestionarResponsables resp = new GestionarResponsables();
            resp.ShowDialog();
        }
    }
}
