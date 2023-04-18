﻿using TheBugTracker.Models;

namespace TheBugTracker.Services.Interfaces
{
    public interface IBTTicketService
    {
        //CRUD methods
        //CREATE
        public Task AddNewTicketAsync(Ticket ticket);
        //UPDATE
        public Task UpdateTicketAsync(Ticket ticket);
        //READ
        public Task<Ticket> GetTicketByIdAsync(int ticketId);
        //ARCHIVE
        public Task ArchiveTicketAsync(Ticket ticket);

        public Task AssignTicketAsync(int ticketId, string userId);
        public Task<List<Ticket>> GetArchivedTicketsAsync(int companyId);
        public Task<List<Ticket>> GetAllTicketsByCompanyAsync(int companyId);
        public Task<List<Ticket>> GetAllTicketsByPriorityAsync(int companyId, string priorityName);
        public Task<List<Ticket>> GetAllTicketsByStatusAsync(int companyId, string statusName);
        public Task<List<Ticket>> GetAllTicketsByTypeAsync(int companyId, string typeName);
        public Task<BTUser> GetTicketDeveloperAsync(int ticketId);
        public Task<List<Ticket>> GetTicketsByRoleAsync(string role, string userId, int companyId);
        public Task<List<Ticket>> GetTicketsByUserIdAsync(string userId, int companyId);
        public Task<List<Ticket>> GetProjectTicketsByRoleAsync(string role, string userId, int projectId, int companyId);
        public Task<List<Ticket>> GetProjectTicketsByStatusAsync(string statusName, int companyId, int projectId);
        public Task<List<Ticket>> GetProjectTicketsByPriorityAsync(string priorityName, int companyId, int projectId);
        public Task<List<Ticket>> GetProjectTicketsByTypeAsync(string typeName, int companyId, int projectId);


        public Task<int?> LookupTicketPriorityIdAsync(string priorityName);
        public Task<int?> LookupTicketStatusIdAsync(string statusName);
        public Task<int?> LookupTicketTypeIdAsync(string typeName);


    }
}