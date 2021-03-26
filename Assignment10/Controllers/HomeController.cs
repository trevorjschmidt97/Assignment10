using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assignment10.Models;
using Microsoft.EntityFrameworkCore;
using Assignment10.Models.ViewModels;

namespace Assignment10.Controllers
{
    public class HomeController : Controller
    {
        private BowlingLeagueContext context { get; set; }

        public HomeController(BowlingLeagueContext con)
        {
            context = con;
        }

        public IActionResult Index(long? teamID, string teamname, int pageNum = 1)
        {
            int pageSize = 5;


            return View(new IndexViewModel
            {
                Bowlers = (context.Bowlers
                    .Where(b => b.TeamId == teamID || teamID == null)
                    .OrderBy(b => b.BowlerFirstName)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()),
                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    // If the team hasn't been selected, return the count of total players,
                    // else, do a freaking query to find it
                    TotalNumItems = (teamID == null ? context.Bowlers.Count()
                    : context.Bowlers.Where(x => x.TeamId == teamID).Count())

                },
                TeamName = teamname
            });
        }
    }
}
