using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.Json;
using dynamify.Models;
using dynamify.Models.SiteModels;
using dynamify.Models.JsonModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;


namespace dynamify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SiteController : ControllerBase
    {
        private MyContext dbContext;

        private readonly ILogger<SiteController> _logger;

        public SiteController(ILogger<SiteController> logger, MyContext context)
        {
            _logger = logger;
            dbContext = context;
        }

        [HttpGet] //all sites by admin
        [Route("/[controller]/get_by_admin/{admin_id}")]
        public IEnumerable<Site> GetByAdmnId(int admin_id){
            List<Site> OwnedSites = dbContext.Sites.Where(x => x.admin_id == admin_id).ToList();
            return OwnedSites;
        }

        [HttpGet]
        [Route("/[controller]/get/{site_id_parameter}")]
        public ActionResult<Site> GetSiteById(int site_id_parameter){

            System.Console.WriteLine($"Query id: {site_id_parameter}");
            
            Site foundSite = dbContext.Sites.Where(x => x.site_id == site_id_parameter).Select( site => new Site()
            {
                site_id = site_id_parameter,
                title = site.title,
                active = site.active,
                admin_id = site.admin_id,
                owner = dbContext.Admins.Where(x => x.admin_id == site.admin_id).Select(s => new Admin()
                {
                    admin_id = s.admin_id,
                    first_name = s.first_name,
                    last_name = s.last_name,
                    email = s.email,
                    password = s.password
                }).FirstOrDefault(),
                paragraph_boxes = dbContext.ParagraphBoxes.Where(x => x.site_id == site.site_id).Select(box => new ParagraphBox()
                {
                    paragraph_box_id = box.paragraph_box_id,
                    title = box.title,
                    content = box.content,
                    site_id = site.site_id
                }).Where(x => x.site_id == site_id_parameter).ToList()

            }).FirstOrDefault();

            if(foundSite.owner != null){
                System.Console.WriteLine($"Site owner: {foundSite.owner.first_name}");
            }
            return foundSite;
        }

        [HttpGet] //first active site
        [Route("/[controller]/get_active")]
        public ActionResult<Site> GetActiveSite(){
            List<Site> AllSites = dbContext.Sites.Where(x => x.active == true).Include(x=>x.paragraph_boxes).ToList();
            System.Console.WriteLine(AllSites);
            if(AllSites.Count < 1){ //no sites active
                Site default_site = new Site();
                default_site.site_id = 0; //non-standard sql id acts as null value
                default_site.title = "No Active Site";
                return default_site;
            }else{ //return first active site found
                System.Console.WriteLine(JsonSerializer.Serialize(GetSiteById(AllSites[0].site_id)));
                return GetSiteById(AllSites[0].site_id);
            }
        }

        [HttpPost]
        [Route("/[controller]/set_active")]
        public ActionResult<Site> SetActiveSite([FromBody] string _NewActiveSite){
            Site NewActiveSite = JsonSerializer.Deserialize<Site>(_NewActiveSite);
            List<Site> AllSites = dbContext.Sites.Where(x => x.active == true).Include(x=>x.paragraph_boxes).ToList();
            List<Site> SiteToSetActive = dbContext.Sites.Where(x => x.site_id == NewActiveSite.site_id).ToList();
            System.Console.WriteLine(JsonSerializer.Serialize(SiteToSetActive));
            if(SiteToSetActive.Count == 1){ //must be one and only one
                for(int x = 0; x < AllSites.Count; x++){ 
                if(AllSites[x].active != false){ //clear all active sites
                    AllSites[x].active = false;
                    }
                }
                SiteToSetActive[0].active = true; //set new active site
                System.Console.WriteLine("doe");
                dbContext.SaveChanges();
            }
            System.Console.WriteLine("ray");
            return GetActiveSite();
        }

        [HttpPost]
        [Route("/[controller]/create_site")]
        [Produces("application/json")]
        public JsonResponse Post([FromBody] string _NewSite){
            Site NewSite = JsonSerializer.Deserialize<Site>(_NewSite);
            List<Site> test = dbContext.Sites.Where(x => x.title == NewSite.title).ToList();
            if( test.Count > 0 ){
                JsonResponse message = new JsonFailure("Site must not have duplicate title with existing site.");
                return message;
            }else{
                dbContext.Add(NewSite);
                dbContext.SaveChanges();
                Site querySite = dbContext.Sites.FirstOrDefault(x => x.title == NewSite.title); //title must be unique
                JsonResponse message = new JsonSuccess($"Site created with title: ${NewSite.title}");
                return message;
            }
        }

        [HttpPost] //create paragraph box
        [Route("/[controller]/create_paragraph_box")]
        [Produces("application/json")]
        public JsonResponse PostBox([FromBody] string _paragraph_box){
            ParagraphBox NewBox = JsonSerializer.Deserialize<ParagraphBox>(_paragraph_box);
            dbContext.Add(NewBox);
            dbContext.SaveChanges();
            JsonResponse r = new JsonSuccess("Paragraph box posted sucessfully!");
            return r;
        }



        [HttpDelete]
        [Route("/[controller]/delete/{site_id_parameter}")]
        public ActionResult<Site> DestroySite(int site_id_parameter){
            System.Console.WriteLine("Site Deleted >:O");
            Site FoundSite = dbContext.Sites.SingleOrDefault(x => x.site_id == site_id_parameter);
            dbContext.Remove( FoundSite );
            dbContext.SaveChanges();
            return FoundSite;
        }
    }
}