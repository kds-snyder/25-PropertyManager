using AutoMapper;
using PropertyManager.Core.Constant;
using PropertyManager.Core.Domain;
using PropertyManager.Core.Infrastructure;
using PropertyManager.Core.Models;
using PropertyManager.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PropertyManager.Controllers
{
    [Authorize]
    public class DashboardController : ApiController
    {
        private PropertyManagerDbContext db = new PropertyManagerDbContext();

        // GET: api/Dashboard
        [ResponseType(typeof(DashboardModel))]
        public DashboardModel GetDashboard()
        {
            var sixMonthsFromNow = DateTime.Now.AddMonths(6);

            return new DashboardModel
            {
                LeaseCount = db.Leases.Count(),
                PropertyCount = db.Properties.Count(),
                TenantCount = db.Tenants.Count(),
                ExpiringLeases = Mapper.Map<IEnumerable<LeaseModel>>
                                (db.Leases.Where(
                                                l => l.EndDate.HasValue &&
                                                   l.EndDate >= DateTime.Now &&
                                                   l.EndDate <= sixMonthsFromNow
                                                )
                                            .OrderBy(l => l.EndDate)
                                            .Take(3))
            };

        }

        // Calculate income from rents this month
<<<<<<< HEAD
        private decimal monthlyRentIncome()
=======
        private decimal monthlyIncome()
>>>>>>> d4b5d93b63c3a373073a5be8ed333adec11741ad
        {
            decimal sum = 0;
            int daysInThisMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            // For leases that are current, calculate the income according to the lease type, 
            //   and add income to sum:
            //  Daily: income = #days in lease this month * rent
            //  Weekly: income = (#days in lease this month)/7 * rent
            //  Monthly: income =  (#days in lease this month)/(#days in this month) * rent
            //  Yearly: If this month is completely included between start date and end date,
            //           then income = rent/12
            //          Otherwise income = (#days in lease this month)/(#days in this month) * rent / 12
            foreach (var lease in db.Leases)
            {
                if (lease.StartDate <= DateTime.Now &&
                    ((lease.EndDate.HasValue && lease.EndDate >= DateTime.Now) ||
                    (!lease.EndDate.HasValue)))
                {
                    switch (lease.LeaseType)
                    {
                        case Constants.RentPeriod.Daily:
                            sum += daysInLeaseThisMonth(lease) * lease.Rent; 
                            break;

                        case Constants.RentPeriod.Weekly:
                            sum += (daysInLeaseThisMonth(lease) / 7) * lease.Rent;
                            break;

                        case Constants.RentPeriod.Monthly:                           
                            sum += (daysInLeaseThisMonth(lease)/daysInThisMonth) * lease.Rent;
                            break;

                        case Constants.RentPeriod.Yearly:
                            if (lease.StartDate >= DateTimeExtensions.FirstDayOfMonth(DateTime.Now) &&
                                ((lease.EndDate.HasValue && lease.EndDate >= 
                                    DateTimeExtensions.LastDayOfMonth(DateTime.Now)) ||
                                (!lease.EndDate.HasValue)))
                            {
                                sum += lease.Rent / 12;
                            }
                            else
                            {
                                sum += ((daysInLeaseThisMonth(lease) / daysInThisMonth) * lease.Rent)/12;
                            }
                                break;

                        default:
                            break;

                    }
                }                
            }

            return sum;
        }

        // Calculate the number of days in current month that are included between
        //   the input lease start date and end date
        private decimal daysInLeaseThisMonth(Lease lease)
        {
            DateTime today = DateTime.Now;

            // Lower day is the maximum of the first day of this month and the lease start date
            DateTime lowerDay = DateTimeExtensions.MaxDate
                                (DateTimeExtensions.FirstDayOfMonth(today), lease.StartDate);

            // If lease end date is not null, upper day is the minimum of the last day of this month 
            //   and the lease end date
            // If lease end date is null, upper day is the last day of this month
            DateTime upperDay;
            if (lease.EndDate.HasValue)
            {
                DateTime endDate = lease.EndDate.GetValueOrDefault();
                upperDay = DateTimeExtensions.MinDate
                                        (DateTimeExtensions.LastDayOfMonth(today), endDate);
            }
            else
            {
                upperDay = DateTimeExtensions.LastDayOfMonth(today);
            }

            // Number of days in lease this month = upper day - lower day + 1
            return (upperDay - lowerDay).Days + 1;
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
