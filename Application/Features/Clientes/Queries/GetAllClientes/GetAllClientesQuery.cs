using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clientes.Queries.GetAllClientes
{
    public class GetAllClientesQuery :IRequest<PagedResponse<List<ClienteDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
    public class GetAllClientesQueryHandler : IRequestHandler<GetAllClientesQuery, PagedResponse<List<ClienteDto>>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;

        public GetAllClientesQueryHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper, IDistributedCache distributedCache)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<PagedResponse<List<ClienteDto>>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"listadoClientes_{request.PageNumber}_{request.PageSize}_{request.Nombre}_{request.Apellido}";
            string serializedlistadoClientes;

            var redisListadoClientes = await _distributedCache.GetAsync(cacheKey);
            var listadoClientes = new List<Cliente>();
            if(redisListadoClientes != null)
            {
                serializedlistadoClientes  = Encoding.UTF8.GetString(redisListadoClientes);
                listadoClientes = JsonConvert.DeserializeObject<List<Cliente>>(serializedlistadoClientes);
            }
            else
            {
                listadoClientes = await _repositoryAsync.ListAsync(new PagedClientesSpecification(request.PageNumber, request.PageSize, request.Nombre, request.Apellido));
                serializedlistadoClientes = JsonConvert.SerializeObject(listadoClientes);
                redisListadoClientes = Encoding.UTF8.GetBytes(serializedlistadoClientes);

                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                await _distributedCache.SetAsync(cacheKey, redisListadoClientes, options);
            }

            var clientesDto = _mapper.Map<List<ClienteDto>>(listadoClientes);
            return new PagedResponse<List<ClienteDto>>(clientesDto, request.PageNumber, request.PageSize);


        }
    }
}
