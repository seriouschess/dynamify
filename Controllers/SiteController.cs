//standard dependencies
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

//project dependencies
using dynamify.Models.QueryClasses;
using dynamify.Classes.Auth;
using dynamify.Controllers.ControllerMethods;

//models
using dynamify.Models.SiteModels;
using dynamify.Models.JsonModels;
using dynamify.dtos;


namespace dynamify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteController : ControllerBase
    {
        private SiteQueries dbQuery;
        private Auth authenticator;
        private SiteControllerMethods methods;

        public SiteController(SiteQueries _SiteQueries, AdminQueries _AdminQueries)
        {
            dbQuery = _SiteQueries;
            authenticator = new Auth(_AdminQueries);
            methods = new SiteControllerMethods(_SiteQueries, _AdminQueries);
        }

        [HttpPost] //to allow access token payload
        [Route("get")]
        public ActionResult<SiteContentDto> GetSiteById([FromBody] SiteRequestDto request){
            return methods.GetSiteByIdMethod(request);
        }           

        [HttpPost]
        [Route("create_site")]
        [Produces("application/json")]
        public JsonResponse Post([FromBody] NewSiteDto NewSite){
            return methods.PostMethod(NewSite);
        }

        [HttpGet] //all sites by admin
        [Route("get_by_admin/{admin_id}/{admin_token}")]
        public IEnumerable<Site> GetAdmnById(int admin_id, string admin_token){
            return methods.GetByAdminIdMethod(admin_id, admin_token);
        }

        [HttpGet] //retrieve site content for one site by url
        [Route("get_by_url/{leaf_url}")]
        public ActionResult<SiteContentDto> GetByURL(string leaf_url){
            return methods.GetByURLMethod(leaf_url);
        }

        //create site components
        [HttpPost] //create paragraph box
        [Route("create/paragraph_box/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public JsonResponse PostBox([FromBody] string _paragraph_box, int admin_id, string admin_token){
           ParagraphBox NewBox = JsonSerializer.Deserialize<ParagraphBox>(_paragraph_box);
           return methods.PostBoxMethod(NewBox, admin_id, admin_token);  
        }

        [HttpPost] //create image
        [Route("create/image/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public JsonResponse PostImage([FromBody] string _image, int admin_id, string admin_token){
            Image NewImage = JsonSerializer.Deserialize<Image>(_image);
            return methods.PostImageMethod(NewImage, admin_id, admin_token);
        }

        [HttpPost] //create portrait
        [Route("create/portrait/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public JsonResponse PostPortrait([FromBody] string _portrait, int admin_id, string admin_token){
            Portrait NewPortrait = JsonSerializer.Deserialize<Portrait>(_portrait);
           return methods.PostPortraitMethod(NewPortrait, admin_id, admin_token);
        }

        [HttpPost] //create two column box
        [Route("create/2c_box/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public JsonResponse PostTwoColumnBox([FromBody] string _two_column_box, int admin_id,  string admin_token){
            TwoColumnBox NewTwoColumnBox = JsonSerializer.Deserialize<TwoColumnBox>(_two_column_box);
            return methods.PostTwoColumnBoxMethod(NewTwoColumnBox, admin_id, admin_token);
        }

        [HttpPost] //create two column box
        [Route("create/link_box/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public JsonResponse PostLinkBox([FromBody] NewLinkBoxDto NewLinkBox, int admin_id,  string admin_token){
            return methods.PostLinkBoxMethod(NewLinkBox, admin_id, admin_token);
        }

        [HttpPost] //create or replace nav bar
        [Route("create/nav_bar/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public JsonResponse PostNavBar([FromBody] NavBarDto NewNavBar, int admin_id, string admin_token){
            return methods.PostNavBarMethod(NewNavBar, admin_id, admin_token);
        }

        //delete site

        [HttpDelete] 
        [Route("delete/{site_id_parameter}/{admin_id}/{admin_token}")]
        public JsonResponse DestroySite(int site_id_parameter, int admin_id, string admin_token){
            return methods.DeleteSite(site_id_parameter, admin_id, admin_token);
        }

        //delete site components

        [HttpPost]
        [Route("delete/site_component/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public JsonResponse DeleteSiteComponent([FromBody] ComponentReference Component, int admin_id, string admin_token){
            return methods.DeleteSiteComponentMethod(Component, admin_id, admin_token);
        }
    }
}