using System.Collections.Generic;
using dynamify.dtos;
using dynamify.Models.DataPlans;
using dynamify.Models.JsonModels;
using dynamify.ServerClasses.Email;
using dynamify.ServerClasses.QueryClasses;

namespace dynamify.Controllers.ControllerMethods
{
    public class ReportsControllerMethods
    {
        private SiteQueries _siteQueries;
        private AdminQueries _adminQueries;
        private Mailer _mailer;
        public ReportsControllerMethods(Mailer mailer, SiteQueries siteQueries, AdminQueries adminQueries)
        {
            _mailer = mailer;
            _siteQueries = siteQueries;
            _adminQueries = adminQueries;
        }

        public JsonResponse SendSummaryReportMethod(){
            SummaryReportDto summary_data = new SummaryReportDto();
            summary_data.total_admins = _adminQueries.GetAdminCount();
            summary_data.total_sites = _siteQueries.GetSiteCount();
            summary_data.total_new_admins_this_month = _adminQueries.GetNewAdminsThisMonth();
            summary_data.total_new_sites_this_month = _siteQueries.GetNewSitesThisMonth();
            summary_data.total_storage_megabytes = _adminQueries.GetTotalMegabytesFromAllDataPlans();
            _mailer.SendSummaryReportEmail(summary_data);
            return new JsonSuccess("Summary report sent.");
        }
    }
}