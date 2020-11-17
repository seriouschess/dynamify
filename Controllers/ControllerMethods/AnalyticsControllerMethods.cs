using System;
using dynamify.Models.AnalyticsModels;
using dynamify.Models.JsonModels;
using dynamify.ServerClasses.Analytics;
using dynamify.ServerClasses.Auth;
using dynamify.ServerClasses.QueryClasses;
using Microsoft.AspNetCore.Mvc;

namespace dynamify.Controllers.ControllerMethods
{
    public class AnalyticsControllerMethods: ControllerBase
    {
        private AnalyticsQueries _dbQuery;
        private TokenGenerator _gen;

        private Formatter _urlFormatter;
        public AnalyticsControllerMethods(AnalyticsQueries dbQuery){
            _dbQuery = dbQuery;
            _gen = new TokenGenerator();
            _urlFormatter = new Formatter();
        }

        public ActionResult<ViewSession> genUserSessionMethod(ViewSession NewSession){
            
            bool verdict = false;
            try{ //load object
                verdict = (NewSession.token == "duaiosfbol");
                NewSession.session_id = 0;
                NewSession.url = _urlFormatter.StripDomainFromUrl(NewSession.url);
                NewSession.token = _gen.GenerateToken();
                NewSession.site_id = _dbQuery.querySiteIdForUrl(NewSession.url);
            }catch{ //invalid object
                JsonFailure f = new JsonFailure($"Unable to parse object. See documentation.");
                return StatusCode(400, f);
            }
            
            if(verdict == true){                
                return _dbQuery.addSession(NewSession);
            }else{                //authentication fail
                JsonFailure f = new JsonFailure($"Unauthorised use. See documentation.");
                return StatusCode(400, f);
            } 
        }

        public ActionResult<JsonResponse> UpdateSessionMethod( ViewSession CurrentSession ){
            ViewSession QueriedSession;
            System.Console.WriteLine($"Current session ID: {CurrentSession.session_id}");
            try{
                QueriedSession = _dbQuery.querySessionById(CurrentSession.session_id);
            }catch(ArgumentException e){
                JsonFailure f = new JsonFailure(e.Message);
                return StatusCode(400, f);
            }
             
            if(QueriedSession.token == CurrentSession.token){
                _dbQuery.updateSession( CurrentSession );
                return new JsonSuccess("Session Updated.");
            }else{  //auth fail
                JsonFailure f = new JsonFailure($"Invalid token for session ID: {CurrentSession.session_id}.");
                return StatusCode(400,f);
            }

        }


    }
}