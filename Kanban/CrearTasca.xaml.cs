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
    /// Lógica de interacción para CrearTasca.xaml
    /// </summary>
    public partial class CrearTasca : Window //Finestra de Windows que s'obra quant volem crear una nova tasca
    {
        public CrearTasca()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e) //Event del propi Windows que és crida quant mentenim el mouse en la pestanya de windows
        {
            this.DragMove(); //Mètode per poder moure la finestra
        }
    }
}
