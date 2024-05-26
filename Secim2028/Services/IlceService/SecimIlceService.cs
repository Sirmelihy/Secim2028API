using AutoMapper;
using Secim2028.Models;

namespace Secim2028.Services.IlceService
{
    public class SecimIlceService : ISecimIlceService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SecimIlceService(DataContext context , IMapper mapper) { 
        
            _context = context;
            _mapper = mapper;
        
        }


        

        public async Task<List<ilceResponseDto>> GetIlces()
        {
            var ilces = await _context.Ilceler.Include(e => e.Il).Select(e => _mapper.Map<ilceResponseDto>(e)).ToListAsync();
            return ilces;
        }

        public async Task<List<Ilce>> GetOnlyIlces()
        {

            return await _context.Ilceler.ToListAsync();
        }



        public async Task<List<Ilce>> AddIlces(List<IlceRequestDto> ilceList)
        {
            char temp = '-';

            List<Ilce> ilceler = new List<Ilce>();

            foreach (var x in ilceList)
            {

                x.ilceAdi = x.ilceAdi.ToLower();
                temp = char.ToUpper(x.ilceAdi[0]);
                x.ilceAdi = temp + x.ilceAdi.Substring(1);

                Ilce ilce = new Ilce();

                ilce.IlceAdi = x.ilceAdi;
                ilce.Il = _context.Iller.Find(x.ilid);
                ilceler.Add(ilce);

            }

            await _context.Ilceler.AddRangeAsync(ilceler);
            await _context.SaveChangesAsync();

            return await _context.Ilceler.ToListAsync();
        }



        public async Task<List<Ilce>> DeleteIlce(int id)
        {

            var ilce = await _context.Ilceler.FindAsync(id);

            if(ilce == null)
            {
                return null;

            }

            _context.Remove(ilce);
            await _context.SaveChangesAsync();

            return await _context.Ilceler.ToListAsync();


        }

    }
}
