using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Secim2028.Dtos.AdayDto;
using System.Net.WebSockets;

namespace Secim2028.Services.AdayService
{
    public class SecimAdayService : ISecimAdayService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SecimAdayService(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<List<AdayResponseDto>> GetAdays()
        {
            var adaylar = await _context.Adaylar
                .Include(e=> e.SiyasiParti)
                .ThenInclude(e=>e.Ittifak)
                .Select(e => _mapper.Map<AdayResponseDto>(e))
                .ToListAsync();

            return adaylar;

        }

        public async Task<List<AdayResponseDto>> AddAdays(List<AdayRequestDto> request)
        {
            List<AdayCumhurbaskani> adaysToAdd = new List<AdayCumhurbaskani>();

            foreach (var x in request)
            {
                var siyasiParti = await _context.Partiler.FirstOrDefaultAsync(e => e.SiyasiPartiId == x.partiId );

                if(siyasiParti != null)
                {
                    AdayCumhurbaskani aday = new AdayCumhurbaskani();
                    aday = _mapper.Map<AdayCumhurbaskani>(x);
                    aday.SiyasiParti = siyasiParti;
                    adaysToAdd.Add(aday);
                }

            }

            if(adaysToAdd.Count == 0 || (adaysToAdd == null))
            {
                return null;
            }

            Console.WriteLine(adaysToAdd.Count());

            await _context.Adaylar.AddRangeAsync(adaysToAdd);
            await _context.SaveChangesAsync();

            var returner = await _context.Adaylar
                .Include(e => e.SiyasiParti)
                .ThenInclude(e => e.Ittifak)
                .Select(e => _mapper.Map<AdayResponseDto>(e))
                .ToListAsync();
            return returner;

        }

        public async Task<AdayResponseDto> ChangeAdayName(int id,string name)
        {
            var aday = await _context.Adaylar
                .Include(e => e.SiyasiParti)
                .FirstOrDefaultAsync(e => e.AdayId == id);

            if(aday == null)
            {
                return null;
            }

            aday.AdayAdi = name;
            await _context.SaveChangesAsync();

            var returner = _mapper.Map<AdayResponseDto>(aday);

            return returner;

        }


        public async Task<List<AdayResponseDto>> DeleteAday(int id)
        {
            var aday = await _context.Adaylar.FindAsync(id);

            if (aday == null)
            {
                return null;
            }

            _context.Adaylar.Remove(aday);
            await _context.SaveChangesAsync();

            var returner = await _context.Adaylar
                .Include(e => e.SiyasiParti)
                .ThenInclude(e => e.Ittifak)
                .Select(e => _mapper.Map<AdayResponseDto>(e))
                .ToListAsync();

            return returner;

        }



    }
}
