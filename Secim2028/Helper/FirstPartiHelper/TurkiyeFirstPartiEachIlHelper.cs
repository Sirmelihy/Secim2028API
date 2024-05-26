using AutoMapper;
using Secim2028.Dtos.OyOranDto.IlOyOranDto;
using Secim2028.Dtos.SiyasiPartiDto;

namespace Secim2028.Helper.FirstPartiHelper
{
    public class TurkiyeFirstPartiEachIlHelper : ITurkiyeFirstPartiEachIlHelper
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;


        public TurkiyeFirstPartiEachIlHelper(DataContext context,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<List<IlPartiOyOranDto>> GetTurkiyePartiOranEachIl()
        {

            List<IlPartiOyOranDto> result = new List<IlPartiOyOranDto>();
            var partiler = await _context.Partiler.ToListAsync();
            var iller = await _context.Iller.ToListAsync();

            foreach (var il in iller)
            {
                var partioylar = await _context.PartiOylar
                    .Where(e => e.Sandik.Ilce.Il.IlId == il.IlId)
                    .ToListAsync();

                var toplamOy = partioylar.Sum(e => e.OySayisi);

                foreach (var x in partiler)
                {
                    IlPartiOyOranDto indv = new IlPartiOyOranDto();
                    var oylar = await _context.PartiOylar
                        .Where(e => e.Sandik.Ilce.Il.IlId == il.IlId && e.SiyasiParti.SiyasiPartiId == x.SiyasiPartiId)
                        .ToListAsync();
                    var toplamPartiOySayisi = oylar.Sum(e => e.OySayisi);

                    double oyOrani = 0;
                    if (toplamOy != 0)
                    {
                        oyOrani = (double)toplamPartiOySayisi / (double)toplamOy * 100;
                    }

                    indv.ilID = il.IlId;
                    indv.ilAdi = il.IlAdi;
                    indv.SiyasiParti = _mapper.Map<SiyasiPartiForOyPartiDto>(x);
                    indv.OyOrani = oyOrani;
                    indv.oySayisi = toplamPartiOySayisi;
                    indv.yuzdeOyOrani = "%" + oyOrani.ToString("F2");
                    result.Add(indv);
                }
            }

            List<IlPartiOyOranDto> resulter = new List<IlPartiOyOranDto>();

            foreach (var il in iller)
            {
                var adder = result.Where(e => e.ilID == il.IlId)
                    .OrderByDescending(e => e.oySayisi)
                    .FirstOrDefault();

                if (adder != null)
                {
                    resulter.Add(adder);
                }
            }

            return resulter;

        }


    }
}
