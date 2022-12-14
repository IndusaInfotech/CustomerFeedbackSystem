using Application.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public  interface IAnswerRepository
    {
        Task<List<Answer>> GetListAsync();
        Task<long> InsertAnswerAsync(List<Answer> answers);
        Task<long> InsertAsync(Answer answer); 
        Task<List<AnswerInfo>> GetByIdAsync(long? questionId);
       
    }
}
