using GuessMyNumber.Core.Interfaces;
using System.Linq;

namespace GuessMyNumber.Core
{
    public class NumberComparer : INumberComparer
    {
        public IAttemptResult Compare(INumber mainNumber, INumber triedNumber)
        {
            var goodsCount = triedNumber.Units.Count(u => 
                mainNumber.Units.Any(x => x.Equals(u)));
            var regularsCount = triedNumber.Units.Count(u =>
                mainNumber.Units.Any(x => x.Position != u.Position && x.Value == u.Value));

            return new AttemptResult
            {
                Number = triedNumber,
                Goods = goodsCount,
                Regulars = regularsCount,
                Bads = triedNumber.Units.Count() - goodsCount - regularsCount
            };
        }
    }
}
