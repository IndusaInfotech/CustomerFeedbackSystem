using Application.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ISurveyRepository
    {
        IQueryable<Survey> Surveys { get; }

        Task<List<Survey>> GetListAsync();

        Task<SurveyInfo> GetByIdAsync(long? surveyId);

        Task<long> InsertAsync(Survey survey);

        Task UpdateAsync(Survey survey);

        Task DeleteAsync(Survey survey);
        
    }
}
