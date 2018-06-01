using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Application.Model;


namespace Application.DLL
{
    public class tblDatabaseRepository : BaseRepository
    {
        string Qry = string.Empty;

        public bool CreateDatabase()
        {
            try
            {
                connection();
                string ConnectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=master;Data Source=" + Conn.DataSource + ";";
                var x_DynamicParameters = new DynamicParameters();
                string databasename = Conn.Database;

                Qry = @" IF EXISTS(SELECT name FROM master.sys.databases WHERE [name] = @Database)
                            begin
                            select 1
                            end
                            else
                            begin
                            select 0
                            end";
                x_DynamicParameters.Add("Database", databasename);
                Conn.ConnectionString = ConnectionString;
                int exist = Conn.ExecuteScalar<int>(Qry, x_DynamicParameters);


                if (exist == 0)
                {


                    Qry = @"CREATE DATABASE " + databasename;

                    Conn.ExecuteScalar(Qry);
                    connection();
                    Qry = @"CREATE TABLE [dbo].[tblPersons](
	                    [id] [int] IDENTITY(1,1) NOT NULL,
	                    [first_name] [nvarchar](50) NULL,
	                    [last_name] [nvarchar](50) NULL,
	                    [phone] [nvarchar](15) NULL,
                     CONSTRAINT [PK_tblPersons] PRIMARY KEY CLUSTERED 
                    (
	                    [id] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
                    Conn.ExecuteScalar(Qry);

                    tblPersonsRepository x_tblPersonsRepository = new tblPersonsRepository();

                    x_tblPersonsRepository.Create(new tblPersons() { first_name = "Rey", last_name = "H", phone = "1234-123-1234" });
                    x_tblPersonsRepository.Create(new tblPersons() { first_name = "David", last_name = "Mark", phone = "1234-123-1234" });
                    x_tblPersonsRepository.Create(new tblPersons() { first_name = "Steve", last_name = "Jobs", phone = "1234-123-1234" });
                    x_tblPersonsRepository.Create(new tblPersons() { first_name = "Warren", last_name = "Buffett", phone = "4444-444-4444" });
                    x_tblPersonsRepository.Create(new tblPersons() { first_name = "Larry", last_name = "Page", phone = "1234-123-1234" });
                    x_tblPersonsRepository.Create(new tblPersons() { first_name = "Jeff", last_name = "Bezos", phone = "1234-123-1234" });
                    x_tblPersonsRepository.Create(new tblPersons() { first_name = "Mark", last_name = "Zuckerberg", phone = "1234-123-1234" });
                }

               

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
