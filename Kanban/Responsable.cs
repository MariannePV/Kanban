using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban
{
    public class Responsable
    {
        private static int lastId = 0;

        //Atributs
        private int id;
        private string nom;
        private string cognoms;
        private string email;
        private string dni;
        private DateTime dataNaix;

        //Constructor
        public Responsable()
        {
            id = ++lastId;
            nom = "";
            cognoms = "";
            email = "";
            dni = "";
            dataNaix = DateTime.Now;
        }

        public Responsable(string nom, string cognoms, string email, string dni, DateTime dataNaix)
        {
            id = ++lastId;
            this.nom = nom;
            this.cognoms = cognoms;
            this.email = email;
            this.dni = dni;
            this.dataNaix = dataNaix;
        }

        //Funcionalitat dels atributs
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public string Cognoms
        {
            get { return cognoms; }
            set { cognoms = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Dni
        {
            get { return dni; }
            set { dni = value; }
        }

        public DateTime DataNaix
        {
            get { return dataNaix; }
            set { dataNaix = value; }
        }
    }
}
