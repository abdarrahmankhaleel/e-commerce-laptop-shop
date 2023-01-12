using LapShop.Bl;
using LapShop.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LapShop.apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItems items;

        public ItemsController(IItems items)
        {
            this.items = items;
        }
        // GET: api/<ItemsController>
        /// <summary>
        /// get all items 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public APIResponse Get()
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.Data = items.GetAll();
            aPIResponse.Errors = null;
            aPIResponse.StatusCode = "200";
            return aPIResponse;
        }

        // GET api/<ItemsController>/5
        /// <summary>
        /// get item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public APIResponse Get(int id)
        {
            APIResponse aPIResponse=new APIResponse();
            aPIResponse.Data = items.GetById(id);
            aPIResponse.Errors = null;
            aPIResponse.StatusCode = "200";
            return aPIResponse;
        }

        // GET api/<ItemsController>/5
        [HttpGet("GetBy/{categoryId}")]
        public APIResponse GetBy(int categoryId)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.Data = items.GetAllDataForIt(categoryId);
            aPIResponse.Errors = null;
            aPIResponse.StatusCode = "200";
            return aPIResponse;

        }

        // POST api/<ItemsController>
        [HttpPost]
        public APIResponse Post([FromBody] TbItem item)
        {
            try
            {
                items.Save(item);
                APIResponse aPIResponse = new APIResponse();
                aPIResponse.Data = "done";
                aPIResponse.Errors = null;
                aPIResponse.StatusCode = "200";
                return aPIResponse;
            }
            catch (Exception ex)
            {
                APIResponse aPIResponse = new APIResponse();
                aPIResponse.Data = null;
                aPIResponse.Errors = ex.Message;
                aPIResponse.StatusCode = "502";
                return aPIResponse;
                
            }
        }

        // POST api/<ItemsController>
        [HttpPost]
        [Route("Delete")]
        public void Delete([FromBody] int id)
        {
            items.Dekete(id);
        }

   

   
    }
}
