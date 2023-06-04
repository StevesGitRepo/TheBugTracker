﻿using Microsoft.EntityFrameworkCore;
using TheBugTracker.Data;
using TheBugTracker.Models;
using TheBugTracker.Models.Enums;
using TheBugTracker.Services.Interfaces;

namespace TheBugTracker.Services
{
    public class BTTicketService : IBTTicketService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBTTicketService _ticketService;
        private readonly IBTRolesService _roleService;
        private readonly IBTProjectService _projectService;

        public BTTicketService(ApplicationDbContext context,
                                IBTTicketService ticketService,
                                IBTRolesService roleService,
                                IBTProjectService projectService)
        {
            _context = context;
            _ticketService = ticketService;
            _roleService = roleService;
            _projectService = projectService;
        }

        public async Task AddNewTicketAsync(Ticket ticket)
        {
            try
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddTicketAttachmentAsync(TicketAttachment ticketAttachment)
        {
            try
            {
                await _context.AddAsync(ticketAttachment);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddTicketCommentAsync(TicketComment ticketComment)
        {
            try
            {
                await _context.AddAsync(ticketComment);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //ARCHIVE (Update)
        public async Task ArchiveTicketAsync(Ticket ticket)
        {
            try
            {
                ticket.Archived = true;
                _context.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AssignTicketAsync(int ticketId, string userId)
        {
            Ticket ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);

            try
            {
                ticket.DeveloperUserId = userId;
                //Revisit this code when assigning the Tickets
                ticket.TicketStatusId = (await LookupTicketStatusIdAsync("Development")).Value;
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<List<Ticket>> GetAllTicketsByCompanyAsync(int companyId)
        {
            try
            {
                List<Ticket> tickets = await _context.Projects
                                                     .Where(p => p.CompanyId == companyId)
                                                     .SelectMany(p => p.Tickets)
                                                         .Include(t => t.Attachments)
                                                         .Include(t => t.Comments)
                                                         .Include(t => t.DeveloperUser)
                                                         .Include(t => t.History)
                                                         .Include(t => t.OwnerUser)
                                                         .Include(t => t.TicketPriority)
                                                         .Include(t => t.TicketStatus)
                                                         .Include(t => t.TicketType)
                                                         .Include(t => t.Project)
                                                      .ToListAsync();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Ticket>> GetAllTicketsByPriorityAsync(int companyId, string priorityName)
        {
            int priorityId = (await LookupTicketPriorityIdAsync(priorityName)).Value;

            try
            {
                List<Ticket> tickets = await _context.Projects
                                                     .Where(p => p.CompanyId == companyId)
                                                     .SelectMany(p => p.Tickets)
                                                         .Include(t => t.Attachments)
                                                         .Include(t => t.Comments)
                                                         .Include(t => t.DeveloperUser)
                                                         .Include(t => t.OwnerUser)
                                                         .Include(t => t.Project)
                                                         .Include(t => t.TicketPriority)
                                                         .Include(t => t.TicketStatus)
                                                         .Include(t => t.TicketType)
                                                      .Where(t => t.TicketPriorityId == priorityId)
                                                      .ToListAsync();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<Ticket>> GetAllTicketsByStatusAsync(int companyId, string statusName)
        {
            int statusId = (await LookupTicketStatusIdAsync(statusName)).Value;

            try
            {
                List<Ticket> tickets = await _context.Projects
                                                     .Where(p => p.CompanyId != companyId)
                                                     .SelectMany(p => p.Tickets)
                                                         .Include(t => t.Attachments)
                                                         .Include(t => t.Comments)
                                                         .Include(t => t.DeveloperUser)
                                                         .Include(t => t.OwnerUser)
                                                         .Include(t => t.TicketPriority)
                                                         .Include(t => t.TicketStatus)
                                                         .Include(t => t.TicketType)
                                                         .Include(t => t.Project)
                                                     .Where(t => t.TicketStatusId == statusId)
                                                     .ToListAsync();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Ticket>> GetAllTicketsByTypeAsync(int companyId, string typeName)
        {
            int typeId = (await LookupTicketTypeIdAsync(typeName)).Value;

            try
            {
                List<Ticket> tickets = await _context.Projects
                                                     .Where(p => p.CompanyId == companyId)
                                                     .SelectMany(p => p.Tickets)
                                                         .Include(t => t.Attachments)
                                                         .Include(t => t.Comments)
                                                         .Include(t => t.DeveloperUser)
                                                         .Include(t => t.OwnerUser)
                                                         .Include(t => t.TicketPriority)
                                                         .Include(t => t.TicketStatus)
                                                         .Include(t => t.TicketType)
                                                         .Include(t => t.Project)
                                                     .Where(t => t.TicketTypeId == typeId)
                                                     .ToListAsync();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Ticket>> GetArchivedTicketsAsync(int companyId)
        {
            try
            {
                List<Ticket> tickets = (await GetAllTicketsByCompanyAsync(companyId)).Where(t => t.Archived == true).ToList();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Ticket>> GetProjectTicketsByPriorityAsync(string priorityName, int companyId, int projectId)
        {
            List<Ticket> tickets = new();

            try
            {
                tickets = (await GetAllTicketsByPriorityAsync(companyId, priorityName)).Where(t => t.ProjectId == projectId).ToList();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<Ticket>> GetProjectTicketsByRoleAsync(string role, string userId, int projectId, int companyId)
        {
            List<Ticket> tickets = new();

            try
            {
                tickets = (await GetTicketsByRoleAsync(role, userId, companyId)).Where(t => t.ProjectId == projectId).ToList();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Ticket>> GetProjectTicketsByStatusAsync(string statusName, int companyId, int projectId)
        {
            List<Ticket> tickets = new();

            try
            {
                tickets = (await GetAllTicketsByStatusAsync(companyId, statusName)).Where(t => t.ProjectId == projectId).ToList();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Ticket>> GetProjectTicketsByTypeAsync(string typeName, int companyId, int projectId)
        {
            List<Ticket> tickets = new();

            tickets = (await GetAllTicketsByTypeAsync(companyId, typeName)).Where(t => t.ProjectId == projectId).ToList();
            return tickets;
        }

        public async Task<TicketAttachment> GetTicketAttachmentByIdAsync(int ticketAttachmentId)
        {
            try
            {
                TicketAttachment ticketAttachment = await _context.TicketAttachments
                                                                  .Include(t => t.User)
                                                                  .FirstOrDefaultAsync(t => t.Id == ticketAttachmentId);
                return ticketAttachment;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            try
            {
                return await _context.Tickets
                                     .Include(t => t.DeveloperUser)
                                     .Include(t => t.OwnerUser)
                                     .Include(t => t.Project)
                                     .Include(t => t.TicketPriority)
                                     .Include(t => t.TicketStatus)
                                     .Include(t => t.TicketType)
                                     .Include(t => t.Comments)
                                     .Include(t => t.Attachments)
                                     .Include(t => t.History)
                                     .FirstOrDefaultAsync(t => t.Id == ticketId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<BTUser> GetTicketDeveloperAsync(int ticketId, int companyId)
        {
            BTUser developer = new();

            try
            {
                Ticket ticket = (await GetAllTicketsByCompanyAsync(companyId)).FirstOrDefault(t => t.Id == ticketId);

                if (ticket != null)
                {
                    developer = ticket.DeveloperUser;
                }

                return developer;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Ticket>> GetTicketsByRoleAsync(string role, string userId, int companyId)
        {
            List<Ticket> tickets = new List<Ticket>();

            try
            {
                if (role == Roles.Admin.ToString())
                {
                    tickets = await GetAllTicketsByCompanyAsync(companyId);

                }
                else if (role == Roles.Developer.ToString())
                {
                    tickets = (await GetAllTicketsByCompanyAsync(companyId)).Where(t => t.DeveloperUserId == userId).ToList();
                }
                /*                else if(role == Roles.DemoUser.ToString())
                                {

                                }*/
                else if (role == Roles.Submitter.ToString())
                {
                    tickets = (await GetAllTicketsByCompanyAsync(companyId)).Where(t => t.OwnerUserId == userId).ToList();
                }
                else if (role == Roles.ProjectManager.ToString())
                {
                    tickets = await GetTicketsByUserIdAsync(userId, companyId);
                }

                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Ticket>> GetTicketsByUserIdAsync(string userId, int companyId)
        {

            BTUser btUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            List<Ticket> tickets = new List<Ticket>();
            try
            {
                if (await _roleService.IsUserInRoleAsync(btUser, Roles.Admin.ToString()))
                {
                    tickets = (await _projectService.GetAllProjectsByCompanyAsync(companyId))
                                                    .SelectMany(p => p.Tickets).ToList();

                }
                else if (await _roleService.IsUserInRoleAsync(btUser, Roles.Developer.ToString()))
                {
                    tickets = (await _projectService.GetAllProjectsByCompanyAsync(companyId))
                                                    .SelectMany(p => p.Tickets).Where(t => t.DeveloperUserId == userId).ToList();

                }
                else if (await _roleService.IsUserInRoleAsync(btUser, Roles.Submitter.ToString()))
                {
                    tickets = (await _projectService.GetAllProjectsByCompanyAsync(companyId))
                                                    .SelectMany(p => p.Tickets).Where(t => t.OwnerUserId == userId).ToList();

                }
                else if (await _roleService.IsUserInRoleAsync(btUser, Roles.ProjectManager.ToString()))
                {
                    tickets = (await _projectService.GetUserProjectsAsync(userId)).SelectMany(t => t.Tickets).ToList();

                }
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Ticket>> GetUnassignedTicketsAsync(int companyId)
        {
            List<Ticket> tickets = new();
            try
            {
                tickets = (await GetAllTicketsByCompanyAsync(companyId)).Where(t => string.IsNullOrEmpty(t.DeveloperUserId)).ToList();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }


        //HELPER METHODS
        public async Task<int?> LookupTicketPriorityIdAsync(string priorityName)
        {
            try
            {
                TicketPriority priority = await _context.TicketPriorities.FirstOrDefaultAsync(t => t.Name == priorityName);
                return priority?.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int?> LookupTicketStatusIdAsync(string statusName)
        {
            try
            {
                TicketStatus status = await _context.TicketStatuses.FirstOrDefaultAsync(t => t.Name == statusName);
                return status?.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int?> LookupTicketTypeIdAsync(string typeName)
        {
            try
            {
                TicketType type = await _context.TicketTypes.FirstOrDefaultAsync(t => t.Name == typeName);
                return type?.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            try
            {
                _context.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
