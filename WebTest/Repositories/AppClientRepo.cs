using CouchDBService;
using Shared.Dtos;
using Shared.Entities;
using Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Repositories
{
    public class AppClientRepo : IAppClient
    {
        private readonly ICouchContext couchContext;
        private readonly CouchDBHelper couchDBHelper;

        public AppClientRepo(ICouchContext couchContext, CouchDBHelper couchDBHelper)
        {
            this.couchContext = couchContext;
            this.couchDBHelper = couchDBHelper;
        }

        public async Task<bool> CreateClient(AppClient request)
        {
            var result = await couchContext.InsertAsync<AppClient>
                (couchDBHelper, request);

            return result.IsSuccess;
        }

        public async Task<bool> DeleteClient(string id, string rev)
        {
            var result = await couchContext.DeleteAsync(couchDBHelper,
                id, rev);

            return result.IsSuccess;
        }

        public async Task<IEnumerable<AppClientDto>> ListAllClient()
        {
            var result = await couchContext.ViewQueryAsync<AppClientDto>
                (couchDBHelper, "query", "list", "none", 10, 0, false, false);

            var res = result.Rows.Select(x => new AppClientDto
            {
                AppID = x.Value.AppID,
                ClientId = x.Value.ClientId,
                ClientName = x.Value.ClientName,
                Created = x.Value.Created,
                Description = x.Value.Description,
                Id = x.Value.Id,
                Revision = x.Value.Revision,
                UserID = x.Value.UserID
            }).ToList();

            return res;
        }

        public async Task<IEnumerable<AppClientDto>> ListAllClient(string userid)
        {
            var result = await couchContext.ViewQueryAsync<AppClientDto>
                (couchDBHelper, "query", "list", userid, 10, 0, false, false);

            var res = result.Rows.Select(x => new AppClientDto
            {
                AppID = x.Value.AppID,
                ClientId = x.Value.ClientId,
                ClientName = x.Value.ClientName,
                Created = x.Value.Created,
                Description = x.Value.Description,
                Id = x.Value.Id,
                Revision = x.Value.Revision,
                UserID = x.Value.UserID
            }).ToList();

            return res;
        }

        public async Task<bool> UpdateClient(AppClientDto request)
        {
            var result = await couchContext.EditAsync<AppClientDto>
                (couchDBHelper, request);

            return result.IsSuccess;
        }
    }
}
