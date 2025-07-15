using SD_Fuar.Models;

namespace SD_Fuar.Services
{
    public class IndirimService
    {
        private readonly SD_FuarContext _context;

        private const decimal INDIRIM_ORANI_1 = 5.0m;
        private const decimal INDIRIM_ORANI_2 = 10.0m;
        private const decimal INDIRIM_ORANI_3 = 15.0m;
        private const decimal INDIRIM_ORANI_4_PLUS = 20.0m;
        private const decimal MUDUR_ONAYI_SINIRI = 20.0m;

        public IndirimService(SD_FuarContext context)
        {
            _context = context;
        }

        public decimal HesaplaIndirimOrani(int firmaId, int fuarId)
        {
            var oncekiKatilimlar = _context.KatilimGecmisleri
                .Count(k => k.FirmaId == firmaId && k.FuarId != fuarId);

            var indirimOrani = oncekiKatilimlar switch
            {
                1 => INDIRIM_ORANI_1,
                2 => INDIRIM_ORANI_2,
                3 => INDIRIM_ORANI_3,
                _ => oncekiKatilimlar >= 4 ? INDIRIM_ORANI_4_PLUS : 0.0m
            };

            return indirimOrani;
        }

        public bool MudurOnayiGerekliMi(decimal indirimOrani)
        {
            return indirimOrani > MUDUR_ONAYI_SINIRI;
        }
    }
} 