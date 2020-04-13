//project dependencies
using dynamify.Classes.Auth;
using dynamify.Models.QueryClasses;

//project models
using dynamify.dtos;
using dynamify.Models.JsonModels;
using dynamify.Models.SiteModels;
using System.Collections.Generic;

namespace dynamify.Controllers.ControllerMethods
{
    public class SiteControllerMethods
    {
        private SiteQueries dbQuery;

        private Auth authenticator;

        public SiteControllerMethods(SiteQueries _dbQuery, AdminQueries _AdbQuery){
            dbQuery = _dbQuery;
            authenticator = new Auth(_AdbQuery);
        }

       public SiteContentDto GetSiteByIdMethod(SiteRequestDto request){
            if(authenticator.VerifyAdmin(request.admin_id, request.token)){
                SiteContentDto foundSite = dbQuery.QuerySiteContentById(request.site_id);
               return foundSite;
            }else{
                return new SiteContentDto();
            }
       }

       public SiteContentDto GetByURLMethod(string leaf_url){
            SiteContentDto foundSite = dbQuery.QuerySiteContentByURL(leaf_url);
            return foundSite;
       }

       public JsonResponse SetActiveSiteMethod(SiteRequestDto request){
            if(authenticator.VerifyAdmin(request.admin_id, request.token)){
                List<Site> SiteToSetActive = dbQuery.QueryFeaturelessSiteById(request.site_id);
                if(SiteToSetActive.Count != 1){ //ensure only one site
                    JsonResponse r = new JsonFailure($"Set Active API route failure: Ensure correct site id was sent.");
                    return r;
                }else{
                    Site ActiveSite = dbQuery.SetActiveSiteDB(SiteToSetActive[0]);
                    JsonResponse r = new JsonSuccess($"Site Title: < {ActiveSite.title} > is now active!");
                    return r;
                } 

            }else{
                return new JsonFailure("Invalid Token. Stranger Danger");
            }
       }

        public JsonResponse PostMethod(NewSiteDto NewSite){
            if(authenticator.VerifyAdmin(NewSite.admin_id, NewSite.token)){
                List<Site> test = dbQuery.QueryFeaturelessSiteByUrl(NewSite.url);
                if(test.Count > 0){
                    JsonResponse r = new JsonFailure("Site must not have duplicate title with existing site.");
                    return r;
                }else{
                    Site SoonToAddSite = new Site();
                    SoonToAddSite.title = NewSite.title;
                    SoonToAddSite.admin_id = NewSite.admin_id;
                    SoonToAddSite.url = NewSite.url;
                    dbQuery.AddSite(SoonToAddSite);
                    JsonResponse r = new JsonSuccess($"Site created with title: ${NewSite.title}");
                    return r;
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
            System.Console.WriteLine("ray");
            if(authenticator.VerifyAdmin(admin_id, admin_token)){
                dbQuery.AddParagraphBox(NewBox);
                JsonResponse r = new JsonSuccess("Paragraph box posted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        public JsonResponse PostImageMethod(Image NewImage, int admin_id, string admin_token){
             if(authenticator.VerifyAdmin(admin_id, admin_token)){
                dbQuery.AddImage(NewImage);
                JsonResponse r = new JsonSuccess("Image posted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        public JsonResponse PostTwoColumnBoxMethod(TwoColumnBox NewTwoColumnBox, int admin_id, string admin_token){
             if(authenticator.VerifyAdmin(admin_id, admin_token)){
                dbQuery.AddTwoColumnBox(NewTwoColumnBox);
                JsonResponse r = new JsonSuccess("Two column box posted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        public JsonResponse PostNavBarMethod(NavBarDto NewNavBar, int admin_id, string admin_token){
             if(authenticator.VerifyAdmin(admin_id, admin_token)){
                dbQuery.AddNavBar(NewNavBar);
                JsonResponse r = new JsonSuccess("Nav Bar posted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        public JsonResponse PostPortraitMethod(Portrait NewPortrait, int admin_id, string admin_token){
             if(authenticator.VerifyAdmin(admin_id, admin_token)){
                dbQuery.AddPortrait(NewPortrait);
                JsonResponse r = new JsonSuccess("Portrait posted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        public JsonResponse PostLinkBoxMethod(NewLinkBoxDto _NewLinkBox, int admin_id, string admin_token){
             if(authenticator.VerifyAdmin(admin_id, admin_token)){
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

        public JsonResponse DeleteSite(int site_id_parameter, int admin_id, string admin_token){
              if(authenticator.VerifyAdmin(admin_id, admin_token)){
                Site DeletedSite = dbQuery.DeleteSiteById(site_id_parameter);
                JsonResponse r = new JsonSuccess($"Site {DeletedSite.title} deleted sucessfully!");
                return r;
            }else{
                return new JsonFailure("Invalid Token. Stranger Danger.");
            }
        }

        public JsonResponse DeleteSiteComponentMethod(ComponentReference Component, int admin_id, string admin_token){
            System.Console.WriteLine($"admin id {admin_id}, admin token {admin_token}");
            if(authenticator.VerifyAdmin(admin_id, admin_token)){
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
    }
}