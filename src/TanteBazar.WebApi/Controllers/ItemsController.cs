using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using TanteBazar.WebApi.Models;
using TanteBazar.Core.Services;
using TanteBazar.WebApi.Mappers;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TanteBazar.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result =  await _itemService.GetItems();
            var response = new List<Models.Item>();

            if (result.Count == 0)
            {
                return NotFound();
            }

            foreach(var r in result)
            {
                response.Add(ItemMapper.MapFromDto(r));
            }

            return Ok(response);


        }
    }
}
