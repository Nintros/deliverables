using Deleverables.Web.Models;
using Deliverables.Business.Abstraction;
using Deliverables.Business.Implementation;
using Deliverables.Data;
using Deliverables.Data.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Deleverables.Web.Api
{
    public class TechnicalSkillController : ApiController
    {
        IBaseService<TechnicalSkill> _technicalSkillService;
        public TechnicalSkillController()
        {
            var dataContext = new DataContext();
            _technicalSkillService = new BaseService<TechnicalSkill>(new DataContext(dataContext), new BaseRepository<TechnicalSkill>(dataContext));
        }

        // GET: api/TechnicalSkill
        public async Task<HttpResponseMessage> Get()
        {
            var result = (await _technicalSkillService.GetAllAsync()).Select(x => new TechnicalSkillDto
            {
                LevelId = x.LevelId,
                Name = x.Name
            }); 

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }
    }
}
