namespace HttpClientCommunicationClassLibraryTest.Model
{
    public class PayrollTypeModelView
    {
        public int PayrollTypeId { get; set; }
        public int CompanyId { get; set; }
        public int PeriodPaymentId { get; set; }
        public string CompanyDescription { get; set; }
        public string PeriodPaymentDescription { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public double CommissionValue { get; set; }
        public double TaxesValue { get; set; }
        public bool IsCheck { get; set; }
        public bool IsActive { get; set; }
    }
}
