using AutoMapper;
using Secim2028.Dtos.OyAdayDto;
using Secim2028.Dtos.OyPartiDto;
using Secim2028.Helper.RandomOyHelper;
using Secim2028.Models;

namespace Secim2028.Services.OyServices
{
    public class OyPartiService : IOyPartiService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IFirstAndLastSandikHelper _firstAndLastSandikHelper;
        public OyPartiService(DataContext context, IMapper mapper, IFirstAndLastSandikHelper firstAndLastSandikHelper)
        {
            _mapper = mapper;
            _context = context;
            _firstAndLastSandikHelper = firstAndLastSandikHelper;
        }

        public async Task<List<OyPartiResponseDto>> GetOylar(int sandikNo)
        {
            var oylar = await _context.PartiOylar
                .Include(e => e.SiyasiParti)
                .OrderBy(e => e.SiyasiParti.SiyasiPartiId)
                .Select(e => _mapper.Map<OyPartiResponseDto>(e))
                .ToListAsync();

            return oylar;

        }


        public async Task<List<OyPartiResponseDto>> AddOyParti(int sandikNo, int partiId, int OySayisi)
        {
            var existingOy = await _context.PartiOylar
                .FirstOrDefaultAsync(o => o.Sandik.SandikNo == sandikNo && o.SiyasiParti.SiyasiPartiId == partiId);

            if (existingOy != null)
            {
                existingOy.OySayisi += OySayisi;
                await _context.SaveChangesAsync();
            }

            else
            {
                var oy = new OyParti
                {
                    SiyasiParti = await _context.Partiler.FindAsync(keyValues: partiId),
                    Sandik = await _context.Sandiklar.FirstOrDefaultAsync(e => e.SandikNo == sandikNo),
                    OySayisi = OySayisi
                };

                if (oy.SiyasiParti == null || oy.Sandik == null)
                {
                    return null;
                }

                await _context.PartiOylar.AddAsync(oy);
                await _context.SaveChangesAsync();

            }



            var returner = await _context.PartiOylar
                .Include(e => e.SiyasiParti)
                .OrderBy(e => e.SiyasiParti.SiyasiPartiId)
                .Select(e => _mapper.Map<OyPartiResponseDto>(e))
                .ToListAsync();

            return returner;

        }

        public async Task<List<OyPartiResponseDto>> AddOyPartiJson(OyPartiRequestDto request)
        {
            var existingOy = await _context.PartiOylar
               .FirstOrDefaultAsync(o => o.Sandik.SandikNo == request.sandikNo && o.SiyasiParti.SiyasiPartiId == request.partiId);

            if (existingOy != null)
            {
                existingOy.OySayisi += request.oySayisi;
                await _context.SaveChangesAsync();
            }

            else
            {

                var oy = new OyParti
                {
                    SiyasiParti = await _context.Partiler.FindAsync(request.partiId),
                    Sandik = await _context.Sandiklar.FirstOrDefaultAsync(e => e.SandikNo == request.sandikNo),
                    OySayisi = request.oySayisi
                };

                if (oy.SiyasiParti == null || oy.Sandik == null)
                {
                    return null;
                }

                await _context.PartiOylar.AddAsync(oy);
                await _context.SaveChangesAsync();

            }



            var returner = await _context.PartiOylar
                .Include(e => e.SiyasiParti)
                .OrderBy(e => e.SiyasiParti.SiyasiPartiId)
                .Select(e => _mapper.Map<OyPartiResponseDto>(e))
                .ToListAsync();

            return returner;

        }

        public async Task<string> ClearSandik(int sandikNo)
        {

            var sandik = await _context.Sandiklar.FindAsync(sandikNo);

            var oylar = await _context.PartiOylar
                .Where(e => e.Sandik == sandik)
                .ToListAsync();

            if (sandik == null)
            {
                return null;
            }

            _context.PartiOylar.RemoveRange(oylar);
            await _context.SaveChangesAsync();

            return $"{sandikNo} no'lu sandik temizlendi!";


        }

        public async Task<List<OyPartiResponseDto>> AddOyPartiRandom(int ilid, int partiId, int OySayisi)
        {


            int min = _firstAndLastSandikHelper.getFirstAndLastSandik(ilid)[0];
            int max = _firstAndLastSandikHelper.getFirstAndLastSandik(ilid)[1];

            Random rnd = new Random();
            int randomSandikNo = rnd.Next(min, max + 1);

            var existingOy = await _context.PartiOylar
                .FirstOrDefaultAsync(o => o.Sandik.SandikNo == randomSandikNo && o.SiyasiParti.SiyasiPartiId == partiId);

            if (existingOy != null)
            {
                existingOy.OySayisi += OySayisi;
                await _context.SaveChangesAsync();
            }

            else
            {
                var oy = new OyParti
                {
                    SiyasiParti = await _context.Partiler.FindAsync(keyValues: partiId),
                    Sandik = await _context.Sandiklar.FirstOrDefaultAsync(e => e.SandikNo == randomSandikNo),
                    OySayisi = OySayisi
                };

                if (oy.SiyasiParti == null || oy.Sandik == null)
                {
                    return null;
                }

                await _context.PartiOylar.AddAsync(oy);
                await _context.SaveChangesAsync();

            }

            var returner = await _context.PartiOylar
                .Include(e => e.SiyasiParti)
                .Where(e => e.Sandik.SandikNo == randomSandikNo)
                .Select(e => _mapper.Map<OyPartiResponseDto>(e))
                .ToListAsync();

            return returner;







        }
    }
}
