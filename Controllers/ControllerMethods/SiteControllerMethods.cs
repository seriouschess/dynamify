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
using dynamify.Models.DataPlans;

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
                return StatusCode(404, "url not found");
            }
        }

        public ActionResult<JsonResponse> PostMethod(NewSiteDto NewSite){
            if(authenticator.VerifyAdmin(NewSite.admin_id, NewSite.token)){
                string verdict = validator.ValidateSiteUrl(NewSite.url);
                if(verdict == "pass"){
                        DataPlan data_plan;
                        try{
                            data_plan = _dataLimiter.ValidateSiteAdditionForDataPlan(NewSite.admin_id);
                        }catch(System.ArgumentException e){
                            return StatusCode(400, e.Message);
                        }
                        
                        Site SoonToAddSite = new Site();
                        SoonToAddSite.title = NewSite.title;
                        SoonToAddSite.admin_id = NewSite.admin_id;
                        SoonToAddSite.url = NewSite.url.ToLower();
                        List<string> format_errors = authenticator.ValidateIncomingSite(SoonToAddSite);
                        if( format_errors.Count != 0){
                            return StatusCode(400, format_errors[0] );
                        }
                        dbQuery.AddSite(SoonToAddSite);
                        _dataLimiter.UpdateDataPlan(data_plan);
                        JsonResponse r = new JsonSuccess($"Site created with title: ${NewSite.title}");
                        return r;

                }else{
                    JsonFailure f = new JsonFailure(verdict);
                    return StatusCode(400, f);
                }
            }else{
                return StatusCode(400, "Invalid Token. Stranger Danger.");
            }
        }

        public ActionResult<IEnumerable<Site>> GetByAdminIdMethod(int admin_id, string admin_token){
            if(authenticator.VerifyAdmin(admin_id, admin_token)){
                List<Site> OwnedSites = dbQuery.QuerySitesByAdmin(admin_id);
                return OwnedSites;
            }else{
                return StatusCode(400, "Admin credentials refused.");
            } 
        }

        public ActionResult<JsonResponse> PostBoxMethod(ParagraphBox NewBox, int admin_id, string admin_token){
            if(authenticator.VerifyAdminForLeaf(admin_id, NewBox.site_id, admin_token)){

                List<string> errors = authenticator.ValidateIncomingComponent(NewBox);
                if(errors.Count == 0){

                    DataPlan data_plan;
                    try{
                        data_plan = _dataLimiter.ValidateComponentAdditionForDataPlan(admin_id, NewBox);
                    }catch(System.ArgumentException e){
                        return StatusCode(400, e.Message);
                    }

                    NewBox.byte_size =  NewBox.FindCharLength();
                    dbQuery.AddParagraphBox(NewBox);
                    _dataLimiter.UpdateDataPlan(data_plan);
                    JsonResponse r = new JsonSuccess("Paragraph box posted sucessfully!");
                    return r;

                }else{
                    return StatusCode(400, errors);
                }

            }else{
                return StatusCode(400, "Invalid Token. Stranger Danger.");
            }
        }

        public ActionResult<JsonResponse> PostImageMethod(Image NewImage, int admin_id, string admin_token){
             if(authenticator.VerifyAdminForLeaf(admin_id, NewImage.site_id, admin_token)){

                List<string> errors = authenticator.ValidateIncomingComponent(NewImage);
                if(errors.Count == 0){

                    DataPlan data_plan;
                    try{
                        data_plan = _dataLimiter.ValidateComponentAdditionForDataPlan(admin_id, NewImage);
                    }catch(System.ArgumentException e){
                        return StatusCode(400, e.Message);
                    }
                    NewImage.byte_size = NewImage.FindCharLength();
                    dbQuery.AddImage(NewImage);
                    _dataLimiter.UpdateDataPlan(data_plan);

                    JsonResponse r = new JsonSuccess("Image posted sucessfully!");
                    return r;

                }else{
                    return StatusCode(400, errors);
                }

            }else{
                return StatusCode(400, "Invalid Token. Stranger Danger.");
            }
        }

        public ActionResult<JsonResponse> PostTwoColumnBoxMethod(TwoColumnBox NewTwoColumnBox, int admin_id, string admin_token){
             if(authenticator.VerifyAdminForLeaf(admin_id, NewTwoColumnBox.site_id, admin_token)){

                List<string> errors = authenticator.ValidateIncomingComponent(NewTwoColumnBox);
                if(errors.Count == 0){

                    DataPlan data_plan;
                    try{
                        data_plan = _dataLimiter.ValidateComponentAdditionForDataPlan(admin_id, NewTwoColumnBox);
                    }catch(System.ArgumentException e){
                        return StatusCode(400, e.Message);
                    }

                    NewTwoColumnBox.byte_size = NewTwoColumnBox.FindCharLength();
                    dbQuery.AddTwoColumnBox(NewTwoColumnBox);
                    _dataLimiter.UpdateDataPlan(data_plan);
                    JsonResponse r = new JsonSuccess("Two column box posted sucessfully!");
                    return r;

                }else{
                    return StatusCode(400, errors);
                }

            }else{
                return StatusCode(400, "Invalid Token. Stranger Danger.");
            }
        }

        public ActionResult<JsonResponse> PostPortraitMethod(Portrait NewPortrait, int admin_id, string admin_token){
             if(authenticator.VerifyAdminForLeaf(admin_id, NewPortrait.site_id, admin_token)){

                List<string> errors = authenticator.ValidateIncomingComponent(NewPortrait);
                if(errors.Count == 0){

                    DataPlan data_plan;
                    try{
                        data_plan = _dataLimiter.ValidateComponentAdditionForDataPlan(admin_id, NewPortrait);
                    }catch(System.ArgumentException e){
                        return StatusCode(400, e.Message);
                    }

                    NewPortrait.byte_size = NewPortrait.FindCharLength();
                    dbQuery.AddPortrait(NewPortrait);
                    _dataLimiter.UpdateDataPlan(data_plan);

                    JsonResponse r = new JsonSuccess("Portrait posted sucessfully!");
                    return r;

                }else{
                    return StatusCode(400, errors);
                }

            }else{
                return StatusCode(400, "Invalid Token. Stranger Danger.");
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

            if(authenticator.VerifyAdminForLeaf(admin_id, NewLinkBox.site_id, admin_token)){

                List<string> errors = authenticator.ValidateIncomingComponent(NewLinkBox);
                if(errors.Count == 0){

                    DataPlan data_plan;
                    try{
                        data_plan = _dataLimiter.ValidateComponentAdditionForDataPlan(admin_id, NewLinkBox);
                    }catch(System.ArgumentException e){
                        return StatusCode(400, e.Message);
                    }

                    dbQuery.AddLinkBox(NewLinkBox);
                    _dataLimiter.UpdateDataPlan(data_plan);
                    JsonResponse r = new JsonSuccess("Link Box posted sucessfully!");
                    return r;
                }else{
                    return StatusCode(400, errors);
                }
            }else{
                return StatusCode(400, "Invalid Token. Stranger Danger.");
            }
        }
        
        //data plan currently not involved with nav bars
        public ActionResult<JsonResponse> PostNavBarMethod(int admin_id, string admin_token, int site_id){
             if(authenticator.VerifyAdminForLeaf(admin_id, site_id, admin_token)){
                dbQuery.AddNavBarToSite( site_id );
                JsonResponse r = new JsonSuccess("Nav Bar posted sucessfully!");
                return r;
            }else{
                return StatusCode(400, "Invalid Token. Stranger Danger.");
            }
        }

        public ActionResult<NavLinkDto> PostNavLinkMethod( NewNavLinkDto new_link, int admin_id, string admin_token, int site_id ){
             if(authenticator.VerifyAdminForLeaf(admin_id, site_id, admin_token)){
                DataPlan data_plan;
                    try{
                        data_plan = _dataLimiter.ValidateNavLinkAdditionForDataPlan(admin_id, new_link);
                    }catch(System.ArgumentException e){
                        return StatusCode(400, e.Message);
                    } 
                NavLinkDto added_link = dbQuery.AddNavBarLinkToSite(new_link, site_id);
                _dataLimiter.UpdateDataPlan(data_plan);
                return added_link;
            }else{
                return StatusCode(400, "Invalid Token. Stranger Danger.");
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

                //manually remove nav link data from data plan
                List<NavLink> found_nav_links;
                try{
                    found_nav_links = dbQuery.QueryNavBarLinksBySiteId(site_id);
                }catch{
                    found_nav_links = null;
                }
                if(found_nav_links != null){
                    foreach(NavLink nav_link in found_nav_links){
                        _dataLimiter.RemoveNavLinkFromDataPlan(nav_link, admin_id);
                    }
                }

                _dataLimiter.RemoveSiteFromDataPlan(admin_id);
                Site DeletedSite = dbQuery.DeleteSiteById(site_id);
                JsonResponse r = new JsonSuccess($"Site {DeletedSite.title} deleted sucessfully!");
                return r;
            }else{
                return StatusCode(400, "Invalid Token. Stranger Danger.");
            }
        }

        //Site edit methods
        public ActionResult<Site> EditSiteTitleMethod(SiteTitleUpdateDto updated_site, string admin_token){
            Site found_site = dbQuery.QueryFeaturelessSiteById(updated_site.site_id);
            if(authenticator.VerifyAdminForLeaf(found_site.admin_id, found_site.site_id, admin_token )){
                found_site.title = updated_site.title;
                
                List<string> format_errors = authenticator.ValidateIncomingSite(found_site);
                if( format_errors.Count != 0){
                    return StatusCode(400, format_errors[0] );
                }

                return dbQuery.EditSiteTitle(found_site);
            }else{
                return StatusCode(400, "Invalid credentials.");
            }
        }

        //------         COMPONENT QUERY METHODS        -----
        public ActionResult<NavBarDto> GetNavBarMethod(int site_id){
            try{
                return dbQuery.QueryNavBarDtoBySiteId(site_id);
            }catch{
                return StatusCode(400, $"Nav Bar for site id {site_id} not found.");
            }
            
        }
    
        public ActionResult<ParagraphBox> GetParagraphBoxMethod(int p_box_id){
            try{
                ParagraphBox paragraph_box = dbQuery.QueryParagraphBoxById( p_box_id );
                return paragraph_box;
            }catch{
                return StatusCode(400, "Component Not Found");
            }
        }

        public ActionResult<Portrait> GetPortraitMethod(int portrait_id){
            try{
                Portrait portrait = dbQuery.QueryPortraitById( portrait_id );
                return portrait;
            }catch{
                return StatusCode(400, "Component Not Found");
            }
        }

        public ActionResult<TwoColumnBox> GetTwoColumnBoxMethod(int two_column_box_id){
            try{
                TwoColumnBox two_column_box = dbQuery.QueryTwoColumnBoxById( two_column_box_id );
                return two_column_box;
            }catch{
                return StatusCode(400, "Component Not Found");
            }
        }

        public ActionResult<LinkBox> GetLinkBoxMethod(int link_box_id){
            try{
                LinkBox link_box = dbQuery.QueryLinkBoxById( link_box_id );
                return link_box;
            }catch{
                return StatusCode(400, "Component Not Found");
            } 
        }

        public ActionResult<Image> GetImageMethod( int image_id ){
            try{
                Image image = dbQuery.QueryImageById(image_id);
                return image;
            }catch{
                return StatusCode(400, "Component Not Found");
            }      
        }

        public ActionResult<JsonSuccess> SwapComponentPriorityMethod(ComponentSwapDto Components, int admin_id, string admin_token, int site_id){
            if(authenticator.VerifyAdminForLeaf( admin_id, site_id, admin_token )){
                dbQuery.SwapSiteComponentOrder(Components.component_one, Components.component_two);
                return new JsonSuccess(
                    $"Component id {Components.component_one.component_id} type {Components.component_one.component_type} sucessfully swapped with Component id {Components.component_two.component_id} type {Components.component_two.component_type}");
            }else{
                return StatusCode(400, "Invalid credentials.");
            }
        }

        public ActionResult<JsonResponse> DeleteSiteComponentMethod(ComponentReference Component, int admin_id, string admin_token, int site_id){

            if(authenticator.VerifyAdminForLeaf(admin_id, site_id, admin_token)){
                return DeleteAuthenticatedSiteComponentMethod(Component);
            }else{
                return StatusCode(400, "Invalid credentials.");
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

                List<NavLink> found_nav_links;
                try{
                    found_nav_links = dbQuery.QueryNavBarLinksBySiteId(site_id);
                }catch{
                    found_nav_links = null;
                }
                if(found_nav_links != null){
                    foreach(NavLink nav_link in found_nav_links){
                        _dataLimiter.RemoveNavLinkFromDataPlan(nav_link, admin_id);
                    }
                }
                dbQuery.DeleteNavBarBySiteId(site_id);
                
                return new JsonSuccess($"NavBar Deleted for site id: {site_id}");
            }else{
                return StatusCode(400, "Invalid credentials.");
            }
        }

        public ActionResult<JsonResponse> DeleteNavLinkMethod(int admin_id, string admin_token, int site_id, int link_id){
            if(authenticator.VerifyAdminForLeaf( admin_id, site_id, admin_token )){
                List<NavLink> found_nav_links;
                try{
                    found_nav_links = dbQuery.QueryNavBarLinksBySiteId(site_id);
                }catch{
                    return StatusCode(400, $"Link ID {link_id} not found on site id {site_id}'s Nav Bar");
                }
                NavLink found_link = null;
                foreach(NavLink link in found_nav_links){
                    if(link.link_id == link_id){
                        found_link = link;
                    }
                }
                if(found_link == null){
                    return StatusCode(400, $"Link ID {link_id} not found on site id {site_id}'s Nav Bar");
                }
                _dataLimiter.RemoveNavLinkFromDataPlan(found_link, admin_id);

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
            if(authenticator.VerifyAdminForLeaf(admin_id, queried_paragraph_box.site_id, admin_token)){

                DataPlan data_plan;
                try{
                    data_plan = _dataLimiter.ValidateDataPlanB(admin_id, queried_paragraph_box, paragraph_box);
                }catch( System.ArgumentException e ){
                    return StatusCode(400, e.Message);
                }

                ParagraphBox changed_tcb = dbQuery.EditParagraphBox( paragraph_box );
                _dataLimiter.UpdateDataPlan( data_plan );
                return paragraph_box;

            }else{
                return StatusCode(400, "Invalid credentials.");
            }
        }

         public ActionResult<TwoColumnBox> EditTwoColumnBoxMethod(TwoColumnBox tc_box, int admin_id, string admin_token, int site_id){
             //check available (better way to do this?)
            TwoColumnBox queried_tc_box;
            try{
                queried_tc_box = dbQuery.QueryTwoColumnBoxById(tc_box.two_column_box_id);
            }catch{
                JsonFailure f = new JsonFailure($"Two Column Box Id: {tc_box.two_column_box_id} not found.");
                return StatusCode(400, f);
            }

            if(authenticator.VerifyAdminForLeaf(admin_id, queried_tc_box.site_id, admin_token)){

                DataPlan data_plan;
                try{
                    data_plan = _dataLimiter.ValidateDataPlanB(admin_id, queried_tc_box, tc_box);
                }catch( System.ArgumentException e ){
                    return StatusCode(400, e.Message);
                }

                TwoColumnBox changed_tcb = dbQuery.EditTwoColumnBox( tc_box );
                _dataLimiter.UpdateDataPlan( data_plan );
                return changed_tcb;

            }else{
                return StatusCode(400, "Invalid credentials.");
            }
        }

        public ActionResult<Image> EditImageMethod(Image image, int admin_id, string admin_token, int site_id){
            //check available (better way to do this?);
            Image queried_image;
            try{
                queried_image = dbQuery.QueryImageById(image.image_id);
            }catch{
                JsonFailure f = new JsonFailure($"Image Id: {image.image_id} not found.");
                return StatusCode(400, f);
            }

            //verify and change
            if(authenticator.VerifyAdminForLeaf(admin_id, queried_image.site_id, admin_token)){

                DataPlan data_plan;
                try{
                    data_plan = _dataLimiter.ValidateDataPlanB(admin_id, queried_image, image);
                }catch( System.ArgumentException e ){
                    return StatusCode(400, e.Message);
                }

                Image changed_image = dbQuery.EditImage( image );
                _dataLimiter.UpdateDataPlan( data_plan );
                return changed_image;

            }else{
                return StatusCode(400, "Invalid credentials.");
            }
        }
        
         public ActionResult<Portrait> EditPortraitMethod(Portrait portrait, int admin_id, string admin_token, int site_id){

            Portrait queried_portrait;
            try{
                queried_portrait = dbQuery.QueryPortraitById(portrait.portrait_id);
            }catch{
                JsonFailure f = new JsonFailure($"Portrait Id: {portrait.portrait_id} not found.");
                return StatusCode(400, f);
            }

            if(authenticator.VerifyAdminForLeaf(admin_id, queried_portrait.site_id, admin_token)){

                DataPlan data_plan;
                try{
                    data_plan = _dataLimiter.ValidateDataPlanB(admin_id, queried_portrait, portrait);
                }catch( System.ArgumentException e ){
                    return StatusCode(400, e.Message);
                }

                Portrait changed_portrait = dbQuery.EditPortrait( portrait );
                _dataLimiter.UpdateDataPlan( data_plan );
                return changed_portrait;

            }else{
                return StatusCode(400, "Invalid credentials.");
            }
        }

        public ActionResult<LinkBox> EditLinkBoxMethod(LinkBox link_box, int admin_id, string admin_token, int site_id){
            LinkBox queried_link_box;
            try{
               queried_link_box = dbQuery.QueryLinkBoxById(link_box.link_box_id);
            }catch{
                JsonFailure f = new JsonFailure($"link_box Id: {link_box.link_box_id} not found.");
                return StatusCode(400, f);
            }

            //verify and change
            if(authenticator.VerifyAdminForLeaf(admin_id, queried_link_box.site_id, admin_token)){

                DataPlan data_plan;
                try{
                    data_plan = _dataLimiter.ValidateDataPlanB(admin_id, queried_link_box, link_box);
                }catch( System.ArgumentException e ){
                    return StatusCode(400, e.Message);
                }

                LinkBox changed_portrait = dbQuery.EditLinkBox( link_box );
                _dataLimiter.UpdateDataPlan( data_plan );
                return changed_portrait;

            }else{
                return StatusCode(400, "Invalid credentials.");
            }
        }
    }
}