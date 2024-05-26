using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Secim2028.Dtos.SandikDto;
using Secim2028.Helper.RandomOyHelper;

namespace Secim2028.Services.SandikService
{
    public class SecimSandikService : ISecimSandikService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IFirstAndLastSandikHelper _firstAndLastHelper;
        public SecimSandikService(DataContext context,IMapper mapper, IFirstAndLastSandikHelper firstAndLastSandikHelper)
        {
            _context = context;
            _mapper = mapper;
            _firstAndLastHelper = firstAndLastSandikHelper;
        }


        public async Task<List<SandikResponseDto>> GetSandiks(int ilid)
        {

            var sandiklar = await _context.Sandiklar
                .Where(e => e.Ilce.Il.IlId == ilid)
                .Select(e => _mapper.Map<SandikResponseDto>(e))
                .ToListAsync();

            return sandiklar;
        }

        public async Task<List<SandikResponseDto>> AddSandiks(List<SandikCycleDto> request)
        {


            foreach (var x in request)
            {

                var starterNo = await _context.Sandiklar
                    .OrderByDescending(e => e.SandikNo)
                    .Select(s => s.SandikNo)
                    .FirstOrDefaultAsync();

                var ilce = await _context.Ilceler.Where(e => e.IlceAdi == x.ilcename && e.Il.IlId == x.ilid).FirstOrDefaultAsync();

                if(starterNo == null || ilce == null)
                {
                    return null;
                }
                

                for (int i = 1;i<= x.sandikSayisi;i++)
                {
                    Sandik tempSandik = new Sandik();
                    tempSandik.SandikNo = starterNo + i;
                    tempSandik.Ilce = ilce;

                    await _context.Sandiklar.AddAsync(tempSandik);
                    
                    
                }

                await _context.SaveChangesAsync();


            }

            var returner = await _context.Sandiklar
                .Where(e => e.Ilce.Il.IlId == 34)
                .Select(e => _mapper.Map<SandikResponseDto>(e)).ToListAsync();
            return returner;



        }

        

        public async Task<SandikFirstAndLastResponseDto> GetFirstAndLastSandik(int ilid)
        {

            SandikFirstAndLastResponseDto sandik = new SandikFirstAndLastResponseDto();

            sandik.firstSandikNo = _firstAndLastHelper.getFirstAndLastSandik(ilid)[0];
            sandik.lastSandikNo = _firstAndLastHelper.getFirstAndLastSandik(ilid)[1];

            return sandik;
        }
    }
}
