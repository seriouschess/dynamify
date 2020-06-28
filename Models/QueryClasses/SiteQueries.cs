using System.Linq;
using dynamify.Models.SiteModels;
using System.Collections.Generic;
using dynamify.dtos;
using System;

namespace dynamify.Models.QueryClasses
{
    public class SiteQueries
    {
        private MyContext dbContext;
        
        public SiteQueries(MyContext _context){
            dbContext = _context;
        }

        public List<Site> QuerySitesByAdmin(int admin_id){
            return dbContext.Sites.Where(x => x.admin_id == admin_id).ToList();
        }

        public List<Site> QueryFeaturelessSiteByUrl(string url){ //Used for more efficient querying e.g. no content
            return dbContext.Sites.Where(x => x.url == url).ToList();
        }

        public Site QueryFeaturelessSiteById(int site_id){
            List<Site> queryList = dbContext.Sites.Where(x => x.site_id == site_id).ToList();
            if(queryList.Count == 1){
                return queryList[0];
            }else{
                Site blank_site = new Site();
                blank_site.admin_id = 0; //impossible sql id for impossible site
                blank_site.site_id = 0;
                return new Site(); //query not found, bad user action
            }
        }

        public Site QuerySkeletonSiteById(int site_id){
            List<Site> FoundSites = dbContext.Sites.Where(x => x.site_id == site_id).Select( s => new Site(){
                site_id = s.site_id,
                title = s.title,
                active = s.active,
                admin_id = s.admin_id,
                owner = null,
                paragraph_boxes = dbContext.ParagraphBoxes.Where(x => x.site_id == s.site_id).Select(box => new ParagraphBox(){
                    paragraph_box_id = box.paragraph_box_id,
                    site_id = box.site_id,
                    priority = box.priority
                }).ToList(),
                images = dbContext.Images.Where(x => x.site_id == s.site_id).Select(i => new Image()
                {
                    image_id = i.image_id,
                    site_id = i.site_id,
                    priority = i.priority
                }).ToList(),
                portraits = dbContext.Portraits.Where(x => x.site_id == s.site_id).Select(p => new Portrait()
                {
                    portrait_id = p.portrait_id,
                    site_id = p.site_id,
                    priority = p.priority
                }).ToList(),
                two_column_boxes = dbContext.TwoColumnBoxes.Where(x => x.site_id == s.site_id).Select(tcb => new TwoColumnBox()
                {
                    two_column_box_id = tcb.two_column_box_id,
                    site_id = s.site_id,
                    priority = tcb.priority
                }).ToList(),
                link_boxes = dbContext.LinkBoxes.Where(x => x.site_id == s.site_id).Select(lb => new LinkBox()
                {
                    link_box_id = lb.link_box_id,
                    site_id = s.site_id,
                    priority = lb.priority
                }).ToList(),
                nav_bars = dbContext.NavBars.Where(x => x.site_id == s.site_id).Select(nb => new NavBar(){
                    nav_bar_id= nb.nav_bar_id,
                    site_id = nb.site_id,
                    string_of_links = nb.string_of_links
                    
                }).ToList()
            }).ToList();
             if(FoundSites.Count == 1){
                return FoundSites[0];
            }else{
                throw new System.ArgumentException("site not found");
            }
        }

        public Site QuerySiteById(int site_id_parameter){ //Used to get full site data to the frontend for rendering

            List<Site> FoundSites = dbContext.Sites.Where(x => x.site_id == site_id_parameter).Select( site => new Site()
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
                    priority = box.priority,
                    content = box.content,
                    site_id = box.site_id
                }).Where(x => x.site_id == site_id_parameter).ToList(),
                images = dbContext.Images.Where(x => x.site_id == site.site_id).Select(i => new Image()
                {
                    image_id = i.image_id,
                    title = i.title,
                    priority = i.priority,
                    image_src = i.image_src,
                    site_id = i.site_id
                }).Where(x => x.site_id == site_id_parameter).ToList(),

                portraits = dbContext.Portraits.Where(x => x.site_id == site.site_id).Select(p => new Portrait()
                {
                    portrait_id = p.portrait_id,
                    title = p.title,
                    priority = p.priority,
                    image_src = p.image_src,
                    content = p.content,
                    site_id = p.site_id
                }).Where(x => x.site_id == site_id_parameter).ToList(),

                two_column_boxes = dbContext.TwoColumnBoxes.Where(x => x.site_id == site.site_id).Select(tcb => new TwoColumnBox()
                {
                    two_column_box_id = tcb.two_column_box_id,
                    title = tcb.title,
                    priority = tcb.priority,
                    heading_one = tcb.heading_one,
                    heading_two = tcb.heading_two,
                    content_one = tcb.content_one,
                    content_two = tcb.content_two,
                    site_id = tcb.site_id
                }).Where(x => x.site_id == site_id_parameter).ToList(),
                link_boxes = dbContext.LinkBoxes.Where(x => x.site_id == site.site_id).Select(lb => new LinkBox()
                {
                    title = lb.title,
                    priority = lb.priority,
                    site_id = lb.site_id,
                    link_box_id = lb.link_box_id,
                    type = lb.type,
                    content = lb.content,
                    url = lb.url,
                    link_display = lb.link_display
                }).Where(x => x.site_id == site.site_id).ToList(),
            
                nav_bars = dbContext.NavBars.Where(x => x.site_id == site.site_id).Select(nb => new NavBar(){
                    site_id = nb.site_id,
                    string_of_links = nb.string_of_links
                    
                }).ToList()
            }).ToList();

            if(FoundSites.Count != 1){ //no sites active
                Site default_site = new Site();
                default_site.site_id = 0; //impossible SQL id signifies no sites are active.
                default_site.title = "Query error: Either no site was found or too many sites were found. Contact admin.";
                return default;
            }else{ //return first active site found
                System.Console.WriteLine($"Got Site: {FoundSites[0].title}");
                return FoundSites[0];
            }
        }

        public NavBarDto FormatNavBar(NavBar unformatted_nav_bar){
            NavBarDto formatted_nav_bar = new NavBarDto();
            formatted_nav_bar.site_id = 1;
            formatted_nav_bar.links = new List<NavLinkDto>();
            string  s = unformatted_nav_bar.string_of_links;
            string string_label = "";
            string string_url = "";
            bool write_label = true;
            for(var x=0; x<s.Length ;x++){
                if(s[x]+"" == "}"){
                    System.Console.WriteLine($"url: {string_url}");
                    System.Console.WriteLine($"label: {string_label}");
                    formatted_nav_bar.links.Add(new NavLinkDto(){
                        url = string_url,
                        label = string_label
                    });
                    string_url = "";
                    string_label = "";
                    write_label = true;
                }else if( s[x]+"" == "{" ){
                    write_label = false;
                }else{
                    if(write_label == true){
                        string_label += s[x]+"";
                    }else{
                        string_url += s[x]+"";
                    }
                }
            }
            return formatted_nav_bar;
        }

        public SiteContentDto QuerySiteContentById(int site_id){
            Site found_site = QuerySiteById(site_id);
            SiteContentDto converted_format = new SiteContentDto();
            converted_format.title = found_site.title;
            converted_format.images = found_site.images;
            converted_format.paragraph_boxes = found_site.paragraph_boxes;
            converted_format.portraits = found_site.portraits;
            converted_format.two_column_boxes = found_site.two_column_boxes;
            converted_format.link_boxes = found_site.link_boxes;

            try{
                converted_format.nav_bar = FormatNavBar(found_site.nav_bars[0]);
            }catch(Exception e){
                string message = e.Message;
                message = "No nav bar found";
                System.Console.WriteLine(message);
                converted_format.nav_bar = null;
            }

            return converted_format;
        }

        public SiteContentDto QuerySkeletonContentById(int site_id){
            Site FoundSite = QuerySkeletonSiteById( site_id );
            SiteContentDto ReturnSite = new SiteContentDto(){
                title = FoundSite.title,
                paragraph_boxes = FoundSite.paragraph_boxes,
                images = FoundSite.images,
                two_column_boxes = FoundSite.two_column_boxes,
                portraits = FoundSite.portraits,
                link_boxes = FoundSite.link_boxes
            };

            try{
                ReturnSite.nav_bar = FormatNavBar(FoundSite.nav_bars[0]);
            }catch(Exception e){
                string message = e.Message;
                message = "No nav bar found";
                System.Console.WriteLine(message);
                ReturnSite.nav_bar = null;
            }
            return ReturnSite;
        }

           public SiteContentDto QuerySkeletonContentByUrl(string url){
            List<Site> FoundSite = QueryFeaturelessSiteByUrl(url);
            if(FoundSite.Count == 1){
                return QuerySkeletonContentById( FoundSite[0].site_id );
            }else{
                throw new System.ArgumentException("url not found");
            } 
        }



        public SiteContentDto QuerySiteContentByURL(string url){
            List<Site> FoundSite = QueryFeaturelessSiteByUrl(url);
            if(FoundSite.Count == 1){
                return QuerySiteContentById( FoundSite[0].site_id );
            }else{
                throw new System.ArgumentException("url not found");
            } 
        }


        // --- site component queries ---
        public ParagraphBox QueryParagraphBoxById(int paragraph_box_id, int site_id ){
            List<ParagraphBox> FoundBox = dbContext.ParagraphBoxes.Where(x => x.site_id == site_id).Where(x=> x.paragraph_box_id == paragraph_box_id).ToList();
            if(FoundBox.Count == 1){
                return FoundBox[0];
            }else{
                throw new System.ArgumentException("paragraph box not found");
            }
        }

        public Portrait QueryPortraitById(int portrait_id, int site_id ){
            List<Portrait> FoundBox = dbContext.Portraits.Where(x => x.site_id == site_id).Where(x=> x.portrait_id == portrait_id).ToList();
            if(FoundBox.Count == 1){
                return FoundBox[0];
            }else{
                throw new System.ArgumentException("portrait not found");
            }
        }

        public TwoColumnBox QueryTwoColumnBoxById(int two_column_box_id, int site_id ){
            List<TwoColumnBox> FoundBox = dbContext.TwoColumnBoxes.Where(x => x.site_id == site_id).Where(x=> x.two_column_box_id == two_column_box_id).ToList();
            if(FoundBox.Count == 1){
                return FoundBox[0];
            }else{
                throw new System.ArgumentException("two column box not found");
            }
        }

        public Image QueryImageById(int image_id, int site_id ){
            List<Image> FoundImage = dbContext.Images.Where(x => x.site_id == site_id).Where(x=> x.image_id == image_id).ToList();
            if(FoundImage.Count == 1){
                return FoundImage[0];
            }else{
                throw new System.ArgumentException("image not found");
            }
        }

        public LinkBox QueryLinkBoxById(int link_box_id, int site_id ){
            List<LinkBox> FoundLinkBox = dbContext.LinkBoxes.Where(x => x.site_id == site_id).Where(x=> x.link_box_id == link_box_id).ToList();
            if(FoundLinkBox.Count == 1){
                return FoundLinkBox[0];
            }else{
                throw new System.ArgumentException("link box not found");
            }
        }

        //actions 
        public Site AddSite(Site NewSite){
            List<Site> test = QueryFeaturelessSiteByUrl(NewSite.url);
            if(test.Count == 0){
                dbContext.Add(NewSite);
                dbContext.SaveChanges();
                List<Site> confirmation = QueryFeaturelessSiteByUrl(NewSite.url); 
                return confirmation[0];
            }else{ //duplicate title exists!
                return null;
                //throw new System.ArgumentException("Query Error. All site URLs must be unique.", "NewSite.title");
            }
        }
        
        public Site DeleteSiteById(int site_id){
            Site FoundSite = QueryFeaturelessSiteById(site_id);
            dbContext.Remove( FoundSite );
            dbContext.SaveChanges();
            return FoundSite;
        }

        public ParagraphBox DeleteParagraphBox(int p_box_id){
            List<ParagraphBox> Subject = dbContext.ParagraphBoxes.Where(x => x.paragraph_box_id == p_box_id).ToList();
            if(Subject.Count == 1){
                dbContext.Remove(Subject[0]);
                dbContext.SaveChanges();
                return Subject[0];
            }else{
                throw new System.ArgumentException("Query Error. Non unique Id.", "NewSite.title");
            }
        }

        public Image DeleteImage(int image_id){
            List<Image> Subject = dbContext.Images.Where(x => x.image_id == image_id).ToList();
            if(Subject.Count == 1){
                dbContext.Remove(Subject[0]);
                dbContext.SaveChanges();
                return Subject[0];
            }else{
                throw new System.ArgumentException("Query Error. Non unique Id.", "image_id");
            }
            
        }

        public Portrait DeletePortrait(int portrait_id){
            List<Portrait> Subject = dbContext.Portraits.Where(x => x.portrait_id == portrait_id).ToList();
            if(Subject.Count == 1){
                dbContext.Remove(Subject[0]);
                dbContext.SaveChanges();
                return Subject[0];
            }else{
                throw new System.ArgumentException("Query Error. Non unique Id.", "portrait_id");
            }
        }
        public TwoColumnBox DeleteTwoColumnBox(int two_column_box_id){
            List<TwoColumnBox> Subject = dbContext.TwoColumnBoxes.Where(x => x.two_column_box_id == two_column_box_id).ToList();
            if(Subject.Count == 1){
                dbContext.Remove(Subject[0]);
                dbContext.SaveChanges();
                return Subject[0];
            }else{
                throw new System.ArgumentException("Query Error. Non unique ID", "portrait_id");
            }
        }

         public LinkBox DeleteLinkBox(int link_box_id){
            List<LinkBox> Subject = dbContext.LinkBoxes.Where(x => x.link_box_id == link_box_id).ToList();
            if(Subject.Count == 1){
                dbContext.Remove(Subject[0]);
                dbContext.SaveChanges();
                return Subject[0];
            }else{
                throw new System.ArgumentException("Query Error. Non unique ID", "portrait_id");
            }
        }

        public NavBar DeleteNavBar(int nav_bar_id){
              List<NavBar> Subject = dbContext.NavBars.Where(x => x.nav_bar_id == nav_bar_id).ToList();
            if(Subject.Count == 1){
                dbContext.Remove(Subject[0]);
                dbContext.SaveChanges();
                return Subject[0];
            }else{
                throw new System.ArgumentException("Query Error. Non unique ID", "portrait_id");
            }
        }

        public void AddNavBar( NavBarDto nav_bar_dto ){
            List<NavBar> test_query = dbContext.NavBars.Where(x => x.site_id == nav_bar_dto.site_id).ToList();

            NavBar nav_bar = new NavBar();
            nav_bar.site_id = nav_bar_dto.site_id;
            //convert to string_of_links format
            string s = ""; 
            for(var x=0; x < nav_bar_dto.links.Count; x++){
                s += nav_bar_dto.links[x].label;
                s += "{";
                s += nav_bar_dto.links[x].url;
                s += "}";
            }
            nav_bar.string_of_links = s;
            if(test_query.Count > 0){
                System.Console.WriteLine("Detect more than one nav bar");
                test_query[0].site_id = nav_bar.site_id;
                test_query[0].string_of_links = nav_bar.string_of_links;
            }else{
                System.Console.WriteLine("Nav Bar Created");
                dbContext.Add( nav_bar ); 
            }
            dbContext.SaveChanges(); 
        }
        

        public void AddLinkBox( LinkBox link_box ){
            dbContext.Add( link_box );
            dbContext.SaveChanges();
        }

        public void AddTwoColumnBox(TwoColumnBox tc_box){
            dbContext.Add(tc_box);
            dbContext.SaveChanges();
        }

        public void AddPortrait(Portrait portrait){
            dbContext.Add(portrait);
            dbContext.SaveChanges();
        }

        public void AddImage(Image image){
            dbContext.Add(image);
            dbContext.SaveChanges();
        }

        public void AddParagraphBox(ParagraphBox p_box){
            dbContext.Add(p_box);
            dbContext.SaveChanges();
        }

    }
}