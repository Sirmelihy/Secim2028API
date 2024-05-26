using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom.Compiler;

namespace Secim2028.Services.SehirService
{
    public class SecimIlService : ISecimIlService
    {
        private readonly DataContext _context;
        public SecimIlService(DataContext context)
        {

            _context = context;
        }


        public async Task<List<Il>> GetIls()
        {
            //return await _context.Iller.ToListAsync();
            var iller = await _context.Iller.Include(e => e.Ilceler).ToListAsync();
            iller = iller.OrderBy(e => e.IlId).ToList();
            return iller;
        }

        public async Task<List<Il>> GetOnlyIls()
        {
            //return await _context.Iller.ToListAsync();
            var iller = await _context.Iller.ToListAsync();
            iller = iller.OrderBy(e => e.IlId).ToList();
            return iller;
        }


        public async Task<Il> GetIl(int plaka)
        {
            var il = await _context.Iller.FindAsync(plaka);

            if(il == null)
            {
                return null;
            }

            return il;
        }





        public async Task<Il>? ChangeIl(int plaka, string request)
        {
            var il = await _context.Iller.FindAsync(plaka);

            if(il == null)
            {
                return null;
            }

            il.IlAdi = request;

            await _context.SaveChangesAsync();

            return il;
        }


        public async Task<List<Il>?> DeleteIl(int plaka)
        {
            var il = await _context.Iller.FindAsync(plaka);

            if(il == null)
            {
                return null;
            }

            _context.Remove(il);
            await _context.SaveChangesAsync();

            return await _context.Iller.ToListAsync();
        }


        public async Task<List<Il>> AddIl(int plaka, string ilName)
        {
            var il = new Il();

            il.IlId = plaka;
            il.IlAdi = ilName;

            await _context.Iller.AddAsync(il);
            await _context.SaveChangesAsync();


            return await _context.Iller.ToListAsync();

        }

        public async Task<List<Il>> AddIls(List<Il> iller)
        {

            /* Girilen Stringin ilk karakterini büyütüp diğer karakterleri küçültme işlemi */
            char temp = '-';

            foreach (var x in iller)
            {

                x.IlAdi = x.IlAdi.ToLower();
                temp = char.ToUpper(x.IlAdi[0]);
                x.IlAdi = temp + x.IlAdi.Substring(1);

            }

            /* Girilen verilerin Veritabanına eklenmesi işlemi */

            await _context.Iller.AddRangeAsync(iller);
            await _context.SaveChangesAsync();



            return await _context.Iller.ToListAsync();
        }

    }
}
