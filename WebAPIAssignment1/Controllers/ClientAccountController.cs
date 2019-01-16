using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebAPIAssignment1.DatabaseHelper;

namespace WebAPIAssignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientAccountController : Controller
    {
        private readonly BankDbContext _context;

        public ClientAccountController(BankDbContext context)
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

        //[HttpGet("{clientID} {accountID}", Name = "GetAccount")]
        [HttpGet]
        [Route("GetAccount")]
        public IActionResult GetById(int clientID, int accountID)
        {
            dynamic accountArray = new JArray();
            dynamic responseObj = new JObject();

            var item = _context.ClientAccounts.FirstOrDefault(t => t.AccountID == accountID && t.ClientID == clientID);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                responseObj.Balance = item.Balance;
                var item2 = _context.ClientProfiles.FirstOrDefault(t => t.ID == clientID);
                if (item2 == null)
                {
                    return NotFound();
                }
                else
                {
                    responseObj.FirstName = item2.FirstName;
                    responseObj.LastName = item2.LastName;
                    var item3 = _context.AccountTypes.FirstOrDefault(t => t.ID == accountID);
                    if (item3 == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        responseObj.AccountDescription = item3.AccountDescription;
                        accountArray.Add(responseObj);
                    }
                }

                
            }
            return Json(accountArray);

        }
        [HttpPost]
       
        public IActionResult Create([FromBody]ClientAccount clientaccount)
        {
            Console.Write(clientaccount.ClientID);
            Console.Write(clientaccount.AccountID);
            Console.Write(clientaccount.Balance);
            if (clientaccount.Balance >= 0 )
            {
                _context.ClientAccounts.Add(clientaccount);
                _context.SaveChanges();
                return new ObjectResult(clientaccount);
            }
            else
            {
                return BadRequest();
                
            }
            
        }
       
        [HttpDelete]
        [Route("DeleteAccount")] // Custom route
        public IActionResult DeleteAccount(int clientID, int accountID)
        {
            var item = _context.ClientAccounts.Where(t => t.ClientID == clientID && t.AccountID == accountID ).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            _context.ClientAccounts.Remove(item);
            _context.SaveChanges();
            return new ObjectResult(item);
        }
        [HttpPut]
        [Route("EditAccount")] // Custom route
        public IActionResult GetByParams([FromBody]ClientAccount clientaccount)
        {
            var item = _context.ClientAccounts.Where(t => t.ClientID == clientaccount.ClientID && t.AccountID == clientaccount.AccountID).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                item.Balance = clientaccount.Balance;
                _context.SaveChanges();
            }
            return new ObjectResult(item);
        }

        //[HttpPut]
        //[Route("EditAccount")] // Custom route
       // public IActionResult GetByParams([FromBody]ClientAccount clientaccount)
        //{
        //    var item = _context.ClientAccounts.Where(t => t.ClientID == clientaccount.ClientID && t.AccountID == clientaccount.AccountID).FirstOrDefault();
         //   if (item == null)
         //   {
        //        return NotFound();
         //   }
         //   else
         //   {
        //        item.Balance = clientaccount.Balance;
        //        _context.SaveChanges();
        //    }
        //    return new ObjectResult(item);
     //   }



    }

}