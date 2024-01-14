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
using System.Windows.Shapes;

namespace Kanban
{
    /// <summary>
    /// Lógica de interacción para GestionarResponsables.xaml
    /// </summary>
    public partial class GestionarResponsables : Window
    {
        private string selectedResponsableId; // Add this field
        //Declarem un llistat de taulers
        public List<Responsable> responsables = new List<Responsable>();

        public GestionarResponsables()
        {
            InitializeComponent();

            responsables.Add(new Responsable()
            {
                Nom = "Sense assignar",
                Cognoms = "",
                Email = "",
                Dni = "",
                DataNaix = DateTime.Today
            });

            llistaResponsables.ItemsSource = responsables;
        }

        private void CrearResponsables(object sender, RoutedEventArgs e)
        {
            CrearResponsable resp = new CrearResponsable(this);
            resp.ShowDialog();
            llistaResponsables.ItemsSource = null;
            llistaResponsables.ItemsSource = responsables;
        }

        public void ActualitzarResponsable(Responsable resp)
        {
            // Check if a Responsable with the same ID already exists
            Responsable existingResponsable = responsables.FirstOrDefault(r => r.Id == resp.Id);

            if (existingResponsable != null)
            {
                // Update the existing Responsable with the new data
                existingResponsable.Nom = resp.Nom;
                existingResponsable.Cognoms = resp.Cognoms;
                existingResponsable.Email = resp.Email;
                existingResponsable.Dni = resp.Dni;
                existingResponsable.DataNaix = resp.DataNaix;
            }
            else
            {
                // If no existing Responsable with the same ID, add the new one
                responsables.Add(resp);
            }
        }

        private void EditarResponsable(object sender, MouseButtonEventArgs e)
        {
            if (llistaResponsables.SelectedItem is Responsable selectedResponsable)
            {
                if (selectedResponsable.Id == 1)
                {
                    MessageBox.Show("Aquest registre no es pot actualitzar", "Error d'esborrat", MessageBoxButton.OK, MessageBoxImage.Error);
                } else
                {
                    CrearResponsable resp = new CrearResponsable(this, selectedResponsable);
                    resp.ShowDialog();
                    llistaResponsables.ItemsSource = null;
                    llistaResponsables.ItemsSource = responsables;
                }
            }
        }

        private void txtID(object sender, RoutedEventArgs e)
        {
            TextBlock txtBlock = sender as TextBlock;

            // Access the DataContext, which should be the data item (Responsable)
            if (txtBlock.DataContext is Responsable responsable)
            {
                // Store the Id property in the field
                selectedResponsableId = responsable.Id.ToString();

                // Do something with the id...
            }
        }

        private void EsborrarResponsable(object sender, MouseButtonEventArgs e)
        {
            // Check if any item is selected
            if (llistaResponsables.SelectedItem != null)
            {
                // Get the selected Responsable from the ListBox
                Responsable responsableToRemove = llistaResponsables.SelectedItem as Responsable;

                if (responsableToRemove != null)
                {
                    if (responsableToRemove.Id == 1)
                    {
                        MessageBox.Show("Aquest registre no es pot esborrar", "Error d'esborrat", MessageBoxButton.OK, MessageBoxImage.Error);
                    } else
                    {
                        // Remove the Responsable from the list
                        responsables.Remove(responsableToRemove);

                        // Refresh the ListBox
                        llistaResponsables.Items.Refresh();
                    }
                }
                else
                {
                    MessageBox.Show("Unable to retrieve selected Responsable.");
                }
            }
            else
            {
                MessageBox.Show("No item selected.");
            }
        }
    }
}
