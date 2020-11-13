//project dependencies
using dynamify.Classes.Auth;

//project models
using dynamify.dtos;
using dynamify.Models.JsonModels;
using dynamify.Models.SiteModels;
using System.Collections.Generic;
using dynamify.ServerClasses.Auth;
using dynamify.ServerClasses.QueryClasses;

//for StatusCode
using Microsoft.AspNetCore.Mvc;
using dynamify.ServerClasses.DataLimiter;

namespace dynamify.Controllers.ControllerMethods
{
    public class SiteControllerMethods:ControllerBase
    {
        private SiteQueries dbQuery;
        private SiteAuth authenticator;
        private DataLimiter _dataLimiter;

        private SiteCreationValidator validator;

        public SiteControllerMethods(SiteQueries _dbQuery, AdminQueries _AdbQuery){
            dbQuery = _dbQuery;
            _dataLimiter = new DataLimiter(_AdbQuery);
            authenticator = new SiteAuth(_AdbQuery, _dbQuery, _dataLimiter);
            validator = new SiteCreationValidator(dbQuery);
        }

        public ActionResult<SkeletonSiteDto> GetSkeletonSiteByIdMethod(int site_id){
            return dbQuery.QuerySkeletonSiteById(site_id);
        }

        public ActionResult<SkeletonSiteDto> GetSkeletonSiteByUrlMethod( string url ){
            try{
                SkeletonSiteDto FoundSite = dbQuery.QuerySkeletonContentByUrl( url );
                return FoundSite;
            }catch{
                JsonFailure err = new JsonFailure("url not found");
                return StatusCode(404, err);
            }
        }

        public ActionResult<JsonResponse> PostMethod(NewSiteDto NewSite){
            if(authenticator.VerifyAdmin(NewSite.admin_id, NewSite.token)){
                string verdict = validator.ValidateSiteUrl(NewSite.url);
                if(verdict == "pass"){
                    if(_dataLimiter.ValidateSiteAdditionForDataPlan(NewSite.admin_id)){
                        Site SoonToAddSite = new Site();
                        SoonToAddSite.title = NewSite.title;
                        SoonToAddSite.admin_id = NewSite.admin_id;
                        SoonToAddSite.url = NewSite.url.ToLower();
                        dbQuery.AddSite(SoonToAddSite);
                        JsonResponse r = new JsonSuccess($"Site created with title: ${NewSite.title}");
                        return r;
                    }else{
                        JsonFailure f = new JsonFailure("Data plan exceeds limit. Check Data limits for this account.");
                        return StatusCode(400, f);
                    }
                }else{
                    JsonFailure f = new JsonFailure(verdict);
                    return StatusCode(400, f);
                }
            }else{
                JsonFailure f = new JsonFailure("Invalid Token. Stranger Danger.");
                return StatusCode(400, f);
            }
        }

        public ActionResult<IEnumerable<Site>> GetByAdminIdMethod(int admin_id, string admin_token){
            if(authenticator.VerifyAdmin(admin_id, admin_token)){
                List<Site> OwnedSites = dbQuery.QuerySitesByAdmin(admin_id);
                return OwnedSites;
            }else{
                JsonFailure f = new JsonFailure("Admin credentials refused.");
                return StatusCode(400, f);
            } 
        }

        public ActionResult<JsonResponse> PostBoxMethod(ParagraphBox NewBox, int admin_id, string admin_token){
            if(authenticator.VerifyComponentModification(admin_id, NewBox.site_id, admin_token, NewBox)){
                NewBox.byte_size =  NewBox.FindCharLength();
                dbQuery.AddParagraphBox(NewBox);
                JsonResponse r = new JsonSuccess("Paragraph box posted sucessfully!");
                return r;
            }else{
                JsonFailure f = new JsonFailure("Invalid Token. Stranger Danger.");
                return StatusCode(400, f);
            }
        }

        public ActionResult<JsonResponse> PostImageMethod(Image NewImage, int admin_id, string admin_token){
             if(authenticator.VerifyComponentModification(admin_id, NewImage.site_id, admin_token, NewImage)){
                NewImage.byte_size = NewImage.FindCharLength();
                dbQuery.AddImage(NewImage);
                JsonResponse r = new JsonSuccess("Image posted sucessfully!");
                return r;
            }else{
                JsonFailure f = new JsonFailure("Invalid Token. Stranger Danger.");
                return StatusCode(400, f);
            }
        }

        public ActionResult<JsonResponse> PostTwoColumnBoxMethod(TwoColumnBox NewTwoColumnBox, int admin_id, string admin_token){
             if(authenticator.VerifyComponentModification(admin_id, NewTwoColumnBox.site_id, admin_token, NewTwoColumnBox)){
                NewTwoColumnBox.byte_size = NewTwoColumnBox.FindCharLength();
                dbQuery.AddTwoColumnBox(NewTwoColumnBox);
                JsonResponse r = new JsonSuccess("Two column box posted sucessfully!");
                return r;
            }else{
                JsonFailure f = new JsonFailure("Invalid Token. Stranger Danger.");
                return StatusCode(400, f);
            }
        }

        public ActionResult<JsonResponse> PostPortraitMethod(Portrait NewPortrait, int admin_id, string admin_token){
             if(authenticator.VerifyComponentModification(admin_id, NewPortrait.site_id, admin_token, NewPortrait)){
                NewPortrait.byte_size = NewPortrait.FindCharLength();
                dbQuery.AddPortrait(NewPortrait);
                JsonResponse r = new JsonSuccess("Portrait posted sucessfully!");
                return r;
            }else{
                JsonFailure f = new JsonFailure("Invalid Token. Stranger Danger.");
                return StatusCode(400, f);
            }
        }

        public ActionResult<JsonResponse> PostLinkBoxMethod(NewLinkBoxDto _NewLinkBox, int admin_id, string admin_token){

                LinkBox NewLinkBox = new LinkBox();

                NewLinkBox.title = _NewLinkBox.title;
                NewLinkBox.content = _NewLinkBox.content;
                NewLinkBox.url = _NewLinkBox.url;
                NewLinkBox.link_display = _NewLinkBox.link_display;

                
                NewLinkBox.priority = _NewLinkBox.priority;
                NewLinkBox.site_id = _NewLinkBox.site_id;
                NewLinkBox.byte_size = NewLinkBox.FindCharLength();

             if(authenticator.VerifyComponentModification(admin_id, NewLinkBox.site_id, admin_token, NewLinkBox)){

                dbQuery.AddLinkBox(NewLinkBox);
                JsonResponse r = new JsonSuccess("Link Box posted sucessfully!");
                return r;
            }else{
                JsonFailure f = new JsonFailure("Invalid Token. Stranger Danger.");
                return StatusCode(400, f);
            }
        }
        
        public ActionResult<JsonResponse> PostNavBarMethod(int admin_id, string admin_token, int site_id){
             if(authenticator.VerifyAdminForLeaf(admin_id, site_id, admin_token)){
                dbQuery.AddNavBarToSite( site_id );
                JsonResponse r = new JsonSuccess("Nav Bar posted sucessfully!");
                return r;
            }else{
                JsonFailure f = new JsonFailure("Invalid Token. Stranger Danger.");
                return StatusCode(400, f);
            }
        }

        public ActionResult<NavLinkDto> PostNavLinkMethod( NewNavLinkDto new_link, int admin_id, string admin_token, int site_id ){
             if(authenticator.VerifyAdminForLeaf(admin_id, site_id, admin_token)){
                return dbQuery.AddNavBarLinkToSite(new_link, site_id);
            }else{
                JsonFailure f = new JsonFailure("Invalid Token. Stranger Danger.");
                return StatusCode(400, f);
            }
        }

        public ActionResult<JsonResponse> DeleteSite(int site_id, int admin_id, string admin_token){
            if(authenticator.VerifyAdminForLeaf(admin_id, site_id, admin_token)){
                SkeletonSiteDto found_site = dbQuery.QuerySkeletonSiteById(site_id);

                //must be done manually by the app to properly update the data limiter
                foreach(SiteComponentDto site_component in found_site.site_components){
                    ComponentReference component_reference = new ComponentReference(){
                        component_id = site_component.component_id,
                        component_type = site_component.type
                    };
                    DeleteAuthenticatedSiteComponentMethod(component_reference);
                }
                Site DeletedSite = dbQuery.DeleteSiteById(site_id);
                JsonResponse r = new JsonSuccess($"Site {DeletedSite.title} deleted sucessfully!");
                return r;
            }else{
                JsonFailure f = new JsonFailure("Invalid Token. Stranger Danger.");
                return StatusCode(400, f);
            }
        }


        //------         COMPONENT QUERY METHODS        -----
        public ActionResult<NavBarDto> GetNavBarMethod(int site_id){
            try{
                return dbQuery.QueryNavBarDtoBySiteId(site_id);
            }catch{
                JsonFailure f = new JsonFailure($"Nav Bar for site id {site_id} not found.");
                return StatusCode(400, f);
            }
            
        }
    
        public ActionResult<ParagraphBox> GetParagraphBoxMethod(int p_box_id){
            try{
                ParagraphBox paragraph_box = dbQuery.QueryParagraphBoxById( p_box_id );
                return paragraph_box;
            }catch{
                JsonFailure f = new JsonFailure("Component Not Found");
                return StatusCode(400, f);
            }
        }

        public ActionResult<Portrait> GetPortraitMethod(int portrait_id){
            try{
                Portrait portrait = dbQuery.QueryPortraitById( portrait_id );
                return portrait;
            }catch{
                JsonFailure f = new JsonFailure("Component Not Found");
                return StatusCode(400, f);
            }
        }

        public ActionResult<TwoColumnBox> GetTwoColumnBoxMethod(int two_column_box_id){
            try{
                TwoColumnBox two_column_box = dbQuery.QueryTwoColumnBoxById( two_column_box_id );
                return two_column_box;
            }catch{
                JsonFailure f = new JsonFailure("Component Not Found");
                return StatusCode(400, f);
            }
        }

        public ActionResult<LinkBox> GetLinkBoxMethod(int link_box_id){
            try{
                LinkBox link_box = dbQuery.QueryLinkBoxById( link_box_id );
                return link_box;
            }catch{
                JsonFailure f = new JsonFailure("Component Not Found");
                return StatusCode(400, f);
            } 
        }

        public ActionResult<Image> GetImageMethod( int image_id ){
            try{
                Image image = dbQuery.QueryImageById(image_id);
                return image;
            }catch{
                JsonFailure f = new JsonFailure("Component Not Found");
                return StatusCode(400, f);
            }      
        }

        public ActionResult<JsonSuccess> SwapComponentPriorityMethod(ComponentSwapDto Components, int admin_id, string admin_token, int site_id){
            if(authenticator.VerifyAdminForLeaf( admin_id, site_id, admin_token )){
                dbQuery.SwapSiteComponentOrder(Components.component_one, Components.component_two);
                return new JsonSuccess(
                    $"Component id {Components.component_one.component_id} type {Components.component_one.component_type} sucessfully swapped with Component id {Components.component_two.component_id} type {Components.component_two.component_type}");
            }else{
                JsonFailure f = new JsonFailure("Invalid credentials.");
                return StatusCode(400, f);
            }
        }

        public ActionResult<JsonResponse> DeleteSiteComponentMethod(ComponentReference Component, int admin_id, string admin_token, int site_id){

            if(authenticator.VerifyAdminForLeaf(admin_id, site_id, admin_token)){
                return DeleteAuthenticatedSiteComponentMethod(Component);
            }else{
                JsonFailure f = new JsonFailure("Invalid credentials.");
                return StatusCode(400, f);
            }
        }

        public ActionResult<JsonResponse> DeleteAuthenticatedSiteComponentMethod(ComponentReference Component){
                if(Component.component_type == "p_box"){
                    try{
                        ParagraphBox paragraph_box = dbQuery.DeleteParagraphBox(Component.component_id);
                        Site parent_site = dbQuery.QueryFeaturelessSiteById(paragraph_box.site_id);
                        _dataLimiter.RemoveFromDataPlan(paragraph_box, parent_site.admin_id);
                        JsonResponse r = new JsonSuccess("Paragraph box deleted sucessfully!");
                        return r;
                    }catch{
                        JsonFailure f = new JsonFailure($"Unable to find paragraph box id {Component.component_id}");
                        return StatusCode(400, f);
                    }
                    
                }else if(Component.component_type == "image"){
                    try{
                        Image image = dbQuery.DeleteImage(Component.component_id);
                        Site parent_site = dbQuery.QueryFeaturelessSiteById(image.site_id);
                        _dataLimiter.RemoveFromDataPlan(image, parent_site.admin_id);
                        JsonResponse r = new JsonSuccess("Image deleted sucessfully!");
                        return r;
                    }catch{
                        JsonFailure f = new JsonFailure($"Unable to find image id {Component.component_id}");
                        return StatusCode(400, f);
                    }
                }else if(Component.component_type == "portrait"){
                    try{
                        Portrait portrait = dbQuery.DeletePortrait(Component.component_id);
                        Site parent_site = dbQuery.QueryFeaturelessSiteById(portrait.site_id);
                        _dataLimiter.RemoveFromDataPlan(portrait, parent_site.admin_id);
                        JsonResponse r = new JsonSuccess("Portrait component deleted sucessfully!");
                        return r; 
                    }catch{
                        JsonFailure f = new JsonFailure($"Unable to find portrait id {Component.component_id}");
                        return StatusCode(400, f);
                    }
                }else if(Component.component_type == "2c_box"){
                    try{
                        TwoColumnBox two_column_box = dbQuery.DeleteTwoColumnBox(Component.component_id);
                        Site parent_site = dbQuery.QueryFeaturelessSiteById(two_column_box.site_id);
                        _dataLimiter.RemoveFromDataPlan(two_column_box, parent_site.admin_id);
                        JsonResponse r = new JsonSuccess("Two Column Box component deleted sucessfully!");
                        return r;
                    }catch{
                        JsonFailure f = new JsonFailure($"Unable to find two column box id {Component.component_id}");
                        return StatusCode(400, f);
                    }
                }else if(Component.component_type == "link_box"){
                    try{
                        LinkBox link_box = dbQuery.DeleteLinkBox(Component.component_id);
                        Site parent_site = dbQuery.QueryFeaturelessSiteById(link_box.site_id);
                        _dataLimiter.RemoveFromDataPlan(link_box, parent_site.admin_id);
                        JsonResponse r = new JsonSuccess("Link Box component deleted sucessfully!");
                        return r;
                    }catch{
                        JsonFailure f = new JsonFailure($"Unable to find link box id {Component.component_id}");
                        return StatusCode(400, f);
                    }
                }else{
                    JsonFailure f = new JsonFailure("Type mismatch. Type does not match any known components.");
                    return StatusCode(400, f);
                }
        }
        

        public ActionResult<JsonResponse> DeleteNavBarMethod(int admin_id, string admin_token, int site_id){
            if(authenticator.VerifyAdminForLeaf( admin_id, site_id, admin_token )){
                dbQuery.DeleteNavBarBySiteId(site_id);
                return new JsonSuccess($"NavBar Deleted for site id: {site_id}");
            }else{
                JsonFailure f = new JsonFailure("Invalid credentials.");
                return StatusCode(400, f);
            }
        }

        public ActionResult<JsonResponse> DeleteNavLinkMethod(int admin_id, string admin_token, int site_id, int link_id){
            if(authenticator.VerifyAdminForLeaf( admin_id, site_id, admin_token )){
                dbQuery.DeleteNavLinkById(link_id);
                return new JsonSuccess($"NavLink Deleted for link id: {link_id}");
            }else{
                JsonFailure f = new JsonFailure("Invalid credentials.");
                return StatusCode(400, f);
            }
            
        }

        //Component Edit Methods
        public ActionResult<ParagraphBox> EditParagraphBoxMethod(ParagraphBox paragraph_box, int admin_id, string admin_token, int site_id){
            //check available (better way to do this?)
            ParagraphBox queried_paragraph_box;
            try{
                queried_paragraph_box = dbQuery.QueryParagraphBoxById(paragraph_box.paragraph_box_id);
            }catch{
                JsonFailure f = new JsonFailure($"paragraph_box Id: {paragraph_box.paragraph_box_id} not found.");
                return StatusCode(400, f);
            }

            //verify and change
            if(authenticator.VerifyComponentModification(admin_id, queried_paragraph_box.site_id, admin_token, paragraph_box)){
                paragraph_box.byte_size = paragraph_box.FindCharLength();
                return dbQuery.EditParagraphBox( paragraph_box );
            }else{
                JsonFailure f = new JsonFailure("Invalid credentials.");
                return StatusCode(400, f);
            }
        }

         public ActionResult<TwoColumnBox> EditTwoColumnBoxMethod(TwoColumnBox tc_box, int admin_id, string admin_token, int site_id){
             //check available (better way to do this?)
            TwoColumnBox queried_two_column_box;
            try{
                queried_two_column_box = dbQuery.QueryTwoColumnBoxById(tc_box.two_column_box_id);
            }catch{
                JsonFailure f = new JsonFailure($"Two Column Box Id: {tc_box.two_column_box_id} not found.");
                return StatusCode(400, f);
            }

            if(authenticator.VerifyComponentModification(admin_id, queried_two_column_box.site_id, admin_token, tc_box)){
                tc_box.byte_size = tc_box.FindCharLength();
                return dbQuery.EditTwoColumnBox( tc_box );
            }else{
                JsonFailure f = new JsonFailure("Invalid credentials.");
                return StatusCode(400, f);
            }
        }

        public ActionResult<Image> EditImageMethod(Image image, int admin_id, string admin_token, int site_id){
            //check available (better way to do this?)
            Image queried_image;
            try{
                queried_image = dbQuery.QueryImageById(image.image_id);
            }catch{
                JsonFailure f = new JsonFailure($"Image Id: {image.image_id} not found.");
                return StatusCode(400, f);
            }

            //verify and change
            if(authenticator.VerifyComponentModification(admin_id, queried_image.site_id, admin_token, image)){
                image.byte_size = image.FindCharLength();
                return dbQuery.EditImage( image );
            }else{
                JsonFailure f = new JsonFailure("Invalid credentials.");
                return StatusCode(400, f);
            }
        }
        
         public ActionResult<Portrait> EditPortraitMethod(Portrait portrait, int admin_id, string admin_token, int site_id){
            //check available (better way to do this?)
            Portrait queried_portrait;
            try{
                queried_portrait = dbQuery.QueryPortraitById(portrait.portrait_id);
            }catch{
                JsonFailure f = new JsonFailure($"Portrait Id: {portrait.portrait_id} not found.");
                return StatusCode(400, f);
            }

            //verify and change
            dbQuery.QueryPortraitById(portrait.portrait_id); 
            if(authenticator.VerifyComponentModification(admin_id, queried_portrait.site_id, admin_token, portrait)){
                portrait.byte_size = portrait.FindCharLength();
                return dbQuery.EditPortrait( portrait );
            }else{
                JsonFailure f = new JsonFailure("Invalid credentials.");
                return StatusCode(400, f);
            }
        }

        public ActionResult<LinkBox> EditLinkBoxMethod(LinkBox link_box, int admin_id, string admin_token, int site_id){
            //check available (better way to do this?)
            LinkBox queried_link_box;
            try{
               queried_link_box = dbQuery.QueryLinkBoxById(link_box.link_box_id);
            }catch{
                JsonFailure f = new JsonFailure($"link_box Id: {link_box.link_box_id} not found.");
                return StatusCode(400, f);
            }

            //verify and change
            if(authenticator.VerifyComponentModification(admin_id, queried_link_box.site_id, admin_token, link_box)){
                link_box.byte_size = link_box.FindCharLength();
                return dbQuery.EditLinkBox( link_box );
            }else{
                JsonFailure f = new JsonFailure("Invalid credentials.");
                return StatusCode(400, f);
            }
        }
    }
}