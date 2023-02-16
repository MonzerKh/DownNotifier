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
    public interface ITargetApplicationReprository
    {
        Task<int> Add(TargetApplicationInsert item);
        Task Delete(int id);
        Task<bool> Update(TargetApplicationUpdate item);
        Task<List<TargetApplicationDto>> GetAsync();
        Task<TargetApplicationDto> GetByIdAsync(int Id);
    }
    public class TargetApplicationReprository : BaseReprository, ITargetApplicationReprository
    {
        public TargetApplicationReprository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<int> Add(TargetApplicationInsert item)
        {
            var Data = _mapper.Map<TargetApplication>(item);
            Data.IsActive =true;
            Data.Interval *= 1000;
            _context.TargetApplications.Add(Data);
            await _context.SaveChangesAsync();
            return Data.Id;
        }

        public async Task Delete(int id)
        {
            var apphist = _context.AppCheckHistories.Where(r=>r.TargetApplication_Id== id);
            _context.AppCheckHistories.RemoveRange(apphist);
            await _context.SaveChangesAsync();
            _context.TargetApplications.Remove(new TargetApplication() { Id = id });
            await _context.SaveChangesAsync();
        }
        public async Task<bool> Update(TargetApplicationUpdate item)
        {
            var Data = await _context.TargetApplications.FindAsync(item.Id);
            _mapper.Map(item, Data);
            Data.Interval *= 1000;
            _context.Entry(Data).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<List<TargetApplicationDto>> GetAsync()
        {
            var query = _context.TargetApplications.AsQueryable();

            return await query.ProjectTo<TargetApplicationDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<TargetApplicationDto> GetByIdAsync(int Id)
        {
            var query = _context.TargetApplications.Where(r => r.Id == Id).AsQueryable();

            return await query.ProjectTo<TargetApplicationDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }


    }
}
