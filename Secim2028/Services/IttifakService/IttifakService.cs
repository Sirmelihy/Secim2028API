using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Secim2028.Dtos.IttifakDto;

namespace Secim2028.Services.IttifakService
{
    public class IttifakService : IIttifakService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public IttifakService(DataContext context,IMapper mapper) {

            _context = context;
            _mapper = mapper;
        }

        public async Task<List<IttifakResponseDto>> GetIttifaks()
        {
            var result = await _context.Ittifaklar
                .Include(e => e.SiyasiPartiler)
                .Select(e => _mapper.Map<IttifakResponseDto>(e))
                .ToListAsync();

            return result;
        }

        public async Task<List<IttifakResponseDto>> AddIttifak(IttifakRequestDto request)
        {
            var ittifak = _mapper.Map<Ittifak>(request);

            await _context.Ittifaklar.AddAsync(ittifak);
            await _context.SaveChangesAsync();

            var result = await _context.Ittifaklar
                .Include(e => e.SiyasiPartiler)
                .Select(e => _mapper.Map<IttifakResponseDto>(e))
                .ToListAsync();
            return result;


        }

        public async Task<List<IttifakResponseDto>> DeleteIttifak (int ittifakid)
        {

            var ittifak = await _context.Ittifaklar.FindAsync(ittifakid);

            if(ittifak == null)
            {
                return null;
            }

            var partiler = await _context.Partiler
                .Where(partiler => partiler.Ittifak.IttifakId == ittifakid)
                .ToListAsync();

            if(partiler != null) {

                foreach (var x in partiler)
                {
                    x.Ittifak = null;
                }

            }

            _context.Ittifaklar.Remove(ittifak);
            await _context.SaveChangesAsync();

            return await _context.Ittifaklar.Select(e=> _mapper.Map<IttifakResponseDto>(e)).ToListAsync();




        }

    }
}
