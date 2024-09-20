namespace NotificationEngine.Process.Data
{
    public class Policy
    {
        public int PolicyId { get; set; }
        public int ClientId { get; set; }
        public int EpicUniqPolicy { get; set; }
        public string Description { get; set; } = default!;
        public int LineOfBusinessId { get; set; }
        public string Number { get; set; } = default!;
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsAutomaticRenewal { get; set; }
        public bool IsContracted { get; set; }

    }
}
