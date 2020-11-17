using System.Linq;
using dynamify.ServerClasses.QueryClasses;
using System.Collections.Generic;
using dynamify.dtos;
using System;
using dynamify.Models;
using dynamify.Models.SiteModels;

namespace dynamify.ServerClasses.QueryClasses
{
    public class SiteQueries
    {
        private DatabaseContext dbContext;
        
        public SiteQueries(DatabaseContext _context){
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
                throw new System.ArgumentException($"Site id: {site_id} not found.");
            }
        }

        public SkeletonSiteDto QuerySkeletonSiteById(int site_id){
            List<SkeletonSiteDto> FoundSites = dbContext.Sites.Where(x => x.site_id == site_id).Select( s => new SkeletonSiteDto(){
                site_id = s.site_id,
                title = s.title,
                site_components = GetSiteComponentDtosForId(site_id)
            }).ToList();
             if(FoundSites.Count == 1){
                return FoundSites[0];
            }else{
                throw new System.ArgumentException("site not found");
            }
        }

        public SkeletonSiteDto QuerySkeletonContentByUrl(string url){
            List<Site> FoundSite = QueryFeaturelessSiteByUrl(url);
            if(FoundSite.Count == 1){
                return QuerySkeletonSiteById( FoundSite[0].site_id );
            }else{
                throw new System.ArgumentException("url not found");
            } 
        }

        public List<SiteComponentDto> GetSiteComponentDtosForId(int site_id){
            List<SiteComponentDto> site_components = new List<SiteComponentDto>();
            List<SiteComponentDto> paragraph_boxes = dbContext.ParagraphBoxes.Where(x => x.site_id == site_id).Select(box => new SiteComponentDto(){
                    component_id = box.paragraph_box_id,
                    priority = box.priority,
                    type = box.type
                }).ToList();
                site_components.AddRange(paragraph_boxes);

            List<SiteComponentDto> images = dbContext.Images.Where(x => x.site_id == site_id).Select(box => new SiteComponentDto(){
                    component_id = box.image_id,
                    priority = box.priority,
                    type = box.type
                }).ToList();
                site_components.AddRange(images);

            List<SiteComponentDto> portraits = dbContext.Portraits.Where(x => x.site_id == site_id).Select(box => new SiteComponentDto(){
                    component_id = box.portrait_id,
                    priority = box.priority,
                    type = box.type
                }).ToList();
                site_components.AddRange(portraits);

            List<SiteComponentDto> two_column_boxes = dbContext.TwoColumnBoxes.Where(x => x.site_id == site_id).Select(box => new SiteComponentDto(){
                    component_id = box.two_column_box_id,
                    priority = box.priority,
                    type = box.type
                }).ToList();
                site_components.AddRange(two_column_boxes);

            List<SiteComponentDto> link_boxes = dbContext.LinkBoxes.Where(x => x.site_id == site_id).Select(box => new SiteComponentDto(){
                    component_id = box.link_box_id,
                    priority = box.priority,
                    type = box.type
                }).ToList();
                site_components.AddRange(link_boxes);
    
            return site_components.OrderBy( x => x.priority).ToList();
        }

        public Site QuerySiteById(int site_id_parameter){ //Used to get full site data to the frontend for rendering

            List<Site> FoundSites = dbContext.Sites.Where(x => x.site_id == site_id_parameter).Select( site => new Site()
            {
                site_id = site_id_parameter,
                title = site.title,
                admin_id = site.admin_id,
                owner = dbContext.Admins.Where(x => x.admin_id == site.admin_id).Select(s => new Admin()
                {
                    admin_id = s.admin_id,
                    username = s.username,
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
                    nav_bar_id = nb.nav_bar_id,
                    site_id = nb.site_id,
                    links = dbContext.NavLinks.Where( x => x.nav_bar_id == nb.nav_bar_id ).ToList()
                }).ToList()
            }).ToList();

            if(FoundSites.Count != 1){ //no sites active
                Site default_site = new Site();
                default_site.site_id = 0; //impossible SQL id signifies no sites are active.
                default_site.title = "Query error: Either no site was found or too many sites were found. Contact admin.";
                return default;
            }else{ //return first active site found
                return FoundSites[0];
            }
        }

        // --- site component queries ---

        public void SwapSiteComponentOrder( ComponentReference component_one, ComponentReference component_two ){
            int priority_one = ChangeComponentPriorityAndReturnOldPriority(component_one, 0); //0 is a no consequence value
            int priority_two = ChangeComponentPriorityAndReturnOldPriority(component_two, priority_one);
            priority_one = ChangeComponentPriorityAndReturnOldPriority(component_one, priority_two);
            dbContext.SaveChanges();
        }

        public int ChangeComponentPriorityAndReturnOldPriority( ComponentReference cr, int new_priority ){
            int old_priority = 0;
            if(cr.component_type == "p_box"){
                ParagraphBox pb = QueryParagraphBoxById(cr.component_id);
                old_priority = pb.priority;
                pb.priority = new_priority;
            }else if(cr.component_type == "2c_box"){
                TwoColumnBox tcb = QueryTwoColumnBoxById(cr.component_id);
                old_priority = tcb.priority;
                tcb.priority = new_priority;
            }else if(cr.component_type == "image"){
                Image image = QueryImageById(cr.component_id);
                old_priority = image.priority;
                image.priority = new_priority;
            }else if(cr.component_type == "portrait"){
                Portrait portrait = QueryPortraitById(cr.component_id);
                old_priority = portrait.priority;
                portrait.priority = new_priority;
            }else if(cr.component_type == "link_box"){
                LinkBox lb = QueryLinkBoxById(cr.component_id);
                old_priority = lb.priority;
                lb.priority = new_priority;
            }else{
                throw new System.ArgumentException($"Component component_type {cr.component_type} is not suitable to swap");
            }

            return old_priority;
        }

        public NavBarDto QueryNavBarDtoBySiteId( int site_id ){
            List<NavBarDto> QueriedBars = dbContext.NavBars.Where(x => x.site_id == site_id).Select(nb => new NavBarDto(){
                links = dbContext.NavLinks.Where(x => x.nav_bar_id == nb.nav_bar_id).Select( li => new NavLinkDto(){
                    label = li.label,
                    url = li.url
                }).ToList(),
                site_id = nb.site_id
            }).ToList();
            if( QueriedBars.Count == 1 ){
                return QueriedBars[0];
            }else if( QueriedBars.Count == 0 ){
                return null;
            }else{
                throw new System.ArgumentException($"Site id {site_id} has {QueriedBars.Count} NavBars and may not exceed 1.");
            }
        }

        public NavBar QueryNavBarBySiteId(int site_id){
            List<NavBar> QueriedBars = dbContext.NavBars.Where(x => x.site_id == site_id).ToList();
            if( QueriedBars.Count == 1 ){
                return QueriedBars[0];
            }else if( QueriedBars.Count == 0 ){
                throw new System.ArgumentException($"No nav bar currently exists for site id: ${site_id}");
            }else{
                throw new System.ArgumentException($"Site id {site_id} has {QueriedBars.Count} NavBars and may not exceed 1.");
            }
        }

        public ParagraphBox QueryParagraphBoxById(int paragraph_box_id ){
            List<ParagraphBox> FoundBox = dbContext.ParagraphBoxes.Where(x=> x.paragraph_box_id == paragraph_box_id).ToList();
            if(FoundBox.Count == 1){
                return FoundBox[0];
            }else{
                throw new System.ArgumentException("paragraph box not found");
            }
        }

        public Portrait QueryPortraitById(int portrait_id ){
            List<Portrait> FoundBox = dbContext.Portraits.Where(x=> x.portrait_id == portrait_id).ToList();
            if(FoundBox.Count == 1){
                return FoundBox[0];
            }else{
                throw new System.ArgumentException("portrait not found");
            }
        }

        public TwoColumnBox QueryTwoColumnBoxById(int two_column_box_id ){
            List<TwoColumnBox> FoundBox = dbContext.TwoColumnBoxes.Where(x=> x.two_column_box_id == two_column_box_id).ToList();
            if(FoundBox.Count == 1){
                return FoundBox[0];
            }else{
                throw new System.ArgumentException("two column box not found");
            }
        }

        public Image QueryImageById(int image_id ){
            List<Image> FoundImage = dbContext.Images.Where(x=> x.image_id == image_id).ToList();
            if(FoundImage.Count == 1){
                return FoundImage[0];
            }else{
                throw new System.ArgumentException("image not found");
            }
        }

        public LinkBox QueryLinkBoxById(int link_box_id ){
            List<LinkBox> FoundLinkBox = dbContext.LinkBoxes.Where(x=> x.link_box_id == link_box_id).ToList();
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

        public NavBar DeleteNavBarBySiteId(int site_id){
            List<NavBar> found_nb = dbContext.NavBars.Where(x => x.site_id == site_id).ToList();
            if(found_nb.Count == 1){
                dbContext.Remove(found_nb[0]);
                dbContext.SaveChanges();
                return found_nb[0];
            }else{
                throw new System.ArgumentException($"Unable to find nav bar with site id {site_id}");
            }
        }

        public NavLink DeleteNavLinkById( int link_id ){
            List<NavLink> found_links = dbContext.NavLinks.Where(x => x.link_id == link_id).ToList();
            if(found_links.Count == 1){
                dbContext.Remove(found_links[0]);
                dbContext.SaveChanges();
                return found_links[0];
            }else{
                throw new System.ArgumentException($"Could not find link id: {link_id}");
            }
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

        public NavBar AddNavBarToSite( int site_id){

            Site test_site = QuerySiteById(site_id);
            NavBar nav_bar = new NavBar();
            nav_bar.site_id = site_id;
            List<NavBar> test_query = dbContext.NavBars.Where(x => x.site_id == nav_bar.site_id).ToList();

            //only one may exist per site
            if(test_query.Count == 0){
                dbContext.Add(nav_bar);
                dbContext.SaveChanges(); 
            }

            return nav_bar;
        }

        public NavLinkDto AddNavBarLinkToSite(NewNavLinkDto new_nav_link, int site_id){
            // add nav bar to site if not exists
            AddNavBarToSite(site_id);

            //add nav link
            NavLink link_to_add = new NavLink();
            link_to_add.nav_bar_id = QueryNavBarBySiteId(site_id).nav_bar_id;
            link_to_add.label = new_nav_link.label;
            link_to_add.url = new_nav_link.url;
            dbContext.Add(link_to_add);
            dbContext.SaveChanges();
            NavLinkDto link_to_send = new NavLinkDto();
            link_to_send.link_id = link_to_add.link_id;
            link_to_send.label = link_to_add.label;
            link_to_send.url = link_to_add.url;
            return link_to_send;
        }
        

        public void AddLinkBox( LinkBox link_box ){
            if(link_box.byte_size == 0){
                throw new System.ArgumentException("Link box must have byte size greater than 0. Is it being calculated?");
            }
            dbContext.Add( link_box );
            dbContext.SaveChanges();
        }

        public void AddTwoColumnBox(TwoColumnBox tc_box){
            if(tc_box.byte_size == 0){
                throw new System.ArgumentException("Two column box must have byte size greater than 0. Is it being calculated?");
            }
            dbContext.Add(tc_box);
            dbContext.SaveChanges();
        }

        public void AddPortrait(Portrait portrait){
            if(portrait.byte_size == 0){
                throw new System.ArgumentException("Portrait must have byte size greater than 0. Is it being calculated?");
            }
            dbContext.Add(portrait);
            dbContext.SaveChanges();
        }

        public void AddImage(Image image){
            if(image.byte_size == 0){
                throw new System.ArgumentException("Image must have byte size greater than 0. Is it being calculated?");
            }
            dbContext.Add(image);
            dbContext.SaveChanges();
        }

        public void AddParagraphBox(ParagraphBox p_box){
            if(p_box.byte_size == 0){
                throw new System.ArgumentException("Paragraph box must have byte size greater than 0. Is it being calculated?");
            }
            dbContext.Add(p_box);
            dbContext.SaveChanges();
        }


        //edit queries
        public ParagraphBox EditParagraphBox(ParagraphBox updated_box){
            if(updated_box.byte_size == 0){
                throw new System.ArgumentException("Paragraph box must have byte size greater than 0. Is it being calculated?");
            }
            ParagraphBox box_to_update = QueryParagraphBoxById(updated_box.paragraph_box_id);
            box_to_update.title = updated_box.title;
            box_to_update.content = updated_box.content;
            box_to_update.UpdatedAt = DateTime.Now;
            UpdateSiteDateTime(box_to_update.site_id);
            dbContext.SaveChanges();
            return box_to_update;
        }

        public Portrait EditPortrait(Portrait updated_portrait){
            if(updated_portrait.byte_size == 0){
                throw new System.ArgumentException("Portrait must have byte size greater than 0. Is it being calculated?");
            }
            Portrait portrait_to_update = QueryPortraitById(updated_portrait.portrait_id);
            portrait_to_update.title = updated_portrait.title;
            portrait_to_update.image_src = updated_portrait.image_src;
            portrait_to_update.content = updated_portrait.content;
            portrait_to_update.UpdatedAt = DateTime.Now;
            UpdateSiteDateTime(portrait_to_update.site_id);
            dbContext.SaveChanges();
            return portrait_to_update;
        }

        public Image EditImage(Image updated_image){
            if(updated_image.byte_size == 0){
                throw new System.ArgumentException("Image must have byte size greater than 0. Is it being calculated?");
            }
            Image image_to_update = QueryImageById(updated_image.image_id);
            image_to_update.title = updated_image.title;
            image_to_update.image_src = updated_image.image_src;
            image_to_update.UpdatedAt = DateTime.Now;
            UpdateSiteDateTime(image_to_update.site_id);
            dbContext.SaveChanges();
            return image_to_update;
        }

        public TwoColumnBox EditTwoColumnBox(TwoColumnBox updated_two_column_box){
            if(updated_two_column_box.byte_size == 0){
                throw new System.ArgumentException("Paragraph box must have byte size greater than 0. Is it being calculated?");
            }
            TwoColumnBox two_column_box_to_update = QueryTwoColumnBoxById(updated_two_column_box.two_column_box_id);
            two_column_box_to_update.heading_one = updated_two_column_box.heading_one;
            two_column_box_to_update.heading_two = updated_two_column_box.heading_two;
            two_column_box_to_update.content_one = updated_two_column_box.content_one;
            two_column_box_to_update.content_two = updated_two_column_box.content_two;
            two_column_box_to_update.title = updated_two_column_box.title;
            two_column_box_to_update.UpdatedAt = DateTime.Now;
            return two_column_box_to_update;
        }

        public LinkBox EditLinkBox(LinkBox updated_link_box){
            if(updated_link_box.byte_size == 0){
                throw new System.ArgumentException("Link box must have byte size greater than 0. Is it being calculated?");
            }
            LinkBox link_box_to_update = QueryLinkBoxById(updated_link_box.link_box_id);
            link_box_to_update.title = updated_link_box.title;
            link_box_to_update.url = updated_link_box.url;
            link_box_to_update.link_display = updated_link_box.link_display;
            link_box_to_update.content = updated_link_box.content;
            UpdateSiteDateTime(updated_link_box.site_id);
            dbContext.SaveChanges();
            return link_box_to_update;
        }

        public Site UpdateSiteDateTime(int site_id){
            Site found_site = QueryFeaturelessSiteById(site_id);
            found_site.UpdatedAt = DateTime.Now;
            return found_site;
        }

    }
}