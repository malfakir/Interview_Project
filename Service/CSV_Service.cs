using CsvHelper;
using CsvHelper.Configuration;
using Models;
using System.Globalization;

namespace Interview_Project.Service
{
    public class CSV_Service
    {
        public void Upload_Users(IFormFile file)
        {
            // Code to process the uploaded file
            if (file != null && file.Length > 0)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    { HeaderValidated = null, MissingFieldFound = null });

                    var records = csv.GetRecords<Users>().ToList();

                    if (records.Count > 0)
                    {
                        using (var ctx = new InterviewContext())
                        {
                            var _TempRate = ctx.Users.ToList();
                            foreach (var record in records)
                            {

                                if (_TempRate != null
                                    && _TempRate.Any(x => x.ID != record.ID))
                                {
                                    _TempRate.Add(new Users
                                    {
                                        ID = record.ID,
                                        Name = record.Name,
                                        EmailAddress = record.EmailAddress,
                                        CompanyID = record.CompanyID,
                                        PhoneNumber = record.PhoneNumber,
                                    });

                                    ctx.SaveChanges();
                                }
                                else
                                {
                                    ctx.Add(new Users
                                    {
                                        ID = record.ID,
                                        Name = record.Name,
                                        EmailAddress = record.EmailAddress,
                                        CompanyID = record.CompanyID,
                                        PhoneNumber = record.PhoneNumber,
                                    });

                                    ctx.SaveChanges();
                                }

                            }
                        }
                    }
                }
            }
        }
        public void Upload_Companies(IFormFile file)
        {
            // Code to process the uploaded file
            if (file != null && file.Length > 0)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    { HeaderValidated = null, MissingFieldFound = null });

                    var records = csv.GetRecords<Companies>().ToList();

                    if (records.Count > 0)
                    {
                        using (var ctx = new InterviewContext())
                        {
                            var _TempRate = ctx.Companies.ToList();
                            foreach (var record in records)
                            {

                                if (_TempRate != null
                                    && _TempRate.Any(x => x.ID != record.ID))
                                {
                                    _TempRate.Add(new Companies
                                    {
                                        ID = record.ID,
                                        Name = record.Name,
                                        PlanID = record.PlanID,
                                    });

                                    ctx.SaveChanges();
                                }
                                else
                                {
                                    ctx.Add(new Companies
                                    {
                                        ID = record.ID,
                                        Name = record.Name,
                                        PlanID = record.PlanID,
                                    });

                                    ctx.SaveChanges();
                                }

                            }
                        }
                    }
                }
            }
        }
        public void Read_Plans(IFormFile file)
        {
            // Code to process the uploaded file
            if (file != null && file.Length > 0)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    { HeaderValidated = null, MissingFieldFound = null });

                    var records = csv.GetRecords<Plan>().ToList();

                    if (records.Count > 0)
                    {
                        using (var ctx = new InterviewContext())
                        {
                            var _TempRate = ctx.Plans.ToList();
                            foreach (var record in records)
                            {

                                if (_TempRate != null
                                    && _TempRate.Any(x => x.ID != record.ID))
                                {
                                    _TempRate.Add(new Plan
                                    {
                                        ID = record.ID,
                                        Name = record.Name,
                                    });

                                    ctx.SaveChanges();
                                }
                                else
                                {
                                    ctx.Add(new Plan
                                    {
                                        ID = record.ID,
                                        Name = record.Name,
                                    });

                                    ctx.SaveChanges();
                                }

                            }
                        }
                    }
                }
            }
        }
        public void Read_Rates(IFormFile file)
        {
            // Code to process the uploaded file
            if (file != null && file.Length > 0)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    using var csv = new CsvReader(reader,new CsvConfiguration(CultureInfo.InvariantCulture)
                    { HeaderValidated = null , MissingFieldFound = null });
                   
                    var records = csv.GetRecords<Rates>().ToList();

                    if (records.Count > 0)
                    {
                        using (var ctx = new InterviewContext())
                        {
                            var _TempRate = ctx.Rates.ToList();
                            foreach (var record in records)
                            {

                                if (_TempRate != null
                                    && _TempRate.Any(x => x.ID != record.ID))
                                {
                                    _TempRate.Add(new Rates
                                    {
                                        ID= record.ID,
                                        Name = record.Name,
                                        PlanID = record.PlanID,
                                        RateType = record.RateType,
                                        Priority = record.Priority,
                                        Filter = record.Filter,
                                        Rate = record.Rate
                                    });

                                    ctx.SaveChanges();
                                }
                                else
                                {
                                    ctx.Add(new Rates
                                    {
                                        ID = record.ID,
                                        Name = record.Name,
                                        PlanID = record.PlanID,
                                        RateType = record.RateType,
                                        Priority = record.Priority,
                                        Filter = record.Filter,
                                        Rate = record.Rate
                                    });

                                    ctx.SaveChanges();
                                }

                            }
                        }
                    }
                }
            }
        }
        public string Read_CDR(IFormFile file)
        {
            string Json_results = "";
            if (file != null && file.Length > 0)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                    var records = csv.GetRecords<CDR>().ToList();

                   if(records.Count > 0) 
                    {
                        using (var ctx = new InterviewContext())
                        {
                            var _Tempcdr = ctx.CDRs.ToList();
                            foreach (var record in records)
                            {
                                
                                if(_Tempcdr !=null  
                                    && _Tempcdr.Any(x => x.CarrierReference != record.CarrierReference))
                                {
                                    _Tempcdr.Add(new CDR
                                    {
                                        CarrierReference = record.CarrierReference,
                                        ConnectDateTime = record.ConnectDateTime,
                                        Duration = record.Duration,
                                        SourceNumber = record.SourceNumber,
                                        Direction = record.Direction,
                                        DestinationNumber = record.DestinationNumber
                                    });

                                    ctx.SaveChanges();
                                }
                                else
                                {
                                    ctx.Add(new CDR
                                    {
                                        CarrierReference = record.CarrierReference,
                                        ConnectDateTime = record.ConnectDateTime,
                                        Duration = record.Duration,
                                        SourceNumber = record.SourceNumber,
                                        Direction = record.Direction,
                                        DestinationNumber = record.DestinationNumber
                                    });

                                    ctx.SaveChanges();
                                }
                                
                            }
                            Invoice invoice = new Invoice();
                            Json_results = invoice.GenerateCharges();
                        }
                    }
                }
            }
            return Json_results;
        }
    }
}
