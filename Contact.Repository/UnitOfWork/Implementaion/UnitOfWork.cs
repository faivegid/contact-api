using Contact.Data;
using Contact.Repository.UnitOfWork.Interface;
using System;

namespace Contact.Repository.UnitOfWork.Implementaion
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
