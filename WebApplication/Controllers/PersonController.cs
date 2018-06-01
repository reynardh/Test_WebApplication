using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DLL;
using Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication6.Controllers
{

    public class PersonController : Controller
    {
        // goes through routers and go through database.
        /// <summary>
        ///  api for get all persons
        /// </summary>
        /// <returns>IEnumerable<tblPersons></returns>
        [Route("api/Person/List")]
        [HttpGet]
        public IEnumerable<tblPersons> List()
        {
           
            tblPersonsRepository x_tblPersonsRepository = new tblPersonsRepository();
            List<tblPersons> _ListOfPersons = x_tblPersonsRepository.GetList();

            return _ListOfPersons;
        }
        /// <summary>
        /// api for get person details
        /// </summary>
        /// <param name="id"></param>
        /// <returns>tblPersons</returns>
        [Route("api/Person/Get/{id}")]
        [HttpGet]
        public tblPersons Get(int id)
        {
            tblPersonsRepository x_tblPersonsRepository = new tblPersonsRepository();
            return x_tblPersonsRepository.Get(id);
        }
        /// <summary>
        /// api for save data of person
        /// </summary>
        /// <param name="tblPersons"></param>
        /// <returns>int personid</returns>
        [HttpPost]
        [Route("api/Person/Create")]
        public int Create(tblPersons tblPersons)
        {
            tblPersonsRepository x_tblPersonsRepository = new tblPersonsRepository();
            return x_tblPersonsRepository.Create(tblPersons);
        }
        /// <summary>
        /// api for delete person
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        [HttpDelete]
        [Route("api/Person/Delete/{id}")]
        public bool Delete(int id)
        {

            tblPersonsRepository x_tblPersonsRepository = new tblPersonsRepository();
            return x_tblPersonsRepository.Delete(id);
        }
        /// <summary>
        ///  api for update person
        /// </summary>
        /// <param name="tblPersons"></param>
        /// <returns>bool</returns>
        [HttpPut]
        [Route("api/Person/Edit")]
        public bool Edit(tblPersons tblPersons)
        {
            tblPersonsRepository x_tblPersonsRepository = new tblPersonsRepository();
            return x_tblPersonsRepository.Update(tblPersons);
        }

    }
}
