using Shared.Dtos;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repositories
{
    public interface IAppClient
    {
        Task<IEnumerable<AppClientDto>> ListAllClient();
        Task<IEnumerable<AppClientDto>> ListAllClient(string userid);
        Task<bool> CreateClient(AppClient request);
        Task<bool> UpdateClient(AppClientDto request);
        Task<bool> DeleteClient(string id, string rev);
    }
}
