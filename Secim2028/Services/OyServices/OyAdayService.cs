using AutoMapper;
using Secim2028.Dtos.OyAdayDto;
using Secim2028.Helper.RandomOyHelper;
using Secim2028.Models;
using System.Reflection.Metadata.Ecma335;

namespace Secim2028.Services.OyServices
{
    public class OyAdayService : IOyAdayService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IFirstAndLastSandikHelper _firstAndLastSandik;

        public OyAdayService(DataContext context, IMapper mapper, IFirstAndLastSandikHelper firstAndLastSandik)
        {
            _context = context;
            _mapper = mapper;
            _firstAndLastSandik = firstAndLastSandik;
        }

        public async Task<List<OyAdayResponseDto>> GetOylar(int sandikNo){

            var oylar = await _context.AdayOylar
                .Include(e => e.CumhurbaskaniAdayi)
                .Where(e => e.Sandik.SandikNo == sandikNo)
                .OrderBy(e => e.CumhurbaskaniAdayi.AdayAdi)
                .Select(e => _mapper.Map<OyAdayResponseDto>(e))
                .ToListAsync();

            return oylar;

        }



        public async Task<List<OyAdayResponseDto>> AddOyAday(int sandikNo, int adayId, int OySayisi)
        {

            var existingOy = await _context.AdayOylar
                .FirstOrDefaultAsync(o => o.Sandik.SandikNo == sandikNo && o.CumhurbaskaniAdayi.AdayId == adayId);

            if (existingOy != null)
            {
                existingOy.OySayisi += OySayisi;
                await _context.SaveChangesAsync();
            }

            else
            {

                var oy = new OyAday
                {
                    CumhurbaskaniAdayi = await _context.Adaylar.FindAsync(adayId),
                    Sandik = await _context.Sandiklar.FirstOrDefaultAsync(e => e.SandikNo == sandikNo),
                    OySayisi = OySayisi
                };

                if (oy.CumhurbaskaniAdayi == null || oy.Sandik == null)
                {
                    return null;
                }

                await _context.AdayOylar.AddAsync(oy);
                await _context.SaveChangesAsync();

            }


            

            var oylar = await _context.AdayOylar
                .Include(e => e.CumhurbaskaniAdayi)
                .Where(e => e.Sandik.SandikNo == sandikNo)
                .Select(e => _mapper.Map<OyAdayResponseDto>(e))
                .ToListAsync();

            return oylar;

        }

        public async Task<List<OyAdayResponseDto>> AddOyAdayJson(OyAdayRequestDto request)
        {

            var existingOy = await _context.AdayOylar
                .FirstOrDefaultAsync(o => o.Sandik.SandikNo == request.sandikNo && o.CumhurbaskaniAdayi.AdayId == request.adayId);

            if (existingOy != null)
            {
                existingOy.OySayisi += request.oySayisi;
                await _context.SaveChangesAsync();
            }

            else
            {
                var oy = new OyAday
                {
                    CumhurbaskaniAdayi = await _context.Adaylar.FindAsync(request.adayId),
                    Sandik = await _context.Sandiklar.FirstOrDefaultAsync(e => e.SandikNo == request.sandikNo),
                    OySayisi = request.oySayisi
                };

                if (oy.CumhurbaskaniAdayi == null || oy.Sandik == null)
                {
                    return null;
                }

                await _context.AdayOylar.AddAsync(oy);
                await _context.SaveChangesAsync();

            }





            var oylar = await _context.AdayOylar
                .Include(e => e.CumhurbaskaniAdayi)
                .Where(e => e.Sandik.SandikNo == request.sandikNo)
                .Select(e => _mapper.Map<OyAdayResponseDto>(e))
                .ToListAsync();

            return oylar;


        }


        public async Task<string> ClearSandik(int sandikNo)
        {

            var sandik = await _context.Sandiklar.FindAsync(sandikNo);

            var oylar = await _context.AdayOylar
                .Where(e => e.Sandik == sandik)
                .ToListAsync();

            if(sandik == null)
            {
                return null;
            }

            _context.AdayOylar.RemoveRange(oylar);
            await _context.SaveChangesAsync();

            return $"{sandikNo} no'lu sandik temizlendi!";


        }


        public async Task<List<OyAdayResponseDto>> AddOyAdayRandom(int ilid , int adayId, int OySayisi)
        {

            int min = _firstAndLastSandik.getFirstAndLastSandik(ilid)[0];
            int max = _firstAndLastSandik.getFirstAndLastSandik(ilid)[1];

            Random rnd = new Random();
            int randomSandikNo = rnd.Next(min, max+1);


            var existingOy = await _context.AdayOylar
                .FirstOrDefaultAsync(o => o.Sandik.SandikNo == randomSandikNo && o.CumhurbaskaniAdayi.AdayId == adayId);

            if (existingOy != null)
            {
                existingOy.OySayisi += OySayisi;
                await _context.SaveChangesAsync();
            }

            else
            {

                var oy = new OyAday
                {
                    CumhurbaskaniAdayi = await _context.Adaylar.FindAsync(adayId),
                    Sandik = await _context.Sandiklar.FirstOrDefaultAsync(e => e.SandikNo == randomSandikNo),
                    OySayisi = OySayisi
                };

                if (oy.CumhurbaskaniAdayi == null || oy.Sandik == null)
                {
                    return null;
                }

                await _context.AdayOylar.AddAsync(oy);
                await _context.SaveChangesAsync();

            }




            var oylar = await _context.AdayOylar
                .Include(e => e.CumhurbaskaniAdayi)
                .Where(e => e.Sandik.SandikNo == randomSandikNo)
                .Select(e => _mapper.Map<OyAdayResponseDto>(e))
                .ToListAsync();

            return oylar;

        }



    }
}
