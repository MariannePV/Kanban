using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Kanban
{
    public class Tauler
    {
        private static int lastId = 0;

        //Atributs
        private int id;
        private string titol;
        private string color;
        public List<Tasca> Tasques { get; set; } = new List<Tasca>();

        //Constructors
        public Tauler()
        {
            id = ++lastId;
            titol = "Tauler";
            color = "Magenta";
        }

        public Tauler (string titol, string color)
        {
            id = ++lastId;
            this.titol = titol;
            this.color = color;
        }

        //Funcionalitat dels atributs
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Titol
        {
            get { return titol; }
            set { titol = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }
    }
}
