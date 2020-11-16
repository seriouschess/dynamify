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
    }
}