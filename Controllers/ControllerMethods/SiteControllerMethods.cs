//project dependencies
using dynamify.Classes.Auth;

//project models
using dynamify.dtos;
using dynamify.Models.JsonModels;
using dynamify.Models.SiteModels;
using System.Collections.Generic;
using dynamify.ServerClasses.Auth;
using dynamify.ServerClasses.QueryClasses;

namespace dynamify.Controllers.ControllerMethods
{
    public class SiteControllerMethods
    {
        private SiteQueries dbQuery;

        private SiteAuth authenticator;
        private SiteCreationValidator validator;

        public SiteControllerMethods(SiteQueries _dbQuery, AdminQueries _AdbQuery){
            dbQuery = _dbQuery;
            authenticator = new SiteAuth(_AdbQuery, _dbQuery);
            validator = new SiteCreationValidator(dbQuery);
        }

        public SkeletonSiteDto GetSkeletonSiteByIdMethod(int site_id){
            return dbQuery.QuerySkeletonSiteById(site_id);
        }

        public SkeletonSiteDto GetSkeletonSiteByUrlMethod( string url ){
            try{
                SkeletonSiteDto FoundSite = dbQuery.QuerySkeletonContentByUrl( url );
                return FoundSite;
            }catch{
                throw new System.ArgumentException("url not found");
            }
        }

        public JsonResponse PostMethod(NewSiteDto NewSite){
            if(authenticator.VerifyAdmin(NewSite.admin_id, NewSite.token)){
                string verdict = validator.ValidateSiteUrl(NewSite.url);
                if(verdict == "pass"){
                    Site SoonToAddSite = new Site();
                    SoonToAddSite.title = NewSite.title;
                    SoonToAddSite.admin_id = NewSite.admin_id;
                    SoonToAddSite.url = NewSite.url.ToLower();
                    dbQuery.AddSite(SoonToAddSite);
                    JsonResponse r = new JsonSuccess($"Site created with title: ${NewSite.title}");
                    return r;
                }else{
                    return new JsonFailure(verdict);
                }
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        public IEnumerable<Site> GetByAdminIdMethod(int admin_id, string admin_token){
            if(authenticator.VerifyAdmin(admin_id, admin_token)){
                List<Site> OwnedSites = dbQuery.QuerySitesByAdmin(admin_id);
                return OwnedSites;
            }else{
                return new List<Site>();
            } 
        }

        public JsonResponse PostBoxMethod(ParagraphBox NewBox, int admin_id, string admin_token){
            if(authenticator.VerifyAdminForLeaf(admin_id, NewBox.site_id, admin_token)){
                dbQuery.AddParagraphBox(NewBox);
                JsonResponse r = new JsonSuccess("Paragraph box posted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        public JsonResponse PostImageMethod(Image NewImage, int admin_id, string admin_token){
             if(authenticator.VerifyAdminForLeaf(admin_id, NewImage.site_id, admin_token)){
                dbQuery.AddImage(NewImage);
                JsonResponse r = new JsonSuccess("Image posted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        public JsonResponse PostTwoColumnBoxMethod(TwoColumnBox NewTwoColumnBox, int admin_id, string admin_token){
             if(authenticator.VerifyAdminForLeaf(admin_id, NewTwoColumnBox.site_id, admin_token)){
                dbQuery.AddTwoColumnBox(NewTwoColumnBox);
                JsonResponse r = new JsonSuccess("Two column box posted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        public JsonResponse PostNavBarMethod(int admin_id, string admin_token, int site_id){
             if(authenticator.VerifyAdminForLeaf(admin_id, site_id, admin_token)){
                dbQuery.AddNavBarToSite( site_id );
                JsonResponse r = new JsonSuccess("Nav Bar posted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        public NavLinkDto PostNavLinkMethod( NewNavLinkDto new_link, int admin_id, string admin_token, int site_id ){
             if(authenticator.VerifyAdminForLeaf(admin_id, site_id, admin_token)){
                return dbQuery.AddNavBarLinkToSite(new_link, site_id);
            }else{
                throw new System.ArgumentException("I'm not sure what that means.");
            }
        }

        public JsonResponse PostPortraitMethod(Portrait NewPortrait, int admin_id, string admin_token){
             if(authenticator.VerifyAdminForLeaf(admin_id, NewPortrait.site_id, admin_token)){
                dbQuery.AddPortrait(NewPortrait);
                JsonResponse r = new JsonSuccess("Portrait posted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        public JsonResponse PostLinkBoxMethod(NewLinkBoxDto _NewLinkBox, int admin_id, string admin_token){
             if(authenticator.VerifyAdminForLeaf(admin_id, _NewLinkBox.site_id, admin_token)){
                LinkBox NewLinkBox = new LinkBox();
                NewLinkBox.title = _NewLinkBox.title;
                NewLinkBox.priority = _NewLinkBox.priority;
                NewLinkBox.site_id = _NewLinkBox.site_id;
                NewLinkBox.content = _NewLinkBox.content;
                NewLinkBox.url = _NewLinkBox.url;
                NewLinkBox.link_display = _NewLinkBox.link_display;

                dbQuery.AddLinkBox(NewLinkBox);
                JsonResponse r = new JsonSuccess("Link Box posted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        public JsonResponse DeleteSite(int site_id, int admin_id, string admin_token){
              if(authenticator.VerifyAdminForLeaf(admin_id, site_id, admin_token)){
                Site DeletedSite = dbQuery.DeleteSiteById(site_id);
                JsonResponse r = new JsonSuccess($"Site {DeletedSite.title} deleted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }


        //------         COMPONENT QUERY METHODS        -----
        public NavBarDto GetNavBarMethod(int site_id){
            try{
                return dbQuery.QueryNavBarDtoBySiteId(site_id);
            }catch{
                throw new System.ArgumentException($"Nav Bar for site id {site_id} not found.");
            }
            
        }
    
        public ParagraphBox GetParagraphBoxMethod(ComponentRequestDto request){
            try{
                ParagraphBox paragraph_box = dbQuery.QueryParagraphBoxById( request.component_id );
                return paragraph_box;
            }catch{
                throw new System.ArgumentException("Component Not Found");
            }
        }

        public Portrait GetPortraitMethod(ComponentRequestDto request){
            try{
                Portrait portrait = dbQuery.QueryPortraitById( request.component_id );
                return portrait;
            }catch{
                throw new System.ArgumentException("Component Not Found");
            }
        }

        public TwoColumnBox GetTwoColumnBoxMethod(ComponentRequestDto request){
            try{
                TwoColumnBox two_column_box = dbQuery.QueryTwoColumnBoxById( request.component_id );
                return two_column_box;
            }catch{
                throw new System.ArgumentException("Component Not Found");
            }
        }

        public LinkBox GetLinkBoxMethod(ComponentRequestDto request){
            try{
                LinkBox link_box = dbQuery.QueryLinkBoxById( request.component_id );
                return link_box;
            }catch{
                throw new System.ArgumentException("Component Not Found");
            } 
        }

        public Image GetImageMethod( ComponentRequestDto request ){
            try{
                Image image = dbQuery.QueryImageById(request.component_id);
                return image;
            }catch{
                throw new System.ArgumentException("Component Not Found");
            }      
        }

        public JsonSuccess SwapComponentPriorityMethod(ComponentSwapDto Components, int admin_id, string admin_token, int site_id){
            if(authenticator.VerifyAdminForLeaf( admin_id, site_id, admin_token )){
                dbQuery.SwapSiteComponentOrder(Components.component_one, Components.component_two);
                return new JsonSuccess(
                    $"Component id {Components.component_one.component_id} type {Components.component_one.component_type} sucessfully swapped with Component id {Components.component_two.component_id} type {Components.component_two.component_type}");
            }else{
                throw new System.ArgumentException("Invalid Credentials.");
            }
        }
        
        public JsonResponse DeleteSiteComponentMethod(ComponentReference Component, int admin_id, string admin_token){

            //requires refactor, requires site Id
            if(authenticator.VerifyAdmin(admin_id, admin_token)){ //terrible!!!! Fix This!!!
                if(Component.component_type == "p_box"){
                    ParagraphBox paragraph_box = dbQuery.DeleteParagraphBox(Component.component_id);
                    JsonResponse r = new JsonSuccess("Paragraph box deleted sucessfully!");
                    return r;
                }else if(Component.component_type == "image"){
                    Image image = dbQuery.DeleteImage(Component.component_id);
                    JsonResponse r = new JsonSuccess("Image deleted sucessfully!");
                    return r;
                }else if(Component.component_type == "portrait"){
                    Portrait portrait = dbQuery.DeletePortrait(Component.component_id);
                    JsonResponse r = new JsonSuccess("Portrait component deleted sucessfully!");
                    return r;
                }else if(Component.component_type == "2c_box"){
                    TwoColumnBox two_column_box = dbQuery.DeleteTwoColumnBox(Component.component_id);
                    JsonResponse r = new JsonSuccess("Two Column Box component deleted sucessfully!");
                    return r;
                }else if(Component.component_type == "link_box"){
                    LinkBox portrait = dbQuery.DeleteLinkBox(Component.component_id);
                    JsonResponse r = new JsonSuccess("Link Box component deleted sucessfully!");
                    return r;
                }else{
                    JsonResponse r = new JsonSuccess("Type mismatch. Type does not match any known components.");
                    return r;
                }
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        public JsonResponse DeleteNavBarMethod(int admin_id, string admin_token, int site_id){
            if(authenticator.VerifyAdminForLeaf( admin_id, site_id, admin_token )){
                dbQuery.DeleteNavBarBySiteId(site_id);
                return new JsonSuccess($"NavBar Deleted for site id: {site_id}");
            }else{
                return new JsonFailure("I wish I only knew why.");
            }
        }

        public JsonResponse DeleteNavLinkMethod(int admin_id, string admin_token, int site_id, int link_id){
            if(authenticator.VerifyAdminForLeaf( admin_id, site_id, admin_token )){
                dbQuery.DeleteNavLinkById(link_id);
                return new JsonSuccess($"NavLink Deleted for link id: {link_id}");
            }else{
                return new JsonFailure("I wish I only knew why.");
            }
            
        }

        //Component Edit Methods
        public ParagraphBox EditParagraphBoxMethod(ParagraphBox paragraph_box, int admin_id, string admin_token, int site_id){
            if(authenticator.VerifyAdminForLeaf(admin_id, site_id, admin_token)){
                return dbQuery.EditParagraphBox( paragraph_box );
            }else{
                throw new System.ArgumentException("Invalid credentials.");
            }
        }

         public TwoColumnBox EditTwoColumnBoxMethod(TwoColumnBox paragraph_box, int admin_id, string admin_token, int site_id){
            if(authenticator.VerifyAdminForLeaf(admin_id, site_id, admin_token)){
                return dbQuery.EditTwoColumnBox( paragraph_box );
            }else{
                throw new System.ArgumentException("Invalid credentials.");
            }
        }

         public Image EditImageMethod(Image image, int admin_id, string admin_token, int site_id){
            if(authenticator.VerifyAdminForLeaf(admin_id, site_id, admin_token)){
                return dbQuery.EditImage( image );
            }else{
                throw new System.ArgumentException("Invalid credentials.");
            }
        }
        
         public Portrait EditPortraitMethod(Portrait portrait, int admin_id, string admin_token, int site_id){
            if(authenticator.VerifyAdminForLeaf(admin_id, site_id, admin_token)){
                return dbQuery.EditPortrait( portrait );
            }else{
                throw new System.ArgumentException("Invalid credentials.");
            }
        }

        public LinkBox EditLinkBoxMethod(LinkBox link_box, int admin_id, string admin_token, int site_id){
            if(authenticator.VerifyAdminForLeaf(admin_id, site_id, admin_token)){
                return dbQuery.EditLinkBox( link_box );
            }else{
                throw new System.ArgumentException("Invalid credentials.");
            }
        }
    }
}