using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using CsvHelper;
using Sat.Recruitment.Infrastructure.Files.Mappers;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using CsvHelper.Configuration;

namespace Sat.Recruitment.Infrastructure.Persistence
{
    public class ApplicationDbContextCSV : IApplicationDbContext
    {
        public readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"DbCSVFiles\Users.txt"); //TODO fix magic string: Send to a config file.
        public readonly CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,            
        };

        
        public List<User> Users { get; } = new List<User>(); //TODO this must be a DbSet from Entity Framework Core



        public ApplicationDbContextCSV()
        {
            new ApplicationDbContextSeederCSV(this).SeedAsync().Wait();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                using var s = File.Open(filePath, FileMode.Create);
                using var sw = new StreamWriter(s);
                using var csvw = new CsvWriter(sw, csvConfig);

                csvw.Context.RegisterClassMap<UserRecordMapCSV>();
                await csvw.WriteRecordsAsync(Users, cancellationToken);
                return Users.Count;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: {0}{1}  {2}", e.Message, Environment.NewLine, e.ToString());
                return -1;
            }
        }

        public void PoPaQueVea()
        {
            throw new Exception(filePath);
        }
    }
}
