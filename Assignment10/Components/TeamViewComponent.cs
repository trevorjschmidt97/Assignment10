using System;
using System.Linq;
using Assignment10.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
namespace Assignment10.Components
{
    public class TeamViewComponent : ViewComponent
    {
        private BowlingLeagueContext context { get; set; }
        
        public TeamViewComponent(BowlingLeagueContext con)
        {
            context = con;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedTeam = RouteData?.Values["teamname"];

            return View(context.Teams
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
