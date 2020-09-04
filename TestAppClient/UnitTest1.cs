using Shared.Repositories;
using System;
using System.Threading.Tasks;
using WebTest.Repositories;
using Xunit;
using System.Collections;
using System.Linq;
using CouchDBService;
using Shared.Entities;
using System.Collections.Generic;
using Shared.Dtos;

namespace TestAppClient
{
    public class UnitTest1
    {
        private IAppClient appclient;

        public UnitTest1()
        {
            ICouchContext couchContext = new CouchContext();
            CouchDBHelper couchDBHelper = new CouchDBHelper("http",
                "127.0.0.1",
                "appclient",
                "admin",
                "1qaz2wsx");
            appclient = new AppClientRepo(couchContext, couchDBHelper);
        }

        [Theory(DisplayName = "ສະແດງລາຍການ Apps(Clients) ທັງຫມົດທີ່ມີ")]
        [InlineData(10)]
        public async Task ListAllClientAsync(int expected)
        {
            //
            var result = await appclient.ListAllClient();

            Assert.Equal(expected, result.Count());
        }

        [Theory(DisplayName = "ສະແດງລາຍການ Apps(Clients) ທັງຫມົດທີມີ ຕາມ ຂອງ Users ທີ່ເປັນເຈົ້າຂອງ")]
        [InlineData(9, "1qaz2wsx")]
        public async Task ListAllClientByUserIdAsync(int expected, string userid)
        {
            //
            var result = await appclient.ListAllClient(userid);

            Assert.Equal(expected, result.Count());
        }

        [Theory(DisplayName = "ສ້າງ App(Client) ໃຫມ່ ຕາມຂໍ້ມູນເລີ່ມຕົ້ນ ສຳເລັດ")]
        [InlineData(15, "demo_client15", "Demo Client 15", "This is desciption", "pop", "2020-09-04 ")]
        public async Task CreateNewClient(int? appid, string clientid, string clientname,
            string description, string userid, string created)
        {

            AppClient req = new AppClient()
            {
                AppID = appid,
                ClientId = clientid,
                ClientName = clientname,
                Created = created,
                Description = description,
                UserID = userid
            };

            var result = await appclient.CreateClient(req);

            Assert.True(result);
        }

        [Theory(DisplayName = "ອັບເດດ App(Client) ຕາມຂໍ້ມູນທີ່ກຳຫນົດ ສຳເລັດ")]
        [ClassData(typeof(UpdateClientDtoTest))]
        public async Task UpdateClient(AppClientDto req)
        {
            var result = await appclient.UpdateClient(req);

            Assert.True(result);
        }

        [Theory(DisplayName = "ລຶບ App(Client) ຕາມຂໍ້ມູນທີ່ກຳຫນົດ ສຳເລັດ")]
        [InlineData("870cbca13cb5e6fb0f6c305643013eff", "3-dab9b1e062fda1a82c73f92e83fd78cf")]
        public async Task DeleteClient(string id, string rev)
        {
            var result = await appclient.DeleteClient(id, rev);

            Assert.True(result);
        }
    }

    public class UpdateClientDtoTest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new AppClientDto
                {
                    AppID = 10,
                    ClientId = "thely_client",
                    ClientName = "Update Client App",
                    Description = "This is test update client app",
                    Created = "2020-09-04 00:36:00",
                    Id = "870cbca13cb5e6fb0f6c305643013eff",
                    Revision = "2-1060e631ff9549333b7cacfc11eca159",
                    UserID = "tommy"
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    //    1.
    //2.
    //3.
    //4.
    //        12.
}
