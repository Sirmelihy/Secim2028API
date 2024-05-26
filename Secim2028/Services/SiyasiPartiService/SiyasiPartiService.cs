using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc;
using Secim2028.Dtos.IttifakDto;
using Secim2028.Dtos.SiyasiPartiDto;

namespace Secim2028.Services.SiyasiPartiService
{
    public class SiyasiPartiService : ISiyasiPartiService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SiyasiPartiService(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SiyasiPartiResponseDto>> GetSiyasiPartiler()
        {

            var partiler = await _context.Partiler
                .Include(e => e.Ittifak)
                .Select(partiler => _mapper.Map<SiyasiPartiResponseDto>(partiler))
                .ToListAsync();

            partiler = partiler.OrderBy(e => e.SiyasiPartiAdi).ToList();
            return partiler;


        }

        public async Task<List<SiyasiPartiResponseDto>> AddSiyasiPartis (List<SiyasiPartiRequestDto> request) 
        {

            var result = _mapper.Map<List<SiyasiParti>>(request);

            await _context.Partiler.AddRangeAsync(result);
            await _context.SaveChangesAsync();

            var partiler = await _context.Partiler
                .Select(partiler => _mapper.Map<SiyasiPartiResponseDto>(partiler))
                .ToListAsync();

            return partiler;

        }

        public async Task<List<SiyasiPartiResponseDto>> DeleteSiyasiParti(int id)
        {
            var parti = await _context.Partiler.FindAsync(id);

            if(parti == null)
            {
                return null;
            }

            _context.Partiler.Remove(parti);
            await _context.SaveChangesAsync();

            var result = await _context.Partiler.Select(e => _mapper.Map<SiyasiPartiResponseDto>(e)).ToListAsync();
            return result;

        }

        public async Task<ActionResult<SiyasiParti>> ChangeIttifak(int id, string ittifakName)
        {
            var parti = await _context.Partiler.FindAsync(id);
            var ittifak = await _context.Ittifaklar
                .Where(i => i.IttifakAdi == ittifakName)
                .FirstOrDefaultAsync();

            if(parti == null)
            {
                return null;
            }

            if (ittifak == null)
            {
                IttifakRequestDto request = new IttifakRequestDto();
                request.IttifakAdi = ittifakName;
                Ittifak yeni = new Ittifak();
                yeni = _mapper.Map<Ittifak>(request);
                parti.Ittifak = yeni;
                await _context.Ittifaklar.AddAsync(yeni);
                await _context.SaveChangesAsync();
            }

            else
            {
                Ittifak? yeni = new Ittifak();
                yeni = await _context.Ittifaklar
                    .Where(e => e.IttifakAdi == ittifakName)
                    .FirstOrDefaultAsync();

                

                parti.Ittifak = yeni;
                await _context.SaveChangesAsync();

            }

            var value = await _context.Partiler.
                Where(i => i.SiyasiPartiAdi == parti.SiyasiPartiAdi)
                .FirstOrDefaultAsync();

            return value;


        }
    }
}
