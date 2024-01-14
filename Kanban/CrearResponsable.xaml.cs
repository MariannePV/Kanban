using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Lógica de interacción para CrearResponsable.xaml
    /// </summary>
    public partial class CrearResponsable : Window
    {
        private GestionarResponsables mainWindow;
        private Responsable responsableExistent;

        public CrearResponsable(GestionarResponsables mainWindow, Responsable responsableExistent = null)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.responsableExistent = responsableExistent;

            if (responsableExistent != null)
            {
                txtNom.Text = responsableExistent.Nom;
                txtCognoms.Text = responsableExistent.Cognoms;
                txtEmail.Text = responsableExistent.Email;
                txtDni.Text = responsableExistent.Dni;
                selectedDate.SelectedDate = responsableExistent.DataNaix;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) //Event del propi Windows que és crida quant mentenim el mouse en la pestanya de windows
        {
            this.DragMove(); //Mètode per poder moure la finestra
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            lblError.Text = "";

            if (string.IsNullOrWhiteSpace(txtNom.Text) || string.IsNullOrWhiteSpace(txtCognoms.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtDni.Text) || selectedDate.SelectedDate == null)
            {
                // Muestra mensaje de error
                lblError.Text = "Has d'emplenar tots els camps";
            }
            else
            {
                // Update existing Responsable or create a new one
                if (responsableExistent != null)
                {
                    // Edit existing Responsable
                    responsableExistent.Nom = txtNom.Text;
                    responsableExistent.Cognoms = txtCognoms.Text;
                    responsableExistent.Email = txtEmail.Text;
                    responsableExistent.Dni = txtDni.Text;
                    responsableExistent.DataNaix = (DateTime)selectedDate.SelectedDate;
                }
                else
                {
                    // Create new Responsable
                    Responsable responsable = new Responsable
                    {
                        Nom = txtNom.Text,
                        Cognoms = txtCognoms.Text,
                        Email = txtEmail.Text,
                        Dni = txtDni.Text,
                        DataNaix = (DateTime)selectedDate.SelectedDate
                    };

                    mainWindow.ActualitzarResponsable(responsable);
                }

                this.Close();
            }
        }

        private void CerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
