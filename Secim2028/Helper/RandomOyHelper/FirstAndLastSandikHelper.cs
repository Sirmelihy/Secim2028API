namespace Secim2028.Helper.RandomOyHelper
{
    public class FirstAndLastSandikHelper : IFirstAndLastSandikHelper
    {


        private readonly DataContext _context;

        public FirstAndLastSandikHelper(DataContext context)
        {
            _context = context;
        }


        public int[] getFirstAndLastSandik (int ilid)
        {
            var first = _context.Sandiklar
                .Where(e => e.Ilce.Il.IlId == ilid)
                .OrderBy(e => e.SandikNo)
                .FirstOrDefault();

            var last = _context.Sandiklar
                .Where(e => e.Ilce.Il.IlId == ilid)
                .OrderByDescending(e => e.SandikNo)
                .FirstOrDefault();

            int[] sandikNos = new int[2];
            sandikNos[0] = first.SandikNo;
            sandikNos[1] = last.SandikNo;

            return sandikNos;

        }

    }
}
