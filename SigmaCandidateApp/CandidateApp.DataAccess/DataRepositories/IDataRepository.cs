using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.DataAccess.DataRepositories
{
    public interface IDataRepository<T> where T : class
    {
        Task CreateAsync(T entity, CancellationToken cancellationToken);
        Task<T> ReadAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken);
    }
}
