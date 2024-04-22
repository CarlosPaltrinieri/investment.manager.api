namespace investiment.manager.api.Models.Investment
{
    public class InvestmentModel : InvestmentBase
    {
        public string Type { get; set; }
        public InvestmentValue Value { get; set; }
    }
}
