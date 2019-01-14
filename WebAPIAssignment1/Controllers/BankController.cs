using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIAssignment1.DatabaseHelper;

namespace WebAPIAssignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : Controller
    {
        private readonly BankDbContext _context;

        public BankController(BankDbContext context)
        {
            _context = context;

        }
        // GetAll() is automatically recognized as
        // http://localhost:<port #>/api/clientaccount
        [HttpGet]
        public IEnumerable<ClientAccount> GetAll()
        {
            return _context.ClientAccounts.ToList();
        }

        // GetById() is automatically recognized as
        // http://localhost:<port #>/api/clientaccount/{id1}{id2}

        // For example:
        // http://localhost:<port #>/api/clientaccount/1 1

        [HttpGet("{id}", Name = "GetClientAccount")]
        public IActionResult GetById(long accountId, long clientId)
        {
            var item = _context.ClientAccounts.FirstOrDefault(t => t.AccountID == accountId && t.ClientID == clientId);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpPost]
        public IActionResult Create([FromBody]ClientAccount clientaccounts)
        {
            if (clientaccounts.Balance < 0)
            {
                return BadRequest();
            }
            _context.ClientAccounts.Add(clientaccounts);
            _context.SaveChanges();
            return new ObjectResult(clientaccounts);
        }
        [HttpPut]
        [Route("MyEdit")] // Custom route
        public IActionResult GetByParams([FromBody]ClientAccount clientaccounts)
        {
            var item = _context.ClientAccounts.Where(t => t.AccountID == clientaccounts.AccountID && t.ClientID == clientaccounts.AccountID).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }

            else
            {
                if (item.Balance < 0)
                {
                    Console.Write("Item must be a positive number");
                }
                else
                {
                    item.Balance = clientaccounts.Balance;
                    _context.SaveChanges();
                }

            }
            return new ObjectResult(item);
        }
        [HttpDelete]
        [Route("MyDelete")] // Custom route
        public IActionResult MyDelete(long accountId, long clientId)
        {
            var item = _context.ClientAccounts.Where(t => t.AccountID == accountId && t.ClientID == clientId).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            _context.ClientAccounts.Remove(item);
            _context.SaveChanges();
            return new ObjectResult(item);
        }

    }