//standard modules
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

//project modules
using dynamify.Models.SiteModels;
using dynamify.Models.JsonModels;
using dynamify.Models.QueryClasses;
using dynamify.Classes.Auth;

//dtos
using dynamify.dtos;

namespace dynamify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SiteController : ControllerBase
    {
        private SiteQueries theQueryer;
        private Auth authenticator;

        private readonly ILogger<SiteController> _logger;

        public SiteController(ILogger<SiteController> logger, SiteQueries _SiteQueries, AdminQueries _adminQueries)
        {
            _logger = logger;
            theQueryer = _SiteQueries;
            authenticator = new Auth(_adminQueries);
        }

        [HttpPost] //to allow access token payload
        [Route("get")]
        public ActionResult<SiteContentDto> GetSiteById([FromBody] SiteRequestDto request){
            if(authenticator.VerifyAdmin(request.admin_id, request.token)){
                SiteContentDto foundSite = theQueryer.QuerySiteContentById(request.site_id);
               return foundSite;
            }else{
                return new SiteContentDto();
            }
        }

        [HttpGet] //first active site
        [Route("active")]
        public ActionResult<SiteContentDto> GetActiveSite(){
            SiteContentDto ActiveSite = theQueryer.QueryActiveSiteContent();
            return ActiveSite;
        }

        [HttpPost]
        [Route("set_active")]
        public JsonResponse SetActiveSite([FromBody] SiteRequestDto request){
            if(authenticator.VerifyAdmin(request.admin_id, request.token)){
                List<Site> SiteToSetActive = theQueryer.QueryFeaturelessSiteById(request.site_id);
                if(SiteToSetActive.Count != 1){ //ensure only one site
                    JsonResponse r = new JsonFailure($"Set Active API route failure: Ensure correct site id was sent.");
                    return r;
                }else{
                    Site ActiveSite = theQueryer.SetActiveSiteDB(SiteToSetActive[0]);
                    JsonResponse r = new JsonSuccess($"Site Title: < {ActiveSite.title} > is now active!");
                    return r;
                } 

            }else{
                return new JsonFailure("Invalid Token. Stranger Danger");
            }
        }            

        [HttpPost]
        [Route("create_site")]
        [Produces("application/json")]
        public JsonResponse Post([FromBody] NewSiteDto NewSite){
            if(authenticator.VerifyAdmin(NewSite.admin_id, NewSite.token)){
                List<Site> test = theQueryer.QueryFeaturelessSiteByTitle(NewSite.title);
                if( test.Count > 0 ){
                    JsonResponse r = new JsonFailure("Site must not have duplicate title with existing site.");
                    return r;
                }else{
                    Site SoonToAddSite = new Site();
                    SoonToAddSite.title = NewSite.title;
                    SoonToAddSite.admin_id = NewSite.admin_id;
                    theQueryer.AddSite(SoonToAddSite);
                    JsonResponse r = new JsonSuccess($"Site created with title: ${NewSite.title}");
                    return r;
                }
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        [HttpGet] //all sites by admin
        [Route("get_by_admin/{admin_id}/{admin_token}")]
        public IEnumerable<Site> GetByAdmnId(int admin_id, string admin_token){
            if(authenticator.VerifyAdmin(admin_id, admin_token)){
                List<Site> OwnedSites = theQueryer.QuerySitesByAdmin(admin_id);
                return OwnedSites;
            }else{
                return new List<Site>();
            }
        }

        //create site components
        [HttpPost] //create paragraph box
        [Route("create/paragraph_box/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public JsonResponse PostBox([FromBody] string _paragraph_box, int admin_id, string admin_token){
            if(authenticator.VerifyAdmin(admin_id, admin_token)){
                ParagraphBox NewBox = JsonSerializer.Deserialize<ParagraphBox>(_paragraph_box);

                theQueryer.AddParagraphBox(NewBox);
                JsonResponse r = new JsonSuccess("Paragraph box posted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
            
        }

        [HttpPost] //create image
        [Route("create/image/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public JsonResponse PostImage([FromBody] string _image, int admin_id, string admin_token){
            if(authenticator.VerifyAdmin(admin_id, admin_token)){
                Image NewImage = JsonSerializer.Deserialize<Image>(_image);
                theQueryer.AddImage(NewImage);
                JsonResponse r = new JsonSuccess("Image posted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        [HttpPost] //create portrait
        [Route("create/portrait/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public JsonResponse PostPortrait([FromBody] string _portrait, int admin_id, string admin_token){
            if(authenticator.VerifyAdmin(admin_id, admin_token)){
                Portrait NewPortrait = JsonSerializer.Deserialize<Portrait>(_portrait);
                theQueryer.AddPortrait(NewPortrait);
                JsonResponse r = new JsonSuccess("Portrait posted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        [HttpPost] //create two column box
        [Route("create/2c_box/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public JsonResponse PostTwoColumnBox([FromBody] string _two_column_box, int admin_id,  string admin_token){
            if(authenticator.VerifyAdmin(admin_id, admin_token)){
                TwoColumnBox TwoColumnBox = JsonSerializer.Deserialize<TwoColumnBox>(_two_column_box);
                theQueryer.AddTwoColumnBox( TwoColumnBox );
                JsonResponse r = new JsonSuccess("Two column box posted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        //delete site //Add an authentication token here as well!

        [HttpDelete] 
        [Route("delete/{site_id_parameter}")] //not secure yet
        public ActionResult<Site> DestroySite(int site_id_parameter){
            System.Console.WriteLine("Site Deleted >:O");
            Site DeletedSite = theQueryer.DeleteSiteById(site_id_parameter);
            return DeletedSite;
        }

        //delete site components //Add an authentication token here as well!

        [HttpPost]
        [Route("delete/site_component")]
        [Produces("application/json")]
        public JsonResponse DeleteSiteComponent([FromBody] ComponentReference Component){
            if(Component.component_type == "p_box"){
                ParagraphBox DeletedSite = theQueryer.DeleteParagraphBox(Component.component_id);
                JsonResponse r = new JsonSuccess("Paragraph box deleted sucessfully!");
                return r;
            }else if(Component.component_type == "image"){
                Image DeletedSite = theQueryer.DeleteImage(Component.component_id);
                JsonResponse r = new JsonSuccess("Image deleted sucessfully!");
                return r;
            }else if(Component.component_type == "portrait"){
                Portrait portrait = theQueryer.DeletePortrait(Component.component_id);
                JsonResponse r = new JsonSuccess("Portrait component deleted sucessfully!");
                return r;
            }else if(Component.component_type == "2c_box"){
                TwoColumnBox portrait = theQueryer.DeleteTwoColumnBox(Component.component_id);
                JsonResponse r = new JsonSuccess("Two Column Box component deleted sucessfully!");
                return r;
            }else{
                JsonResponse r = new JsonSuccess("Type mismatch. Type does not match any known components.");
                return r;
            }
            
        }
    }
}