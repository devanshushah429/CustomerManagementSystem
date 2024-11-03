using Excercise02.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.DAL
{
    public class DAL_Helper : IDisposable
    {
        protected readonly AppDbContext _context;

        private static DbContextOptions<AppDbContext>? _options;

        public static void SetDbContextOptions(DbContextOptions<AppDbContext> options)
        {
            _options = options;
        }

        public DAL_Helper()
        {
            if (_options == null)
            {
                throw new InvalidOperationException("DbContext options not set. Call SetDbContextOptions first.");
            }
            _context = new AppDbContext(_options);
        }

        #region Save changes to the context
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        #endregion

        #region Save changes to the context async
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Dispose the context
        public void Dispose()
        {
            _context.Dispose();
        }
        #endregion
    }

}
