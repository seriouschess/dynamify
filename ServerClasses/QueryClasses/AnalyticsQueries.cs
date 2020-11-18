using System;
using System.Collections.Generic;
using System.Linq;
using dynamify.Models;
using dynamify.Models.AnalyticsModels;
using dynamify.Models.SiteModels;

namespace dynamify.ServerClasses.QueryClasses
{
    public class AnalyticsQueries
    {
        private DatabaseContext dbContext;

        public AnalyticsQueries(DatabaseContext _dbContext){
            dbContext = _dbContext;
        }

        public ViewSession addSession(ViewSession NewSession){
            dbContext.Add(NewSession);
            dbContext.SaveChanges();
            return querySessionById(NewSession.session_id);
        }

        public void updateSession(ViewSession CurrentSession){
            ViewSession QueriedSession = querySessionById(CurrentSession.session_id);
            QueriedSession.time_on_homepage = CurrentSession.time_on_homepage;
            System.Console.WriteLine($"Queried session time on homepage {QueriedSession.time_on_homepage}");
            dbContext.SaveChanges();
        }

        public ViewSession querySessionById( int session_id ){
            List<ViewSession> FoundSessions = dbContext.ViewSessions.Where(x => x.session_id == session_id).ToList();
            if(FoundSessions.Count == 1){
                return FoundSessions[0];
            }else{
                throw new System.ArgumentException($"Unable to find view session for {session_id}");
            }
        }

        public int querySiteIdForUrl( string site_url ){
            List<Site> FoundSites = dbContext.Sites.Where(x => x.url == site_url).ToList();
            if(FoundSites.Count == 1){
                return FoundSites[0].site_id;
            }else{
                throw new System.ArgumentException($"Site url -{site_url}- not found");
            }
        }

        public List<ViewSession> querySessionsBySiteUrl( string site_url ){
            List<ViewSession> FoundSessions = dbContext.ViewSessions.Where(x => x.url == site_url).ToList();
            return FoundSessions;
        }

        public List<ViewSession> querySessionsBySiteId( int site_id ){
            List<ViewSession> FoundSessions = dbContext.ViewSessions.Where(x => x.site_id == site_id).ToList();
            return FoundSessions;
        }


        //Analytics Data
        public int returnViewCountForSiteId(int site_id){
            int count = dbContext.ViewSessions.Where(x => x.site_id == site_id).Count();
            return count;
        }

        public int returnViewCountForSiteIdThisMonth(int site_id){
            int month_count = dbContext.ViewSessions.Where(x => x.site_id == site_id).Where(y => y.created_at.Month == DateTime.Now.Month).Count();
            return month_count;
        }

        public int returnTotalViewHoursForSite(int site_id){
            int total_view_hours = 0;
            List<ViewSession> FoundSessions = dbContext.ViewSessions.Where(x => x.site_id == site_id).ToList();
            foreach(ViewSession s in FoundSessions){
                total_view_hours = s.time_on_homepage;
            }
            return total_view_hours;
        } 
    }
}