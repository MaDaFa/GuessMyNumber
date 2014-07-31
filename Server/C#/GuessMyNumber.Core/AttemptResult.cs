using GuessMyNumber.Core.Interfaces;

namespace GuessMyNumber.Core
{
    public class AttemptResult : IAttemptResult
    {
        public INumber Number { get; set; }

        public int Goods { get; set; }

        public int Regulars { get; set; }

        public int Bads { get; set; }

        public AttemptResult(INumber number, int goods, int regulars, int bads)
        {
            this.Number = number;
            this.Goods = goods;
            this.Regulars = regulars;
            this.Bads = bads;
        }

        public AttemptResult()
        {
            this.Goods = 0;
            this.Regulars = 0;
            this.Bads = 0;
        }
    }
}
