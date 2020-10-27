//standard dependencies
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

//project dependencies
using dynamify.Classes.Auth;
using dynamify.Controllers.ControllerMethods;

//models
using dynamify.Models.SiteModels;
using dynamify.Models.JsonModels;
using dynamify.dtos;
using dynamify.ServerClasses.QueryClasses;

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
            authenticator = new SiteAuth(_AdminQueries, _SiteQueries);
            methods = new SiteControllerMethods(_SiteQueries, _AdminQueries);
        }

        [HttpGet]
        [Route("get_by_id/skeleton/{site_id}")]
        public ActionResult<SkeletonSiteDto> GetSkeletonSiteById(int site_id){
            return methods.GetSkeletonSiteByIdMethod(site_id);
        }           

        [HttpPost]
        [Route("create_site")]
        [Produces("application/json")]
        public ActionResult<JsonResponse> Post([FromBody] NewSiteDto NewSite){
            return methods.PostMethod(NewSite);
        }

        [HttpGet] //all sites by admin
        [Route("get_by_admin/{admin_id}/{admin_token}")]
        public ActionResult<IEnumerable<Site>> GetAdmnById(int admin_id, string admin_token){
            return methods.GetByAdminIdMethod(admin_id, admin_token);
        }

        [HttpGet] //retrieve site content for one site by url
        [Route("get_by_url/skeleton/{leaf_url}")]
        public ActionResult<SkeletonSiteDto> GetSkeletonSiteByURL (string leaf_url){
            return methods.GetSkeletonSiteByUrlMethod(leaf_url);
        }

        // Component Queries
        [HttpGet]
        [Route("get_component/paragraph_box/{component_id}/{site_id}")]
        public ActionResult<ParagraphBox> GetParagraphBox(int component_id, int site_id){
            ComponentRequestDto request = new ComponentRequestDto();
            try{
                request.component_id = component_id;
                request.site_id = site_id;
            }catch{
                JsonFailure f =  new JsonFailure("Improper url format, one or more fields are missing");
                return StatusCode(400, f);
            }

            try{
                return methods.GetParagraphBoxMethod( request );
            }catch(System.ArgumentException e){
                return StatusCode(400, e);
            }
        }

        [HttpGet]
        [Route("get_component/portrait/{component_id}/{site_id}")]
        public ActionResult<Portrait> GetPortrait(int component_id, int site_id){
            ComponentRequestDto request = new ComponentRequestDto();
            try{
                request.component_id = component_id;
                request.site_id = site_id;
            }catch{
                JsonFailure f =  new JsonFailure("Improper url format, one or more fields are missing");
                return StatusCode(400, f);
            }

            try{
                return methods.GetPortraitMethod( request );
            }catch(System.ArgumentException e){
                return StatusCode(400, e);
            }
        }

        [HttpGet]
        [Route("get_component/two_column_box/{component_id}/{site_id}")]
        public ActionResult<TwoColumnBox> GetTwoColumnBoxMethod(int component_id, int site_id){
            ComponentRequestDto request = new ComponentRequestDto();
            try{
                request.component_id = component_id;
                request.site_id = site_id;
            }catch{
                JsonFailure f =  new JsonFailure("Improper url format, one or more fields are missing");
                return StatusCode(400, f);
            }

            try{
                return methods.GetTwoColumnBoxMethod( request );
            }catch(System.ArgumentException e){
                return StatusCode(400, e);
            }
        }

        [HttpGet]
        [Route("get_component/link_box/{component_id}/{site_id}")]
        public ActionResult<LinkBox> GetTwoLinkBoxMethod(int component_id, int site_id){
            ComponentRequestDto request = new ComponentRequestDto();
            try{
                request.component_id = component_id;
                request.site_id = site_id;
            }catch{
                JsonFailure f =  new JsonFailure("Improper url format, one or more fields are missing");
                return StatusCode(400, f);
            }

            try{
                return methods.GetLinkBoxMethod( request );
            }catch(System.ArgumentException e){
                return StatusCode(400, e);
            }
        }

        [HttpGet]
        [Route("get_component/image/{component_id}/{site_id}")]
        public ActionResult<Image> GetImageMethod(int component_id, int site_id){
            ComponentRequestDto request = new ComponentRequestDto();
            try{
                request.component_id = component_id;
                request.site_id = site_id;
            }catch{
                JsonFailure f =  new JsonFailure("Improper url format, one or more fields are missing");
                return StatusCode(400, f);
            }

            try{
                return methods.GetImageMethod( request );
            }catch(System.ArgumentException e){
                return StatusCode(400, e);
            }
        }

        [HttpGet]
        [Route("get_component/navbar/{site_id}")]
        public ActionResult<NavBarDto> GetNavBar(int site_id){
            return methods.GetNavBarMethod(site_id);
        }

        //create site components
        [HttpPost] //create paragraph box
        [Route("create/paragraph_box/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public ActionResult<JsonResponse> PostBox([FromBody] string _paragraph_box, int admin_id, string admin_token){
           ParagraphBox NewBox = JsonSerializer.Deserialize<ParagraphBox>(_paragraph_box);
           return methods.PostBoxMethod(NewBox, admin_id, admin_token);  
        }

        [HttpPost] //create image
        [Route("create/image/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public ActionResult<JsonResponse> PostImage([FromBody] string _image, int admin_id, string admin_token){
            Image NewImage = JsonSerializer.Deserialize<Image>(_image);
            return methods.PostImageMethod(NewImage, admin_id, admin_token);
        }

        [HttpPost] //create portrait
        [Route("create/portrait/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public ActionResult<JsonResponse> PostPortrait([FromBody] string _portrait, int admin_id, string admin_token){
            Portrait NewPortrait = JsonSerializer.Deserialize<Portrait>(_portrait);
           return methods.PostPortraitMethod(NewPortrait, admin_id, admin_token);
        }

        [HttpPost] //create two column box
        [Route("create/2c_box/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public ActionResult<JsonResponse> PostTwoColumnBox([FromBody] string _two_column_box, int admin_id, string admin_token){
            TwoColumnBox NewTwoColumnBox = JsonSerializer.Deserialize<TwoColumnBox>(_two_column_box);
            return methods.PostTwoColumnBoxMethod(NewTwoColumnBox, admin_id, admin_token);
        }

        [HttpPost] //create two column box
        [Route("create/link_box/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public ActionResult<JsonResponse> PostLinkBox([FromBody] NewLinkBoxDto NewLinkBox, int admin_id,  string admin_token){
            return methods.PostLinkBoxMethod(NewLinkBox, admin_id, admin_token);
        }

        [HttpPost] //create or replace nav bar
        [Route("create/nav_bar/{admin_id}/{admin_token}/{site_id}")]
        [Produces("application/json")]
        public ActionResult<JsonResponse> PostNavBar(int admin_id, string admin_token, int site_id ){
            return methods.PostNavBarMethod(admin_id, admin_token, site_id);
        }

        [HttpPost]
        [Route("create/nav_link/{admin_id}/{admin_token}/{site_id}")]
        public ActionResult<NavLinkDto> PostNavLink( [FromBody] NewNavLinkDto new_link, int admin_id, string admin_token, int site_id ){
            return methods.PostNavLinkMethod(new_link, admin_id, admin_token, site_id);
        }

        //delete site

        [HttpDelete] 
        [Route("delete/{site_id_parameter}/{admin_id}/{admin_token}")]
        public ActionResult<JsonResponse> DestroySite(int site_id_parameter, int admin_id, string admin_token){
            return methods.DeleteSite(site_id_parameter, admin_id, admin_token);
        }

        //delete site components

        [HttpPost]
        [Route("delete/site_component/{admin_id}/{admin_token}")]
        [Produces("application/json")]
        public ActionResult<JsonResponse> DeleteSiteComponent([FromBody] ComponentReference Component, int admin_id, string admin_token){
            return methods.DeleteSiteComponentMethod(Component, admin_id, admin_token);
        }

        [HttpDelete]
        [Route("delete/site_component/{admin_id}/{admin_token}/{site_id}")]
        [Produces("application/json")]
        public ActionResult<JsonResponse> DeleteSiteComponent(int admin_id, string admin_token, int site_id){
            return methods.DeleteNavBarMethod( admin_id, admin_token, site_id );
        }

        [HttpDelete]
        [Route("delete/navbar/{admin_id}/{admin_token}/{site_id}")]
        [Produces("application/json")]
        public ActionResult<JsonResponse> DeleteNavBar(int admin_id, string admin_token, int site_id){
            return methods.DeleteNavBarMethod(admin_id, admin_token, site_id);
        }

        [HttpDelete]
        [Route("delete/navlink/{admin_id}/{admin_token}/{site_id}/{link_id}")]
        [Produces("application/json")]
        public ActionResult<JsonResponse> DeleteNavLink(int admin_id, string admin_token, int site_id, int link_id){
            return methods.DeleteNavBarMethod(admin_id, admin_token, site_id);
        }

        //edit site components

        [HttpPut]
        [Route("edit/paragraph_box/{admin_id}/{admin_token}/{site_id}")]
        [Produces("application/json")]
        public ActionResult<ParagraphBox> EditParagraphBox([FromBody] ParagraphBox updated_paragraph_box, int admin_id, string admin_token, int site_id){
            return methods.EditParagraphBoxMethod(updated_paragraph_box, admin_id, admin_token, site_id);
        }

        [HttpPut]
        [Route("edit/two_column_box/{admin_id}/{admin_token}/{site_id}")]
        public ActionResult<TwoColumnBox> EditTwoColumnBox( [FromBody] TwoColumnBox two_column_box, int admin_id, string admin_token, int site_id){
            return methods.EditTwoColumnBoxMethod(two_column_box, admin_id, admin_token, site_id);
        }

        [HttpPut]
        [Route("edit/image/{admin_id}/{admin_token}/{site_id}")]
        public ActionResult<Image> EditImage([FromBody] Image image, int admin_id, string admin_token, int site_id){
            return methods.EditImageMethod(image, admin_id, admin_token, site_id);
        }

        [HttpPut]
        [Route("edit/link_box/{admin_id}/{admin_token}/{site_id}")]
        public ActionResult<LinkBox> EditLinkBox([FromBody] LinkBox link_box, int admin_id, string admin_token, int site_id){
            return methods.EditLinkBoxMethod(link_box, admin_id, admin_token, site_id);
        }

        [HttpPut]
        [Route("edit/portrait/{admin_id}/{admin_token}/{site_id}")]
        public ActionResult<Portrait> EditPortrait([FromBody] Portrait portrait, int admin_id, string admin_token, int site_id){
            return methods.EditPortraitMethod(portrait, admin_id, admin_token, site_id);
        }

        //misc

        [HttpPut]
        [Route("edit/swap_components/{admin_id}/{admin_token}/{site_id}")]
        public ActionResult<JsonSuccess> SwapComponentPriority(ComponentSwapDto Components, int admin_id, string admin_token, int site_id){
            return methods.SwapComponentPriorityMethod(Components, admin_id, admin_token, site_id);
        }

    }
}