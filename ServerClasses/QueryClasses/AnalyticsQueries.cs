using System.Collections.Generic;
using System.Linq;
using dynamify.Models;
using dynamify.Models.AnalyticsModels;

namespace dynamify.ServerClasses.QueryClasses
{
    public class AnalyticsQueries
    {
        private DatabaseContext dbContext;

        public AnalyticsQueries(DatabaseContext _dbContext){
            dbContext = _dbContext;
        }

        public void addSession(ViewSession NewSession){
            dbContext.Add(NewSession);
            dbContext.SaveChanges();
        }

        public void updateSession(ViewSession _CurrentSession){
            ViewSession CurrentSession = dbContext.ViewSessions.Where( x => x.session_id == _CurrentSession.session_id).FirstOrDefault();
            CurrentSession.time_on_homepage = _CurrentSession.time_on_homepage;
            dbContext.SaveChanges();
        }

        public List<ViewSession> getAllSessions(){
            List<ViewSession> output = dbContext.ViewSessions.OrderBy(x => x.created_at).ToList();
            return output;
        }
    }
}