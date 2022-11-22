using Application.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IQuestionRepository
    {
        IQueryable<Question> Questions { get; }

        Task<List<Question>> GetListAsync();

        Task<QuestionInfo> GetByIdAsync(long? questionId);

        Task<long> InsertAsync(Question question);
      

        Task UpdateAsync(Question question);

        Task DeleteAsync(Question question);
    }
}
