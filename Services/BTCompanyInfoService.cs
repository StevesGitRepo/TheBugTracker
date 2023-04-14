using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using TheBugTracker.Data;
using TheBugTracker.Models;
using TheBugTracker.Services.Interfaces;

namespace TheBugTracker.Services
{
    public class BTCompanyInfoService : IBTCompanyInfoService
    {
        //Dependency injection
        private readonly ApplicationDbContext _context;

        public BTCompanyInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BTUser>> GetAllMembersAsync(int companyId)
        {
            List<BTUser> result = new List<BTUser>();
            result = await _context.Users.Where(u=> u.CompanyId == companyId).ToListAsync();
            return result;
        }

        public async Task<List<Project>> GetAllProjectsAsync(int companyId)
        {
            List<Project> result = new List<Project>();

            result = await _context.Projects.Where(p => p.CompanyId == companyId)
                                            .Include(p => p.Members)
                                            .Include(p => p.Tickets)
                                            .Include(p => p.ProjectPriority)
                                            .ToListAsync();

            return result;
        }

        public async Task<List<Ticket>> GetAllTicketsAsync(int ticketId)
        {
            List<Ticket> result = new List<Ticket>();
            result = await _context.Tickets.Where(u=> u.ProjectId == ticketId).ToListAsync();
            return result;
        }

        public async Task<Company> GetCompanyInfoByIdAsync(int? companyId)
        {
            List<Company> result = new List<Company>();
            result = await _context.Companies.Where(u=> u.Id == companyId).ToListAsync();
            return result;
        }
    }
}
