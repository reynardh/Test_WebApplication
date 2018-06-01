using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Application.Model;


namespace Application.DLL
{
    public class tblPersonsRepository : BaseRepository
    {
        string Qry = string.Empty;
        /// <summary>
        ///  Persons List 
        /// </summary>
        /// <returns></returns>
        public List<tblPersons> GetList()
        {
            try
            {
                connection();
                Qry = @"SELECT [id],[first_name],[last_name],[phone] FROM [dbo].[tblPersons]";
                return Conn.Query<tblPersons>(Qry).ToList();
            }
            catch (Exception ex)
            {
                return new List<tblPersons>();
            }
        }
        /// <summary>
        ///  Get Person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tblPersons Get(int id)
        {
            try
            {
                connection();
                Qry = @"SELECT [id],[first_name],[last_name],[phone] FROM [dbo].[tblPersons] where id=@id";

                var x_DynamicParameters = new DynamicParameters();
                x_DynamicParameters.Add("id", id);
                return Conn.Query<tblPersons>(Qry, x_DynamicParameters).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new tblPersons();
            }
        }
        /// <summary>
        ///  Create Person Details
        /// </summary>
        /// <param name="p_tblPersons"></param>
        /// <returns></returns>
        public int Create(tblPersons p_tblPersons)
        {
            try
            {

                connection();
                var x_DynamicParameters = new DynamicParameters();
                x_DynamicParameters.Add("first_name", p_tblPersons.first_name);
                x_DynamicParameters.Add("last_name", p_tblPersons.last_name);
                x_DynamicParameters.Add("phone", p_tblPersons.phone);
         
                Qry = @"INSERT INTO [dbo].[tblPersons]
                           ([first_name]
                           ,[last_name]
                           ,[phone])
                            VALUES
                           (@first_name
                           ,@last_name
                           ,@phone)select scope_identity()";

                int x_Id = Conn.ExecuteScalar<int>(Qry, x_DynamicParameters);
                return x_Id;

            }
            catch (Exception ex)
            {
                 return 0;
              
            }
        }
        /// <summary>
        ///  Update Person Details
        /// </summary>
        /// <param name="p_tblPersons"></param>
        /// <returns></returns>
        public bool Update(tblPersons p_tblPersons)
        {
            try
            {
                connection();

                var x_DynamicParameters = new DynamicParameters();
                x_DynamicParameters.Add("first_name", p_tblPersons.first_name);
                x_DynamicParameters.Add("last_name", p_tblPersons.last_name);
                x_DynamicParameters.Add("phone", p_tblPersons.phone);
                x_DynamicParameters.Add("id", p_tblPersons.id);

                Qry = @"UPDATE [dbo].[tblPersons] SET [first_name] = @first_name, [last_name] = @last_name, [phone] = @phone WHERE id=@id";

                Conn.ExecuteScalar<int>(Qry, x_DynamicParameters);
                return true;
               
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        ///  Delete Person Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            try
            {
                connection();

                var x_DynamicParameters = new DynamicParameters();
                x_DynamicParameters.Add("id",id);
                Qry = @"delete from [dbo].[tblPersons] WHERE id=@id";

                Conn.ExecuteScalar<int>(Qry, x_DynamicParameters);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
