using AccessLayer.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ModelsLayer.Dtos;
using ModelsLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{

    public interface IAppCheckHistoryReprository
    {
        Task<int> Add(AppCheckHistoryInsert item);
        void Delete(int id);
        Task<bool> Update(AppCheckHistoryUpdate item);
        Task<List<AppCheckHistoryDto>> GetAsync();
        Task<AppCheckHistoryDto> GetByIdAsync(int Id);
        Task<List<AppCheckHistoryDto>> GetByApplicationIdAsync(int Application_Id);
    }
    internal class AppCheckHistoryReprository : BaseReprository, IAppCheckHistoryReprository
    {
        public AppCheckHistoryReprository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<int> Add(AppCheckHistoryInsert item)
        {
            var Data = _mapper.Map<AppCheckHistory>(item);
            _context.AppCheckHistories.Add(Data);
            await _context.SaveChangesAsync();
            return Data.Id;
        }

        public void Delete(int id)
        {
            _context.AppCheckHistories.Remove(new AppCheckHistory() { Id = id });
        }
        public async Task<bool> Update(AppCheckHistoryUpdate item)
        {
            var Data = await _context.AppCheckHistories.FindAsync(item.Id);
            _mapper.Map(item, Data);
            _context.Entry(Data).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<List<AppCheckHistoryDto>> GetAsync()
        {
            var query = _context.AppCheckHistories.AsQueryable();

            return await query.ProjectTo<AppCheckHistoryDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<AppCheckHistoryDto> GetByIdAsync(int Id)
        {
            var query = _context.AppCheckHistories.Where(r => r.Id == Id).AsQueryable();

            return await query.ProjectTo<AppCheckHistoryDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<List<AppCheckHistoryDto>> GetByApplicationIdAsync(int Application_Id)
        {
            var query = _context.AppCheckHistories.Where(r => r.TargetApplication_Id == Application_Id).AsQueryable();

            return await query.ProjectTo<AppCheckHistoryDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

    }
}
