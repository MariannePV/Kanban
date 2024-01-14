using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Kanban
{
    public enum Prioritat
    {
        Baixa,
        Mitjana,
        Alta
    }
    public class Tasca
    {
        private static int lastId = 0;

        private int id { get; set; }

        public string title { get; set; }
        public string description { get; set; }
        public string responsable { get; set; }
        public DateTime dataCreacio { get; set; }
        public DateTime? dataFinalitzacio { get; set; } //Pot ser null ja que potser no saps la data de finalització
        public string etiqueta { get; set; }
        public Prioritat prioritat { get; set; }
        public SolidColorBrush ColorFons { get; set; }
        public int TaulerAsociadoId { get; set; }

        // Constructor que posa la variable dataCreacio amb la data actual
        public Tasca()
        {
            id = ++lastId;
            dataCreacio = DateTime.Now;

        }
    }
}
