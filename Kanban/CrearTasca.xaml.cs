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
    /// Lógica de interacción para CrearTasca.xaml
    /// </summary>
    public partial class CrearTasca : Window //Finestra de Windows que s'obra quant volem crear una nova tasca
    {
        private MainWindow mainWindow; //Referencia a la pestanya inicial
        private Tasca tascaAEditar;
        public CrearTasca(MainWindow mainWindow) //Primer constructor de la classeTasca
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.DataContext = this;
            txtDataCreacio.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ActualizarComboBox(mainWindow.taulers);

            if (cmbTauler.Items.Count > 0) //Per posar per defecte el desplegable de taulers amb el primer
            {
                cmbTauler.SelectedIndex = 0;
            }
        }
        public CrearTasca(MainWindow mainWindow, Tasca tascaAEditar) : this(mainWindow) //Segon constructor només l'utilitzem al editar la tasca
        {
            this.tascaAEditar = tascaAEditar;

            // Posem els valors de la tasca en la finestra
            if (tascaAEditar != null)
            {
                txtTitle.Text = tascaAEditar.title;
                txtDescription.Text = tascaAEditar.description;
                txtResponsable.Text = tascaAEditar.responsable;
                selectedDate.SelectedDate = tascaAEditar.dataFinalitzacio;
                txtLabel.Text = tascaAEditar.etiqueta;
                ColorRectangle.Fill = tascaAEditar.ColorFons;
                cmbPrioritat.SelectedIndex = (int)tascaAEditar.prioritat;

                // El combobox de mostrar els taulers busca la id associada que te aquella tasca
                cmbTauler.SelectedItem = mainWindow.taulers.FirstOrDefault(t => t.Id == tascaAEditar.TaulerAsociadoId);
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

            }
        }
        private void Guardar_Click(object sender, RoutedEventArgs e) //Funció que és crida al clicar guardar.
        {
            lblError.Text = "";

            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtDescription.Text) || string.IsNullOrWhiteSpace(txtResponsable.Text))
            {
                // Missatge de error
                lblError.Text = "Has d'emplenar el títol, descripció i responsable";
            }
            else
            {
                if (tascaAEditar != null) //Això implica que hem cridat aquesta pestanya desde el botó de editar, ja que tascaAEditar se li posa un valor només al segon constructor
                {
                    // Els valors que té la pestanya l'hi posem dintre de la classe
                    tascaAEditar.title = txtTitle.Text;
                    tascaAEditar.description = txtDescription.Text;
                    tascaAEditar.responsable = txtResponsable.Text;
                    tascaAEditar.dataFinalitzacio = selectedDate.SelectedDate;
                    tascaAEditar.etiqueta = txtLabel.Text;
                    tascaAEditar.ColorFons = (SolidColorBrush)ColorRectangle.Fill;
                    tascaAEditar.prioritat = ObtenirPrioritatSeleccionada();

                    // Obtenim el tauler del combobox
                    Tauler taulerSeleccionat = (Tauler)cmbTauler.SelectedItem;

                    if (taulerSeleccionat != null && tascaAEditar.TaulerAsociadoId != taulerSeleccionat.Id)
                    {
                        // Li canviem la id 
                        tascaAEditar.TaulerAsociadoId = taulerSeleccionat.Id;

                        // Elimina la tasca del tauler anterior
                        Tauler taulerAnterior = mainWindow.taulers.FirstOrDefault(t => t.Tasques.Contains(tascaAEditar));
                        if (taulerAnterior != null)
                        {
                            taulerAnterior.Tasques.Remove(tascaAEditar);
                        }

                        // Afegim la tasca al nou tauler
                        taulerSeleccionat.Tasques.Add(tascaAEditar);
                    }
                }
                else
                {
                    // Si tascaAeditar és null, implica que hem cridat aquesta pestanya per crear una nova tasca
                    Tasca tasca = new Tasca
                    {
                        title = txtTitle.Text,
                        description = txtDescription.Text,
                        responsable = txtResponsable.Text,
                        dataFinalitzacio = selectedDate.SelectedDate,
                        etiqueta = txtLabel.Text,
                        ColorFons = (SolidColorBrush)ColorRectangle.Fill,
                        prioritat = ObtenirPrioritatSeleccionada()
                    };

                    txtDataCreacio.Text = tasca.dataCreacio.ToString("yyyy-MM-dd HH:mm:ss");

                    // Referenciem el tauler amb el combobox
                    Tauler taulerSeleccionat = (Tauler)cmbTauler.SelectedItem;

                    if (taulerSeleccionat != null)
                    {
                        //Afegim la tasca dintre del tauler
                        taulerSeleccionat.Tasques.Add(tasca);
                        tasca.TaulerAsociadoId = taulerSeleccionat.Id; //L'hi posem la id del tauler
                    }
                }
                this.Close();
            }
        }

        private Prioritat ObtenirPrioritatSeleccionada()
        {
            switch (cmbPrioritat.SelectedIndex)
            {
                case 0:
                    return Prioritat.Baixa;
                case 1:
                    return Prioritat.Mitjana;
                case 2:
                    return Prioritat.Alta;
                default:
                    return Prioritat.Baixa;
            }
        }


        public void ActualizarComboBox(List<Tauler> taulers)
        {
            cmbTauler.ItemsSource = taulers;
        }

        private void AgregarEtiqueta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
