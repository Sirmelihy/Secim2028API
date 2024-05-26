using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Secim2028.Dtos.AdayDto;
using Secim2028.Dtos.OyOranDto.IlOyOranDto;

namespace Secim2028.Helper.FirstAdayHelper
{
    public class TurkiyeFirstAdayEachIlHelper : ITurkiyeFirstAdayEachIlHelper
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TurkiyeFirstAdayEachIlHelper(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }


        public async Task<List<IlAdayOyOranDto>> GetTurkiyeAdayOranEachIl()
        {
            List<IlAdayOyOranDto> result = new List<IlAdayOyOranDto>();
            var adaylar = await _context.Adaylar.ToListAsync();
            var iller = await _context.Iller.ToListAsync();

            foreach (var il in iller)
            {
                var adayOylar = await _context.AdayOylar
                    .Where(e => e.Sandik.Ilce.Il.IlId == il.IlId)
                    .ToListAsync();

                var toplamOy = adayOylar.Sum(e => e.OySayisi);

                foreach (var x in adaylar)
                {
                    IlAdayOyOranDto indv = new IlAdayOyOranDto();
                    var oylar = await _context.AdayOylar
                        .Where(e => e.Sandik.Ilce.Il.IlId == il.IlId && e.CumhurbaskaniAdayi.AdayId == x.AdayId)
                        .ToListAsync();
                    var toplamAdayOySayisi = oylar.Sum(e => e.OySayisi);

                    double oyOrani = 0;
                    if (toplamOy != 0)
                    {
                        oyOrani = (double)toplamAdayOySayisi / (double)toplamOy * 100;
                    }

                    indv.ilID = il.IlId;
                    indv.ilAdi = il.IlAdi;
                    indv.aday = _mapper.Map<AdayResponseForOyAdayDto>(x);
                    indv.OyOrani = oyOrani;
                    indv.oySayisi = toplamAdayOySayisi;
                    indv.yuzdeOyOrani = "%" + oyOrani.ToString("F2");
                    result.Add(indv);
                }
            }


            List<IlAdayOyOranDto> resulter = new List<IlAdayOyOranDto>();

            foreach (var il in iller)
            {
                var adder = result.Where(e => e.ilID == il.IlId)
                    .MaxBy(e => e.oySayisi);
                resulter.Add(adder);

            }

            return resulter;
        }
    }
}
