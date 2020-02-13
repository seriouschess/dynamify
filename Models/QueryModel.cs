using System.Linq;
using dynamify.Models.SiteModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace dynamify.Models.QueryModel
{
    public class QueryModel
    {
        private MyContext dbContext;
        
        public QueryModel(MyContext _context){
            dbContext = _context;
        }

        public List<Site> QuerySitesByAdmin(int admin_id){
            return dbContext.Sites.Where(x => x.admin_id == admin_id).ToList();
        }

        public List<Site> QueryFeaturelessSiteByTitle(string title){
            return dbContext.Sites.Where(x => x.title == title).ToList();
        }

        public List<Site> QueryFeaturelessSiteById(int active_site_id){
                return dbContext.Sites.Where(x => x.site_id == active_site_id).Select( site => new Site()
                {
                    site_id = site.site_id,
                    active = site.active,
                    title = site.title
                }).ToList();
        }

        public List<Site> QueryActiveSite(){
            List<Site> ActiveSites = dbContext.Sites.Where(x => x.active == true).Select( site => new Site()
           {
                site_id = site.site_id,
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
                }).Where(x => x.site_id == site.site_id).ToList(),
                images = dbContext.Images.Where(x => x.site_id == site.site_id).Select(i => new Image()
                {
                    image_id = i.image_id,
                    title = i.title,
                    priority = i.priority,
                    image_src = i.image_src,
                    site_id = i.site_id
                }).Where(x => x.site_id == site.site_id).ToList(),

                portraits = dbContext.Portraits.Where(x => x.site_id == site.site_id).Select(p => new Portrait()
                {
                    portrait_id = p.portrait_id,
                    title = p.title,
                    priority = p.priority,
                    image_src = p.image_src,
                    content = p.content,
                    site_id = p.site_id
                }).Where(x => x.site_id == site.site_id).ToList(),

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
                }).Where(x => x.site_id == site.site_id).ToList()
            }).ToList();


            return ActiveSites;
        }

        public Site QuerySiteById(int site_id_parameter){
            
            Site FoundSite =dbContext.Sites.Where(x => x.site_id == site_id_parameter).Select( site => new Site()
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
                }).Where(x => x.site_id == site_id_parameter).ToList()
            }).FirstOrDefault();

            return FoundSite;
        }


        //actions 
        public Site SetActiveSite(Site SiteToSetActive){
            List<Site> AllSites = dbContext.Sites.Where(x => x.active == true).Select( site => new Site()
            {
                    site_id = site.site_id,
                    active = site.active
                }).ToList();

            for(int x = 0; x < AllSites.Count; x++){ //clear all active sites
                    AllSites[x].active = false; 
            }

            SiteToSetActive.active = true; //set new active site
            dbContext.SaveChanges();
            return SiteToSetActive;
        }

        public Site AddSite(Site NewSite){
            dbContext.Add(NewSite);
            dbContext.SaveChanges();
            List<Site> test = QueryFeaturelessSiteByTitle(NewSite.title);
            if(test.Count == 1){
                return test[0]; //success
            }else{
                throw new System.ArgumentException("Query Error. All site titles must be unique.", "NewSite.title");
            }
            
        }
        

        public Site DeleteSiteById(int site_id){
            Site FoundSite = QueryFeaturelessSiteById(site_id)[0]; //kinda bad
            dbContext.Remove( FoundSite );
            dbContext.SaveChanges();
            return FoundSite;
        }

        public void AddParagraphBox(ParagraphBox p_box){
            dbContext.Add(p_box);
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

    }
}