
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;

namespace ReaderObject
{
    class EmployeServices
    {
        private static EmployeServices instance;

        public static EmployeServices Instance
        {
            get
            {
                return instance ?? (instance = new EmployeServices());
            }
        }

        public static Employe HydrateEmploye(DbDataReader readerEmploye)
        {
            Employe employe = new Employe();
            employe.Numemp = Convert.ToInt16(readerEmploye["NUMEMP"]);
            employe.Nomemp = readerEmploye["NOMEMP"] as String;
            employe.Prenoemp = readerEmploye["PRENOMEMP"] as String;
            employe.Poste = readerEmploye["POSTE"] as String;
            employe.Salaire = Convert.ToSingle(readerEmploye["SALAIRE"]);
            if (readerEmploye["Prime"] == DBNull.Value)
            {
                employe.Prime = null;
            }
            else
            {
                employe.Prime = Convert.ToSingle(readerEmploye["Prime"]);
            }

            employe.CodeProjet = readerEmploye["CODEPROJET"] as String;
            if (readerEmploye["SUPERIEUR"] == DBNull.Value)
            {
                employe.Superieur = null;
            }
            else
            {
                employe.Superieur = Convert.ToInt16(readerEmploye["SUPERIEUR"]);
            }
            return employe;
        }

        public List<Employe> FindAllEmployes()
        {
            var MaConnexion = Bdd.GetConnexion;
            
                List<Employe> employes = new List<Employe>();
                try
                {
                    using (DbCommand cmdTousLesEmployes = Bdd.GetCommande)
                    {
                        cmdTousLesEmployes.CommandText = "select * from EMPLOYE";
                        cmdTousLesEmployes.Connection = MaConnexion;
                        MaConnexion.Open();
                        DbDataReader readerEmploye = cmdTousLesEmployes.ExecuteReader();
                        while (readerEmploye.Read())
                        {
                            Employe employe = HydrateEmploye(readerEmploye);
                            employes.Add(employe);
                        }
                        readerEmploye.Close();
                    }
                    MaConnexion.Close();
                }
                catch (DbException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return employes;
            
        }

        public Employe FindEmployeById(Int16 id)
        {
            using(var MaConnexion = Bdd.GetConnexion)
            {
                Employe employe = null;
                try
                {
                    using (DbCommand cmdTousLesEmployes = Bdd.GetCommande)
                    {
                        cmdTousLesEmployes.CommandText = "select * from EMPLOYE where numemp= :numemp";
                        DbParameter pId = cmdTousLesEmployes.CreateParameter();
                        pId.ParameterName = "numemp";
                        pId.Direction = ParameterDirection.Input;
                        pId.DbType = DbType.Int16;
                        pId.Value = id;
                        cmdTousLesEmployes.Parameters.Add(pId);
                        MaConnexion.Open();
                        DbDataReader readerEmploye = cmdTousLesEmployes.ExecuteReader();
                        if (readerEmploye.Read())
                        {
                            employe = HydrateEmploye(readerEmploye);
                        }
                        readerEmploye.Close();
                    }
                    MaConnexion.Close();
                }
                catch (DbException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return employe;
            }
        }
    }
}
