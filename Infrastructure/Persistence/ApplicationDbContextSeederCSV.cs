using CsvHelper;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Application.Users.Commands.CreateUser;
using Sat.Recruitment.Infrastructure.Files.Mappers;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.Persistence
{
    public class ApplicationDbContextSeederCSV
    {
        private readonly ApplicationDbContextCSV _context;

        public ApplicationDbContextSeederCSV(IApplicationDbContext context)
        {
            _context = (ApplicationDbContextCSV) context;
        }



        public async Task InitialiseAsync()
        {
            try
            {
                //TODO using ApiAuthorizationDbContext with actual SQL
                //if (_context.Database.IsSqlServer()) { await _context.Database.MigrateAsync(); }
                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: {0}{1}  {2}", e.Message, Environment.NewLine, e.ToString());
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: {0}{1}  {2}", e.Message, Environment.NewLine, e.ToString());
            }
        }

        public async Task TrySeedAsync()
        {
            try
            {
                if (_context.Users.Count > 0) return;

                using (var sr = new StreamReader(_context.filePath))
                {
                    using var csvr = new CsvReader(sr, _context.csvConfig);
                    csvr.Context.RegisterClassMap<UserRecordMapCSV>();
                    var users = csvr.GetRecords<CreateUserCommand>().ToList();
                    _context.Users.AddRange(users);
                }

                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: {0}{1}  {2}", e.Message, Environment.NewLine, e.ToString());
            }
        }
    }
}
