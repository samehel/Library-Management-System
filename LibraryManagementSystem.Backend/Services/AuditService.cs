using LibraryManagementSystem.Backend.Contexts;
using LibraryManagementSystem.Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagementSystem.Backend.Services
{
    public class AuditService : IAuditService
    {
        private readonly LibraryContext _context;

        public AuditService(LibraryContext context)
        {
            this._context = context;
        }

        public async Task<Audit> CreateAuditAsync(Audit audit)
        {
            this._context.Add(audit);
            await this._context.SaveChangesAsync();
            return audit;
        }

        public async Task<bool> DeleteAuditAsync(int auditID)
        {
            Audit? audit = await this._context.Audits.FindAsync(auditID);

            if (audit != null)
            {
                
                audit.isDeleted = true;
                await this._context.SaveChangesAsync();
                return true;
            }

            return true;
        }

        public async Task<List<Audit>> GetAllAuditsAsync()
        {
            List<Audit> audits = await this._context.Audits.ToListAsync();

            return audits.Where(audit => !audit.isDeleted).ToList(); 
        }

        public async Task<List<Audit>> GetAllDeletedAuditsAsync()
        {
            List<Audit> audits = await this._context.Audits.ToListAsync();

            return audits.Where(audit => audit.isDeleted).ToList();
        }

        public async Task<Audit?> GetAuditAsync(int auditID)
        {
            return await this._context.Audits.FindAsync(auditID);
        }
    }
}
