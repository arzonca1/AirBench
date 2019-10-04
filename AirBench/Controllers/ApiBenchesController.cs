using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AirBench.Data;
using AirBench.Models;
using System.Threading.Tasks;

namespace AirBench.Controllers
{
    public class ApiBenchesController : ApiController
    {
        private IBenchRepository _repository;

        public ApiBenchesController(IBenchRepository repository)
        {
            _repository = repository;
        }

        //[Route("api/bench/create")]
        //async public Task<Bench> Post(BenchCreateRequest bcr)
        //{
            

        //    return await _repository.AddBenchAsync(0, bcr.Seats, bcr.Longitude, bcr.Latitude, bcr.Name, bcr.CreatorId);
        //}

        [Route("api/bench/all")]
        async public Task<List<Bench>> Get()
        {
            return await _repository.GetBenchListAsync();
        }

        [Route("api/bench/{benchId}")]
        async public Task<Bench> Get(int benchId)
        {
            return await _repository.GetBenchSingleAsync(benchId);
        }

        //[Route("api/bench/{benchId}/addcomment")]
        //async public Task<Comment> Post(int benchId, CommentAddRequest car)
        //{
        //    return await _repository.AddCommentAsync(benchId, car.Text, car.Rating);
        //}

    }
}