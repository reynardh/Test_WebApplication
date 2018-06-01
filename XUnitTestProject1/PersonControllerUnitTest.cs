using System;
using WebApplication6.Controllers;
using Xunit;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Application.Model;
using Application.DLL;

namespace XUnitTestProject1
{
    public class PersonControllerUnitTest
    {

        public PersonControllerUnitTest()
        {
            var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
            Application.Util.AppSetting.init(config);
        }


        [Fact]
        public void List_Test()
        {
            try
            {
                PersonController personController = new PersonController();
                var list = personController.List();
                Assert.IsAssignableFrom<IEnumerable<tblPersons>>(list);
            }
            catch (Exception ex)
            {

                Assert.True(false);
            }

        }
        [Fact]
        public void Create_Test()
        {
            try
            {
                PersonController personController = new PersonController();
                int id = personController.Create(new tblPersons()
                {
                    first_name = "test",
                    last_name = "test",
                    phone = "1111-1111-111"
                });

                Assert.True(id > 0);
                //clean up data
                tblPersonsRepository x_tblPersonsRepository = new tblPersonsRepository();
                x_tblPersonsRepository.Delete(id);

            }
            catch (Exception ex)
            {

                Assert.True(false);
            }

        }
       [Fact]
        public void Get_Test()
        {
            PersonController personController = new PersonController();
            int id = personController.Create(new tblPersons()
            {
                first_name = "test",
                last_name = "test",
                phone = "1111-1111-111"
            });

            var person = personController.Get(id);
            Assert.IsAssignableFrom<tblPersons>(person);
            Assert.Equal("test", person.first_name);
            //clean up data
            tblPersonsRepository x_tblPersonsRepository = new tblPersonsRepository();
            x_tblPersonsRepository.Delete(id);

        }
        [Fact]
        public void Delete_Test()
        {

            PersonController personController = new PersonController();
            int id = personController.Create(new tblPersons()
            {
                first_name = "test",
                last_name = "test",
                phone = "1111-1111-111"
            });

            var person = personController.Get(id);
            Assert.IsAssignableFrom<tblPersons>(person);
            Assert.Equal("test", person.first_name);
            bool result = personController.Delete(id);
            Assert.True(result);
        }
        [Fact]
        public void Edit_Test()
        {
            PersonController personController = new PersonController();
            int id = personController.Create(new tblPersons()
            {
                first_name = "test",
                last_name = "test",
                phone = "1111-1111-111"
            });

            var person = personController.Get(id);
            person.first_name = "testedit";
            personController.Edit(person);
            var personedited = personController.Get(id);
            Assert.Equal("testedit", personedited.first_name);
            //clean up data
            tblPersonsRepository x_tblPersonsRepository = new tblPersonsRepository();
            x_tblPersonsRepository.Delete(id);

        }
    }
}
