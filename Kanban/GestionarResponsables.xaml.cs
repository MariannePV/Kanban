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
        private string selectedResponsableId;
        //Declarem un llistat de taulers
        public List<Responsable> responsables = new List<Responsable>();

        public GestionarResponsables()
        {
            InitializeComponent();

            //Creem el primer responsable
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
            //Comprovem si existeix un responsable amb la id indicada
            Responsable existingResponsable = responsables.FirstOrDefault(r => r.Id == resp.Id);

            if (existingResponsable != null)
            {
                //Actualitzem el responsable en qüestió
                existingResponsable.Nom = resp.Nom;
                existingResponsable.Cognoms = resp.Cognoms;
                existingResponsable.Email = resp.Email;
                existingResponsable.Dni = resp.Dni;
                existingResponsable.DataNaix = resp.DataNaix;
            }
            else
            {
                //En cas que no existeixi cap responsable amb la id, en creem un de nou
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

        //Per poder accedir al id associat a la llista quan s'ha carregat
        private void txtID(object sender, RoutedEventArgs e)
        {
            TextBlock txtBlock = sender as TextBlock;

            //Accedim a la data generada
            if (txtBlock.DataContext is Responsable responsable)
            {
                //Guardem la ID del responsable
                selectedResponsableId = responsable.Id.ToString();
            }
        }

        private void EsborrarResponsable(object sender, MouseButtonEventArgs e)
        {
            //Mira si hi ha algun item seleccionat
            if (llistaResponsables.SelectedItem != null)
            {
                //Obtenim el responsable de l'item seleccionat
                Responsable responsableToRemove = llistaResponsables.SelectedItem as Responsable;

                if (responsableToRemove != null)
                {
                    if (responsableToRemove.Id == 1)
                    {
                        MessageBox.Show("Aquest registre no es pot esborrar", "Error d'esborrat", MessageBoxButton.OK, MessageBoxImage.Error);
                    } else
                    {
                        //Eliminem el responsable
                        responsables.Remove(responsableToRemove);

                        //Actualitzem la llista
                        llistaResponsables.Items.Refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("No s'ha seleccionat cap ítem", "Error d'esborrat", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
