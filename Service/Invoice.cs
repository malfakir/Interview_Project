using Interview_Project.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;


namespace Interview_Project.Service
{
    public class Invoice : Iinvoice
    {

        //TODO: Filter all calls per Company
        //TODO: Filter all calls per user using RegX
        //TODO: Apply Call Type
        //TODO: Add Call Duration per call type
        //TODO: Calculate Charges per user
        //TODO: Calculate Monthly charges
        //TODO: Calculate By Company
        public string GenerateCharges()
        {
            string JsonResults = "";

            using (var ctx = new InterviewContext())
            {

                //TODO: Filter all calls per Company
                var a_Companies = ctx.Companies.ToList();

                List<Company_Invoice> companyInvoice = new List<Company_Invoice>();
                if (a_Companies != null && a_Companies.Count > 0)
                {
                    foreach (var a_Company in a_Companies)
                    {
                        Company_Invoice company_ = new Company_Invoice();
                        List<User_Invoice> user_Invoices = new List<User_Invoice>();
                        var a_Plan = ctx.Plans.FirstOrDefault(x => x.ID == a_Company.PlanID);
                        if (a_Plan != null)
                        {
                            a_Company.Plan = a_Plan;
                            if (a_Company.Users == null)
                            {
                                var a_Users = ctx.Users.Where(x => x.CompanyID == a_Company.ID).ToList();
                                a_Company.Users = a_Users;
                            }
                            if (a_Company.Plan.Rates == null)
                            {
                                var a_Rates = ctx.Rates.Where(x => x.PlanID == a_Company.Plan.ID).ToList();
                                a_Company.Plan.Rates = a_Rates;
                            }
                            foreach (var a_User in a_Company.Users)
                            {
                                var Calls_Made = ctx.CDRs.Where(x => x.SourceNumber == a_User.PhoneNumber).ToList();

                                //Monthly Charge
                                user_Invoices.Add(new User_Invoice()
                                {
                                    UserName = a_User.Name,
                                    CallDate = Calls_Made.FirstOrDefault().ConnectDateTime.ToShortDateString(),
                                    Call_Duration_Minute = 0,
                                    RateType = "Monthly User Charge",
                                    CallCharge = a_Company.Plan.Rates.FirstOrDefault(x => x.Name == "Monthly User Charge").Rate
                                });

                                foreach (var Call in Calls_Made)
                                {

                                    //User Call Charges
                                    user_Invoices.Add(new User_Invoice()
                                    {
                                        UserName = a_User.Name,
                                        CallDate = Call.ConnectDateTime.ToString("dd/MM/yyyy:hh:mm:ss"),
                                        Call_Duration_Minute = Calltime(Call.Duration),
                                        RateType = MatchRegexCallType(Call.DestinationNumber),
                                        CallCharge = Calltime(Call.Duration) * MatchRegexFilter(Call.DestinationNumber, a_Company.Plan.ID),
                                    });
                                }

                            }

                            company_.CompanyName = a_Company.Name;
                            company_.PlanName = a_Company.Plan.Name;
                            company_.Company_Charge = user_Invoices.Select(x => x.CallCharge).ToList().Sum();
                            company_.Users = user_Invoices;
                        }

                        companyInvoice.Add(company_);
                    }
                }
                JsonResults = JsonConvert.SerializeObject(companyInvoice);

            }
            return JsonResults;

        }

        public string MatchRegexCallType(string phoneNumber)
        {
            string Filter = @"^(?:\*|611[38]|614|61|61[2378])\d{7}$";
            string CallType = "";
            Dictionary<string, string> callTypes = new Dictionary<string, string>()
            {
               {@"611[38]", "13/1800 Outbound"},
               {@"614", "Mobile Outbound"},
               {@"61[2378]", "National Outbound"},
                {@"*", "International"},
            };

            bool isMatch = Regex.IsMatch(phoneNumber, Filter);
            if (isMatch)
            {
                foreach (KeyValuePair<string, string> kvp in callTypes)
                {
                    if (Regex.IsMatch(phoneNumber, kvp.Key))
                    {
                        CallType = kvp.Value;
                        break;
                    }
                    else
                    {
                        CallType = "International";
                    }
                }
            }
            return CallType;
        }

        //public string MatchRegexCallType(string inputString)
        //{
        //    string[] Filters = { @"^.*\*$", @"^611[38].*$", @"^61.*$", @"^61[2378].*$", @"^614.*$" };
        //    string CallType = "";
        //    foreach (string filter in Filters)
        //    {
        //        if (Regex.IsMatch(inputString, filter))
        //        {
        //            switch (filter)
        //            {
        //                //*
        //                case @"^.*\*$":
        //                    CallType = "Monthly User Charge";
        //                    break;
        //                //611[38]
        //                case @"^611[38].*$":
        //                    CallType = "13/1800 Outbound";
        //                    break;
        //                //61
        //                case @"^61.*$":
        //                    CallType = "National Inbound";
        //                    break;
        //                //614
        //                case @"^614.*$":
        //                    CallType = "Mobile Outbound";
        //                    break;
        //                //61[2378]
        //                case @"^61[2378].*$":
        //                    CallType = "National Outbound";
        //                    break;
        //                default:
        //                    CallType = "";
        //                    break;

        //            }

        //        }
        //        else
        //        {
        //            CallType = "64 unknown Regx";
        //        }
        //    }

        //    return CallType;
        //}
        public decimal MatchRegexFilter(string phoneNumber, Guid PlanID)
        {
            string Filter = @"^(?:\*|611[38]|614|61|61[2378])\d{7}$";
            decimal Rate = 0;
            //TODO Get Plan ID & Get Rate

            Dictionary<string, decimal> callrate = new Dictionary<string, decimal>()
            {
               {@"611[38]", 0},
               {@"614", 0},
               {@"61[2378]", 0},
               {@"*", 0},
            };
            var ctx = new InterviewContext();
            var Rates = ctx.Rates.Where(x => x.PlanID == PlanID).ToList();
            bool isMatch = true; //Regex.IsMatch(phoneNumber, Filter);
            if (isMatch)
            {
                foreach (KeyValuePair<string, decimal> kvp in callrate)
                {
                    if (Regex.IsMatch(phoneNumber, kvp.Key))
                    {
                        Rate = Rates.FirstOrDefault(x => x.Filter == kvp.Key).Rate;
                        break;
                    }
                    else
                    {
                        Rate = 0;
                    }
                }
            }
            return Rate;
        }
        //public decimal MatchRegexFilter(string inputString)
        //{
        //    string[] Filters = { @"^.*\*$", @"^611[38].*$", @"^61.*$", @"^61[2378].*$", @"^614.*$" };

        //    // string[] Filters = { @"^\*$", @"^611[38]$", @"^61$", @"^61[2378]$", @"^614$", @"^611[38]$" };
        //    decimal Rate = 0;
        //    foreach (string filter in Filters)
        //    {
        //        if (Regex.IsMatch(inputString, filter))
        //        {
        //            switch (filter)
        //            {
        //                //*
        //                case @"^.*\*$":
        //                    Rate = 10;
        //                    break;
        //                //611[38]
        //                case @"^611[38].*$":
        //                    Rate = 1.5M;
        //                    break;
        //                //61
        //                case @"^61.*$":
        //                    Rate = 0;
        //                    break;
        //                //61[2378]
        //                case @"^61[2378].*$":
        //                    Rate = 0.13M;
        //                    break;
        //                //614
        //                case @"^614.*$":
        //                    Rate = 0.3M;
        //                    break;
        //                default: break;

        //            }

        //        }
        //    }
        //    return Rate;

        //}

        public int Calltime(int CallTime)
        {
            int minutes = CallTime / 60;  // Calculate the number of minutes
            int seconds = CallTime % 60;  // Calculate the number of remaining seconds

            if (seconds > 0)  // If there are remaining seconds, add an extra minute
            {
                minutes += 1;
            }
            return minutes;
        }



    }
}
