using System;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderObject
{
    public class Bdd
    {
        public static DbConnection GetConnexion
        {
            get
            {
                string fournisseurDeDonnees = ConfigurationManager.AppSettings["PROVIDER"];
                DbProviderFactory factory = DbProviderFactories.GetFactory(fournisseurDeDonnees);
                DbConnection connection = factory.CreateConnection();
                string ch = String.Format(ConfigurationManager.ConnectionStrings["fournisseurDeDonnees"].ToString(), ConfigurationManager.AppSettings["SERVERIN"],
                                                                                        ConfigurationManager.AppSettings["PORTIN"],
                                                                                        ConfigurationManager.AppSettings["SID"],
                                                                                        ConfigurationManager.AppSettings["USERID"],
                                                                                        ConfigurationManager.AppSettings["PWD"]
                                                                                        );

                /*String CnString = "server=localhost; User Is=adomysql;PAssword=Boite de 5; Persist Security Info=true;database=dbcours";*/
                connection.ConnectionString = ch;
              
                return connection;
            }
        }
        public static DbCommand GetCommande
        {
            get
            {
                string fournisseurDeDonnees = ConfigurationManager.AppSettings["PROVIDER"];
                DbProviderFactory factory = DbProviderFactories.GetFactory(fournisseurDeDonnees);
                DbCommand commande = factory.CreateCommand();
                return commande;
            }
        }
    }
}
