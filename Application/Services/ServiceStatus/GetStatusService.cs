using Application.Interfaces.IStatus;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServiceStatus
{
    public class GetStatusService : IGetStatusService
    {
        private readonly IStatusQuery _statusQuery;

        public GetStatusService(IStatusQuery statusQuery)
        {
            _statusQuery = statusQuery;
        }

        public async Task<List<StatusResponse>> GetAllStatus()
        {
            var statues = await _statusQuery.GetAllStatus();

            return statues.Select(status => new StatusResponse
            {
                id = status.Id,
                name = status.Name,
            }).ToList();
        }
    }
}
