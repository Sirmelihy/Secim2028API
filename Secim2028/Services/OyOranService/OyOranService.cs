using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Secim2028.Dtos.AdayDto;
using Secim2028.Dtos.IttifakDto;
using Secim2028.Dtos.OyOranDto.IlceOyOranDto;
using Secim2028.Dtos.OyOranDto.IlOyOranDto;
using Secim2028.Dtos.OyOranDto.TurkiyeOyOranDto;
using Secim2028.Dtos.OyOranDto.WinnerAdaysDto;
using Secim2028.Dtos.OyOranDto.WinnerIlTimesDto;
using Secim2028.Dtos.SiyasiPartiDto;
using Secim2028.Helper.FirstAdayHelper;
using Secim2028.Helper.FirstPartiHelper;
using System.Reflection.Metadata.Ecma335;

namespace Secim2028.Services.OyOranService
{
    public class OyOranService : IOyOranService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ITurkiyeFirstAdayEachIlHelper _turkiyeFirstAdayEachIlHelper;
        private readonly ITurkiyeFirstPartiEachIlHelper _turkiyeFirstPartiEachIlHelper;
        public OyOranService(DataContext context, IMapper mapper, ITurkiyeFirstAdayEachIlHelper turkiyeFirstAdayEachIlHelper, ITurkiyeFirstPartiEachIlHelper turkiyeFirstPartiEachIlHelper)
        {
            _context = context;
            _mapper = mapper;
            _turkiyeFirstAdayEachIlHelper = turkiyeFirstAdayEachIlHelper;
            _turkiyeFirstPartiEachIlHelper = turkiyeFirstPartiEachIlHelper;

        }


        public async Task<List<IlAdayOyOranDto>> GetIlAdayOyOran(int ilid)
        {

            var adayOylar = await _context.AdayOylar
                .Where(e => e.Sandik.Ilce.Il.IlId == ilid)
                .ToListAsync();

            var toplamOy = adayOylar.Sum(e => e.OySayisi);

            if (toplamOy == 0)
            {
                return null;
            }

            List<IlAdayOyOranDto> result = new List<IlAdayOyOranDto>();

            var adaylar = await _context.Adaylar.ToListAsync();


            foreach (var x in adaylar)
            {
                IlAdayOyOranDto indv = new IlAdayOyOranDto();
                var il = await _context.Iller.FindAsync(ilid);
                var oylar = await _context.AdayOylar.Where(e => e.Sandik.Ilce.Il.IlId == ilid && e.CumhurbaskaniAdayi.AdayId == x.AdayId).ToListAsync();
                var toplamAdayOySayisi = oylar.Sum(e => e.OySayisi);
                double oyOrani = (double)toplamAdayOySayisi / (double)toplamOy * 100;

                indv.ilID = ilid;
                indv.ilAdi = il.IlAdi;
                indv.aday = _mapper.Map<AdayResponseForOyAdayDto>(x);
                indv.OyOrani = oyOrani;
                indv.oySayisi = toplamAdayOySayisi;
                indv.yuzdeOyOrani = "%" + oyOrani.ToString("F2");
                result.Add(indv);

            }
            result = result.OrderByDescending(e => e.oySayisi).ToList();
            return result;

        }

        public async Task<List<IlceAdayOranDto>> GetIlceAdayOran(int ilceid)
        {

            var adayOylar = await _context.AdayOylar
                .Where(e => e.Sandik.Ilce.IlceId == ilceid)
                .ToListAsync();

            var toplamOy = adayOylar.Sum(e => e.OySayisi);

            List<IlceAdayOranDto> result = new List<IlceAdayOranDto>();

            var adaylar = await _context.Adaylar.ToListAsync();

            foreach (var x in adaylar)
            {
                IlceAdayOranDto indv = new IlceAdayOranDto();
                var il = await _context.Ilceler.FindAsync(ilceid);
                var oylar = await _context.AdayOylar.Where(e => e.Sandik.Ilce.IlceId == ilceid && e.CumhurbaskaniAdayi.AdayId == x.AdayId).ToListAsync();
                var toplamAdayOySayisi = oylar.Sum(e => e.OySayisi);
                double oyOrani = (double)toplamAdayOySayisi / (double)toplamOy * 100;

                indv.ilceID = ilceid;
                indv.ilceAdi = il.IlceAdi;
                indv.aday = _mapper.Map<AdayResponseForOyAdayDto>(x);
                indv.OyOrani = oyOrani;
                indv.yuzdeOyOrani = "%" + oyOrani.ToString("F2");
                result.Add(indv);

            }

            return result;

        }

        public async Task<List<IlPartiOyOranDto>> GetIlPartiOyOran(int ilid)
        {

            var partioylar = await _context.PartiOylar
                .Where(e => e.Sandik.Ilce.Il.IlId == ilid)
                .ToListAsync();

            var toplamOy = partioylar.Sum(e => e.OySayisi);

            List<IlPartiOyOranDto> result = new List<IlPartiOyOranDto>();

            var partiler = await _context.Partiler.ToListAsync();


            foreach (var x in partiler)
            {
                IlPartiOyOranDto indv = new IlPartiOyOranDto();
                var il = await _context.Iller.FindAsync(ilid);
                var oylar = await _context.PartiOylar.Where(e => e.Sandik.Ilce.Il.IlId == ilid && e.SiyasiParti.SiyasiPartiId == x.SiyasiPartiId).ToListAsync();
                var topamPartiOySayisi = oylar.Sum(e => e.OySayisi);
                double oyOrani = (double)topamPartiOySayisi / (double)toplamOy * 100;

                indv.ilID = ilid;
                indv.ilAdi = il.IlAdi;
                indv.SiyasiParti = _mapper.Map<SiyasiPartiForOyPartiDto>(x);
                indv.OyOrani = oyOrani;
                indv.oySayisi = topamPartiOySayisi;
                indv.yuzdeOyOrani = "%" + oyOrani.ToString("F2");
                result.Add(indv);

            }


            result = result.Where(e => e.OyOrani > 0)
                .OrderByDescending(e => e.OyOrani)
                .ToList();

            return result;



        }

        public async Task<List<IlcePartiOyOranDto>> GetIlcePartiOyOran(int ilceid)
        {
            var partioylar = await _context.PartiOylar
                .Where(e => e.Sandik.Ilce.IlceId == ilceid)
                .ToListAsync();

            var toplamOy = partioylar.Sum(e => e.OySayisi);

            List<IlcePartiOyOranDto> result = new List<IlcePartiOyOranDto>();

            var partiler = await _context.Partiler.ToListAsync();


            foreach (var x in partiler)
            {
                IlcePartiOyOranDto indv = new IlcePartiOyOranDto();
                var ilce = await _context.Ilceler.FindAsync(ilceid);
                var oylar = await _context.PartiOylar.Where(e => e.Sandik.Ilce.IlceId == ilceid && e.SiyasiParti.SiyasiPartiId == x.SiyasiPartiId).ToListAsync();
                var toplamAdayOySayisi = oylar.Sum(e => e.OySayisi);
                double oyOrani = (double)toplamAdayOySayisi / (double)toplamOy * 100;

                indv.ilceID = ilceid;
                indv.ilceAdi = ilce.IlceAdi;
                indv.SiyasiParti = _mapper.Map<SiyasiPartiForOyPartiDto>(x);
                indv.OyOrani = oyOrani;
                indv.yuzdeOyOrani = "%" + oyOrani.ToString("F2");
                result.Add(indv);

            }


            result = result.Where(e => e.OyOrani > 0)
                .OrderByDescending(e => e.OyOrani)
                .ToList();

            return result;

        }

        public async Task<List<TurkiyeAdayOyOranDto>> GetTurkiyeAdayOyOran()
        {

            var adayOylar = await _context.AdayOylar
                .ToListAsync();

            var toplamOy = adayOylar.Sum(e => e.OySayisi);

            List<TurkiyeAdayOyOranDto> result = new List<TurkiyeAdayOyOranDto>();

            var adaylar = await _context.Adaylar.ToListAsync();


            foreach (var x in adaylar)
            {
                TurkiyeAdayOyOranDto indv = new TurkiyeAdayOyOranDto();

                var oylar = await _context.AdayOylar.Where(e => e.CumhurbaskaniAdayi.AdayId == x.AdayId).ToListAsync();
                var toplamAdayOySayisi = oylar.Sum(e => e.OySayisi);
                double oyOrani = (double)toplamAdayOySayisi / (double)toplamOy * 100;

                indv.aday = _mapper.Map<AdayResponseForOyAdayDto>(x);
                indv.OyOrani = oyOrani;
                indv.oySayisi = toplamAdayOySayisi;
                indv.yuzdeOyOrani = "%" + oyOrani.ToString("F2");
                result.Add(indv);

            }

            result = result.OrderByDescending(e => e.OyOrani).ToList();

            return result;


        }

        public async Task<List<TurkiyePartiOyOranDto>> GetTurkiyePartiOran()
        {
            var partiOylar = await _context.PartiOylar
                .ToListAsync();

            var toplamOy = partiOylar.Sum(e => e.OySayisi);

            List<TurkiyePartiOyOranDto> result = new List<TurkiyePartiOyOranDto>();

            var partiler = await _context.Partiler.ToListAsync();

            int i = 1;
            foreach (var x in partiler)
            {
                TurkiyePartiOyOranDto indv = new TurkiyePartiOyOranDto();

                var oylar = await _context.PartiOylar.Where(e => e.SiyasiParti.SiyasiPartiId == x.SiyasiPartiId).ToListAsync();
                var toplamAdayOySayisi = oylar.Sum(e => e.OySayisi);
                double oyOrani = (double)toplamAdayOySayisi / (double)toplamOy * 100;


                indv.id = i;
                indv.SiyasiParti = _mapper.Map<SiyasiPartiForOyPartiDto>(x);
                indv.OyOrani = oyOrani;
                indv.oySayisi = toplamAdayOySayisi;
                indv.yuzdeOyOrani = "%" + oyOrani.ToString("F2");
                result.Add(indv);
                i++;


            }

            result = result
                .Where(e => e.OyOrani > 0)
                .OrderByDescending(e => e.OyOrani).ToList();

            return result;

        }

        public async Task<List<TurkiyeIttifakOyOranDto>> GetTurkiyeIttifakOyOran()
        {

            var partiOylar = await _context.PartiOylar
                .ToListAsync();

            var toplamOy = partiOylar.Sum(e => e.OySayisi);

            List<TurkiyeIttifakOyOranDto> result = new List<TurkiyeIttifakOyOranDto>();

            var ittifaklar = await _context.Ittifaklar.ToListAsync();

            foreach (var x in ittifaklar)
            {
                TurkiyeIttifakOyOranDto indv = new TurkiyeIttifakOyOranDto();

                var oylar = await _context.PartiOylar.Where(e => e.SiyasiParti.Ittifak.IttifakId == x.IttifakId).ToListAsync();
                var toplamAdayOySayisi = oylar.Sum(e => e.OySayisi);
                double oyOrani = (double)toplamAdayOySayisi / (double)toplamOy * 100;


                indv.Ittifak = _mapper.Map<IttifakResponseOfOyOranDto>(x);
                indv.OyOrani = oyOrani;
                indv.oySayisi = toplamAdayOySayisi;
                indv.yuzdeOyOrani = "%" + oyOrani.ToString("F2");
                result.Add(indv);


            }

            result = result
                .OrderByDescending(e => e.OyOrani).ToList();

            return result;
        }

        public async Task<List<IlAdayOyOranDto>> GetTurkiyeAdayOranEachIl()
        {
            var resulter = await _turkiyeFirstAdayEachIlHelper.GetTurkiyeAdayOranEachIl();
            return resulter;
        }

        public async Task<List<IlPartiOyOranDto>> GetTurkiyePartiOranEachIl()
        {

            var resulter = await _turkiyeFirstPartiEachIlHelper.GetTurkiyePartiOranEachIl();
            return resulter;
        }

        public async Task<List<WinnerAdayDto>> GetWinnerAdays()
        {
            var ilWinners = await _turkiyeFirstAdayEachIlHelper.GetTurkiyeAdayOranEachIl();
            List<WinnerAdayDto> winnersTR = new List<WinnerAdayDto>();

            foreach (var ilWin in ilWinners)
            {

                if(winnersTR.Any(winner => winner.adayName == ilWin.aday.AdayAdi))
                {
                    var winnerToUpdate = winnersTR.FirstOrDefault(e => e.adayName == ilWin.aday.AdayAdi);
                    if(winnerToUpdate != null)
                    {
                        winnerToUpdate.winCount++;
                    }
                }

                else
                {
                    winnersTR.Add(new WinnerAdayDto
                    {
                        adayName = ilWin.aday.AdayAdi,
                        winCount = 1
                    }) ;
                }
            }
            winnersTR = winnersTR.OrderByDescending(e => e.winCount).ToList();
            return winnersTR;

        }

        public async Task<List<WinnerPartiDto>> GetWinnerPartis()
        {
            var ilWinners = await _turkiyeFirstPartiEachIlHelper.GetTurkiyePartiOranEachIl();
            List<WinnerPartiDto> winnersTR = new List<WinnerPartiDto>();

            foreach (var ilWin in ilWinners)
            {

                if (winnersTR.Any(winner => winner.partiName == ilWin.SiyasiParti.siyasiPartiKisaltma))
                {
                    var winnerToUpdate = winnersTR.FirstOrDefault(e => e.partiName == ilWin.SiyasiParti.siyasiPartiKisaltma);
                    if (winnerToUpdate != null)
                    {
                        winnerToUpdate.winCount++;
                    }
                }

                else
                {
                    winnersTR.Add(new WinnerPartiDto
                    {
                        partiName = ilWin.SiyasiParti.siyasiPartiKisaltma,
                        winCount = 1
                    });
                }
            }
            winnersTR = winnersTR.OrderByDescending(e => e.winCount).ToList();
            return winnersTR;

        }

        public async Task<int> PartiKullanılanOy()
        {

            var partiOylar = await _context.PartiOylar
                .ToListAsync();

            var toplamOy = partiOylar.Sum(e => e.OySayisi);

            return toplamOy;

        }

        

        public async Task<int> AdayKullanılanOy()
        {

            var adayOylar = await _context.AdayOylar
                .ToListAsync();

            var toplamOy = adayOylar.Sum(e => e.OySayisi);

            return toplamOy;

        }

    }
}
